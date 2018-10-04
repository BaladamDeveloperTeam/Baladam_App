﻿using System.Collections;
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

[System.Serializable]
public class UserInfo
{
    public string _id;
    public string username;
    public string loged_on;
    public string phone;
    public string melli;
    public string email;
    public string lvl;
    public double rate;
    public int f_prj;
    public int c_prj;
    public int is_seller;
    public int app_run;
    public string name;
    public LoginAT login_AT;
    public string shaba;
    public int is_send_melli_shenasname;
    public int raters;
    public string bio;
    public string gender;
    public string age;
    public string madrak_tahsili;
    public string major_field;
    public string city;
    public string town;
    public string address;
    public string image_pro;
    public string banner_pro;
    public int is_vip;
    public int wallet;
    public string updated_at;
    public string created_at;
}

[System.Serializable]
public class LoginAT
{
    public string date;
    public int timezone_type;
    public string timezone;
}

[System.Serializable]
public class UserInfo1
{
    public string _id;
    public string username;
    public string phone;
    public string melli;
    public string email;
    public string lvl;
    public string rate;
    public string is_seller;
    public string app_run;
    public string f_prj;
    public string c_prj;
    public string name;
    public string login_AT;
    public string shaba;
    public string is_send_melli_shenasname;
    public string raters;
    public string bio;
    public string gender;
    public string age;
    public string madrak_tahsili;
    public string major_field;
    public string city;
    public string town;
    public string address;
    public string image_pro;
    public string banner_pro;
    public string is_vip;
    public string wallet;
    public string updated_at;
    public string created_at;
    public string loged_on;

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