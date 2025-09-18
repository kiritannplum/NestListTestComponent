using System;
using System.Collections.Generic;
using UnityEngine;

namespace NestListTestComponent
{
    [Serializable]
    [AddComponentMenu("NestListTestComponent")]
    public class NestListTestComponent : MonoBehaviour
    {
        public List<TestList> _TestList = new();
    }

    [Serializable]
    public class TestList
    {
        public List<TestList> _TestInners = new();
    }
}