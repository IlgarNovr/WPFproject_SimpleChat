using System;
using System.Windows.Media;

namespace Telegram
{
    public class Chat
    {
        public string Text { get; set; }

        public string Time { get; set; }

        public string Alignment { get; set; }

        public SolidColorBrush BackColor { get; set; }

        public SolidColorBrush TextColor { get; set; }

        public Chat(string text, string alignment, SolidColorBrush backColor, SolidColorBrush textColor)
        {
            Text = text;
            Alignment = alignment;
            BackColor = backColor;
            TextColor = textColor;
            Time = DateTime.Now.ToShortTimeString();
        }
    }
}
