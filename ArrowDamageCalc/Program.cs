using System;

namespace ArrowDamageCalc
{
    class Program
    {
        static Random random = new Random();
        static void Main(string[] args)
        {

            SwordDamage swordDamage = new SwordDamage(RollDice(3));
            ArrowDamage arrowDamage = new ArrowDamage(RollDice(1));

            while (true)
            {
                Console.Write("0 for no magic/flaming, 1 for magic, 2 for flaming, " +
                                "3 for both, anything else to quit: ");
                char key = Console.ReadKey().KeyChar;
                if (key != '0' && key != '1' && key != '2' && key != '3') return;

                Console.Write("\nS for sword, A for arrow, anything else to quit: ");
                char weaponKey = Char.ToUpper(Console.ReadKey().KeyChar);

                switch (weaponKey)
                {
                    case 'S':
                        swordDamage.Roll = RollDice(3);
                        swordDamage.Magic = (key == '1' || key == '3');
                        swordDamage.Flaming = (key == '2' || key == '3');
                        Console.WriteLine(
                           $"\nRolled {swordDamage.Roll} for {swordDamage.Damage} HP\n");
                        break;

                    case 'A':
                        arrowDamage.Roll = RollDice(1);
                        arrowDamage.Magic = (key == '1' || key == '3');
                        arrowDamage.Flaming = (key == '2' || key == '3');
                        Console.WriteLine(
                           $"\nRolled {arrowDamage.Roll} for {arrowDamage.Damage} HP\n");
                        break;

                    default:
                        return;
                }
            }


        }


        private static int RollDice(int numOfRolls)
        {

            int result = 0;
            for(int i = 0; i <= numOfRolls; i++)
            {
                result += random.Next(1, 7);
            }
            return result;
        }
    }

    abstract class WeaponDamage
    {

        /// <summary>
        /// Constructs an WeaponDamage Object
        /// </summary>
        /// <param name="roll"> takes random integer dice roll as parameter </param>
        public WeaponDamage(int roll)
        {
            Roll = roll;
            CalculateDamage();
        }

        private int roll;
        private bool flaming;
        private bool magic;


        /// <summary>
        /// Final Damage inflicted by sword
        /// </summary>
        public int Damage { get; protected set; }

        /// <summary>
        /// random integer from a dice roll
        /// </summary>
        public int Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                CalculateDamage();
            }
        }


        /// <summary>
        /// returns whether sword is flaming or not
        /// </summary>
        public bool Flaming
        {
            get { return flaming; }
            set
            {
                flaming = value;
                CalculateDamage();
            }
        }

        /// <summary>
        /// returns whether sword is magic or not
        /// </summary>
        public bool Magic
        {
            get { return magic; }
            set
            {
                magic = value;
                CalculateDamage();
            }
        }

        protected abstract void CalculateDamage();
        
    }

    class ArrowDamage : WeaponDamage
    {
        public const decimal BASE_DAMAGE_MULTIPLIER = 0.35M;
        public  decimal FLAME_DAMAGE = 1.25M;
        private  decimal MAGIC_MULTIPLIER = 2.5M;
        

        /// <summary>
        /// Constructs an SwordDamage Object
        /// </summary>
        /// <param name="roll"> takes random integer dice roll as parameter </param>
        public ArrowDamage(int roll) : base(roll)
        {
            
        }

       

        protected override void CalculateDamage()
        {
            decimal baseDamage = Roll * BASE_DAMAGE_MULTIPLIER;
            if (Magic) baseDamage *= MAGIC_MULTIPLIER;

            if (Flaming) Damage = (int)Math.Ceiling(baseDamage + FLAME_DAMAGE);
            else Damage = (int)Math.Ceiling(baseDamage); ;
            

        }
    }

    class SwordDamage  : WeaponDamage
    {
        public const int BASE_DAMAGE = 3;
        public  int FLAME_DAMAGE = 2;
        private float MAGIC_MULTIPLIER = 1.75F;
       
        /// <summary>
        /// Constructs an SwordDamage Object
        /// </summary>
        /// <param name="roll"> takes random integer dice roll as parameter </param>
        public SwordDamage(int roll) : base(roll)
        {
            
        }

        protected override void CalculateDamage()
        {
            if (!Magic) MAGIC_MULTIPLIER = 1F;
            else MAGIC_MULTIPLIER = 1.75f;
            if (!Flaming) FLAME_DAMAGE = 0;
            else FLAME_DAMAGE = 2;
            Damage = (int)(Roll * MAGIC_MULTIPLIER) + BASE_DAMAGE + FLAME_DAMAGE;

        }


    }
}
