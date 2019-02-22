using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IHouseChoreList
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Fields
            List<People> members = new List<People>();
            List<People> weekChores = new List<People>();
            StreamWriter write = null;
            StreamReader read = null;

            int chores = 16;

            Random rand = new Random();
            int x;
            //int y = 0;
            int k = 0;
            List<int> chosen = new List<int>();
            #endregion


            #region All Member Creation
            People _gabe = new People("Gabe");
            People _akif = new People("Akif");
            People _daniel = new People("Daniel");
            People _dante = new People("Dante");
            People _eloy = new People("Eloy");
            People _grace = new People("Grace");
            People _harleigh = new People("Harleigh");
            People _jacob = new People("Jacob");
            People _jessika = new People("Jessika");
            People _john = new People("John");
            People _kensey = new People("Kensey");
            People _leah = new People("Leah");
            People _marco = new People("Marco");
            People _mayuri = new People("Mayuri");
            People _millie = new People("Millie");
            People _monse = new People("Monse");
            People _nahum = new People("Nahum");
            People _paulina = new People("Paulina");
            People _rachelM = new People("Rachel M");
            People _rachelR = new People("Rachel R");
            People _rose = new People("Rose");
            People _sean = new People("Sean");
            People _sierra = new People("Sierra");
            People _simran = new People("Simran");
            People _stanley = new People("Stanley");
            People _tenyu = new People("Tenyu");
            People _theo = new People("Theo");
            People _theresa = new People("Theresa");
            People _xy = new People("Xy");
            People _leo = new People("Leo");
            
            #endregion

            
            #region All Members
            members.Add(_john);
            members.Add(_akif);
            members.Add(_gabe);
            members.Add(_simran);
            members.Add(_eloy);
            members.Add(_grace);
            members.Add(_harleigh);
            members.Add(_jessika);
            members.Add(_kensey);
            members.Add(_leah);
            members.Add(_marco);
            members.Add(_mayuri);
            members.Add(_daniel);
            members.Add(_dante);
            members.Add(_millie);
            members.Add(_monse);
            members.Add(_nahum);
            members.Add(_rachelR);
            members.Add(_jacob);
            members.Add(_rachelM);
            members.Add(_sean);
            members.Add(_stanley);
            members.Add(_tenyu);
            members.Add(_theo);
            members.Add(_paulina);
            members.Add(_rose);
            members.Add(_theresa);
            members.Add(_sierra);
            members.Add(_xy);
            members.Add(_leo);
            #endregion

            // include all members that have chores this week
            #region Current Week's Chores
            weekChores.Add(_theo);
            weekChores.Add(_xy);
            weekChores.Add(_marco);
            weekChores.Add(_rose);
            weekChores.Add(_nahum);
            weekChores.Add(_jacob);
            weekChores.Add(_kensey);
            weekChores.Add(_sierra);
            weekChores.Add(_sean);
            weekChores.Add(_theresa);
            weekChores.Add(_daniel);
            weekChores.Add(_rachelM);
            weekChores.Add(_stanley);
            weekChores.Add(_leo);
            weekChores.Add(_mayuri);
            weekChores.Add(_dante);
            #endregion
            

            #region Commented Code
            // Uncomment to clear all of the members chores 
            // for the new week
            //Clear(members);

            // Uncomment to show the chores that people have done before
            //foreach (People element in members)
            //{
            //    if (element.Chores != null)
            //    {
            //        Console.WriteLine(element.Chores);
            //        Console.WriteLine();
            //    }

            //}
            #endregion


            #region Method Instantiation
            Clear(weekChores);

            // Gets all of the chores already done
            GetChores();

            //Assigns a random chore to everyone
            RandomChore();

            // Saves the chores for who has done what chore
            Save(members);
            #endregion


            #region Main Methods
            // Assigns a random chore
            void RandomChore()
            {
                List<int> picked = new List<int>();
                bool y = true;
                int repeat = 0;

                while (y)
                {
                    k = 0;
                    chosen.Clear();

                    // Adds a chore to each member
                    while (k != chores)
                    {
                        x = rand.Next(0, chores);

                        if (!chosen.Contains(x))
                        {
                            AssignChore(weekChores[x], k);
                            chosen.Add(x);
                            k++;

                        }
                        
                    }

                    // Checks to see if there are any repeats in a list
                    foreach (People element in weekChores)
                    {
                        #region Counts
                        int count0 = 0;
                        int count1 = 0;
                        int count2 = 0;
                        int count3 = 0;
                        int count4 = 0;
                        int count5 = 0;
                        int count6 = 0;
                        int count7 = 0;
                        int count8 = 0;
                        int count9 = 0;
                        int count10 = 0;
                        int count11 = 0;
                        int count12 = 0;
                        int count13 = 0;
                        int count14 = 0;
                        int count15 = 0;
                        int count16 = 0;
                        #endregion

                        for (int i = 0; i < element.Chores.Count; i++)
                        {
                            if(element.Chores[i] == 0)
                            {
                                count0++;

                            }

                            else if (element.Chores[i] == 1)
                            {
                                count1++;

                            }

                            else if (element.Chores[i] == 2)
                            {
                                count2++;

                            }

                            else if (element.Chores[i] == 3)
                            {
                                count3++;

                            }

                            else if (element.Chores[i] == 4)
                            {
                                count4++;

                            }

                            else if (element.Chores[i] == 5)
                            {
                                count5++;

                            }

                            else if (element.Chores[i] == 6)
                            {
                                count6++;

                            }

                            else if (element.Chores[i] == 7)
                            {
                                count7++;

                            }

                            else if (element.Chores[i] == 8)
                            {
                                count8++;

                            }

                            else if (element.Chores[i] == 9)
                            {
                                count9++;

                            }

                            else if (element.Chores[i] == 10)
                            {
                                count10++;

                            }

                            else if (element.Chores[i] == 11)
                            {
                                count11++;

                            }

                            else if (element.Chores[i] == 12)
                            {
                                count12++;

                            }

                            else if (element.Chores[i] == 13)
                            {
                                count13++;

                            }

                            else if (element.Chores[i] == 14)
                            {
                                count14++;

                            }

                            else if (element.Chores[i] == 15)
                            {
                                count15++;

                            }

                            else if (element.Chores[i] == 16)
                            {
                                count16++;

                            }

                        }

                        if((count0 > 1) || (count1 > 1) || (count2 > 1) || (count3 > 1) || (count4 > 1) || (count5 > 1) || (count6 > 1)
                             || (count7 > 1) || (count8 > 1) || (count9 > 1) || (count10 > 1) || (count11 > 1) || (count12 > 1) || (count13 > 1)
                              || (count14 > 1) || (count15 > 1) || (count16 > 1))
                        {                            
                            repeat++;

                        }

                        else
                        {
                            y = false;

                        }

                    }

                    // If there are repeats erase added chores and repeat
                    if (repeat > 0)
                    {
                        foreach (People element in weekChores)
                        {
                            element.Chores.RemoveAt(element.Chores.Count() - 1);

                        }
                        RandomChore();
                    }



                }


            }

            // Clears the past chores that the members have had
            void Clear(List<People> person)
            {               
                for (int i = person.Count - 1; i > 0; i--)
                {
                    person[i].Picked = false;

                }

            }

            // Gets all of the chores and puts them in each members list
            void GetChores()
            {
                try
                {
                    read = new StreamReader("ChoresDone.txt");
                    string line = "";

                    while ((line = read.ReadLine()) != null)
                    {
                        foreach (People element in members)
                        {
                            if (element.Name == line)
                            {
                                string[] data = read.ReadLine().Split(',');

                                for (int i = 0; i < data.Length - 1; i++)
                                {
                                    element.Chores.Add(int.Parse(data[i]));

                                }
                            }

                        }

                    }




                }

                catch (Exception e)
                {
                    Console.WriteLine("AssignChore : Error reading to file: " + e.Message);

                }

                finally
                {
                    if (read != null)
                    {
                        read.Close();

                    }

                }
            }

            // Adds chore to a members list
            void AssignChore(People member, int random)
            {
                member.Chores.Add(random);              

            }

            // Saves/Writes all of the chores out to the txt document
            void Save(List<People> people)
            {                
                try
                {
                    write = new StreamWriter("ChoresDone.txt");
                    foreach (People element in people)
                    {
                        write.WriteLine(element.Name);

                        for (int i = 0; i < element.Chores.Count; i++)
                        {
                            element.WeeksChore += element.Chores[i] + ",";

                        }

                        write.WriteLine(element.WeeksChore);
                    }

                }

                catch (Exception e)
                {
                    Console.WriteLine("Error writing to file: " + e.Message);

                }

                finally
                {
                    if (write != null)
                    {
                        write.Close();

                    }

                }


            }
            #endregion




        }
    }
}
