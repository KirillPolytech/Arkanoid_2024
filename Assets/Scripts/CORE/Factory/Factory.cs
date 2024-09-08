using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Arkanoid
{
    public class Factory
    {
        private const string ParentName = "Parent";

        private readonly IInstantiator _instantiator;
        private readonly GameObject _parent;

        [Inject]
        public Factory(IInstantiator instantiator)
        {
            _instantiator = instantiator;

            _parent = new GameObject(ParentName);
        }

        public GameObject CreateInstance(GameObject prefab)
        {
            GameObject temp = _instantiator.InstantiatePrefab(prefab);
            temp.transform.SetParent(_parent.transform);
            return temp;
        }
        
        public T CreateInstance<T>(IEnumerable<object> param)
        {
            return _instantiator.Instantiate<T>(param);
        }
    }
}