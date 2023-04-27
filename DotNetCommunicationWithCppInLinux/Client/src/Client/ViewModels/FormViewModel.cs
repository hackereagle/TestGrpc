using Client.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SclinMiscLib;
using System.Reactive.Linq;

namespace Client.ViewModels
{
    public class FormViewModel : ViewModelBase
    {
        private Base.IAsyncCommand<object> _getImageCommandInstance = null;
        private Bitmap _image;
        private BusinessLogics.ImageClient _imageClient;
        public FormViewModel() 
        {
            _getImageCommandInstance = new Base.AsyncCommand<object>(GetImage);
            _image = new Bitmap(255, 255);
            _imageClient = new BusinessLogics.ImageClient();
            _imageClient.ObserverImageReceive.Subscribe(image => 
            {
                ReceiveImage = image;
            });
        }

        #region Properties
        public ICommand GetImageCommand => _getImageCommandInstance;
        public Image ReceiveImage
        {
            set 
            { 
                _image = value as Bitmap;
                OnPropertyChanged("ReceiveImage");
            }
            get { return _image; }
        }
        #endregion Properties

        private async Task GetImage(object obj)
        {
            ConsoleWriterWrapper.Log($"command parameter = {(string)obj}");
            await _imageClient.SendImageRequest(obj);
        }
    }
}
