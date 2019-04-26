using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [System.Serializable]
    public class User
    {
        public string id;
        public string username;
        public string pwd;
        public string token;
        public UserProfile profile;
        public UserContact contact;
        public string pushe_id;
        public int role;
        public string created_at;
        public string updated_at;
    }

    [System.Serializable]
    public class UserContact
    {
        public string phone;
        public string mail;
        public string address;
        public UserProvince province;
    }

    [System.Serializable]
    public class UserProfile
    {
        public string firstName;
        public string lastName;
        public string melliCode;
        public string creditCard;
        public string image;
        public string banner;
    }

    [System.Serializable]
    public class UserProvince
    {
        public int country;
        public int city;
        public int town;
    }

    [System.Serializable]
    public class MySkills
    {
        public string id;
        public string accept;
        public string user_id;
        public string status;
        public string[] imgUrl;
        public string[] tags;
        public Box boxes;
        public string category;
        public string subCategory;
        public string description;
        public double rate;
        public string skillCode;
        public int vip;
        public string name;
        public int stamina;
        public string updated_at;
        public string created_at;
    }
}
