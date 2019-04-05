using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [System.Serializable]
    public class CategoryInfo
    {
        public string id;
        public string name;
        public string nameEN;
        public SubCategory[] subCategories;
        public string tags;
    }

    [System.Serializable]
    public class SubCategory
    {
        public string id;
        public string name;
        public string nameEN;
    }

}
