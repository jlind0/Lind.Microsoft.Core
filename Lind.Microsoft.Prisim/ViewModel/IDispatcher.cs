using System;
using System.Collections.Generic;
using System.Text;

namespace Lind.Microsoft.Prism.ViewModel
{
    public interface IDispatcher
    {
        void Invoke(Action act);
    }
}
