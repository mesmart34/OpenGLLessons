//
// Created by mesmart34 on 22.10.2021.
//

#include "Application.h"

Application::Application(int width, int height) : m_width(width), m_height(height), m_running(true), m_window(nullptr) {

}

void Application::Init() {
    glfwInit();
    m_window = glfwCreateWindow(m_width, m_height, "OpenGL Lessons", nullptr, nullptr);
    glfwSwapInterval(1);
    glfwMakeContextCurrent(m_window);
    if (!gladLoadGLLoader((GLADloadproc) glfwGetProcAddress)) {
        std::cout << "Failed to initialize GLAD" << std::endl;
        glfwTerminate();
        return;
    }
    glfwSetWindowSizeCallback(m_window, ResizeCallback);
    glfwSetWindowUserPointer(m_window, this);
    Run();
}

void Application::Run() {
    Start();
    while (m_running) {
        glfwPollEvents();
        m_running = !glfwWindowShouldClose(m_window);

        glViewport(0, 0, m_width, m_height);
        glClearColor(0.1, 0.1, 0.1, 1.0);
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
        glEnable(GL_DEPTH_TEST);
        glCullFace(GL_FRONT_AND_BACK);

        glMatrixMode(GL_PROJECTION);
        glLoadIdentity();
        const auto aspect = (float)m_width / (float)m_height;
        if (aspect >= 1.0)
            glOrtho(-1 * aspect, 1 * aspect, 1, -1, -1, 100.0f);
        else
            glOrtho(-1, 1, 1.0f / aspect, -1.0f / aspect, -1, 100.0f);
        glMatrixMode(GL_MODELVIEW);
        glLoadIdentity();

        if(glfwGetKey(m_window, GLFW_KEY_ESCAPE))
            Shutdown();

        Update(0.0f);


        glfwSwapBuffers(m_window);
    }
}

void Application::Start() {
    std::vector<glm::vec3> coords = {{0.0f,  1.0f,  0.0f},
                                     {-1.0f, -1.0f, 0.0f},
                                     {1.0f,  -1.0f, 0.0f}};
    m_mesh = Mesh(coords);
    m_mesh.SetScale(glm::vec3(0.1, 0.1, 0));
    m_mesh.SetPosition(glm::vec3(0, 0, -6));
}

void Application::Update(float dt) {
    m_mesh.Rotate(glm::vec3(1, 0, 0));
    m_mesh.Draw(GL_POLYGON);
}

void Application::Shutdown() {
    m_running = false;
}

void Application::ResizeCallback(GLFWwindow *window, int w, int h) {
    auto app = (Application *) glfwGetWindowUserPointer(window);
    app->m_width = w;
    app->m_height = h;
    glViewport(0, 0, w, h);
}


