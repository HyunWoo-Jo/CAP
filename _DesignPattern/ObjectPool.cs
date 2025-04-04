using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using static UnityEditor.ObjectChangeEventStream;
namespace CA.DesignPattern {
    public interface IObjectPool {
        void RepayItem(GameObject item, int index);

    }
    public static class ObjectPoolExtensions { 
        // Builder 확장 매서드
        public static ObjectPoolBuilder<T> DontDestroy<T>(this ObjectPoolBuilder<T> builder) where T : MonoBehaviour {
            GameObject.DontDestroyOnLoad(builder.pool.ownerObj);
            builder.pool.isDontDestroy = true;
            return builder;
        }
        public static ObjectPoolBuilder<T> Static<T>(this ObjectPoolBuilder<T> builder) where T : MonoBehaviour {
            builder.pool.isStatic = true;
            return builder;
        }

        public static ObjectPool<T> Build<T>(this ObjectPoolBuilder<T> builder) where T : MonoBehaviour {
            return builder.pool;
        }

    }
    public class ObjectPoolBuilder<T> where T : MonoBehaviour {
        internal ObjectPool<T> pool;
        public static ObjectPoolBuilder<T> Instance(GameObject itemObj, int capacity = 10) {
            GameObject parentObj = new() {
                isStatic = true,
                name = itemObj.name + "_objectPool"
            };
            ObjectPoolBuilder<T> builder = new ObjectPoolBuilder<T>();

            builder.pool = new ObjectPool<T>();
            builder.pool.ownerObj = parentObj;
            builder.pool.itemObj = itemObj;
            builder.pool.item_que = new Queue<T>();
            builder.pool.index_T_list = new List<T>();
            Enumerable.Range(0, capacity).ToList().ForEach(_ => builder.pool.CreateItem());         
            return builder;
        }
    }

    public class ObjectPool<T> : IObjectPool where T : MonoBehaviour
    {
        internal GameObject ownerObj;
        internal GameObject itemObj;
        internal Queue<T> item_que;
        internal List<T> index_T_list;
        public bool isStatic;
        internal bool isDontDestroy;

        private int index = 0;
        
        // Builder를 통해 생성
        internal ObjectPool() {
        }

        public void Dipose() {
           
            itemObj = null;
            while (item_que.Count > 0) {
                T item = item_que.Dequeue();
                GameObject.DestroyImmediate(item.gameObject);
            }
            GameObject.DestroyImmediate(ownerObj);
            ownerObj = null;
        }

        internal void CreateItem() {
            GameObject obj = GameObject.Instantiate(itemObj);
            T t = obj.GetComponent<T>();
            obj.AddComponent<ObjectPoolItem>().Init(this, index++);
            item_que.Enqueue(t);
            index_T_list.Add(t);
            obj.SetActive(false);
            obj.transform.SetParent(ownerObj.transform);
            obj.isStatic = this.isStatic;
        }

        public T BorrowItem() {
            
            if (item_que.Count <= 0) {
                CreateItem();   
            }
            return item_que.Dequeue();
        }

        public void RepayItem(GameObject item, int index) {
            if (!isStatic) {
                item.gameObject.transform.SetParent(ownerObj.transform);
                item.gameObject.SetActive(false);
            }
            item_que.Enqueue(index_T_list[index]);
        }

    }
}
