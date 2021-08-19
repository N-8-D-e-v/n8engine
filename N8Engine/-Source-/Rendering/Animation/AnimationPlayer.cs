﻿namespace N8Engine.Rendering.Animation
{
    public sealed class AnimationPlayer : Component
    {
        private Animation _animation;

        public bool IsPlaying { get; private set; }
        public Animation Animation
        {
            get => _animation;
            set
            {
                if (value == _animation) return;
                _animation?.Reset();
                _animation = value;
                SpriteRenderer.Sprite = _animation.CachedFrames[0];
            }
        }

        internal AnimationPlayer(GameObject gameObject) : base(gameObject) { }

        public void Play() => IsPlaying = true;

        public void Stop() => IsPlaying = false;

        internal void Tick(float deltaTime)
        {
            if (!IsPlaying) return;
            Animation?.Tick(SpriteRenderer, deltaTime);
        }
    }
}