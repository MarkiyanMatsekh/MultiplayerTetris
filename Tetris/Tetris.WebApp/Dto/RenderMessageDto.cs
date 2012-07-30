using System.Collections.Generic;
using Tetris.Core.GameContracts;
using Tetris.Core.Helpers;
using Tetris.WebApp.Dto;

namespace Tetris.WebApp
{
    public class RenderMessageDto : CommunicationMessageBase
    {
        public List<PointDTO> coords { get; set; }

        public RenderMessageDto() :base("render")
        {
            coords = new List<PointDTO>();
        }
        public RenderMessageDto(ISprite sprite) : this()
        {
            sprite.ForEachCell((i, j) => coords.Add(new PointDTO(i, j, sprite[i, j])));
        }
        public RenderMessageDto(List<PointDTO> coordinates) :base("render")
        {
            coords = coordinates;
        }
    }
}