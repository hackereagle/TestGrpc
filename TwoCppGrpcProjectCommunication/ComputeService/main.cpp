#include <iostream>
#include <sstream>
#include <memory>
#include "ComputeService.hpp"
// begin needed
#include <grpc/grpc.h>
#include <grpcpp/security/server_credentials.h>
#include <grpcpp/server.h>
#include <grpcpp/server_builder.h>
#include <grpcpp/server_context.h>
// needed end

void RunServer()
{
	std::string server_address = "localhost:50051";
	ComputeService service = ComputeService();

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