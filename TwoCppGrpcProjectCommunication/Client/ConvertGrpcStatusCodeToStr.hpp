#pragma once

#include <iostream>
#include <map>
#include <string>
#include <grpc/grpc.h>
#include <grpc/status.h>
#include <grpcpp/support/status_code_enum.h>

class ConvertGrpcStatusCodeToStr
{
public:
	ConvertGrpcStatusCodeToStr()
	{
		if (_table.size() == 0){
			_table[grpc::StatusCode::OK]                  = "OK";
			_table[grpc::StatusCode::CANCELLED]           = "CANCELLED";
			_table[grpc::StatusCode::UNKNOWN]             = "UNKNOWN";
			_table[grpc::StatusCode::INVALID_ARGUMENT]    = "INVALID_ARGUMENT";
			_table[grpc::StatusCode::DEADLINE_EXCEEDED]   = "DEADLINE_EXCEEDED";
			_table[grpc::StatusCode::NOT_FOUND]           = "NOT_FOUND";
			_table[grpc::StatusCode::ALREADY_EXISTS]      = "ALREADY_EXISTS";
			_table[grpc::StatusCode::PERMISSION_DENIED]   = "PERMISSION_DENIED";
			_table[grpc::StatusCode::ABORTED]             = "ABORTED";
			_table[grpc::StatusCode::DATA_LOSS]           = "DATA_LOSS";
			_table[grpc::StatusCode::DO_NOT_USE]          = "DO_NOT_USE";
			_table[grpc::StatusCode::FAILED_PRECONDITION] = "FAILED_PRECONDITION";
			_table[grpc::StatusCode::INTERNAL]            = "INTERNAL";
			_table[grpc::StatusCode::OUT_OF_RANGE]        = "OUT_OF_RANGE";
			_table[grpc::StatusCode::RESOURCE_EXHAUSTED]  = "RESOURCE_EXHAUSTED";
			_table[grpc::StatusCode::UNAUTHENTICATED]     = "UNAUTHENTICATED";
			_table[grpc::StatusCode::UNAVAILABLE]         = "UNAVAILABLE";
			_table[grpc::StatusCode::UNIMPLEMENTED]       = "UNIMPLEMENTED";
		}
	}

	std::string operator()(int code)
	{
		ConvertGrpcStatusCodeToStr tmp;
		return _table[(grpc::StatusCode)code];
	}

	std::string operator()(grpc::StatusCode code)
	{
		ConvertGrpcStatusCodeToStr tmp;
		return _table[code];
	}

private:
	static std::map<grpc::StatusCode, std::string> _table;

};

std::map<grpc::StatusCode, std::string> ConvertGrpcStatusCodeToStr::_table;