using System.Collections.Generic;

namespace RandomUser.Core.Infrastructure
{
    internal interface ISeedDataStore
    {
        IEnumerable<string> Titles();
        IEnumerable<string> FirstNames();
        IEnumerable<string> LastNames();
    }
}