namespace GameEngine
{
    public class Tile
    {
        public static int tileSize = 16;
        private int _id = 0;

        internal static uint width = 16;
        internal static uint height = 16;

        public int Id { get => _id; set => _id = value; }

        public Tile(int id = 0)
        {
            _id = id;
        }

        public Tile(Tile copy)
        {
            _id = copy._id;
        }

        public string GetTexture()
        {
            switch (this._id)
            {
                case 0:
                    return "Void";
                case 1:
                    return "Earth";
                case 2:
                    return "Grass";
                case 3:
                    return "Stone";
            }
            return null;
        }

        public bool IsSolid()
        {
            switch (this._id)
            {
                case 0:
                    return true;
                case 1:
                    return false;
                case 2:
                    return false;
                case 3:
                    return true;
            }
            return true;
        }
    }
}
