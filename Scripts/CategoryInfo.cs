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

[System.Serializable]
public class UserInfo
{
    public string _id;
    public string username;
    public string phone;
    public string melli;
    public string email;
    public string lvl;
    public double rate;
    public int f_prj;
    public int c_prj;
    public int count_skill;
    public int is_seller;
    public int app_run;
    public string name;
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
    public Session session;
}

[System.Serializable]
public class LoginAT
{
    public string date;
    public int timezone_type;
    public string timezone;
}

[System.Serializable]
public class gigsInfo
{
    public string _id;
    public string pz_id;
    public Skillexpress express;
    public int rate;
    public string[] gigs;
    public int comments;
    public string status;
    public SearchGigs_Skills skills;
    public string skillCode;
    public int vip;
    public string name;
    public string updated_at;
    public string created_at;
}

[System.Serializable]
public class Skillexpress
{
    public string more_cost;
    public string more_time;
}

[System.Serializable]
public class SearchGigs_Skills
{
    public string[] images;
    public SearchGigs_Boxs[] box;
    public string decep;
    public string title;
    public string subtitle;
}

[System.Serializable]
public class SearchGigs_Boxs
{
    public string cost;
    public string time;
    public string[] options;
}

[System.Serializable]
public class UserSearchInfo
{
    public string _id;
    public string username;
    public string lvl;
    public double rate;
    public int f_prj;
    public string name;
}

[System.Serializable]
public class SearchResult
{
    public CategoryInfo[] category;
    public gigsInfo[] gig;
    public UserSearchInfo[] user;
}

[System.Serializable]
public class Skill
{
    public string SkillName;
    public string SkillCategory;
    public string SkillSubCategory;
    public string SkillDescription;
    public string[] ImageName = new string[4];
    public SkillPoint[] SkillPoints = new SkillPoint[3];
    public int IsExpress;
    public int ExpressCost;
    public int ExpressTime;
    public int IsVIP = 0;
}

[System.Serializable]
public class SkillPoint
{
    public string[] SkillPoints = new string[50];
    public int SkillCost;
    public int SkillPeriod;
}

[System.Serializable]
public class Session
{
    public string name;
    public string mode = "App";
    public SessionLog log;
}

[System.Serializable]
public class SessionLog
{
    public string address;
    public SessionDevice Device;
}

[System.Serializable]
public class SessionDevice
{
    public string DeviceModel;
    public string DeviceUsername;
    public string DeviceType;
    public string IMEI;
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