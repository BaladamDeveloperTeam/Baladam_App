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
    public string pro_image;
    public string banner_pro;
    public int is_vip;
    public int wallet;
    public string updated_at;
    public string created_at;
    public Session[] session;
    public MySkills[] skill;
    public Buy buy;
    public Sell sell;
}

[System.Serializable]
public class Buy
{
}


[System.Serializable]
public class Sell
{

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
    public string title;
    public string subtitle;
    public string[] url;
    public string decep;
    public string updated_at;
    public string created_at;
}

[System.Serializable]
public class MySkills
{
    public string _id;
    public string accept;
    public string pz_id;
    public string status;
    public SearchGigs_Skills skills;
    public string[] gigs;
    public string[] url;
    public int comments;
    public string title;
    public string subtitle;
    public string decep;
    public double rate;
    public string skillCode;
    public int vip;
    public string name;
    public Skillexpress express;
    public string updated_at;
    public string created_at;
    public Buy buy;
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
    public string[] ImageName = { "", "", "", "" };
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
public class ReadSession
{
    public string name;
    public string mode;
    public SessionLogWeb log;
}

[System.Serializable]
public class SessionLog
{
    public string address;
    public SessionDevice Device;
    public LoginAT at;
}

[System.Serializable]
public class SessionLogWeb
{
    public string address;
    public SessionWeb Device;
    public LoginAT at;
}

[System.Serializable]
public class SessionDevice
{
    public string DeviceModel;
    public string DeviceUsername;
    public string DeviceType;
    public string IMEI;
}

[System.Serializable]
public class SessionWeb
{
    public string Browser;
    public string Ver;
    public string Platform;
    public string Plat_Ver;
    public string Type;
}

[System.Serializable]
public class Param
{
    public string Key;
    public string Value;
}

[System.Serializable]
public class ParamList
{
    public List<Param> Params = new List<Param>();
}

[System.Serializable]
public class SkillButton
{
    public int id;
    public string _id;
    public string name;
    public string SkillCode;
    public UnityEngine.UI.Button Button;
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