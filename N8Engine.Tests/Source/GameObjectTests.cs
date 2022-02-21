﻿using System;
using N8Engine.SceneManagement;
using NUnit.Framework;

namespace N8Engine.Tests;

sealed class GameObjectTests
{
    sealed class B : Component { }
    sealed class C : Component { }
    sealed class S : Scene { public override void Load() { } }
    
    static GameObject GameObject() => new(new S(), NAME);
    static C Component() => new();

    const string NAME = "nate";
    
    [Test]
    public void TestDestroyGameObject()
    {
        var go = GameObject(); 
        Assert.IsFalse(go.IsDestroyed);
        go.Destroy();
        Assert.IsTrue(go.IsDestroyed);
    }
    
    [Test]
    public void TestGetComponent()
    {
        var go = GameObject();
        Assert.IsNull(go.GetComponent<C>());
        go.AddComponent(Component());
        Assert.IsNotNull(go.GetComponent<C>());
        Assert.IsNull(go.GetComponent<B>());
    }

    [Test]
    public void TestAddComponent()
    {
        var go = GameObject();
        go.AddComponent(Component());
        Assert.IsNotNull(go.GetComponent<C>());
    }

    [Test]
    public void TestRemoveComponent()
    {
        var go = GameObject();
        var c = Component();
        go.AddComponent(c);
        go.RemoveComponent(c);
        Assert.IsNull(go.GetComponent<C>());
        go.AddComponent(Component());
        go.RemoveComponent(go.GetComponent<C>());
        Assert.IsNull(go.GetComponent<C>());
        Assert.Catch(() => go.RemoveComponent(new B()));
    }

    [Test]
    public void TestNameGameObject()
    {
        var go = GameObject();
        Assert.IsTrue(go.Name == NAME);
    }
    
    [Test]
    public void TestRenameGameObject()
    {
        var go = GameObject();
        const string name = "fdjkl;asfdsa";
        go.Name = name;
        Assert.IsTrue(go.Name == name);
    }
}