using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTBL
{
    public class Testbl
    {
        public Users LogIn(string email,string pass)
        {
            Users user;
            try
            {
                using (TestsDBEntities DB = new TestsDBEntities())
                {
                   user=DB.Users.FirstOrDefault(x => x.Email == email && x.Pass == pass);
                    
                }
            }
            catch (Exception err)
            {

                throw err;
            }
            return user;
        }
      
        public List<Answers> AnswerForOneQuestion(int questionID)
        {
            List<Answers> answers;
            try
            {
                using (TestsDBEntities DB = new TestsDBEntities())
                {
                    answers = DB.Answers.Where(x => x.QuestionID == questionID).OrderBy(x => Guid.NewGuid()).ToList();

                }
            }
            catch (Exception err)
            {

                throw err;
            }
            return answers;
        }
        
         public List<Questions> StartTest()
        {
            
            List<Questions> questions1;
            List<Questions> questions2;
            try
            {
                using (TestsDBEntities DB = new TestsDBEntities())
                {

                    questions1 = DB.Questions.Where(x => x.Question_Points == 10).OrderBy(x=>Guid.NewGuid()).Take(8).ToList();
                    questions2 = DB.Questions.Where(x => x.Question_Points == 5).OrderBy(x => Guid.NewGuid()).Take(4).ToList();
                    questions1.AddRange(questions2);
                }

            }
            catch (Exception err)
            {

                throw err;
            }
            return questions1;
        }

       public void InsertToHistoryTable(Users user,int mark)
        {
            try
            {
                using (TestsDBEntities DB = new TestsDBEntities())
                {
                    MarksHistory history = new MarksHistory();
                    history.UserID = user.UserID;
                    history.Mark = mark;
                    history.TestDate = DateTime.Now;
                    
                    DB.MarksHistory.Add(history);
                    DB.SaveChanges();
                   
                }
            }
            catch (Exception err)
            {

                throw err;
            }
        }
    }
}
