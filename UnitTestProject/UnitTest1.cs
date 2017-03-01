using System.Collections.Generic;
using MCC.Email;
using MCC.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        class TestObj
        {
            public List<User> CSharpProgrammers { get; set; }
            public List<User> JavaProgrammers { get; set; }

        }
        class User
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }


        [TestMethod]
        public void TestMethod1()
        {
            var test = @"
Comments: [Comments]
<br>
<br>
Date Submitted: [FeedbackDate]
<br>
{<br>
<br>
<b>Date of Comment:</b> [Comments.DateCreated] <b>Username:</b> [Comments.UserName]
[FeedbackComments.Comment]
<b>Feedback Status:  </b>[FeedbackStatus]
}
";
            //test = "asdfasd{sdfasdf}sdfasdf";
            var pattern = @"\{(.|\s)*\}";
            var x =System.Text.RegularExpressions.Regex.Matches(test, pattern);


            Assert.IsTrue(x.Count>0);
        }
    }
}
