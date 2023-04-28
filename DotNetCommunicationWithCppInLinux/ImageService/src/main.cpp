#include <iostream>
#include <grpc/grpc.h>
#include <grpcpp/security/server_credentials.h>
#include <grpcpp/server.h>
#include <grpcpp/server_builder.h>
#include <grpcpp/server_context.h>
#include "imageService.grpc.pb.h"
#include "imageService.pb.h"
#include "ImageService.hpp"

void RunServer()
{
	//std::string server_address = "localhost:50055";
	std::string server_address = "0.0.0.0:50055";
	ImageService service = ImageService();

	grpc::ServerBuilder builder;
	builder.AddListeningPort(server_address, grpc::InsecureServerCredentials());
	builder.RegisterService(&service);
	std::unique_ptr<grpc::Server> server = std::move(builder.BuildAndStart());
	std::cout << "Server listening on " << server_address << std::endl;
	server->Wait();
}

int main(int argc, const char** argv)
{
	RunServer();
	return EXIT_SUCCESS;
}