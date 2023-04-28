#pragma once

#include <iostream>
#include <grpc/grpc.h>
#include "imageService.grpc.pb.h"
#include "imageService.pb.h"

class ImageService : public ImageComputer::ImageService::Service
{
public:
	ImageService()
	{}

    ::grpc::Status GetImage(::grpc::ServerContext* context, const ::ImageComputer::ImageRequest* request, ::ImageComputer::ImageResult* response) override
	{
		grpc::Status status;
		std::string first = request->firstnum();
		std::string opt = request->operator_();
		std::string second = request->secondnum();

		std::cout << "receive request: " << first << ", " << opt << ", " << second << std::endl;

		response->set_width(255);
		response->set_height(255);
		//response->set_data();

		return status;
	}
	
};