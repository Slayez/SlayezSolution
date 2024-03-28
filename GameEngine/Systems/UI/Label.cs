using GameEngine.Systems.Main;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameEngine.Systems.UI
{
    public class Label : UserInterfaceElement
    {
        public string Text { get => _text; set => _text = value; }
        private string _text;

        public new Vector2 GetBound { get => ResourseManager.Font(Font).MeasureString(Lines.ToStringList(), Scale); }
        public struct SMyText
        {
            public string text { get; set; }
            public bool newLine { get; set; }
            public Color color { get; set; }

            public SMyText(string newtext, Color textcolor, bool newline) : this()
            {
                this.text = newtext;
                this.color = textcolor;
                this.newLine = newline;
            }
        }

        public Label()
        {
        }

        public Label(string text, EAlignUI eAlign, float scale, Color color)
        {
            _text = text ?? throw new ArgumentNullException(nameof(text));
            Align = eAlign;
            Scale = scale;
            SetLines();
            for (int i = 0; i < Lines.Count; i++)
            {
                Lines[i] = new SMyText(Lines[i].text, color, Lines[i].newLine);
            }
        }

        private List<SMyText> Lines = new List<SMyText>();

        public string Font { get => _font != null ? _font : ResourseManager.DEFAULT_FONT_NAME; set => _font = value; }
        private string _font;

        public float Scale { get => _scale; set => _scale = value; }
        private float _scale = 1;

        private string Format;
        private string Property;
        private object Link;
        /// <summary>
        /// Назначает ссылку на класс и формат текста
        /// </summary>
        /// <param name="format"> text <b><i>&lt;$! R,G,B></i></b> ColoredText <b><i>&lt;$=></i></b> text </param>
        /// <param name="propertys">param1;param2;param3</param>
        /// <param name="link">Only class</param>
        public void SetValue(string format, string propertys, object link)
        {
            Format = format;
            Property = propertys;
            Link = link;
        }

        public void SetText()
        {
            if (Format != null)
            {
                if (Link != null)
                {
                    if (Property != null)
                    {
                        if (Property.Contains(";"))
                        {
                            List<object> obj = new List<object>();
                            foreach (string property in Property.Split(';'))
                            {
                                obj.Add(((Link.GetType().GetProperty(property)).GetValue(Link)));
                            }
                            _text = string.Format(Format, obj.ToArray());
                        }
                        else
                        {
                            var obj = ((Link.GetType().GetProperty(Property)).GetValue(Link));
                            if (Link.GetType() == typeof(Point))
                                obj = obj.ToString();
                            _text = string.Format(Format, obj);
                        }
                    }
                }
                else
                {
                    _text = string.Format(Format, null);
                }
            }
        }

        public void SetLines()
        {
            if (_text != null)
            {
                Regex regex = new Regex("[^<$=>]*[<][$][!][ ][0-9]{1,3}[, ][0-9]{1,3}[, ][0-9]{1,3}[>][^<$=>]*[<][$][=][>][^<$!>]*");
                MatchCollection collection = regex.Matches(_text);
                List<string> coloredString = new List<string>();
                if (collection.Count > 0)
                    foreach (Match text in collection)
                    {
                        coloredString.Add(text.Value);
                    }
                else
                    coloredString.Add(_text);
                Lines = new List<SMyText>();
                bool newline = false;
                foreach (string text in coloredString)
                {
                    string newtext = RemoveColorTag(text);
                    Color textcolor = GetColorFromTag(text);
                    List<string> colorlines;
                    if (text.Contains("<$"))
                        colorlines = new List<string>(text.Split(new string[] { "<$" }, StringSplitOptions.RemoveEmptyEntries));
                    else
                    {
                        colorlines = new List<string>();
                        colorlines.Add(text);
                    }

                    for (int i = 0; i < colorlines.Count; i++)
                    {
                        char tagchar = colorlines[i][0];
                        if (colorlines.Count > 1)
                            if ((tagchar == '!') | (tagchar == '='))
                                colorlines[i] = "<$" + colorlines[i];
                        Color linecolor = GetColorFromTag(colorlines[i]);
                        string stripedtext = RemoveColorTag(colorlines[i]);
                        if (stripedtext != "\n")
                        {
                            if (stripedtext.IndexOf('\n') >= 0)
                            {
                                List<string> stripedlines = new List<string>(stripedtext.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries));
                                for (int p = 0; p < stripedlines.Count; p++)
                                {
                                    Lines.Add(new SMyText(stripedlines[p], linecolor, newline));
                                    newline = true;
                                }
                            }
                            else
                            {
                                Lines.Add(new SMyText(stripedtext, linecolor, newline));
                                newline = false;
                            }
                        }
                        else
                        {
                            newline = true;
                        }
                    }
                }
            }
        }

        public Color GetColorFromTag(string text)
        {
            Color c = ResourseManager.TextColor;
            string newtext = text;
            Regex regex = new Regex("[<][$][!][ ][0-9]{1,3}[, ][0-9]{1,3}[, ][0-9]{1,3}[>]");
            if (regex.IsMatch(newtext))
            {
                newtext = newtext.Substring(newtext.IndexOf("<$! ") + 4);
                string[] color = newtext.Substring(0, (newtext.IndexOf(">"))).Split(',');
                int r = Int32.Parse(color[0]);
                int g = Int32.Parse(color[1]);
                int b = Int32.Parse(color[2]);
                c = new Color(r, g, b);
            }
            return c;
        }


        public string RemoveColorTag(string text)
        {
            string newtext = text;
            if (newtext.Contains("<$"))
            {
                newtext = newtext.Remove(newtext.IndexOf("<$"), newtext.IndexOf(">") - newtext.IndexOf("<$") + 1);
            }
            return newtext;
        }

        public override void Update()
        {
            SetText();
            SetLines();
        }

        public override void Draw()
        {
            Vector2 point = transform.Position.Add(Align.ToVector(GetBound)).To2d();
            Vector2 pos = point;

            if (Lines.Count > 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    if (Lines[i].newLine)
                    {
                        pos.X = point.X;
                        if (i != 0)
                            pos.Y += ResourseManager.Font(Font).MeasureString(Lines[i - 1].text, Scale).Y;
                    }
                    else
                    {
                        if (i != 0)
                            pos.X += ResourseManager.Font(Font).MeasureString(Lines[i - 1].text, Scale).X;
                    }

                    RenderSystem.window.spriteBatch.DrawString(ResourseManager.Font(Font), Lines[i].text, pos, Lines[i].color, transform.Rotation, new Vector2(0, 0), new Vector2(Scale), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, DepthLayer);
                }
            }
            /*
                RenderSystem.window.spriteBatch.DrawString(ResourseManager.DEFAULT_FONT, Text, transform.Position, Color.Coral);
            else
                RenderSystem.window.spriteBatch.DrawString(ResourseManager.DEFAULT_FONT, "null", transform.Position, Color.Coral);*/
        }

        public void Draw(Vector2 bound)
        {
            Vector2 point = transform.Position.To2d() + Align.ToVector(bound);
            Vector2 pos = point;

            if (Lines.Count > 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    if (Lines[i].newLine)
                    {
                        pos.X = point.X;
                        if (i != 0)
                            pos.Y += ResourseManager.Font(Font).MeasureString(Lines[i - 1].text, Scale).Y;
                    }
                    else
                    {
                        if (i != 0)
                            pos.X += ResourseManager.Font(Font).MeasureString(Lines[i - 1].text, Scale).X;
                    }

                    RenderSystem.window.spriteBatch.DrawString(ResourseManager.Font(Font), Lines[i].text, pos, Lines[i].color, transform.Rotation, new Vector2(0, 0), new Vector2(Scale), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, DepthLayer);
                }
            }
        }
    }
}
