using GameEngine.Systems.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static GameEngine.Tile;

namespace GameEngine
{
    public static class ResourseManager
    {
        public const string FONT_DIR = "Fonts";
        public const string TEXTURE_DIR = "Textures";

        public const string DEFAULT_FONT_NAME = "Vollkorn-Regular";
        public static SpriteFont DEFAULT_FONT { get => _fonts[DEFAULT_FONT_NAME]; }

        public static Color OutlineColor = Color.Black;
        public static Color DisabledColor = new Color(50, 50, 50);
        public static Color NormalColor = new Color(150, 150, 150);
        public static Color MouseOverColor = new Color(200, 200, 200);
        public static Color PressedColor = Color.Firebrick;
        public static Color TextColor = Color.White;

        public static Vector2 MeasureString(this SpriteFont font, string text, float scale)
        {
            return font.MeasureString(text).Multiply(scale);
        }

        public static Vector2 MeasureString(this SpriteFont font, List<string> lines, float scale)
        {
            Vector2 size = new Vector2(0, 0);

            foreach (string text in lines)
            {
                size.Y += font.MeasureString(text, scale).Y;
                if (size.X < font.MeasureString(text, scale).X)
                    size.X = font.MeasureString(text, scale).X;
            }

            return size;
        }

        public static SpriteFont Font(string name)
        {
            return _fonts.ContainsKey(name) ? _fonts[name] : DEFAULT_FONT;
        }

        public static bool LoadedTexture(string name)
        {
            return (_textures.ContainsKey(name));
        }

        public static Texture2D Texture(string name)
        {
            if (_textures.Count > 0)
            {
                if (_textures.ContainsKey(name))
                {
                    return _textures[name];
                }
                else
                {
                    return Texture("not_found");
                }
            }
            else
                return null;
        }

        public static Point GetSize(this Texture2D texture)
        {
            if (texture != null)
                return new Point(texture.Width, texture.Height);
            else
                return new Point(1, 1);
        }

        private static Dictionary<string, SpriteFont> _fonts = new Dictionary<string, SpriteFont>();

        public static List<Texture2D> Textures => new List<Texture2D>(_textures.Values);
        private static Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

        public static void Instalize()
        {
            //LoadTilesId();
            LoadFont("Vollkorn-Regular");
            LoadFont("alagard");

            Texture2D pixel = new Texture2D(RenderSystem.window.GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { new Color(255, 255, 255) });
            _textures.Add("pixel", pixel);
            pixel = new Texture2D(RenderSystem.window.GraphicsDevice, 2, 2);
            pixel.SetData(new Color[] { new Color(255, 0, 255), new Color(0, 0, 0), new Color(0, 0, 0), new Color(255, 0, 255) });
            _textures.Add("not_found", pixel);
            pixel = null;

            LoadTexture("isoselected");

            LoadTexture("Human_Idle");
            LoadTexture("Human_Run");

            LoadTexture("isotransform");
            IsoSliceTexture("isotransform");

            LoadTexture("player");

            LoadTileLibrary();
        }

        private static void LoadTileLibrary()
        {
            foreach (STileInfo info in Tile.TilesLibrary)
            {
                LoadTilePool(info.TextureName, info.SubPool);
            }
        }

        public static void LoadTilePool(string name, int count)
        {
            for (int i = 0; i < count; i++)
            {
                LoadTexture(name + i.ToString());
                IsoSliceTexture(name + i.ToString());
            }
        }

