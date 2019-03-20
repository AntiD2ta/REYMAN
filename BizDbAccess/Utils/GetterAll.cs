using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BizDbAccess.Utils
{
    /// <summary>
    ///  Allow to execute the GetAll method of an IEntityDbAccess implementation of a desired entity using
    ///  Reflection.
    /// </summary>
    public class GetterAll
    {
        private readonly GetterUtils _utils;
        object[] Param { get; set; }

        /// <summary>
        /// Initializes a new instance of the BizDbAccess.Utils.GetterAll class
        /// </summary>
        /// <param name="utils">Object containing the Repositories Names and the Assembly which
        /// contains the entities</param>
        /// <param name="param">Parameters of the targeted Repository constructor</param>
        public GetterAll(GetterUtils utils, params object[] param)
        {
            _utils = utils;
            Param = param;
            //targetAsm: "BizData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        }

        /// <summary>
        /// Given the name of type, search using Reflection a Repository implementation of the type, and if exist,
        /// call its GetAll method creating a instance of it with the parameters given in param array.
        /// </summary>
        /// <param name="type">The name of the type of the entity</param>
        /// <returns>An object containing the IEnumerable of type:entity returned by the GetAll of it Repository</returns>
        public object GetAll(string type)
        {
            var targetAsm = Assembly.Load(_utils.targetAssembly);
            var actualAsm = Assembly.GetExecutingAssembly();

            foreach (var entity in targetAsm.ExportedTypes)
            {
                if (entity.Name == type)
                {
                    foreach (var def in actualAsm.GetTypes())
                    {
                        if (def.Name == _utils.ReposNames[type])
                        {
                            foreach (var method in def.GetMethods())
                            {
                                if (method.Name == "GetAll")
                                {
                                    Type[] types = new Type[Param.Length];

                                    for (int i = 0; i < Param.Length; i++)
                                    {
                                        types[i] = Param[i].GetType();
                                    }

                                    var constructor = def.GetConstructor(types);
                                    var instance = constructor.Invoke(Param);
                                    return method.Invoke(instance, null);
                                }
                            }
                        }
                    }
                    throw new Exception($"Repository of {type} not found");
                }
            }
            throw new Exception($"Entity with name {type} not defined");
        }
    }

}
