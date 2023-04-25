#include <iostream>
#include <grpc/grpc.h>
#include <grpcpp/create_channel.h>
#include <grpcpp/security/server_credentials.h>
#include "Calculator.hpp"
#include "compute.pb.h"
#include "compute.grpc.pb.h"

int main(int argc, const char** argv)
{
	std::cout << "\nWelcom calculator application! Type q to exit!" << std::endl;
	auto channel = grpc::CreateChannel("localhost:50051", 
									   std::dynamic_pointer_cast<grpc::ChannelCredentials>(grpc::InsecureChannelCredentials()));

	bool isContinuous = true;
	std::string ret = "";
	std::string num1 = "";
	std::string opt = "";
	std::string num2 = "";
	Calculator calculator(channel);
	while (isContinuous) {
		std::cout << std::endl << "\nPlease type first number: ";
		std::cin >> num1;
		if ("q" == num1)
			break;

		std::cout << std::endl << "Please type operator(+, -, * or /): ";
		std::cin >> opt;
		if ("q" == opt)
			break;

		std::cout << std::endl << "Please type second number: ";
		std::cin >> num2;
		if ("q" == opt)
			break;

		ret = calculator.Calculate(num1, opt, num2);
		std::cout << num1 << " " << opt << " " << num2 << " = " << ret << std::endl;

		ret = "";
		num1 = "";
		opt = "";
		num2 = "";
	}

	getchar();
	return EXIT_SUCCESS;
}