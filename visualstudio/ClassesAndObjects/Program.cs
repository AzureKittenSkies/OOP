using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesAndObjects
{
    class Colour
    {
        public float r, g, b;
    }

    class Dog
    {
        public string name;
        public int size;
        public string breed;
        public ConsoleColor colour;

        public void Eat(string food)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(name + " is eating " + food);
        }

        public void Sleep()
        {
            Console.WriteLine(name + " is sleeping");
        }
        
        public void Shit()
        {
            Console.WriteLine(name + " is shitting");
        }


    }


    class Program
    {
        static void Main(string[] args)
        {
            Colour red = new Colour();
            red.r = 1;
            red.g = 0;
            red.b = 0;
            // create instance of Dog
            Dog dog1 = new Dog();
            // Set properties of instance
            dog1.name = "Lassie";
            dog1.size = 2;
            dog1.breed = "Dalmation";
            dog1.colour = ConsoleColor.Red;

            dog1.Eat("Meat");
            dog1.Shit();

            Console.ReadLine();


        }
    }
}
