using System;
using System.Collections.Generic;
using System.Linq;

namespace Secret_Santa
{
    class Program
    {

        static void Main()
        {
            SecretSanta();
        }

        static void SecretSanta()
        {
            GetNumberOfParticipants();
        }

        static void GetNumberOfParticipants()
        {
            //Gets the information needed to generate the List forloops of how many people will be participating.
            int numberOfPeople = 0;

            Console.WriteLine("How many people are participating?");
            try
            {
                numberOfPeople = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {

                Console.WriteLine("Please enter a number.");
            }

            GetParticipantsNames(numberOfPeople);
        }



        //User enters how many people are participating in the Secret Santa
        static void GetParticipantsNames(int number)
        {
            List<string> participants = new List<string>();

            for (int i = 0; i < number; i++)
            {
                Console.WriteLine("Type a Person's name:"); 
                participants.Add(Console.ReadLine());
            }

            SortList(participants);
        }

        static List<string> RandomizeList(List<string> list)
        {
            //Taking the incoming list and randomizing it to return it back


            List<string> tempList = new List<string>();


            var random = new Random();
            int iteration = list.Count();

            int n = random.Next(4, 5);

            for (int i = 0; i < n; i++)
            {
                while (iteration > 0)
                {

                    for (int c = 0; c < list.Count; c++)
                    {
                        int num = random.Next(0, list.Count);

                        if (num == c)
                        {
                            tempList.Add(list[c]);
                            list.RemoveAt(c);
                            iteration--;
                        }

                    }

                }
            }

            return tempList;
        }


        static void Matchmaker(List<string> a, List<string> b)
        {
            //Take the lists and run them after they've been randomized to match.
            //Need to run twice in order to generate matches on both sides.

            List<SantasList> santasLists = new List<SantasList>();

            int iterations = a.Count() + b.Count();

            int current = 0;
            for (int i = 0; i < a.Count; i++)
            {
                SantasList santasList = new SantasList();

                if(current == i)
                {
                    santasList.FirstPerson = a.ElementAt(i);
                    santasList.SecondPerson = b.ElementAt(i);
                    current++;
                }
                

                santasLists.Add(santasList);
            }

            //Randomize B
            List<string> tempB = new List<string>();
            tempB = RandomizeList(b);
            

            current = 0;
            for (int i = 0; i < tempB.Count; i++)
            {
                SantasList santasList = new SantasList();
                if(current == i)
                {
                    santasList.FirstPerson = tempB.ElementAt(i);
                    santasList.SecondPerson = a.ElementAt(i);
                    current++;
                }
                

                santasLists.Add(santasList);
            }

            WriteNamesToFile(santasLists);
        }

        static void SortList(List<string> list)
        {
            //Sorts the list into two lists for double matchmaking
            List<string> holdingList = new List<string>();
            holdingList = RandomizeList(list); //Randomize the first list

            List<string> listA = new List<string>();
            List<string> listB = new List<string>();

            //Sort List
            for (int i = 0; i < holdingList.Count; i++)
            {
                if(i%2 == 0) //Add to List A if even
                {
                    listA.Add(holdingList[i]);
                }
                else //Add to List B if odd
                {
                    listB.Add(holdingList[i]);
                }    
            }


            holdingList.Clear();

            List<string> tempA = new List<string>();
            List<string> tempB = new List<string>();

            tempA = RandomizeList(listA);

            Matchmaker(tempA, listB);
        }


        static void WriteNamesToFile(List<SantasList> list)
        {
            //Places it on your desktop.
            string driveLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string dateTime = DateTime.Now.ToString("MM-dd-yyyy");

            string fileName = driveLocation + @"\Santas List " + dateTime;

            Console.WriteLine("Generating text file: {0}", fileName);

            //OpenTextFile
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fileName))
            {
                foreach (var nameSet in list)
                {
                    string match = nameSet.FirstPerson + " is matched with " + nameSet.SecondPerson;
                    file.WriteLine(match);
                }
            }


            


        }

        //Fordebug testing only
        static void PrintDebug(List<string> a, List<string> b)
        {
            foreach (string name in a)
            {
                Console.WriteLine(name + "a");
            }

            foreach (string nameb in b)
            {
                Console.WriteLine(nameb + "b");
            }
        }
    }

    class SantasList
    {
         public string FirstPerson;
         public string SecondPerson;
    }

}
