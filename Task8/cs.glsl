#version 430
layout(binding = 0, rgba32f) uniform image2D framebuffer;
layout (local_size_x = 8, local_size_y = 8) in;

uniform mat4 projection;
uniform mat4 view;
    
#define SPHERES_COUNT 2
#define PLANES_COUNT 6
#define SPHERE 0
#define PLANE 1

struct Material 
{
    float diffuse;
    float specular;
    float shininess;
    float ambience;
    float reflection;
} material1, material2;

struct Sphere 
{
    vec3 position;
    vec3 color;
    float radius;
    Material material;
} spheres[SPHERES_COUNT];   

struct PointLight
{
    vec3 position;
    vec3 color;
    float intensity;
} lights[1];        

struct Plane 
{
    vec3 position;
    vec3 normal;
    vec3 color;
    Material material;
} planes[PLANES_COUNT];


void SetupScene()
{
    material1 = Material(0.514, 0.19, 76.8, 0.7, 1.);
    material2 = Material(0.4, 0.1, 0.1, 0.0, 0.0);
    const Sphere sphere1 = Sphere(
	    vec3(0.1, 0., 0.), 
        vec3(0.5, 0.1, 0.3), 
        0.08, 
	    material1);
    const Sphere sphere2 = Sphere(
	    vec3(-0.1, -0.05, 0.), 
        vec3(0.3, 0.3, 0.3),  
        0.08, 
        material1);
    PointLight light1 = PointLight(
        vec3(0., 0.19, -0.2),
        vec3(1., 1., 1.),    
        35.); 
    Plane plane1 = Plane(
        vec3(0., -0.5, 0.), 
        vec3(0., 1., 0.), 
        vec3(0.5, 0.5, 0.5), 
        material2);
    Plane plane2 = Plane(
        vec3(-0.5, 0., 0.), 
        vec3(1., 0., 0.), 
        vec3(0.5, 0.5, 0.5), 
        material2);
    Plane plane3 = Plane(
        vec3(0.5, 0., 0.), 
        vec3(-1., 0., 0.), 
        vec3(0.5, 0.5, 0.5), 
        material2);
    Plane plane4 = Plane(
        vec3(0., 0.5, 0.), 
        vec3(0., -1., 0.), 
        vec3(0.5, 0.5, 0.5), 
        material2);
    Plane plane5 = Plane(
        vec3(0., 0., 0.5), 
        vec3(0., 0., -1.), 
        vec3(0.5, 0.5, 0.5), 
        material2);
    Plane plane6 = Plane(
        vec3(0., 0., -0.5), 
        vec3(0., 0., 1.), 
        vec3(0.5, 0.5, 0.5), 
        material2);
	spheres[0] = sphere1;
    spheres[1] = sphere2;
    planes[0] = plane1;
    planes[1] = plane2;
    planes[2] = plane3;
    planes[3] = plane4;
    planes[4] = plane5;
    planes[5] = plane6;
    lights[0] = light1;
}    

bool SolveQuadratic(float a, float b, float c, out float t0, out float t1)
{
    float disc = b * b - 4. * a * c;
    if (disc < 0.)
        return false;
    if (disc == 0.)
    {
        t0 = t1 = -b / (2. * a);
        return true;
    }
    t0 = (-b + sqrt(disc)) / (2. * a);
    t1 = (-b - sqrt(disc)) / (2. * a);
    return true;    
}

Material GetMaterial(int type, int index)
{
    if (type == SPHERE)
        return spheres[index].material;
    if (type == PLANE)
        return planes[index].material;
}

bool IntersectsSphere(
    vec3 origin, 
    vec3 direction, 
    Sphere sphere, 
    out float dist, 
    out vec3 surfaceNormal, 
    out vec3 Phit)
{
    vec3 L = origin - sphere.position;
    float a = dot(direction, direction);
    float b = 2. * dot(direction, L);
    float c = dot(L, L) - pow(sphere.radius, 2.);
    float t0;
    float t1;
    if (SolveQuadratic(a, b, c, t0, t1))
    {        
        if (t0 > t1) 
        {
        	float temp = t0;
            t0 = t1;
            t1 = temp;
        } 
        if (t0 < 0.)
        { 
            t0 = t1; 
            if (t0 < 0.) return false;
        }  
        dist = t0;
        Phit = origin + dist * direction;
        surfaceNormal = normalize(Phit - sphere.position);               
        return true;
    }  
    return false;
}

bool IntersectsPlane(in Plane plane, in vec3 origin, in vec3 rayDirection, out float t, out vec3 pHit) 
{    
    float denom = dot(plane.normal, rayDirection); 
    if (denom < 1e-6) 
    { 
        vec3 p0l0 = plane.position - origin; 
        t = dot(p0l0, plane.normal) / denom; 
        if (t >= 0.)
        {
            pHit = origin + rayDirection * t;
            return true;
        }             
    } 
    return false; 
} 

