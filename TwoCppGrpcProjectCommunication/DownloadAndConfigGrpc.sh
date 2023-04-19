#!/bin/bash

echo "Would you like to install compiling tools?[y/n]"
read NeedBuildEnv
if [[ $NeedBuildEnv = "y" ]] || [[ $NeedBuildEnv = "Y" ]]
then
	echo "Begin install compiling tool."
	/bin/bash ./00.ConfigBuildEnv.sh
	echo "End install compiling tools."
fi

# clone gRPC code
