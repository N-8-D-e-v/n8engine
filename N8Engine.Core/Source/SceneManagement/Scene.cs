﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace N8Engine.SceneManagement;

public abstract class Scene : IEnumerable<GameObject>
{
    bool _isLoaded;
    
    readonly List<GameObject> _gameObjects = new();

    public abstract void Load();

    internal void SwitchTo() => _isLoaded = true;

    internal void Unload()
    {
        _isLoaded = false;
        foreach (var gameObject in this.ToArray()) 
            gameObject.Destroy();
        _gameObjects.Clear();
    }

    internal void EarlyUpdate(Frame frame)
    {
        foreach (var gameObject in this.ToArray()) 
            gameObject.EarlyUpdate(frame);
    }
    
    internal void Update(Frame frame)
    {
        foreach (var gameObject in this.ToArray()) 
            gameObject.Update(frame);
    }
    
    internal void LateUpdate(Frame frame)
    {
        foreach (var gameObject in this.ToArray()) 
            gameObject.LateUpdate(frame);
    }

    internal void Destroy(GameObject gameObject) => _gameObjects.Remove(gameObject);

    public GameObject Create(string name) => Create(name, out _);
    
    public GameObject Create(string name, out GameObject gameObject)
    {
        if (!_isLoaded)
        {
            gameObject = null;
            return null;
        }
        gameObject = new(this, name);
        _gameObjects.Add(gameObject);
        return gameObject;
    }

    public IEnumerator<GameObject> GetEnumerator() => _gameObjects.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _gameObjects.GetEnumerator();
}