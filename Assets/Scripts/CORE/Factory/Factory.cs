using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Arkanoid
{
    public class Factory
    {
        private readonly IInstantiator _instantiator;

        [Inject]
        public Factory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public GameObject CreateInstance(GameObject prefab)
        {
            return _instantiator.InstantiatePrefab(prefab);
        }
        
        public T CreateInstance<T>(IEnumerable<object> param)
        {
            return _instantiator.Instantiate<T>(param);
        }
    }
}