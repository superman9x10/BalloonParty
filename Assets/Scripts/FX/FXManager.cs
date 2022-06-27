using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    Dictionary<string, GameObject> gameFxMap = new Dictionary<string, GameObject>();
    [SerializeField] List<GameFx> gameFxList;

    private void OnEnable()
    {
        CharacterBase.hitBallon += instanceFx;
    }

    private void OnDisable()
    {
        CharacterBase.hitBallon -= instanceFx;
    }

    void Start()
    {
        foreach (GameFx fx in gameFxList)
        {
            if (!gameFxMap.ContainsKey(fx.name))
            {
                gameFxMap.Add(fx.name, fx.gameFx);
            }
        }
    }

    void instanceFx(string fxName, Vector3 pos)
    {
        GameObject confetti = Instantiate(gameFxMap[fxName], pos, Quaternion.identity);
        Destroy(confetti, 1f);
        
    }

}

[System.Serializable]
public class GameFx
{
    public string name;
    public GameObject gameFx;
}
