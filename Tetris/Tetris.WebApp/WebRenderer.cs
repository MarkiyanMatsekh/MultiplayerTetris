using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fleck;
using Newtonsoft.Json;
using Tetris.Core.GameContracts;
using Tetris.Core.GameObjects;
using Tetris.Core.Helpers;

namespace Tetris.WebApp
{
    public class WebRenderer : IRenderer
    {
        private readonly IWebSocketConnection _webSocket;

        public WebRenderer(IWebSocketConnection webSocket)
        {
            _webSocket = webSocket;
        }

        public void Render(ISprite sprite)
        {
            if (sprite == null)
                throw new ArgumentNullException("sprite");

            var dto = new SpriteDTO(sprite);
            var json = JsonConvert.SerializeObject(dto, Formatting.Indented);
            _webSocket.Send(json);

        }

        public void RenderDiff(ISprite oldSprite, ISprite newSprite)
        {
            var diffCoords = new List<PointDTO>();
            oldSprite.ForEachDifferentCellFrom(newSprite, (i, j) => diffCoords.Add(new PointDTO(i, j, newSprite[i, j])));

            var dto = new SpriteDTO(diffCoords);
            var json = JsonConvert.SerializeObject(dto, Formatting.Indented);
            _webSocket.Send(json);
        }
    }

    public class PointDTO
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }

        public PointDTO() { }
        public PointDTO(int x, int y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }
    }

    public class SpriteDTO
    {
        public List<PointDTO> Coords;

        public SpriteDTO()
        {
            Coords = new List<PointDTO>();
        }
        public SpriteDTO(ISprite sprite)
            : this()
        {
            sprite.ForEachCell((i, j) => Coords.Add(new PointDTO(i, j, sprite[i, j])));
        }

        public SpriteDTO(List<PointDTO> coords)
        {
            Coords = coords;
        }
    }
}
