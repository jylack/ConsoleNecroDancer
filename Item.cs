namespace NecroDancer
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Potion,
        Gold,
        Key,
        Food,
        Accessory
    }

    public abstract class Item
    {
        protected ItemType _state;
        protected Point _point;//드랍된경우 쓰임

        protected int _atk;
        protected int _life;
        protected int _def;
        protected int _lange;

        protected string _name;
        protected string _image;

        public Item()
        {
            _point = new Point(0, 0);
            _atk = 0;
            _life = 0;
            _def = 0;
            _lange = 0;
        }

        public Item(ItemType state, Point point)
        {
            _state = state;
            _point = point;
        }

        public ItemType State { get { return _state; } }

        public void Use(Unit target) { }
        public void Drop(Point point)
        {
            _point = point;
        }
        public Item Get() { return this; }
        public void Spawn(Point point)
        {
            _point = point;
        }

    }

    public class Weapon : Item
    {
        public Weapon()
        {
            _state = ItemType.Weapon;
        }
        public void Attack() { }
    }

    public class Armor : Item
    {
        public Armor()
        {
            _state = ItemType.Armor;
        }


    }

    public class Potion : Item
    {
        public Potion()
        {
            _state = ItemType.Potion;
        }

        public void Heal() { }
    }

    public class Gold : Item
    {
        int _gold;

        public Gold(Point point, int gold)
        {
            _point = point;
            _state = ItemType.Gold;
            _gold = gold;
        }
        public void GetGold() { }
    }

    public class Key : Item
    {
        public Key()
        {
            _state = ItemType.Key;
        }
        public void Open() { }
    }

    public class Food : Item
    {
        public Food()
        {
            _state = ItemType.Food;
        }
        public void Eat() { }
    }

    public class accessory : Item
    {
        public accessory()
        {
            _state = ItemType.Accessory;
        }
        public void Equip() { }
    }

}
