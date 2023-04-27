using System.Runtime.Versioning;

namespace Client
{
    public partial class Form1 : Form
    {
        private ViewModels.FormViewModel _viewModel = null;
        [RequiresPreviewFeatures]
        public Form1()
        {
            InitializeComponent();
            _viewModel = new ViewModels.FormViewModel();
            this.DataContext = _viewModel;
            this.btnGetImage.DataBindings.Add(new Binding(nameof(btnGetImage.Command), this.DataContext, nameof(this._viewModel.GetImageCommand), true));
            this.btnGetImage.DataBindings.Add(new Binding(nameof(btnGetImage.CommandParameter), this.btnGetImage, nameof(this.btnGetImage.Text), true));
            this.ptbDisplay.DataBindings.Add(new Binding(nameof(this.ptbDisplay.Image), this.DataContext, nameof(this._viewModel.ReceiveImage)));
        }
    }
}