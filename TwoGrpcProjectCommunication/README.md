# Two gRpc Project Communication
<p>
This is my first gRpc project. I will use MVVM testing project to modify.</br>
I am going to abstract computing part to microservice.
</p>

Here note some point:</br>
1. If grpc client, it must to install following packages via NuGet:
```
Google.protobuf 
Grpc.Net.Client 
Grpc.Tools
```
2. No matter client or server, if modified .proto file, it need to rebuild project. When we compiling, IDE will create base class and RPC stub class. We just need to inherit the base class to develop.