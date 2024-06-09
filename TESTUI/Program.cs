using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTBL;

namespace TESTUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Testbl bl = new Testbl();
            Users user=new Users();
            Console.WriteLine("Enter email");
            string email = Console.ReadLine();
            Console.WriteLine("Enter a password");
            string pass = Console.ReadLine();
            try { 
            user = bl.LogIn(email, pass);
            while (user==null)
            {
                Console.WriteLine(user);
                Console.WriteLine("Enter email");
                 email = Console.ReadLine();
                Console.WriteLine("Enter a password");
                 pass = Console.ReadLine();
                user = bl.LogIn(email, pass);
            }
            Console.WriteLine("Hello To "+user.First_Name+"  "+user.Last_Name );
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            List<Questions> listQ=bl.StartTest();
            int cnt = 0;
            int points = 0;
            foreach (Questions q in listQ)
            {
                try
                {
                 List<Answers> answer = bl.AnswerForOneQuestion(q.QuestionID);
                
                
               
                cnt++;
                Console.WriteLine(cnt + ". " + q.Question_Value);
                if (answer.Count==1)//if its a regular question
                {
                    string userResult = Console.ReadLine();
                    if (userResult == answer.First().Answer_Value)
                    {
                        Console.WriteLine("good answer!");
                      points += Convert.ToInt32(q.Question_Points); 

                    }
                }
                else
                {
                    int correctAnswer = Convert.ToInt32(q.AnswerID);
                    int correctIndex = 0;
                    for (int i =0; i < answer.Count; i++)
                    {
                        if (answer[i].AnswerID == correctAnswer)
                        {
                            correctIndex = i+1;
                        }
                        Console.WriteLine((i+1)+") "+answer[i].Answer_Value);
                    }
                    string userResult=Console.ReadLine();
                    if (userResult == (correctIndex.ToString()))
                    {
                        Console.WriteLine("good answer!");
                        points += Convert.ToInt32(q.Question_Points);
                    }
                    else
                    {
                        Console.WriteLine("bad answer!");
                    }

                }
            }catch (Exception ex)
            {
           Console.WriteLine(ex.Message);
            }

                
            }
            Console.WriteLine("you finished the test with mark: "+points+"%");
            bl.InsertToHistoryTable(user, points);

             Console.ReadLine();  
        }
        
    }
}
