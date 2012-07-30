using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Core.GameContracts;
using Tetris.Core.GameLogic;

namespace Tetris.WebApp
{
    public class WebInputListener : IUserInputListener 
    {
        public void BindInputSerializer(IInputQueue queue)
        {
            
        }

        public bool IsListening
        {
            get { return true; }
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}
