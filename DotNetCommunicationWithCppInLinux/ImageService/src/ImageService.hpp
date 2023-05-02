#pragma once

#include <memory>
#include <iostream>
#include <string>
#include <sstream>
#include <grpc/grpc.h>
#include "imageService.grpc.pb.h"
#include "imageService.pb.h"
#include "ImageGrabber.hpp"
#include "GenericException.hpp"

#ifdef PROJECT_DIR
	#define IMAGE_PATH PROJECT_DIR"/Images/"
#else
	#define IMAGE_PATH "../Images/"
#endif // PROJECT_DIR

class ImageService : public ImageComputer::ImageService::Service
{
public:
	ImageService()
	{
		//std::cout << IMAGE_PATH << std::endl;
		_camera = std::make_unique<ImageGrabber>(IMAGE_PATH);
	}

    ::grpc::Status GetImage(::grpc::ServerContext* context, const ::ImageComputer::ImageRequest* request, ::ImageComputer::ImageResult* response) override
	{
		grpc::Status status;
		int lightLevel = -1;
		int reqNumImgs = -1;
		try {
			lightLevel = request->lightlevel();
			reqNumImgs = request->getnumbersofimg();

			std::cout << "*** receive request: light level = " << lightLevel << ", request numbers of image = " << reqNumImgs << std::endl;

			Image* img = _camera->GetImage(lightLevel);
			response->set_width(img->Width);
			response->set_height(img->Height);
			response->set_data((const char*)img->Data, img->Width * img->Height);

			status = grpc::Status(grpc::StatusCode::OK, "Finished grabbing image");
			ReleaseImage(img);
		}
		catch(const std::exception& e) {
			std::ostringstream ss;
			ss << "In ImageService::GetImage, occur ERROR: ";
			ss << e.what();

			std::string errStr = ss.str();
			std::cout << errStr << std::endl;
			status = grpc::Status(grpc::StatusCode::INTERNAL, errStr);
		}
		catch(...){
			std::ostringstream ss;
			ss << "In ImageService::GetImage, occur unexception exception!" << std::endl;
			ss << "Input: \n\tlightLevel = " << lightLevel << "\n\treqNumImgs = " << reqNumImgs << std::endl;

			std::string errStr = ss.str();
			std::cout << errStr << std::endl;
			status = grpc::Status(grpc::StatusCode::INTERNAL, errStr);
		}
		
		return status;
	}
	
private:
	std::unique_ptr<ImageGrabber> _camera;

};