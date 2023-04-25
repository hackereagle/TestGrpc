#include <iostream>
#include <memory>
// begin needed
#include <grpc/grpc.h>
#include <grpcpp/security/server_credentials.h>
#include <grpcpp/server.h>
#include <grpcpp/server_builder.h>
#include <grpcpp/server_context.h>
// needed end
#include "compute.pb.h"
#include "compute.grpc.pb.h"

class ComputeService final : public compute::ComputeService::Service
{
public:
	ComputeService()
	{}

	~ComputeService()
	{}

    virtual ::grpc::Status Arithmetic(::grpc::ServerContext* context, const ::compute::CalculateRequest* request, ::compute::CalculateResult* response) override
	{
		std::string first = request->firstnum();
		std::string opt = request->operator_();
		std::string second = request->secondnum();
		std::cout << "receive request: " << first << " " << opt << " " << second << std::endl;
		response->set_result("123");
		return ::grpc::Status::OK;
	}
};

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