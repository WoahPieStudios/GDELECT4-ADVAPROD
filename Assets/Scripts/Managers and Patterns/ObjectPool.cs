using System.Collections.Generic;
using UnityEngine;

namespace Utilities {
    namespace ObjectPooling {

        public class ObjectPool<T> {
            private string _poolName;
            private T _item;
            private int _numberOfCopies;
            private bool _expandable;
            private bool _isActiveOnCreate;
            private bool _removePool;

            private Queue<GameObject> _copies = new Queue<GameObject>();
            private Transform _parent;

            public ObjectPool(T item, Transform parent = null, int numOfCopies = 10, bool expandable = true, bool isActiveOnCreate = false)
            {
                this._item = item;
                this._parent = parent;
                this._numberOfCopies = numOfCopies;
                this._expandable = expandable;
                this._isActiveOnCreate = isActiveOnCreate;

                _poolName = this._item.ToString();

                CreateCopies(this._item, this._numberOfCopies);
            }

            private void CreateCopies(T item, int numberOfCopies)
            {
                if (_parent == null)
                {
                    GameObject newParent = new GameObject();
                    newParent.name = _poolName;
                    _parent = newParent.transform;
                }
                for (int i = 0; i < numberOfCopies; i++)
                {
                    GameObject copy = Object.Instantiate(_item as GameObject);
                    _copies.Enqueue(copy);
                    copy.transform.SetParent(_parent);
                }
            }

            public GameObject GetObject()
            {
                if (this._expandable && _copies.Count == 0)
                {
                    CreateCopies(this._item, 10);
                }

                GameObject toUse = _copies.Dequeue();
                toUse.SetActive(true);
                return toUse;
            }

            public void ReturnToPool(GameObject objToRelease)
            {

                if (_poolName != objToRelease.name)
                {
                    Debug.LogError($"{objToRelease.name} doesn't belong to this pool or doesn't have instanced pool");
                    return;
                }
                objToRelease.SetActive(false);
                _copies.Enqueue(objToRelease);
            }
        }
    }
}
