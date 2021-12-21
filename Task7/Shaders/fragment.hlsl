#version 330

out vec4 outputColor;

in vec2 _uv;
in vec3 _normal;

uniform sampler2D texture0;

void main()
{
    outputColor = texture(texture0, _uv);
	//outputColor = vec4(1, 0, 1, 1);
}