//
// Created by mesmart34 on 22.10.2021.
//

#ifndef OPENGL_APPLICATION_H
#define OPENGL_APPLICATION_H

#include <glad/glad.h>
#include "GLFW/glfw3.h"
#include <iostream>
#include "Mesh.h"

class Application {

public:
    Application(int width, int height);

    void Init();
    void Run();
    void Start();
    void Update(float dt);
    void Shutdown();

    static void ResizeCallback(GLFWwindow* window, int w, int h);

private:
    GLFWwindow* m_window;
    int m_width;
    int m_height;
    bool m_running;
    Mesh m_mesh;
};


#endif //OPENGL_APPLICATION_H
