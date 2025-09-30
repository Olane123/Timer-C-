using System.Threading.Tasks;

namespace STOP
{
    public partial class MainPage : ContentPage
    {
        private readonly System.Timers.Timer _timer;
        private TimeSpan _targetTime;
        public MainPage()
        {
            InitializeComponent();
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += CheckTime;
            _timer.AutoReset = true;
        }

        private void TimerEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is Entry entry)
            {
                string currentText = entry.Text;
                string filteredText = "";

                foreach (char c in currentText)
                {
                    if (char.IsDigit(c))
                    {
                        filteredText += c;
                    }
                }

                if (entry.Text != filteredText)
                {
                    entry.Text = filteredText;
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            _targetTime = timePicker.Time;

            _timer.Start();

            DisplayAlert("Время установлено",
                $"Действие выполнится в {_targetTime}", "OK");
        }

        private void CheckTime(object sender, System.Timers.ElapsedEventArgs e)
        {
            var now = DateTime.Now.TimeOfDay;

            if (now >= _targetTime && now < _targetTime.Add(TimeSpan.FromSeconds(1)))
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Уведомление", "Время пришло!", "OK");
                    _timer.Stop();
                });
            }
        }
    }
}
