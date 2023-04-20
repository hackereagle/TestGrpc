# TwoCppGrpcProjectCommunication
<p>
This is testing two cpp grpc application communication.</br>
And here is going to note how to build cpp gRpc application.
</p>

## Enviroment
* OS: Win11 WSL - Ubuntu 
* 

## Install
Please execute DownloadAndCOnfigGrpc.sh:
```
sudo ./DownloadAndConfigGrpc.sh
```
If you don't sure whether your enviroment can build gRPC, you can type `y` or `Y` when shell script ask install compiling tools.

## Develop
First, you need to define .proto file. I define and locate it in Protos folder.</br>
Second, compiling .proto file to server and client codes. In this project, I already write compiling command into shell script. You just execute it like
```
./CompileProtoFile.sh
```
It will generate 4 files: `<proto-file-name>.grpc.pb.cc`, `<proto-file-name>.grpc.pb.h`, `<proto-file-name>.pb.cc` and `<proto-file-name>.pb.h` .</br>

## Reference
* [Learning-gRPC 00Protobuf]("https://kevinbird61.github.io/grpc-practice/" "learning gRpc about protobuf")
* [Learning-gRPC 00Protobuf]("https://kevinbird61.github.io/grpc-practice/" "learning gRpc HelloWorld tutorial")
* [github-grpc to start using gRRPC c++]("https://github.com/grpc/grpc/tree/master/src/cpp" "grpc official tutorial")
* [github-grpc install guide]("https://github.com/grpc/grpc/blob/master/BUILDING.md" "gRPC install guide")

## TODO
- [ ] Write install compiling tools in MacOs into 00.ConfigBuildEnv.sh