        public static void IsoSliceTexture(string name)
        {
            IsoSliceTexture(Texture(name), name);
        }
        public static void IsoSliceTexture(Texture2D texture, string name)
        {
            Texture2D texture011 = new Texture2D(RenderSystem.window.GraphicsDevice, texture.Bounds.Size.X, texture.Bounds.Size.Y);
            Color[] color011 = new Color[texture.Bounds.Size.X * texture.Bounds.Size.Y];
            Texture2D texture101 = new Texture2D(RenderSystem.window.GraphicsDevice, texture.Bounds.Size.X, texture.Bounds.Size.Y);
            Color[] color101 = new Color[texture.Bounds.Size.X * texture.Bounds.Size.Y];
            Texture2D texture110 = new Texture2D(RenderSystem.window.GraphicsDevice, texture.Bounds.Size.X, texture.Bounds.Size.Y);
            Color[] color110 = new Color[texture.Bounds.Size.X * texture.Bounds.Size.Y];

            Texture2D texture001 = new Texture2D(RenderSystem.window.GraphicsDevice, texture.Bounds.Size.X, texture.Bounds.Size.Y);
            Color[] color001 = new Color[texture.Bounds.Size.X * texture.Bounds.Size.Y];
            Texture2D texture100 = new Texture2D(RenderSystem.window.GraphicsDevice, texture.Bounds.Size.X, texture.Bounds.Size.Y);
            Color[] color100 = new Color[texture.Bounds.Size.X * texture.Bounds.Size.Y];
            Texture2D texture010 = new Texture2D(RenderSystem.window.GraphicsDevice, texture.Bounds.Size.X, texture.Bounds.Size.Y);
            Color[] color010 = new Color[texture.Bounds.Size.X * texture.Bounds.Size.Y];

            for (int pixel = 0; pixel < texture.Bounds.Size.X * texture.Bounds.Size.Y; pixel++)
            {
                int y = (int)(pixel / texture.Bounds.Size.Y);
                int x = pixel - y * texture.Bounds.Size.Y;

                if (Utilites.PointInTriangle(new Vector2(x, y), new Vector2(-32, 0), new Vector2(32.5f, 32.5f), new Vector2(96, 0)))                

                    if (texture.GetPixel(pixel) != new Color(0, 0, 0, 0))
                    {
                        color011[pixel] = texture.GetPixel(pixel);
                        color001[pixel] = texture.GetPixel(pixel);
                        color010[pixel] = texture.GetPixel(pixel);
                    }
                if (!Utilites.PointInTriangle(new Vector2(x, y), new Vector2(-32, 0), new Vector2(31, 31), new Vector2(96, 0)))
                {
                    if (texture.GetPixel(pixel) != new Color(0, 0, 0, 0))
                    {
                        if (x < 32)
                        {
                            color101[pixel] = texture.GetPixel(pixel);
                            color001[pixel] = texture.GetPixel(pixel);
                            color100[pixel] = texture.GetPixel(pixel);
                        }
                        else
                        {
                            color110[pixel] = texture.GetPixel(pixel);
                            color100[pixel] = texture.GetPixel(pixel);
                            color010[pixel] = texture.GetPixel(pixel);
                        }
                    }
                }
                    
                
            }
            texture011.SetData(color011);
            texture011.Name = name + "_011";

            texture101.SetData(color101);
            texture101.Name = name + "_101";

            texture110.SetData(color110);
            texture110.Name = name + "_110";


            _textures.Add(name + "_011", texture011);
            _textures.Add(name + "_101", texture101);
            _textures.Add(name + "_110", texture110);

            texture100.SetData(color100);
            texture100.Name = name + "_100";

            texture010.SetData(color010);
            texture010.Name = name + "_010";

            texture001.SetData(color001);
            texture001.Name = name + "_001";


            _textures.Add(name + "_100", texture100);
            _textures.Add(name + "_010", texture010);
            _textures.Add(name + "_001", texture001);
        }

        public static Color GetPixel(this Texture2D texture, int pixel)
        {
            Color[] cls = new Color[64 * 64];
            //Rectangle extractRegion = new Rectangle(0, 0, 63, 63);
            texture.GetData(cls);
            return cls[pixel];
        }

        public static Dictionary<int, byte> TileIdDictionary = new Dictionary<int, byte>();
        /*
        public static void LoadGroundTileSet(string name)
        {
            LoadTexture(name);

            int id = 0;

            for (int y = 0; y < Tile.GroundTileSetSize.Y; y++)
                for (int x = 0; x < Tile.GroundTileSetSize.X; x++)
                {
                    Texture2D newtexture = new Texture2D(RenderSystem.window.GraphicsDevice, Tile.tileSize, Tile.tileSize);
                    Color[] colors = new Color[Tile.tileSize * Tile.tileSize];
                    Rectangle extractRegion = new Rectangle(x * Tile.tileSize, y * Tile.tileSize, Tile.tileSize, Tile.tileSize);
                    _textures[name].GetData<Color>(0, extractRegion, colors, 0, Tile.tileSize * Tile.tileSize);
                    newtexture.SetData(colors);

                    if (id > 3) // Совмещаем текстуру
                    {
                        Color[] newcolors = new Color[Tile.tileSize * Tile.tileSize];
                        extractRegion = new Rectangle(3 * Tile.tileSize, 0, Tile.tileSize, Tile.tileSize);
                        _textures[name].GetData<Color>(0, extractRegion, newcolors, 0, Tile.tileSize * Tile.tileSize);
                        for (int i = 0; i < newcolors.Length; i++)
                        {
                            if (colors[i] != new Color(0, 0, 0, 0))
                                newcolors[i] = colors[i];
                        }
                        newtexture.SetData(newcolors);
                    }
                    _textures.Add(name + "_" + id.ToString(), newtexture);

                    newtexture = null;
                    id++;
                    if (id == Tile.GroundTileSetSize.X * Tile.GroundTileSetSize.Y - 1)
                        return;
                }
        }
        */
        /*
        private static void LoadTilesId()
        {
            int count = 0;
            int y = 0;

            int line = 4;

            for (int i = 0; i < 300; i++)
            {
                string bytestr = Convert.ToString(i, 2);

                while (bytestr.Length < 8)
                    bytestr = "0" + bytestr;

                bytestr = Tile.CheckTileByteString(bytestr.Substring(0, 4), bytestr.Substring(4, 4));
                if (i == (bytestr).ToInt(2))
                {
                    TileIdDictionary.Add(i, (byte)(count + y * line));
                    count++;
                    if (count == line)
                    {
                        count = 0;
                        y++;
                    }
                }
            }
        }
        */
        public static bool LoadFont(string name)
        {
            bool a = true;
            if (!_fonts.ContainsKey(name))
            {
                _fonts.Add(name, RenderSystem.window.Content.Load<SpriteFont>($"{FONT_DIR}/{name}"));
            }
            else
                a = false;
            return a;
        }

        public static bool LoadTexture(string name)
        {
            bool a = true;
            if (!_textures.ContainsKey(name))
            {
                _textures.Add(name, RenderSystem.window.Content.Load<Texture2D>($"{TEXTURE_DIR}/{name}"));
            }
            else
                a = false;
            return a;
        }
    }
}
