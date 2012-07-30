using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fleck;
using Newtonsoft.Json;
using Tetris.Core.GameContracts;
using Tetris.Core.Helpers;
using Tetris.WebApp.Dto;

namespace Tetris.WebApp
{
    public class WebRenderer : IRenderer, IEquatable<WebRenderer>
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

            var dto = new RenderMessageDto(sprite);
            var json = JsonConvert.SerializeObject(dto, Formatting.Indented);
            _webSocket.Send(json);

        }

        public void RenderDiff(ISprite oldSprite, ISprite newSprite)
        {
            var diffCoords = new List<PointDTO>();
            oldSprite.ForEachDifferentCellFrom(newSprite, (i, j) => diffCoords.Add(new PointDTO(i, j, newSprite[i, j])));

            var dto = new RenderMessageDto(diffCoords);
            var json = dto.ToJson();
            _webSocket.Send(json);
        }



        public bool Equals(WebRenderer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._webSocket, _webSocket);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (WebRenderer)) return false;
            return Equals((WebRenderer) obj);
        }

        public override int GetHashCode()
        {
            return (_webSocket != null ? _webSocket.GetHashCode() : 0);
        }
    }
}
