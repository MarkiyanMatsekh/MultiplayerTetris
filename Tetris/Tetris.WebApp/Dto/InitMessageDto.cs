using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.WebApp.Dto
{
    public class InitMessageDto : CommunicationMessageBase
    {
        public SizeDto size { get; set; }
        public InitMessageDto() : base("init") { }
        public InitMessageDto(SizeDto size) : this()
        {
            this.size = size;
        }
    }
}
