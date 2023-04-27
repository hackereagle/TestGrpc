cmake_minimum_required(VERSION 3.14)
set(PROJECT_NAME ImageService)
project(${PROJECT_NAME})

# setting binary directory
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib)
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib)
# setting compiling flags
set(CMAKE_VERBOSE_MAKEFILE OFF) # opened if want to debug dependency
set(CMAKE_RULE_MESSAGES ON)
set(CMAKE_CXX_FLAGS "-std=c++17${CMAKE_CXX_FLAGS}")

# compile proto file
add_subdirectory(./Protos)

aux_source_directory(./src SRCS)

add_executable(${PROJECT_NAME} ${SRCS})
target_link_libraries(${PROJECT_NAME} PRIVATE ${CMAKE_DL_LIBS} myproto)