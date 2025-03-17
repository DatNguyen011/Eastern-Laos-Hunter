using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using static UnityEditor.Progress;
using UnityEditorInternal.VersionControl;

public class InGameJsonData : Singleton<InGameJsonData>
{
    private JsonData playerInforJsonData;
    private List<PlayerInfor> playerInforList=new List<PlayerInfor>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LoadResourcesFromTxt()
    {
        string filePath = "StreamingAssets/item";
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        playerInforJsonData = JsonMapper.ToObject(targetFile.text);
    }

    private void ConstructDatabase()
    {
        PlayerInfor playerInfor = new PlayerInfor();
        playerInfor.hp = (int)playerInforJsonData[0]["hp"];
        playerInfor.mp = (int)playerInforJsonData[1]["mp"];
    }
}
class PlayerInfor
{
    public int hp { get; set; }
    public int mp { get; set; }
    public int gold { get; set; }
    public int scene { get; set; }
    public int positionX { get; set; }
    public int positionY { get; set; }
}
