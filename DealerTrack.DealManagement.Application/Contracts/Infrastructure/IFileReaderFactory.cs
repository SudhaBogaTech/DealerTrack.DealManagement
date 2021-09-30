using System;
using System.Collections.Generic;
using System.Text;

namespace DealerTrack.DealManagement.Application.Contracts.Infrastructure
{
    public interface IFileReaderFactory
    {
        IFileImporter GetFileImporter(string type);
    }
}
