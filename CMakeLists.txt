cmake_minimum_required(VERSION 3.20)
project(OpenGL)

set(CMAKE_CXX_STANDARD 20)

find_package(OpenGL)
find_package(X11)

include_directories(/usr/include)
link_directories(/usr/lib)
include_directories(${CMAKE_CURRENT_SOURCE_DIR}/include)
link_directories(${CMAKE_CURRENT_SOURCE_DIR}/lib)

add_executable(OpenGL main.cpp Application.cpp Application.h glad.c Mesh.cpp Mesh.h)

target_link_libraries(OpenGL glfw3 dl X11 pthread libglfw3.a)