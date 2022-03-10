using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace N8Engine.Tests;

sealed class GameModulesTests
{
    class M : GameModule { }

    GameModules _modules = null!;

    [SetUp]
    public void Setup() => _modules = new();
    
    [Test]
    public void TestAddModule()
    {
        _modules.Add(new M());
        IsTrue(_modules.Count == 1);
    }
    
    [Test]
    public void TestRemoveNonExistentModule() => Catch<GameModuleNotFoundException>(() => _modules.Remove<M>());

    [Test]
    public void TestRemoveExistentModule()
    {
        _modules.Add(new M());
        _modules.Remove<M>();
        IsTrue(_modules.Count == 0);
    }

    [Test]
    public void TestGetNonExistentModule() => Catch<GameModuleNotFoundException>(() => _modules.Get<M>());

    [Test]
    public void TestGetExistentModule()
    {
        _modules.Add(new M());
        IsNotNull(_modules.Get<M>());
    }
}