vec3 CalculateColor(in vec3 viewDir, in vec3 surfacePointPosition, in vec3 objectColor, in PointLight pointLight, in vec3 surfaceNormal, in Material material)
{
    vec3 lightVector = surfacePointPosition - pointLight.position;
    vec3 lightDir = normalize(lightVector);   
   	float lightIntensity = (pow(0.1, 2.) / pow(length(lightVector), 2.)) * pointLight.intensity;
    float coeff = -dot(lightDir, surfaceNormal);     
    vec3 ambient = material.ambience * objectColor;
    vec3 diffuse = material.diffuse * max(coeff, 0.) * objectColor * lightIntensity;
    vec3 halfwayDir = normalize(lightDir + viewDir);  
    vec3 specular = pow(max(-dot(surfaceNormal, halfwayDir), 0.0), material.shininess) * material.specular * objectColor * lightIntensity;
    vec3 color = ambient + diffuse + specular;
    return color;
}

void CalculateShadow(vec3 pHit, inout vec3 finalColor, float ambient, int type, int index)
{
    vec3 shadowSurfaceNormal;
    vec3 shadowRay = lights[0].position - pHit;
    vec3 shadowRayDirection = normalize(shadowRay);
    float distanceToLight = sqrt(dot(shadowRay, shadowRay));
    vec3 shadowPhit;
    float dist; 
    for(int i = 0; i < 1; ++i)
	{
        if (type == SPHERE && index == i)
            continue;  
        if (IntersectsSphere(pHit, shadowRay, spheres[i], dist, shadowSurfaceNormal, shadowPhit))
        {
            if (dist > 0. && distanceToLight > dist)
            	finalColor *= 2. * ambient; 
        }
    }
    
    for(int i = 0; i < PLANES_COUNT; ++i)
	{
 		if (type == PLANE && index == i)
            continue;
        if (IntersectsPlane(planes[i], pHit, shadowRay, dist, shadowPhit))
        {    
            if (dist < distanceToLight)
 				finalColor *= 2. * ambient;        
        }
    }     
}


vec3 RayTrace(in vec3 rayDirection, in vec3 rayOrigin)
{
    vec3 finalColor = vec3(0.);
    int BOUNCES = 3;
    int prevType = -1;
    int prevIndex = -1;  
	vec3 pHit = rayOrigin; 
    vec3 passPHit;
    for (int bounce = 0; bounce < BOUNCES; bounce++)
    {    
        float dist = 1. / 0.; 
        float objectHitDistance = dist;
        vec3 surfaceNormal;
        int type = -1;
    	int index = -1;   
        vec3 passColor = vec3(0.);
        for (int i = 0; i < SPHERES_COUNT; ++i)
        {          
            if (prevType == SPHERE && prevIndex == i)
                continue;
            if (IntersectsSphere(rayOrigin, rayDirection, spheres[i], objectHitDistance, surfaceNormal, pHit))
            {                
                if (objectHitDistance < dist)
                {
                    dist = objectHitDistance;
                    passColor = CalculateColor(rayDirection, pHit, spheres[i].color, lights[0], surfaceNormal, spheres[i].material);
                    CalculateShadow(pHit, passColor, spheres[i].material.ambience, SPHERE, i);
                    type = SPHERE;
                    index = i;
                    passPHit = pHit;
                }
            }
        }

        for(int i = 0; i < PLANES_COUNT; ++i)
        {
            if (prevType == PLANE && prevIndex == i)
                continue;
            if (IntersectsPlane(planes[i], rayOrigin, rayDirection, objectHitDistance, pHit))
            {
                if (objectHitDistance <= dist)
                {    
					dist = objectHitDistance;
                    passColor = CalculateColor(rayDirection, pHit, planes[i].color, lights[0], planes[i].normal, planes[i].material);        
                    surfaceNormal = planes[i].normal;
                    CalculateShadow(pHit, passColor, planes[i].material.ambience, PLANE, i);
                	type = PLANE;
                    index = i;                    
                    passPHit = pHit;                    
                }
            }
        } 
    
        if (bounce == 0)
            finalColor += passColor;
        else
        	finalColor += GetMaterial(type, index).specular * passColor;
        if (type < 0) break;
        rayOrigin = passPHit;
        rayDirection = reflect(rayDirection, surfaceNormal);
        prevType = type;
        prevIndex = index;
    }
    return finalColor / float(BOUNCES);
}


void main() {
	ivec2 pixel_coords = ivec2(gl_GlobalInvocationID.xy);
	ivec2 size = imageSize(framebuffer);
	vec2 uv = vec2((pixel_coords.xy + vec2(0.5, 0.5)) / size * 2.0 - 1.0);
    uv.y = -uv.y;
    SetupScene();
    vec3 origin = (view * vec4(0.0, 0.0, 0.0, 1.0)).xyz;
    vec3 direction = (inverse(projection) * vec4(uv, 0.0, 1.0)).xyz;
    direction = (view * vec4(direction, 0.0f)).xyz;
    direction = normalize(direction);
    vec3 result = RayTrace(direction, origin);
	imageStore(framebuffer, pixel_coords, vec4(result, 1.0));
}