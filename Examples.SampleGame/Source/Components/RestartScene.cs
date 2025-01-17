﻿using System;
using N8Engine;
using N8Engine.SceneManagement;

namespace SampleGame;

sealed class RestartScene : Component
{
    readonly Func<bool> _whenToRestart;
    readonly SceneManager _sceneManager;

    public RestartScene(Func<bool> whenToRestart)
    {
        _whenToRestart = whenToRestart;
        _sceneManager = Modules.Get<SceneManager>();
    }

    public override void LateUpdate(Frame frame)
    {
        if (_whenToRestart())
            _sceneManager.Load(_sceneManager.CurrentScene);
    }
}