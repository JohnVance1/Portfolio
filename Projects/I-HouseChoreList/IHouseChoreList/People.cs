using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IHouseChoreList
{
    class People
    {
        // Contains the fields for the name of the member, 
        // the chores that they have done so far, the string 
        // for the string for printing out the chores that the
        // member has done, and the bool to check if the 
        // member has already been picked

        #region Fields
        private string name;
        private List<int> chores;
        private string strChore;
        private string weeksChore;
        private bool picked;
        private StreamReader read = null;
        #endregion


        // Contains properties for the name of the person, 
        // the chores that they have done, and if they have been picked
        // for a chore or not during the cureent week

        #region Properties
        public string Name
        {
            get { return name; }

        }

        public List<int> Chores
        {
            get { return chores; }
            

        }

        public bool Picked
        {
            get { return picked; }
            set { picked = value; }

        }

        public string WeeksChore
        {
            get { return weeksChore; }
            set { weeksChore = value; }

        }
        #endregion


        // Contains the constuctor for creating a member

        #region Constructor
        public People(string name)
        {
            this.name = name;
            chores = new List<int>();
            strChore = "";
            picked = false;
            
        }
        #endregion


        // Contains the methods for returning the chores that 
        // a member has completed as well as the method for
        // assigning chores to a specific member

        #region Methods
        /// <summary>
        /// Displays the chores that a member has done
        /// </summary>
        /// <param name="person">The person that has chores done</param>
        /// <returns></returns>
        public void ChoresDone(People person)
        {
            try
            {

                read = new StreamReader("ChoresDone.txt");

                //for(int i = 0; i <= person.Chores.Count - 1; i++)
                //{
                //    person.Chores.RemoveAt(i);

                //}

                string line = null;
                //string info = null;
                while ((line = read.ReadLine()) != null)
                {
                    if ((line == person.Name))
                    {
                        string[] data = read.ReadLine().Split(',');

                        for (int i = 0; i < data.Length - 1; i++)
                        {                                                          
                            person.Chores.Add(int.Parse(data[i]));                            

                        }

                    }
                    
                }
                
            }

            catch(Exception e)
            {
                Console.WriteLine("ChoresDone : Error reading from file : " + e.Message);

            }

            finally
            {
                if (read != null)
                {
                    read.Close();

                }
                

            }
        }

        /// <summary>
        /// Assigns a chore to a specific person 
        /// Will return null if the member has already done the chore
        /// </summary>
        /// <param name="member">The member that is having a chore assigned</param>
        /// <param name="rand">The number of the chore that they are getting</param>
        /// <returns></returns>
        

      /*  public void GetChore(People member)
        {
            read = new StreamReader("ChoresDone.txt");
            string line = null;
            int[] adding;
            //member.ChoresDone(member);
            //string info = null;
            while ((line = read.ReadLine()) != null)
            {
                if (line == member.Name)
                {
                    string[] data = read.ReadLine().Split(',');

                    for (int i = 0; i < data.Length - 1; i++)
                    {
                        member.Chores.Add(int.Parse(data[i]));

                    }


                }



            }

        } */

        #region More Comments
        //public void Save(List<People> people)
        //{
        //    try
        //    {
        //        write = new StreamWriter("ChoresDone.txt");
        //        foreach (People element in people)
        //        {
        //            write.WriteLine(element.Name);

        //            for (int i = 0; i < element.Chores.Count; i++)
        //            {
        //                element.WeeksChore += element.Chores[i] + ",";

        //            }

        //            write.WriteLine(element.WeeksChore);
        //        }

        //    }

        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Error writing to file: " + e.Message);

        //    }

        //    finally
        //    {
        //        if (write != null)
        //        {
        //            write.Close();

        //        }

        //    }


        //}
        #endregion

        #region ToString Method
        public override string ToString()
        {
            for(int i = 0; i < chores.Count; i++)
            {
                strChore += chores[i] + ", ";

            }

            return name + " \n" + strChore;

        }
        #endregion

        #endregion




    }
}
