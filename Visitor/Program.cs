using System;
using System.Collections;

namespace Visitor
{
    public interface Feeder
    {
        public void FeedFruitTree(FruitTree fr);
        public void FeedCitrusTree(CitrusTree ct);
        public void FeedFlower(Flower fl);
    }

    public class ConcreteFeeder : Feeder
    {
        private int Zinc { get; set; } = 10;
        private int Hidrate { get; set; } = 10;
        private int Ferrum { get; set; } = 10;

        private void Calculate(string plant, int k)
        {
            switch (plant)
            {
                case "C":
                    {
                        Zinc = 20 * k;
                        Hidrate = 15 * k;
                        Ferrum = 0 * k;
                        break;
                    }
                case "Fr":
                    {
                        Zinc = 40 * k;
                        Hidrate = 25 * k;
                        Ferrum = 10 * k;
                        break;
                    }
                case "Fl":
                    {
                        Zinc = 0 * k;
                        Hidrate = 15 * k;
                        Ferrum = 100 * k;
                        break;
                    }
                default:
                    break;
            }
        }

        public void FeedFruitTree(FruitTree fr)
        {
            int k;
            if (fr.Sort == "Gala Rpyal") k = 1; else k = 2;
            Calculate("Fr", k);
            fr.mix = new int[] { Zinc, Hidrate, Ferrum };
        }

        public void FeedCitrusTree(CitrusTree ct)
        {
            int k;
            if (ct.Length <= 10) k = 1; else k = 2;
            Calculate("C", k);
            ct.mix = new int[] { Zinc, Hidrate, Ferrum };
        }

        public void FeedFlower(Flower fl)
        {
            int k;
            if (fl.Sort == "Rose") k = 2; else k = 1;
            Calculate("Fl", k);
            fl.mix = new int[] { Zinc, Hidrate, Ferrum };
        }
    }

    public class PlantCollection
    {
        public ArrayList plants = new ArrayList();

        public void Add(Plant p)
        {
            plants.Add(p);
        }

        public void Remove(Plant p)
        {
            plants.Remove(p);
        }

        public void Accept(Feeder feeder)
        {
            foreach (Plant pl in plants)
            {
                pl.Accept(feeder);
                pl.PrintInfo();
            }
        }
    }

    public abstract class Plant
    {
        public abstract void Accept(Feeder feeder);
        public abstract void PrintInfo();
    }

    public class CitrusTree : Plant
    {
        public int Length { get; set; } = 10;
        public int[] mix = new int[3];

        public CitrusTree(int l)
        {
            Length = l;
        }

        public override void Accept(Feeder feeder)
        {
            feeder.FeedCitrusTree(this);
        }

        public override void PrintInfo()
        {
            Console.WriteLine(this.Length);
            Console.WriteLine("Zinc ={0}, Hydrate ={1}, Ferrum = {2}", mix[0], mix[1], mix[2]);
        }
    }

    public class FruitTree : Plant
    {
        public string Sort { get; set; } = "Pink Lady";
        public int[] mix = new int[3];

        public FruitTree(string s)
        {
            Sort = s;
        }

        public override void Accept(Feeder feeder)
        {
            feeder.FeedFruitTree(this);
        }

        public override void PrintInfo()
        {
            Console.WriteLine(this.Sort);
            Console.WriteLine("Zinc ={0}, Hydrate ={1}, Ferrum = {2}", mix[0], mix[1], mix[2]);
        }
    }

    public class Flower : Plant
    {
        public string Sort { get; set; } = "Daisy";
        public string Colour { get; set; } = "Green";
        public int[] mix = new int[3];

        public Flower(string s, string c)
        {
            Sort = s;
            Colour = c;
        }

        public override void Accept(Feeder feeder)
        {
            feeder.FeedFlower(this);
        }

        public override void PrintInfo()
        {
            Console.WriteLine(this.Sort);
            Console.WriteLine(this.Colour);
            Console.WriteLine("Zinc ={0}, Hydrate ={1}, Ferrum = {2}", mix[0], mix[1], mix[2]);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PlantCollection plants = new PlantCollection();
            plants.Add(new CitrusTree(20));
            plants.Add(new FruitTree("Gala Royal"));
            plants.Add(new Flower("Rose", "Red"));
            plants.Accept(new ConcreteFeeder());
        }
    }
}
