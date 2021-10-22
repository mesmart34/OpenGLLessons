//
// Created by mesmart34 on 22.10.2021.
//

#ifndef OPENGL_MESH_H
#define OPENGL_MESH_H

#include <glad/glad.h>
#include "glm/glm/vec3.hpp"
#include <utility>
#include <vector>

class Mesh {
public:
    Mesh() = default;
    explicit Mesh(std::vector<glm::vec3> vertices) : m_vertices(std::move(vertices)) {}

    void SetVertices(const std::vector<glm::vec3>& vertices);
    void Draw(GLenum mode);
    void SetPosition(const glm::vec3& position);
    void SetRotation(const glm::vec3& rotation);
    void SetScale(const glm::vec3& scale);
    void Move(const glm::vec3& position);
    void Rotate(const glm::vec3& rotation);

private:
    std::vector<glm::vec3> m_vertices;
    glm::vec3 m_position = glm::vec3();
    glm::vec3 m_rotation = glm::vec3();
    glm::vec3 m_scale = glm::vec3(1, 1, 1);
};


#endif //OPENGL_MESH_H
