//
// Created by mesmart34 on 22.10.2021.
//

#include "Mesh.h"

void Mesh::SetVertices(const std::vector<glm::vec3> &vertices) {
    m_vertices = vertices;
}

void Mesh::Draw(GLenum mode) {
    glTranslatef(m_position.x, m_position.y, m_position.z);
    glRotatef(m_rotation.x, 1, 0, 0);
    glRotatef(m_rotation.y, 0, 1, 0);
    glRotatef(m_rotation.z, 0, 0, 1);
    glScalef(m_scale.x, m_scale.y, m_scale.z);
    glBegin(mode);
    for(auto vertex : m_vertices)
    {
        glColor3f(1.0,0.5,0.0);
        glVertex3f(vertex.x, vertex.y, vertex.z);
    }
    glEnd();
}

void Mesh::SetPosition(const glm::vec3 &position) {
    m_position = position;
}

void Mesh::SetRotation(const glm::vec3 &rotation) {
    m_rotation = rotation;
}

void Mesh::SetScale(const glm::vec3 &scale) {
    m_scale = scale;
}

void Mesh::Move(const glm::vec3 &position) {
    m_position += position;
}

void Mesh::Rotate(const glm::vec3 &rotation) {
    m_rotation += rotation;
}
