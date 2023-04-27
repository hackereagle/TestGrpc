#pragma once

#include <iostream>
#include <grpc/grpc.h>
#include "imageService.grpc.pb.h"
#include "imageService.pb.h"

class ImageService : public imageComputer::ImageService::Service
{
public:
	ImageService()
	{}

    ::grpc::Status GetImage(::grpc::ServerContext* context, const ::imageComputer::ImageRequest* request, ::imageComputer::ImageResult* response) override
	{
		grpc::Status status;
		return status;
	}
	
};