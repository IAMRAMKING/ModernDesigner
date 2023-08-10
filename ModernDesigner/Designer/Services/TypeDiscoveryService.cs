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
    public class TypeDiscoveryService : AbstractService, ITypeDiscoveryService
    {
        private List<Assembly> assemblies;

        public TypeDiscoveryService()
        {
            this.assemblies = new List<Assembly>();
            this.assemblies.AddRange(new Assembly[] {
                typeof(Size).Assembly,
                typeof(Control).Assembly,
                typeof(DataSet).Assembly,
                typeof(XmlElement).Assembly
            });
        }

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
