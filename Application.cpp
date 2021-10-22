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
    auto start = glfwGetTime();
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

        auto end = glfwGetTime();
        auto delta = end - start;
        Update(delta);
        start = end;


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
    Task2(dt);
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

void Application::Task1(float dt) {
    static auto start = [&]() {
        static bool done = false;
        if(done)
            return;
        auto vertices = std::vector<glm::vec3>();
        const auto step = 90;
        const auto r = 1.0f;
        for(auto angle = 0.0f; angle < 360.0f; angle += step)
        {
            const auto x = cosf(glm::radians(angle)) * r;
            const auto y = sinf(glm::radians(angle)) * r;
            const auto vertex = glm::vec3(x, y, 0.0f);
            vertices.push_back(vertex);
        }
        m_mesh.SetVertices(vertices);
        done = true;
    };
    start();
    m_mesh.Draw(GL_POLYGON);
}

void Application::Task2(float dt) {
    static auto start = [&]() {
        static bool done = false;
        if(done)
            return;
        auto vertices = std::vector<glm::vec3>();
        const auto step = 90.0f;
        const auto r = 1.0f;
        for(auto angle = 0.0f; angle < 360.0f; angle += 30)
        {
            const auto x = cosf(glm::radians(angle)) * r;
            const auto y = sinf(glm::radians(angle)) * r;
            const auto vertex = glm::vec3(x, y, 0.0f);
            vertices.push_back(vertex);
        }
        m_mesh.SetVertices(vertices);
        done = true;
    };
    start();
    static glm::vec3 scale = glm::vec3(1, 1, 1);
    static auto timer = 0.0f;
    timer += dt * 10;
    scale.x = sinf(timer) + 10.0f;
    scale.y = sinf(timer) + 10.0f;
    scale *= 0.01f;
    m_mesh.SetScale(scale);

    auto f = [&](float x) {
        return cosf(x);
    };


    auto converted = m_mesh.GetPosition().x / (m_width / (float)m_height);
    static auto dir = -1;
    if(converted <= -1)
        dir = 1;
    else if(converted >= 1)
        dir = -1;
    const auto speed = 0.1f * dt * dir;
    m_mesh.Rotate(glm::vec3(0, 0, dt * 50));
    m_mesh.Move(glm::vec3(speed * 3, f(m_mesh.GetPosition().x * 50) * 0.01f, 0));
    m_mesh.Draw(GL_POLYGON);
}

void Application::Task3(float dt) {
    static auto start = [&]() {
        static bool done = false;
        if(done)
            return;
        auto vertices = std::vector<glm::vec3>();
        const auto step = 90.0f;
        const auto r = 1.0f;
        for(auto angle = 0.0f; angle < 360.0f; angle += step)
        {
            const auto x = cosf(glm::radians(angle)) * r;
            const auto y = sinf(glm::radians(angle)) * r;
            const auto vertex = glm::vec3(x, y, 0.0f);
            vertices.push_back(vertex);
        }
        m_mesh.SetVertices(vertices);
        m_mesh.SetPosition(glm::vec3(0.1f, 0, 0));
        done = true;
    };
    start();

    static auto angle = 0.0f;
    angle += dt * 100;
    const auto speed = 0.1f * dt;
    m_mesh.SetRotation(glm::vec3(0, 0, angle));
    m_mesh.RotateAroundAndDraw();
}


