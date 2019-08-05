using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jointly.Services
{
    public interface ISessionService
    {
        void StartService();

        void KillService();

        Task CheckStatusAsync();
    }
}
