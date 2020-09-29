using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    class Program
    {

        static void Main(string[] args)
        {
            int windowWidth = 100, windowHeight = 25;
            Console.SetWindowSize(windowWidth + 10 , windowHeight + 10);
            int AmountOfPolice = 25, AmountOfThieves = 25, AmountOfCitizens = 15;
            Console.CursorVisible = false;
            List<Person> town = CreatePersons(AmountOfPolice, AmountOfThieves, AmountOfCitizens);
            List<Person> prison = new List<Person>();
            List<Person> prisonRelease = new List<Person>();

            while (true)
            {
                foreach (var person in town)
                {
                    Console.SetCursorPosition(person.position.x, person.position.y);
                    Console.Write(person.name);
                    
                }
                Collision(town, prison);
                Console.SetCursorPosition(0, 26);
                foreach (var person in prison)
                {
                    if (person.GetPrisonTime() < 15)
                    {
                        person.IncreasePrisonTime();
                        Console.WriteLine($"Imprisoned time: {person.GetPrisonTime()}");
                    }
                    else
                    {
                        town.Add(person);
                        prisonRelease.Add(person);
                        Console.SetCursorPosition(0, 27);
                        Console.WriteLine("Thief released from prison.");
                        Thread.Sleep(500);
                    }
                }
                foreach (var person in prisonRelease)
                {
                    prison.Remove(person);
                }
                Thread.Sleep(200);
                foreach (var person in town)
                {
                    Movement(person);
                }
                Console.Clear();
            }

        }

        private static void Collision(List<Person> town, List<Person> prison)
        {
            int thievesCaught = 0, citizensRobbed = 0;

            int citizenItems = 0, thiefItems = 0, policeItems = 0;

            bool ifCollision = false;

            List<Person> tempPrison = new List<Person>();

            foreach (var person in town)
            {
                foreach (var otherPerson in town)
                {
                    if (!person.Equals(otherPerson)) // Personen får inte vara sig själv.
                    {
                        if (person.position.x  == otherPerson.position.x && person.position.y == otherPerson.position.y)
                        {
                            if (person is Thief && otherPerson is Citizen)
                            {
                                if (otherPerson.GetItemsSize() > 0)
                                {
                                    Item stolenItem = otherPerson.GetRngItem();
                                    person.AddItem(stolenItem);
                                    otherPerson.RemoveItem(stolenItem);
                                    citizensRobbed++;
                                    citizenItems--;
                                    thiefItems++;
                                    ifCollision = true;
                                    Console.SetCursorPosition(person.position.x, person.position.y);
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("X");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                }
                            }
                            else if (person is Police && otherPerson is Thief)
                            {
                                if (otherPerson.GetItemsSize() > 0)
                                {
                                    List<Item> stolenGoods = otherPerson.ConfiscateItem();
                                    foreach (var item in stolenGoods)
                                    {
                                        person.AddItem(item);  // Läggs till i polisens inventory.
                                    }
                                    otherPerson.RemoveAllItems(); // Rensar tjuvens inventory.
                                    tempPrison.Add(otherPerson);
                                    thievesCaught++;
                                    thiefItems--;
                                    policeItems++;
                                    ifCollision = true;
                                    Console.SetCursorPosition(person.position.x, person.position.y);
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.Write("X");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                }
                            }
                        }
                    } 
                }
            }
            foreach (var prisoner in tempPrison)
            {
                prison.Add(prisoner);
                town.Remove(prisoner);
            }
            if (ifCollision)
            {
                foreach (var person in town)
                {
                    if (person is Citizen)
                    {
                        citizenItems += person.GetItemsSize();
                    }
                    else if (person is Police)
                    {
                        policeItems += person.GetItemsSize();
                    }
                    else if (person is Thief)
                    {
                        thiefItems += person.GetItemsSize();
                    }
                }
                Console.SetCursorPosition(0, 27);
                Console.WriteLine($"Citizens robbed: {citizensRobbed}");
                Console.WriteLine($"Thieves imprisoned: {prison.Count}");
                Console.WriteLine(" ");
                Console.WriteLine($"Belongings: {citizenItems}");
                Console.WriteLine($"Stolen goods: {thiefItems}");
                Console.WriteLine($"Confiscated items: {policeItems}");
                Thread.Sleep(2000);
            }

        }

        private static List<Person> CreatePersons(int AmountOfPolice, int AmountOfThieves, int AmountOfCitizens)
        {
            List<Person> town = new List<Person>();

            var random = new Random();
            int rndXdir, rndYdir, rndXpos, rndYpos;

            for (int i = 0; i < AmountOfPolice; i++)
            {
                GetRandomDirection(random, out rndXdir, out rndYdir);
                GetRandomPosition(random, out rndXpos, out rndYpos);
                town.Add(new Police(new Position(rndXpos, rndYpos), new Direction(rndXdir, rndYdir), "P", new List<Item>()));
            }
            for (int i = 0; i < AmountOfThieves; i++)
            {
                GetRandomDirection(random, out rndXdir, out rndYdir);
                GetRandomPosition(random, out rndXpos, out rndYpos);
                town.Add(new Thief(new Position(rndXpos, rndYpos), new Direction(rndXdir, rndYdir), "T", new List<Item>(), 0));
            }
            for (int i = 0; i < AmountOfCitizens; i++)
            {
                GetRandomDirection(random, out rndXdir, out rndYdir);
                GetRandomPosition(random, out rndXpos, out rndYpos);
                town.Add(new Citizen(new Position(rndXpos, rndYpos), new Direction(rndXdir, rndYdir), "C"));
            }

            return town;
        }

        private static void GetRandomPosition(Random random, out int rndXpos, out int rndYpos)
        {
            rndXpos = random.Next(0, 101);
            rndYpos = random.Next(0, 26);
        }

        private static void GetRandomDirection(Random random, out int rndXdir, out int rndYdir)
        {
            do
            {
                rndXdir = random.Next(-1, 2);
                rndYdir = random.Next(-1, 2);
            } while (rndXdir == 0 && rndYdir == 0);
        }

        public static void Movement(Person person)
        {
            person.position.x += person.direction.x;
            person.position.y += person.direction.y;

            if (person.position.x > 100)
            {
                person.position.x = 0;
            }
            if (person.position.x < 0)
            {
                person.position.x = 100;
            }
            if (person.position.y > 25)
            {
                person.position.y = 0;
            }
            if (person.position.y < 0)
            {
                person.position.y = 25;
            }
        }

    }

}
