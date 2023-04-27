#pragma once

#include <iostream>
#include <string>
#include <vector>
#include <filesystem>
#include <memory>
#include <string.h>
#include "libbmp.h"

namespace fs = std::filesystem;

typedef struct _image
{
	int Width = 0;
	int Height = 0;
	unsigned char* Data = nullptr;
}Image;

class ImageGrabber
{
public:
	ImageGrabber(std::string path)
	{
		if (this->_filePaths.size() > 0) {
			return ;
		}

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

	Image* GetImage()
	{
		if (this->_filePaths.size() == 0)
		{
			return nullptr;
		}
		if ((++this->_imgIndex) >= this->_filePaths.size())
		{
			this->_imgIndex = 0;
		}

		const std::string imgPath = this->_filePaths[this->_imgIndex];
		bmp_header hdr;
		void* payload = 0;
		bmp_error ret = bmp_img_read_ext_3(imgPath.c_str(), hdr, payload, malloc_ptr);
		if (ret == bmp_error::BMP_OK)
		{
			std::cout << "read image: " << imgPath << " success!" << std::endl;
			if (this->mCurrentImg)
			{
				ReleaseImage(this->mCurrentImg);
			}
			this->mCurrentImg = CreateImage(hdr.biWidth, hdr.biHeight, hdr.biPlanes);
			memcpy(this->mCurrentImg->data, payload, hdr.biWidth * hdr.biHeight * hdr.biPlanes);
			this->mMutex->unlock();
		}
		else{
			std::cout << "In FolderGrabber, read image error!" << std::endl;
		}
		
		// release data pointer from image file module
		if (payload)
		{
			free(payload);
		}

	}

private:
	std::vector<std::string> _filePaths;
	int _imgIndex;
};