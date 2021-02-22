using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI.Interfaces
{
    public interface IUserInterface
    {
        public void Show(List<IUserInterface> userInterfaces=null);
    }
}
