#ifndef _CALCULATOR_H_
#define _CALCULATOR_H_

#include <iostream>
#include <sstream>
#include <string>
#include <memory>
#include <grpc/grpc.h>
#include "ConvertGrpcStatusCodeToStr.hpp"
#include "compute.pb.h"
#include "compute.grpc.pb.h"

class Calculator
{
public:
	Calculator(std::shared_ptr<grpc::Channel> channel)
	{
		this->_stub = compute::ComputeService::NewStub(channel);
	}

	~Calculator()
	{}

	std::string Calculate(std::string num1, std::string opt, std::string num2)
	{
		grpc::ClientContext context;
		compute::CalculateRequest request;
		request.set_firstnum(num1);
		request.set_operator_(opt);
		request.set_secondnum(num2);
		compute::CalculateResult result;
		grpc::Status status = this->_stub->Arithmetic(&context, request, &result);
		std::cout << "status = "    << _converter(status.error_code()) << 
					 ", is_ok = "   << (status.ok() ? "true" : "false") << 
					 ", message = " << status.error_message() << 
					 ", result = "  << result.result() << std::endl;
		return result.result();
	}

private:
	std::unique_ptr<compute::ComputeService::Stub> _stub;
	ConvertGrpcStatusCodeToStr _converter;


};
#endif //_CALCULATOR_H_