#!/bin/bash

if [[ "$OSTYPE" =~ ^darwin ]]
then
	echo "This is Mac OS!"
	read -p "Please ensure already installed brew!" TMP

	brew install grpc
	brew install openssl
elif [[ "$OSTYPE" =~ ^linux ]]
then
	echo "Would you like to install compiling tools?[y/n]"
	read NeedBuildEnv
	if [[ $NeedBuildEnv = "y" ]] || [[ $NeedBuildEnv = "Y" ]]
	then
		echo "Begin install compiling tool."
		/bin/bash ./00.ConfigBuildEnv.sh
		echo "End install compiling tools."
	fi

	# clone gRPC code
	git clone -b v1.54.0 https://github.com/grpc/grpc
	cd grpc
	echo "Current directory = $PWD"
	git submodule update --init

	# compiling and install
	## Use externally provided env to determine build parallelism, otherwise use default.
	GRPC_CPP_DISTRIBTEST_BUILD_COMPILER_JOBS=${GRPC_CPP_DISTRIBTEST_BUILD_COMPILER_JOBS:-4}
	echo "Build and install gRPC with \"module\" - build dependencies alongside gRPC. The source code is obtained from gRPC's git submodules."
	mkdir cmake/build/ -p
	pushd cmake/build
	cmake ../.. -DgRPC_INSTALL=ON                \
				-DCMAKE_BUILD_TYPE=Release       \
				-DgRPC_ABSL_PROVIDER=module      \
				-DgRPC_CARES_PROVIDER=module     \
				-DgRPC_PROTOBUF_PROVIDER=module  \
				-DgRPC_RE2_PROVIDER=module       \
				-DgRPC_SSL_PROVIDER=module       \
				-DgRPC_ZLIB_PROVIDER=module
	make "-j${GRPC_CPP_DISTRIBTEST_BUILD_COMPILER_JOBS}" install
	popd
fi