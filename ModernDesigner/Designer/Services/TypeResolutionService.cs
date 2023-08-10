using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace ModernDesigner.Services
{
    /// <summary>
    /// Provide segment set or type retrieval service
    /// </summary>
    public class TypeResolutionService : AbstractService, ITypeResolutionService, ITypeDiscoveryService
    {
        private List<Assembly> assemblies;

        public TypeResolutionService()
        {
            this.assemblies = new List<Assembly>();
            this.assemblies.AddRange(new Assembly[] {
                typeof(Size).Assembly,
                typeof(Control).Assembly,
                typeof(DataSet).Assembly,
                typeof(XmlElement).Assembly
            });
        }

        // Provide a searched assembly or type according to the name
        #region ITypeResolutionService Interface member

        public Assembly GetAssembly(AssemblyName name, bool throwOnError)
        {
            var assembly = this.assemblies
                .Find(a => a.GetName().FullName.CompareTo(name.FullName) == 0);

            if (assembly != null)
            {
                return assembly;
            }

            try
            {
                assembly = Assembly.Load(name);
            }
            catch (Exception ex)
            {
                if (throwOnError)
                {
                    throw ex;
                }
            }

            if (assembly != null)
            {
                this.assemblies.Add(assembly);
                return assembly;
            }

            return null;
        }

        public Assembly GetAssembly(AssemblyName name)
        {
            return this.GetAssembly(name, false);
        }

        public string GetPathOfAssembly(AssemblyName name)
        {
            string result = this.assemblies
                .Find(a => a.GetName().FullName.CompareTo(name.FullName) == 0)?.Location;
            return result;
        }

        public Type GetType(string name, bool throwOnError, bool ignoreCase)
        {
            var type = Type.GetType(name, throwOnError, ignoreCase);
            if (type == null)
            {
                this.assemblies.Any(assembly =>
                {
                    type = assembly.GetType(name, false, ignoreCase);
                    return type != null;
                });
            }
            if (type == null && throwOnError)
            {
                throw new TypeLoadException($"未找到类型 {name}");
            }
            return type;
        }

        public Type GetType(string name, bool throwOnError)
        {
            return this.GetType(name, throwOnError, false);
        }

        public Type GetType(string name)
        {
            return this.GetType(name, false, false);
        }

        public void ReferenceAssembly(AssemblyName name)
        {
            this.GetAssembly(name, false);
        }

        #endregion

        // The types available when discovering the design
        #region ITypeDiscoveryService Interface member

        /// <inheritdoc />
        public ICollection GetTypes(Type baseType, bool excludeGlobalTypes)
        {
            var list = new List<Type>();
            if (baseType == null)
            {
                baseType = typeof(object);
            }
            foreach (var assembly in this.assemblies)
            {
                if (!excludeGlobalTypes || !assembly.GlobalAssemblyCache)
                {
                    list.AddRange(assembly.GetTypes().Where(t => t.IsSubclassOf(baseType)));
                }
            }
            return list;
        }

        #endregion

    }
}
