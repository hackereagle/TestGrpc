using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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
                    var result = _client.GetImage(request);
                    // convert to bitmap and publish to observers.
                    var img = ConvertByteArr2Bitmap(result.Width, result.Height, result.Data.ToByteArray());
                    _receiveImage.OnNext(img);
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

        private Bitmap ConvertByteArr2Bitmap(int width, int height, byte[] data)
        { 
            Bitmap img = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            ColorPalette pal = img.Palette;
            for (int i = 0; i < 256; i++)
                pal.Entries[i] = Color.FromArgb(255, i, i, i);

            img.Palette = pal;
            BitmapData bitmapData = img.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            unsafe
            {
                fixed (byte* dataPtr = &data[0])
                {
                    byte* imgPtr = (byte*)bitmapData.Scan0;
                    int size = width * height;
                    for (int i = 0; i < size; i++)
                        *(imgPtr + i) = *(dataPtr + i);
                }
            }
            img.UnlockBits(bitmapData);
            return img;
        }
    }
}
