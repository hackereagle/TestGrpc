#!/bin/bash

ProtoFileFolder="Protos"
#ResultFileFolder="${ProtoFileFolder}/Result"
ResultFileFolder="build/${ProtoFileFolder}"
if [ ! -d "${ResultFileFolder}" ]
then
	mkdir -p "${ResultFileFolder}"
fi

protoc -I $ProtoFileFolder --grpc_out=$ResultFileFolder --plugin=protoc-gen-grpc=`which grpc_cpp_plugin` ${ProtoFileFolder}/compute.proto
protoc -I $ProtoFileFolder --cpp_out=$ResultFileFolder ${ProtoFileFolder}/compute.proto