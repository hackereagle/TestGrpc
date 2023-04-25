#pragma once

#include <iostream>
#include <string>
#include <exception>

class GenericException : public std::exception
{
public:
	GenericException() {}

	GenericException(std::string msg) : _message(msg) {}

	~GenericException() {}

    virtual const char* what() const throw()
	{
		return this->_message.c_str();
	}

private:
	std::string _message = "occur any exception!";
};