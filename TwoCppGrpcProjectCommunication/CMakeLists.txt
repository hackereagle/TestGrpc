cmake_minimum_required(VERSION 3.16)
set(PROJECT_NAME TwoCppGrpcProjectCommunication)
project(${PROJECT_NAME})

set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib)
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib)

set(CMAKE_VERBOSE_MAKEFILE OFF) # opened if want to debug dependency
set(CMAKE_RULE_MESSAGES ON)
set(CMAKE_CXX_FLAGS "-std=c++17${CMAKE_CXX_FLAGS}")

add_subdirectory(Client)