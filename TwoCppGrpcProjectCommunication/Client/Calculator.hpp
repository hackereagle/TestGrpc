#ifndef _CALCULATOR_H_
#define _CALCULATOR_H_

#include <iostream>
#include <sstream>
#include <string>
#include <memory>
#include <grpc/grpc.h>
#include "compute.pb.h"
#include "compute.grpc.pb.h"
#include "GenericException.hpp"

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
		std::cout << "yo" << std::endl;
		grpc::Status status = this->_stub->Arithmetic(&context, request, &result);
		std::cout << "yo~! status = " << status.ok() << ", message = " << status.error_message() << ", result = " << result.result() << std::endl;

		bool isFloat = false;
		if (num1.find('.') != std::string::npos || num2.find('.') != std::string::npos)
			isFloat = true;
		
		std::ostringstream ss;
		try{
			if (isFloat) {
				double _num1 = std::stod(num1);
				double _num2 = std::stod(num2);
				double ret = this->CalculateInstance<double>(_num1, opt, _num2);
				ss << ret;
			}
			else {
				int _num1 = std::stod(num1);
				int _num2 = std::stod(num2);
				int ret = this->CalculateInstance<int>(_num1, opt, _num2);
				ss << ret;
			}
		}
		catch(std::exception &ex) {
			std::cout << "In Caculate, call CalculateInstance occur error: \n" << ex.what() << std::endl;
			ss << "NaN";
		}
		catch(...) {
			std::cout << "Catched other exception" << std::endl;
			ss << "NaN";
		}

		return ss.str();
	}

private:
	std::unique_ptr<compute::ComputeService::Stub> _stub;

	template<class T>
	T CalculateInstance(T num1, std::string opt, T num2)
	{
		T ret;
		if ("+" == opt) {
			ret = num1 + num2;
		}
		else if ("-" == opt) {
			ret = num1 - num2;
		}
		else if ("*" == opt) {
			ret =  num1 * num2;
		}
		else if ("/" == opt) {
			if (std::is_same<T, int>::value && num2 == 0)
				throw GenericException("In CalculateInstance, denominator can not be 0!");
			
			ret = num1 / num2;
		}
		else {
			std::ostringstream ss;
			ss << "In CalculateInstance, second argument is " << opt << ". It not be defined!" << std::endl;
			throw GenericException(ss.str());
		}
		return ret;
	}

};
#endif //_CALCULATOR_H_