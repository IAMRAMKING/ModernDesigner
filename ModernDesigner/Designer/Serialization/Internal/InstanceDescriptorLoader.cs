﻿using System.Reflection;

namespace ModernDesigner.Serialization
{
    internal class InstanceDescriptorLoader
    {
        public InstanceDescriptorLoader(MemberInfo memberInfo, object[] args)
        {
            this.MemberInfo = memberInfo;
            this.Arguments = args;
        }
        public MemberInfo MemberInfo { get; set; }

        public object[] Arguments { get; set; }
    }

}
