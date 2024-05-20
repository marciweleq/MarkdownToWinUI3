using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Text;

namespace MarkdownToWinUi3.MdToWinUi
{

    public static class MdSettings
    {
        public static IDictionary<string, MdSetting> DefaultSettings
        {
            get => new Dictionary<string, MdSetting>{
                { "#", new MdSetting { FontWeight = FontWeights.SemiBold, FontSize = 28 } },
                { "##", new MdSetting { FontWeight = FontWeights.SemiBold, FontSize = 20 } },
                { "###", new MdSetting { FontWeight = FontWeights.Normal, FontSize = 18 } },
                { "**", new MdSetting { FontWeight = FontWeights.Bold } },
                { "*", new MdSetting { FontWeight = FontWeights.Bold } },

        };
        }

    }

    public class MdSetting
    {
        public Brush Foreground { get; set; } = null;
        public double FontSize { get; set; } = 14;
        public FontWeight FontWeight { get; set; } = FontWeights.Normal;
        public FontFamily FontFamily { get; set; } = new FontFamily("Segoe UI");
        public Thickness Margin { get; set; } = new Thickness(0);
        public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Left;
        public FontStyle FontStyle { get; set; } = FontStyle.Normal;

        public void ApplyStyle(TextBlock textBlock)
        {
            if (textBlock == null)
                return;

            if (Foreground == null)
            {
                var isDarkTheme = Application.Current.RequestedTheme == ApplicationTheme.Dark;
                textBlock.Foreground = new SolidColorBrush(isDarkTheme ? Colors.White : Colors.Black);
            }
            else
            textBlock.Foreground = Foreground;
            textBlock.FontSize = FontSize;
            textBlock.FontWeight = FontWeight;
            textBlock.FontFamily = FontFamily;
            textBlock.Margin = Margin;
            textBlock.HorizontalAlignment = HorizontalAlignment;
            textBlock.FontStyle = FontStyle;
        }

        public void ApplyStyle(Run textBlock)
        {
            if (textBlock == null)
                return;

            if (Foreground == null)
            {
                var isDarkTheme = Application.Current.RequestedTheme == ApplicationTheme.Dark;
                textBlock.Foreground = new SolidColorBrush(isDarkTheme ? Colors.White : Colors.Black);
            }
            else
                textBlock.Foreground = Foreground;
            textBlock.FontWeight = FontWeight;
            textBlock.FontFamily = FontFamily;
            textBlock.FontStyle = FontStyle;
        }
    }


}