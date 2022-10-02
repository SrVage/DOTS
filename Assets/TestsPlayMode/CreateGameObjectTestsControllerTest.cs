using System.Collections;
using System.Collections.Generic;
using Code;
using Code.UI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class CreateGameObjectTestsControllerTest
{
    private GameObject _blood;

    [UnityTest]
    public IEnumerator ACreateBloodFromResources()
    {
        var prefab = Resources.Load<GameObject>("blood");
        _blood = GameObject.Instantiate(prefab);
        yield return new WaitForSeconds(1);
        UnityEngine.Assertions.Assert.IsNotNull(_blood);
    }

    [UnityTest]
    public IEnumerator BNotDeleteBloodAfterTouchWithNotCharacter()
    {
        var prefab = Resources.Load<GameObject>("bottle");
        var bottle = GameObject.Instantiate(prefab, _blood.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        UnityEngine.Assertions.Assert.IsNotNull(_blood);
    }
    
    [UnityTest]
    public IEnumerator DeleteBloodAfterTouchWithCharacter()
    {
        var prefab = Resources.Load<GameObject>("TestCharacter");
        var character = GameObject.Instantiate(prefab, _blood.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        UnityEngine.Assertions.Assert.IsNull(_blood);
    }
}
