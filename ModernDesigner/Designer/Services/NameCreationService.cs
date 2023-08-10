using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace ModernDesigner.Services
{
    /// <summary>
    /// Provide the only service that can generate the only name that can generate objects。
    /// </summary>
    public class NameCreationService : AbstractService, INameCreationService
    {
        #region INameCreationService Interface member

        public string CreateName(IContainer container, Type dataType)
        {
            int i = 0;
            string typeName = dataType.Name;
            string name;
            do
            {
                i++;
                name = typeName + i.ToString();
                if (container?.Components[name] == null)
                {
                    break;
                }
            } while (true);

            return name;
        }

        public bool IsValidName(string name)
        {
            // 名称为空
            if (name == null || name.Length == 0)
            {
                return false;
            }
            // 不是字母开头
            if (!char.IsLetter(name, 0))
            {
                return false;
            }
            // 含有不允许的字母
            for (int i = 0; i < name.Length; i++)
            {
                var c = name[i];
                if (!char.IsLetterOrDigit(name, i) && c != '_' && c != ' ' && c != '-' && c != '.')
                {
                    return false;
                }
            }
            return true;
        }

        public void ValidateName(string name)
        {
            if (!IsValidName(name))
            {
                throw new ArgumentException($"无效的名称: {name}");
            }
        }

        #endregion
    }
}
