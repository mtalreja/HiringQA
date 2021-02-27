using System;
using System.Collections.Generic;

namespace Hiring.Autmation.Task.Modals
{
    public class User
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public List<Tuple<string, string>> QuestionAnswers { get; set; }
    }
}