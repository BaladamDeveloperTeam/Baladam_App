using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CategoryInfo
{
    public string _id;
    public string name;
    public string nameEN;
    public string tags;

}

[System.Serializable]
public class CategoryNames
{
    public string[] name;
}

[System.Serializable]
public class SubCategoryNames
{
    public string[] name;
    public string[] nameEN;
}

[System.Serializable]
public class SubCategoryInfo
{
    public string _id;
    public string subID;
    public string SubName;
    public string SubNameEN;
    public string databaseID;
}


public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}