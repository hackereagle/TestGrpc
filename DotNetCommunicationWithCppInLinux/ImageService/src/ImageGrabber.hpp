#pragma once

#include <iostream>
#include <string>
#include <sstream>
#include <vector>
#include <filesystem>
#include <memory>
#include <string.h>
#include "libbmp.h"
#include "GenericException.hpp"

namespace fs = std::filesystem;

typedef struct _image
{
	int Width = 0;
	int Height = 0;
	int Channels = 0;
	int FreeImage = 0;
	unsigned char* Data = nullptr;
}Image;

inline void ReleaseImage(Image* &img)
{
	if (img) {
        if (img->FreeImage && img->Data) {
            delete[]img->Data;
            img->Data = nullptr;
        }
        delete img;
        img = nullptr;
    }
}

inline Image* CreateImage(int width, int height, int channels)
{
	Image* ret = new Image();
    if (ret)
    {
        ret->Height = height;
        ret->Width = width;
        ret->Channels = channels;
        
        ret->Data = new unsigned char[width * height * channels];
        if (!ret->Data)
        {
            ret->FreeImage = 0;
            ReleaseImage(ret);
        }
        else
        {
            ret->FreeImage = 1;
        }
    }
    return ret;
}

inline void* malloc_ptr(int sz)
{
	return malloc(sz);
}

Image* ReadImage(std::string filename)
{
	Image* retImg = nullptr;

	bmp_header hdr;
	void* payload = 0;
#if _WIN32
	bmp_error ret = bmp_img_read_ext_2(filename.c_str(), hdr, payload, malloc_ptr);	
#else
	bmp_error ret = bmp_img_read_ext_3(filename.c_str(), hdr, payload, malloc_ptr);
#endif
	if (ret == bmp_error::BMP_OK)
	{
		retImg = CreateImage(hdr.biWidth, hdr.biHeight, hdr.biPlanes);
		memcpy(retImg->Data, payload, hdr.biWidth * hdr.biHeight * hdr.biPlanes);
	}
	else{
		std::cout << "Reading image error!" << std::endl;
	}
	
	// release data pointer from image file module
	if (payload)
	{
		free(payload);
	}

	return retImg;
}

class ImageGrabber
{
public:
	ImageGrabber(std::string path)
	{
		if (this->_filePaths.size() > 0) {
			return ;
		}
		std::cout << "Reading images from " << path << std::endl;

		this->_imgIndex = 0;
		for (const auto& entry : fs::directory_iterator(path)) {
			if(entry.is_regular_file())
			{
				auto ext = entry.path().extension();
				//supporting bmp file reader
				if (ext == ".BMP" || ext == ".bmp")
				{
					this->_filePaths.push_back(entry.path().native());
				}
			}
		}
	}

	Image* GetImage(int lightLevel)
	{
		if (this->_filePaths.size() == 0) {
			return nullptr;
		}

		if ((++this->_imgIndex) >= this->_filePaths.size()) {
			this->_imgIndex = 0;
		}
		std::cout << "Using " << lightLevel << " level light to grab!" << std::endl;

		Image* img = nullptr;
		try {
			img = ReadImage(this->_filePaths[this->_imgIndex]);
		}
		catch(const std::exception& e) {
			std::ostringstream ss;
			ss << "In ImageGrabber::GetImage reading image occur error: " << e.what() << std::endl;
			ss << "\n\tindex = " << this->_imgIndex << "\n\tfile path = " << this->_filePaths[this->_imgIndex] << std::endl;
			
			std::string errStr = ss.str();
			std::cout << errStr << std::endl;
			throw new GenericException(errStr);
		}
		

		return img;
	}

private:
	std::vector<std::string> _filePaths;
	int _imgIndex;
};