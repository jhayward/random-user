using System;

namespace RandomUser.Core.Domain
{
    public class Name
    {
        public Name() : this(string.Empty, string.Empty,string.Empty)
        {}

        public Name(string title, string first, string last)
        {
            Title = title;
            First = first;
            Last = last;
        }

        public string Title { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }
}