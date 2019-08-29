using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jointly.Interfaces
{
    public interface ISessionService
    {
        void StartService();

        void KillService();

        Task CheckStatusAsync();
    }
}
