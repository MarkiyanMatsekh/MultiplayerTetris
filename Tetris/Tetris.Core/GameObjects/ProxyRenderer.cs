using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Core.GameContracts;

namespace Tetris.Core.GameObjects
{
    public class ProxyRenderer : IRenderer
    {
        private readonly object _lock = new object();
        private readonly List<IRenderer> _renderers;

        public ProxyRenderer()
            : this(new List<IRenderer>())
        {
        }

        public ProxyRenderer(List<IRenderer> renderers)
        {
            _renderers = renderers;
        }

        public void AddRenderer(IRenderer renderer)
        {
            lock (_lock)
            {
                _renderers.Add(renderer);
            }
        }

        public void RemoveRenderer(IRenderer renderer)
        {
            lock (_lock)
            {
                _renderers.Add(renderer);
            }
        }

        public void Render(ISprite sprite)
        {
            ExecuteThreadSafe(copiedRenderers =>
            {
                foreach (var renderer in copiedRenderers)
                    renderer.Render(sprite);
            });
        }

        public void RenderDiff(ISprite oldSprite, ISprite newSprite)
        {
            ExecuteThreadSafe(copiedRenderers =>
            {
                foreach (var renderer in copiedRenderers)
                    renderer.RenderDiff(oldSprite, newSprite);
            });
        }

        private void ExecuteThreadSafe(Action<List<IRenderer>> action )
        {
            List<IRenderer> copiedRenderers;
            lock (_lock)
            {
                copiedRenderers = new List<IRenderer>(_renderers);
            }
            action(copiedRenderers);
        }
    }
}
