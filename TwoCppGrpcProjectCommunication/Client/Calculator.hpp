#ifndef _CALCULATOR_H_
#define _CALCULATOR_H_

#include <iostream>
#include <sstream>
#include <string>
#include "GenericException.hpp"

class Calculator
{
public:
	Calculator()
	{}

	~Calculator()
	{}

	std::string Calculate(std::string num1, std::string opt, std::string num2)
	{
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