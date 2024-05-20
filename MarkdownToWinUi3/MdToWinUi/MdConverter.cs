using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;

namespace MarkdownToWinUi3.MdToWinUi
{
    public static class MdConverter
    {

        static List<string> LineSyntaxElements = new List<string> { "#", "##", "###" };
        static List<string> MidlineSyntaxElements = new List<string> { "**", "__", "*" };
        public static StackPanel ConvertMarkdownToStackPanel(string markdownText, IDictionary<string, MdSetting> settings = null)
        {
            if (settings == null) settings = MdSettings.DefaultSettings;

            var stackPanel = new StackPanel();

            foreach (var line in markdownText.Split('\n'))
            {
                var textLine = line;
                var textBlock = new TextBlock();

                MdSetting foundSetting = null;

                foreach (var element in LineSyntaxElements)
                    if (line.StartsWith(element + " "))
                    {
                        textLine = line.Remove(0, element.Length + 1);

                        if (settings.TryGetValue(element, out var s))
                        {
                            foundSetting = s; break;
                        }
                    }

                if (foundSetting != null)
                    foundSetting.ApplyStyle(textBlock);

                var inlineText = line;
                bool foundMidlineStylings = false;
                IDictionary<string, bool> isEnabled = new Dictionary<string, bool>();
                foreach (var element in MidlineSyntaxElements) isEnabled[element] = false;

                List<IDictionary<string, bool>> keyframes = new List<IDictionary<string, bool>>();
                List<int> atIndex = new List<int>();
                bool prevUsedThis = false;
                for (int i = 0; i < inlineText.Length; i++)
                {
                    IDictionary<string, bool> currentKeyframe = new Dictionary<string, bool>((i > 0) ? (keyframes.Last()) : (isEnabled));
                    if (prevUsedThis) prevUsedThis = false;
                    else
                    {
                        if (i + 1 < inlineText.Length)
                            if (MidlineSyntaxElements.Contains(inlineText[i] + "" + inlineText[i + 1]))
                            {
                                foundMidlineStylings = true;

                                var tag = inlineText[i] + "" + inlineText[i + 1];
                                currentKeyframe[tag] = !currentKeyframe[tag];
                                prevUsedThis = true;
                                keyframes.Add(currentKeyframe);
                                atIndex.Add(i);
                                atIndex.Add(i + 1);

                                continue;
                            }

                        if (MidlineSyntaxElements.Contains(inlineText[i] + ""))
                        {
                            foundMidlineStylings = true;

                            var tag = inlineText[i] + "";
                            currentKeyframe[tag] = !currentKeyframe[tag];
                            keyframes.Add(currentKeyframe);
                            atIndex.Add(i);

                            continue;
                        }
                    }

                    keyframes.Add(currentKeyframe);
                }

                if (!foundMidlineStylings)
                    textBlock.Text = textLine;
                else
                {
                    List<Run> runs = new List<Run>();

                    Run currentRun = null;
                    for (int i = 0; i < keyframes.Count; i++)
                    {
                        var prevKeyframe = (i == 0) ? null : keyframes[i - 1];
                        var currentKeyframe = keyframes[i];
                        if (prevKeyframe == null)
                        {
                            var style = GetInlineStyle(currentKeyframe);
                            currentRun = (new Run { FontWeight = style.fontWeight, FontStyle = style.fontStyle }); ;

                            if (!atIndex.Contains(i))
                                currentRun.Text += textLine[i];
                        }
                        else if (AreDictionariesEqual(prevKeyframe, currentKeyframe))
                        {
                            if (!atIndex.Contains(i))
                                currentRun.Text += textLine[i];
                        }
                        else
                        {
                            runs.Add(currentRun);

                            var style = GetInlineStyle(currentKeyframe);
                            currentRun = (new Run { FontWeight = style.fontWeight, FontStyle = style.fontStyle }); ;

                            if (!atIndex.Contains(i))
                                currentRun.Text += textLine[i];
                        }
                    }
                    runs.Add(currentRun);

                    foreach (var run in runs)
                        textBlock.Inlines.Add(run);
                }

                stackPanel.Children.Add(textBlock);
            }

            return stackPanel;
        }

        public static (FontWeight fontWeight, FontStyle fontStyle) GetInlineStyle(IDictionary<string, bool> inlineSettings)
        {
            FontWeight fontWeight = FontWeights.Normal;
            FontStyle fontStyle = FontStyle.Normal;
            if (inlineSettings["**"] || inlineSettings["__"]) fontWeight = FontWeights.Bold;
            if (inlineSettings["*"]) fontStyle = FontStyle.Italic;

            return (fontWeight, fontStyle);
        }

        static bool AreDictionariesEqual<TKey, TValue>(IDictionary<TKey, TValue> dict1, IDictionary<TKey, TValue> dict2)
        {
            if (dict1.Count != dict2.Count)
                return false;

            foreach (var kvp in dict1)
            {
                if (!dict2.TryGetValue(kvp.Key, out TValue value) || !EqualityComparer<TValue>.Default.Equals(kvp.Value, value))
                    return false;
            }

            return true;
        }

    }
}
