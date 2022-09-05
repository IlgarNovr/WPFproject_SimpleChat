using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Telegram
{
    [AddINotifyPropertyChangedInterface]
    public partial class MainWindow : Window
    {
        //Colors
        SolidColorBrush MyTextColor = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        SolidColorBrush OtherTextColor = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        SolidColorBrush MyMsgColor = new SolidColorBrush(Color.FromRgb(130, 0, 180));
        SolidColorBrush OtherMsgColor = new SolidColorBrush(Color.FromRgb(33, 19, 92));

        public string Username { get; set; }
        User user;
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Chat> Chats { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ObservableCollection<Chat> chat1 = new ObservableCollection<Chat>
            {
                new Chat("Hi","Left",OtherMsgColor,OtherTextColor),
            };
            ObservableCollection<Chat> chat2 = new ObservableCollection<Chat>
            {
                new Chat("Hello","Left",OtherMsgColor,OtherTextColor),
            };
            ObservableCollection<Chat> chat3 = new ObservableCollection<Chat>
            {
                new Chat("How are you?","Left",OtherMsgColor,OtherTextColor),
            };
            ObservableCollection<Chat> chat4 = new ObservableCollection<Chat>
            {
                new Chat("See You","Left",OtherMsgColor,OtherTextColor),
            };
            Users = new ObservableCollection<User>
            {
                new User("David", "pack://application:,,,/Images/user.png"),
                new User("John", "pack://application:,,,/Images/user.png"),
                new User("George", "pack://application:,,,/Images/user.png"),
                new User("Anna", "pack://application:,,,/Images/user.png")
            };

            Users[0].Chats = chat1;
            Users[0].LastText();
            Users[0].LastMessageTime = DateTime.Now.ToShortTimeString();

            Users[1].Chats = chat2;
            Users[1].LastText();
            Users[1].LastMessageTime = DateTime.Now.ToShortTimeString();
            
            Users[2].Chats = chat3;
            Users[2].LastText();
            Users[2].LastMessageTime = DateTime.Now.ToShortTimeString();

            Users[3].Chats = chat4;
            Users[3].LastText();
            Users[3].LastMessageTime = DateTime.Now.ToShortTimeString();
            DataContext = this;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            user = UserListView.SelectedItem as User;
            Chats = (UserListView.SelectedItem as User).Chats;
            Username = user.Username;
            MsgBox.Visibility = Visibility.Visible;
            UserInfo.Visibility = Visibility.Visible;
        }


        private void TextBox_MouseEnter(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb) {
                if (tb.Text == "Write a message...")
                {
                    tb.Text = "";
                    tb.Foreground = MyTextColor;
                    ChatScroll.ScrollToEnd();
                }
            }
        }

        private void TextBox_MouseLeave(object sender, RoutedEventArgs e)
        {
            if(sender is TextBox tb)
            {
                if (string.IsNullOrEmpty(tb.Text) && string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.Foreground = Brushes.LightGray;
                    tb.Text = "Write a message...";
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (e.Key == Key.Enter && !string.IsNullOrEmpty(tb.Text) && !string.IsNullOrWhiteSpace(tb.Text))
                {
                    Chats.Add(new Chat(tb.Text, "Right", MyMsgColor, OtherTextColor));
                    switch (tb.Text)
                    {
                        case "Hi":
                            Chats.Add(new Chat("How are you?", "Left", OtherMsgColor, OtherTextColor));
                            break;
                        case "Fine":
                            Chats.Add(new Chat("Very Good", "Left", OtherMsgColor, OtherTextColor));
                            break;
                        case "What is your name":
                            Chats.Add(new Chat("you know my name, if you didn't know we wouldn't be able to speak ;)", "Left", OtherMsgColor, OtherTextColor));
                            break;
                        case "Ok":
                            Chats.Add(new Chat("Ok", "Left", OtherMsgColor, OtherTextColor));
                            break;
                        case "salam":
                            Chats.Add(new Chat("Alekum Salam", "Left", OtherMsgColor, OtherTextColor));
                            break;
                        case "hello":
                            Chats.Add(new Chat("How are you?", "Left", OtherMsgColor, OtherTextColor));
                            break;
                        case "AYE":
                            Chats.Add(new Chat("Vecna", "Left", OtherMsgColor, OtherTextColor));
                            break;
                        case "Bye":
                            Chats.Add(new Chat("See You", "Left", OtherMsgColor, OtherTextColor));
                            break;
                        default:
                            Chats.Add(new Chat("I dont understand you! Bye", "Left", OtherMsgColor, OtherTextColor));
                            break;
                    }
                    Users.ToList().Find(u => u.Username == Username).LastText();
                    Users = new ObservableCollection<User>(Users.OrderByDescending(u => u.Time));
                    tb.Text = "";
                }
            }
        }
    }
}
