using GameEngine.Components.Main;

namespace GameEngine.Components
{
    public class OreDeposit : Component
    {
        private int _count;
        private int _id;
        public int Count { get => _count; set => _count = value; }
        public int Id { get => _id; set => _id = value; }

        public static string IdToName(int id)
        {
            switch (id)
            {
                case 0:
                    return "Copper";
                case 1:
                    return "Coal";
            }
            return "Please check OreDeposit.IdToName() function.";
        }

        public OreDeposit(int count = 800, int id = 0)
        {
            _count = count;
            _id = id;
        }

        public void Extract(int val)
        {
            _count -= val;
        }
    }
}
