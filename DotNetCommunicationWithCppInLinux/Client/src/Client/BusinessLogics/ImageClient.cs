using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Grpc;
using SclinMiscLib;

namespace Client.BusinessLogics
{
    internal class ImageClient
    {
        private ImageComputer.ImageService.ImageServiceClient _client;
        private ISubject<Bitmap> _receiveImage;
        public ImageClient()
        {
            var channel = Grpc.Net.Client.GrpcChannel.ForAddress("http://localhost:50055");
            _client = new ImageComputer.ImageService.ImageServiceClient(channel);
            _receiveImage = new Subject<Bitmap>();
        }

        public IObservable<Bitmap> ObserverImageReceive => _receiveImage.AsObservable();

        public async Task SendImageRequest(object obj)
        {
            await Task.Run(() => 
            {
                try
                {
                    ImageComputer.ImageRequest request = new ImageComputer.ImageRequest();
                    request.LightLevel = 255;
                    request.GetNumbersOfImg = 1;
                    // TODO: set request member
                    var result = _client.GetImage(request);
                    // TODO: trans to bitmap and publish to observers.
                    //_receiveImage.OnNext();
                    ConsoleWriterWrapper.Log($"width = {result.Width}, height = {result.Height}");
                }
                catch (Grpc.Core.RpcException ex)
                {
                    ConsoleWriterWrapper.Log($"In SendImageRequest occur gRPC error: {ex.Status.StatusCode.ToString()}", ex);
                }
                catch (Exception ex)
                { 
                    ConsoleWriterWrapper.Log($"In SendImageRequest occur error", ex);
                }
            });
        }
    }
}
