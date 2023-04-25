# TwoCppGrpcProjectCommunication
<p>
This is testing two cpp grpc application communication.</br>
And here is going to note how to build cpp gRpc application.
</p>

## Enviroment
* OS: Win11 WSL - Ubuntu 20.4.5
* CMake 3.16.3
* gcc 9.4.0

## Install
Please execute DownloadAndCOnfigGrpc.sh:
```
sudo ./DownloadAndConfigGrpc.sh
```
If you don't sure whether your enviroment can build gRPC, you can type `y` or `Y` when shell script ask install compiling tools. If you choose `y`, the script will also install protobuf and cmake function related to protobuf, such as protobuf_generate.

## Develop
First, you need to define .proto file. I define and locate it in Protos folder.</br>
Second, compiling .proto file to server and client codes. In this project, I already write compiling command into shell script. You just execute it like
```
./CompileProtoFile.sh
```
It compile protobuf twice and it will generate 4 files: `<proto-file-name>.grpc.pb.cc`, `<proto-file-name>.grpc.pb.h`, `<proto-file-name>.pb.cc` and `<proto-file-name>.pb.h` .</br>
`<proto-file-name>.pb.cc` and `<proto-file-name>.pb.h` are protobut serialize and deserialize codes.</br>
`<proto-file-name>.grpc.pb.cc` and `<proto-file-name>.grpc.pb.h` are server and client codes.</br>
And you can generate server/client codes via CMake
 (refer to [gRPC and Plugin support in CMake](https://www.f-ax.de/dev/2020/11/08/grpc-plugin-cmake-support.html "This is my compiling .porto file with CMake reference.")). And this way is better than compiling with `CompileProtoFile.sh`. Because using CMake helps with keeping all the dependencies in one place.</br>
I wrote done a CMakeLists.txt in Protos. And I add Protos folder into TwoCppGrpcProjectCommunication/CMakeLists.txt. I define a task that only compile proto files.</br>
**NOTE:** CMakeLists.txt of .proto can not use aux_source_directory. aux_source_directory can not collect .proto files.

## Reference
* [Learning-gRPC 00Protobuf](https://kevinbird61.github.io/grpc-practice/ "learning gRpc about protobuf")
* [Learning-gRPC 00Protobuf](https://kevinbird61.github.io/grpc-practice/ "learning gRpc HelloWorld tutorial")
* [github-grpc to start using gRRPC c++](https://github.com/grpc/grpc/tree/master/src/cpp "grpc official tutorial")
* [github-grpc install guide](https://github.com/grpc/grpc/blob/master/BUILDING.md "gRPC install guide")

## TODO
- [ ] Write install compiling tools in MacOs into 00.ConfigBuildEnv.sh
- [ ] Survey how to control exception as service occur exception.