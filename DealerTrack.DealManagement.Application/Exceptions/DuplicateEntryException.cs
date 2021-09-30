using System;
using System.Collections.Generic;
using System.Text;

namespace DealerTrack.DealManagement.Application.Exceptions
{
    public class DuplicateEntryException: ApplicationException
    {
        public DuplicateEntryException(string name)
            : base($"Duplicate entry found. Record insertion failed: {name} ")
        {

        }
    }
}
