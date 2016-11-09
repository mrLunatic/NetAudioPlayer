using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using NetAudioPlayer.Core.Data;
using NetAudioPlayer.Core.Message;
using NetAudioPlayer.Core.Service;
using SimpleTCP;

namespace NetAudioPlayer.WpfClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SimpleTcpClient _client;
        
        
        public MainWindow()
        {
            InitializeComponent();

            Loaded += OnLoaded;
            GetStatusButton.Click += GetStatusButtonOnClick;
            PlayButton.Click += PlayButtonOnClick;
            Slider.ValueChanged += SliderOnValueChanged;
            Slider.MouseUp += SliderOnMouseUp;
            Slider.PreviewMouseUp += SliderOnPreviewMouseUp;
        }

        private void SliderOnPreviewMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var msg = new SeekMessage()
            {
                Position = (Slider.Value)
            };

            var str = MessageParser.Serialize(msg);

            _client.Write(str);
        }

        private void SliderOnMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var msg = new SeekMessage()
            {
                Position = (Slider.Value)
            };

            var str = MessageParser.Serialize(msg);

            _client.Write(str);
        }


        private void SliderOnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> routedPropertyChangedEventArgs)
        {
            //var msg = new SeekMessage()
            //{
            //    Position = (Slider.Value)
            //};

            //var str = MessageParser.Serialize(msg);

            //_client.Write(str);
        }

        private void PlayButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var msg = new PlayMessage();

//            @"C:\tmp\2004 - Demo\1 - Real intro.mp3",
//@"C:\tmp\2004 - Demo\2 - Святые Подворотен  (Демо).mp3",
//@"C:\tmp\2004 - Demo\3 - Пустой Вокзал (2mancrew).mp3"  ,
//@"C:\tmp\2004 - Demo\00 - New Project.mp3"               ,
//@"C:\tmp\2004 - Demo\00 - Зеркало.mp3"                    ,
//@"C:\tmp\2004 - Demo\00 - О Лжи и Вере.mp3"                ,
//@"C:\tmp\2004 - Demo\00 - Тихойводы.mp3"                    ,

            var tracks = new List<Track>()
            {
                new Track()
                {
                    Id = 2,
                    Uri = @"C:\tmp\2004 - Demo\2 - Святые Подворотен  (Демо).mp3",
                },
                new Track()
                {
                    Id = 3,
                    Uri = @"C:\tmp\2004 - Demo\00 - Зеркало.mp3"   ,
                },
            };
            msg.Items = tracks;

            var str = MessageParser.Serialize(msg);

            _client.Write(str);
        }

        private void GetStatusButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var msg = new StatusInfoMessage();

            var str = MessageParser.Serialize(msg);

            _client.Write(str);
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Thread.Sleep(2500);

            LogBox.Text += "Server started\r\n";

             _client = new SimpleTcpClient()
             {
                 Delimiter = (byte)'|'
             };

            _client.Connect("127.0.0.1", 5515);

            LogBox.Text += "Client connected\r\n";

            _client.DataReceived += ClientOnDataReceived;
        }

        private void ClientOnDataReceived(object sender, Message message)
        {

            var msg = MessageParser.Parse(message.MessageString);
                   
            RecievedBox.Dispatcher.Invoke(() =>
            {
                if (msg is ShortStatusInfoMessage)
                {
                    var info = (ShortStatusInfoMessage)msg;

                    if (!Slider.IsMouseCaptureWithin)
                    Slider.Value = info.CurrentPosition;
                }
                else if (msg is StatusInfoMessage)
                {
                    var status = (StatusInfoMessage) msg;

                    Slider.Maximum = status.CurrentItem.Duration;
                }


                RecievedBox.Text = message.MessageString;
            })
            ;
        }
    }
}
