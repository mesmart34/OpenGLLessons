#version 330 core

layout(location = 0) in vec3 position;
layout(location = 1) in vec2 uv;
layout(location = 2) in vec3 normal;

out vec2 _uv;
out vec3 _normal;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

void main(void)
{
    _uv = uv;
	_normal = normal;
	mat4 mvp = model * view * projection;
    gl_Position = vec4(position, 1.0) * mvp;
}