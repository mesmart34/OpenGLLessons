#version 430
layout(binding = 0, rgba32f) uniform image2D framebuffer;
layout (local_size_x = 8, local_size_y = 8) in;

uniform mat4 projection;
uniform mat4 view;
uniform sampler2D skybox;

const float PI = 3.14159265f;
const float EPSILON = 1e-8;

struct Sphere
{
    vec3 position;
    float radius;
    vec3 albedo;
    vec3 specular;
    float smoothness;
    vec3 emission;
};

struct DirectionalLight
{
    vec3 position;
    float intensivity;
};

vec2 _Pixel;
float _Seed;

float sdot(vec3 x, vec3 y, float f = 1.0f)
{
    return clamp(dot(x, y) * f, 0, 1);
}

float rand()
{
    float result = fract(sin(_Seed / 100.0f * dot(_Pixel, vec2(12.9898f, 78.233f))) * 43758.5453f);
    _Seed += 1.0f;
    return result;
}


DirectionalLight CreateDirectionalLight(vec3 pos, float i)
{
    DirectionalLight light;
    light.position = pos;
    light.intensivity = i;
    return light;
}

DirectionalLight directionalLight = CreateDirectionalLight(vec3(0, 0, -1), 1.0);

Sphere CreateSphere(vec3 pos, float r)
{
    Sphere s;
    s.position = pos;
    s.radius = r;
    s.albedo = vec3(1, 1, 1);
    s.specular = vec3(1, 1, 1);
    return s;
}

Sphere spheres[3];

struct Ray
{
	vec3 origin;
	vec3 direction;
	vec3 energy;
};

Ray CreateRay(vec3 origin, vec3 direction)
{
	Ray ray;
    ray.origin = origin;
    ray.direction = direction;
    ray.energy = vec3(1, 1, 1);
    return ray;
}

struct RayHit
{
    vec3 position;
    float distance;
    vec3 normal;
    vec3 albedo;
    vec3 specular;
    float smoothness;
    vec3 emission;
    bool end;
};

RayHit CreateRayHit()
{
    RayHit hit;
    hit.position = vec3(0.0, 0.0, 0.0);
    hit.distance = 1./0;
    hit.normal = vec3(0.0, 0.0, 0.0);
    hit.albedo = vec3(0, 0, 0);
    hit.specular = vec3(0, 0, 0);
    hit.smoothness = 0.0;
    hit.emission = vec3(0.0, 0.0, 0.0);
    hit.end = false;
    return hit;
}

Ray CreateCameraRay(vec2 uv)
{
    // Transform the camera origin to world space
    vec3 origin = (view * vec4(0.0f, 0.0f, 0.0f, 1.0f)).xyz;
    
    // Invert the perspective projection of the view-space position
    vec3 direction = (inverse(projection) * vec4(uv, 0.0f, 1.0f)).xyz;
    // Transform the direction from camera to world space and normalize
    direction = (view * vec4(direction, 0.0f)).xyz;
    direction = normalize(direction);
    return CreateRay(origin, direction);
}

void IntersectSphere(Ray ray, inout RayHit bestHit, Sphere sphere)
{
    // Calculate distance along the ray where the sphere is intersected
    vec3 d = ray.origin - sphere.position;
    float p1 = -dot(ray.direction, d);
    float p2sqr = p1 * p1 - dot(d, d) + sphere.radius * sphere.radius;
    if (p2sqr < 0)
        return;
    float p2 = sqrt(p2sqr);
    float t = p1 - p2 > 0 ? p1 - p2 : p1 + p2;
    if (t > 0 && t < bestHit.distance)
    {
        bestHit.distance = t;
        bestHit.position = ray.origin + t * ray.direction;
        bestHit.normal = normalize(bestHit.position - sphere.position);
        bestHit.albedo = sphere.albedo;
        bestHit.specular = sphere.specular;
        bestHit.smoothness = sphere.smoothness;
        bestHit.emission = sphere.emission;
    }
}

void IntersectGroundPlane(Ray ray, inout RayHit bestHit)
{
    // Calculate distance along the ray where the ground plane is intersected
    float t = -ray.origin.y / ray.direction.y;
    if (t > 0 && t < bestHit.distance)
    {
        bestHit.distance = t;
        bestHit.position = ray.origin + t * ray.direction;
        bestHit.normal = vec3(0.0f, 0.0f, 0.0f);
        bestHit.albedo = vec3(0.5f, 0.5f, 0.5f);
        bestHit.specular = vec3(0.03, 0.03, 0.03);
        bestHit.smoothness = 0.2f;
        bestHit.emission = vec3(0.0f, 0.0f, 0.0f);
    }
}

