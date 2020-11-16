using UnityEngine;

public class AssetManager
{
    public AssetManager()
    {

    }

    public T LoadAsset<T>(string path)
        where T : Object
    {
        return Resources.Load<T>(path);
    }
}
