using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoonWare.Lua;

namespace MuckInternal
{
    class ObjectCache<T> where T : MoonWare.Lua.Object
    {
        public float UpdateInterval { get; private set; }
        public T[] Objects { get; private set; }
        public T Object { get; private set; }
        public bool Single = false;

        public ObjectCache(float updateInterval = 5.0f, bool single = false)
        {
            UpdateInterval = updateInterval;
            Single = single;
        }

        public IEnumerator Update()
        {
            while (true)
            {
                if (Single)
                    Object = MoonWare.Lua.GameObject.FindObjectOfType<T>();
                else
                    Objects = MoonWare.Lua.GameObject.FindObjectsOfType<T>();

                yield return new WaitForSeconds(UpdateInterval);
            }
        }
        
        public LUA_HANDICAPPED Moonware(LuaL_Function NewFunction, LuaL_String Name)
        {
           lua_register(MoonWare.Lua.VM, Name, NewFunction);
        }
        public void Init(MoonWareObject self)
        {
            self.StartCoroutine(this.Update());
        }
    }
}