RayHit Trace(Ray ray)
{
    RayHit bestHit = CreateRayHit();
    IntersectGroundPlane(ray, bestHit);
	for(int i = 0; i < 3; i++)
    {
        IntersectSphere(ray, bestHit, spheres[i]);
    }
    return bestHit;
}

float saturate(float v)
{
    if(v < 0)
        return 0;
    if(v > 1)
        return 1;
    return v;
}

float atan2(in float y, in float x)
{
    return x == 0.0 ? sign(y)*PI/2 : atan(y, x);
}

mat3x3 GetTangentSpace(vec3 normal)
{
    // Choose a helper vector for the cross product
    vec3 helper = vec3(1, 0, 0);
    if (abs(normal.x) > 0.99f)
        helper = vec3(0, 0, 1);

    // Generate vectors
    vec3 tangent = normalize(cross(normal, helper));
    vec3 binormal = normalize(cross(normal, tangent));
    return mat3x3(tangent, binormal, normal);
}

vec3 SampleHemisphere(vec3 normal, float alpha)
{
    // Sample the hemisphere, where alpha determines the kind of the sampling
    float cosTheta = pow(rand(), 1.0f / (alpha + 1.0f));
    float sinTheta = sqrt(1.0f - cosTheta * cosTheta);
    float phi = 2 * PI * rand();
    vec3 tangentSpaceDir = vec3(cos(phi) * sinTheta, sin(phi) * sinTheta, cosTheta);

    // Transform direction to world space
    return tangentSpaceDir * GetTangentSpace(normal);
}

float SmoothnessToPhongAlpha(float s)
{
    return pow(1000.0f, s * s);
}

vec3 Shade(inout Ray ray, RayHit hit)
{
    if (hit.distance < 1. / 0)
    {
        ray.origin = hit.position + hit.normal * 0.001f;
        ray.direction = reflect(ray.direction, hit.normal);
        ray.energy *= hit.specular;

        bool shadow = false;
        Ray shadowRay = CreateRay(hit.position + hit.normal * 0.001f, -1 * directionalLight.position);
        RayHit shadowHit = Trace(shadowRay);
        if (shadowHit.distance != 1. / 0)
        {
            return vec3(0.0f, 0.0f, 0.0f);
        }

        return clamp(dot(hit.normal, directionalLight.position) * -1, 0, 1) * directionalLight.intensivity * hit.albedo;
        //return directionalLight.intensivity * hit.albedo;
    }
    else
    {
        ray.energy = vec3(0, 0, 0);
        hit.end = true;
        float theta = acos(ray.direction.y) / -PI;
        float phi = atan2(ray.direction.x, -ray.direction.z) / -PI * 0.5f;
        return vec3(0.1, 0.1, 0.1);
    }
}


bool any(vec3 x) {     
    return x.x != 0 || x.y != 0 || x.z != 0;
}

void main() {
	vec4 pixel = vec4(0.5, 0.5, 0.5, 1.0);
	ivec2 pixel_coords = ivec2(gl_GlobalInvocationID.xy);
	ivec2 size = imageSize(framebuffer);
	vec2 uv = vec2((pixel_coords.xy + vec2(0.5f, 0.5f)) / size * 2.0f - 1.0f);
    uv.y = -uv.y;
	spheres[0] = CreateSphere(vec3(-5, 0, -10), 1.0);
    spheres[0].albedo = vec3(1, 0, 0);
    spheres[0].specular = vec3(0.04f, 0.04f, 0.04f);
	spheres[1] = CreateSphere(vec3(1, 0, -10), 2.0);
    spheres[1].albedo = vec3(1, 1, 0);
    spheres[1].specular = vec3(0.04f, 0.04f, 0.04f);
	spheres[2] = CreateSphere(vec3(7, 0, -10), 0.5);
    spheres[2].albedo *= 0.2;
    spheres[2].specular = vec3(0.04f, 0.04f, 0.04f);
	Ray ray = CreateCameraRay(uv);
	
	vec3 result = vec3(0.0, 0.0, 0.0);
    for(int i = 0; i < 3; i++)
    {
        RayHit hit = Trace(ray);
        result += Shade(ray, hit);
        //if(hit.end)
          //  result += Shade(ray, hit);
        if(!any(ray.energy))
            break;
    }
	imageStore(framebuffer, pixel_coords, vec4(result, 1.0));
}