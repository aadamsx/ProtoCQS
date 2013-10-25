using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDecorator
{

    //The main motivation before the decorator pattern is to be able to add data and behavior to objects dynamically 
    //without relying on inheritance.

    //A decorator usually conforms to the interface of the component its decorating. 
    //Lets see this with an example.

    //Am building a system that calculates the price of a pizza. I can have many toppings and the total price of 
    //the pizza is calculated based on what toppings I pick. One way to do this would be to use inheritance and 
    //class hierarchies like Pizza, CheesePizza, ChickenPizza etc…which can get very inflexible.

    //A better way is to implement this using decorators. Below PizzaWithCheese decorates 
    //the Pizza and adds the price of cheese to the total and so does the PizzaWithChicken. 
    //When executed you should see the following output:

    //Total for cheese pizza: 15 
    //Total for chicken pizza: 16 
    //Total for cheese + chicken pizza: 21

    class Program
    {
        static void Main(string[] args)
        {
            var pizzaWithCheese = new PizzaWithCheese(new Pizza(), 5);
            var pizzaWithChicken = new PizzaWithChicken(new Pizza(), 6);
 
            //pizza with chicken and cheese is easy to build now.
            //we can keep decotaring the base pizza with as many decorators as necessary at runtime
            var pizzaWithChickenAndCheese = new PizzaWithChicken(pizzaWithCheese, 6);
 
            Console.WriteLine("Total for cheese pizza: " + pizzaWithCheese.GetPrice());
            Console.WriteLine("Total for chicken pizza: " + pizzaWithChicken.GetPrice());
            Console.WriteLine("Total for cheese + chicken pizza: " + pizzaWithChickenAndCheese.GetPrice());
        }
    }

    public interface IPizza
    {
        int GetPrice();
    }

    public class Pizza : IPizza
    {
        public int GetPrice()
        {
            return 10;
        }
    }
 
    //Decorator 1
    public class PizzaWithCheese : IPizza
    {
        private IPizza _pizza;
        private int _priceofCheese;
 
        public PizzaWithCheese(IPizza pizza, int priceofCheese)
        {
            _pizza = pizza;
            _priceofCheese = priceofCheese;
        }
 
        public int GetPrice()
        {
            //get price of the base pizza and add price of cheese to it
            return _pizza.GetPrice() + _priceofCheese;
        }
    }
 
    //Decorator 2
    public class PizzaWithChicken : IPizza
    {
        private IPizza _pizza;
        private int _priceofChicken;
 
        public PizzaWithChicken(IPizza pizza, int priceofChicken)
        {
            _pizza = pizza;
            _priceofChicken = priceofChicken;
        }
 
        public int GetPrice()
        {
            //get price of the base pizza and add price of chicken to it
            return _pizza.GetPrice() + _priceofChicken;
        }
    }
}
