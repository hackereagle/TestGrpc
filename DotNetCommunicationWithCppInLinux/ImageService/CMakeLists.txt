cmake_minimum_required(VERSION 3.14)
set(PROJECT_NAME ImageService)
project(${PROJECT_NAME})

aux_source_directory(./src SRCS)

add_executable(${PROJECT_NAME} ${SRCS})
target_link_libraries(${PROJECT_NAME} PRIVATE ${CMAKE_DL_LIBS})