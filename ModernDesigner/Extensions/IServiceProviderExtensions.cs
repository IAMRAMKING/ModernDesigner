﻿using System;

namespace ModernDesigner
{
    public static class IServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider serviceProvider)
        {
            return (T)serviceProvider.GetService(typeof(T));
        }
    }
}
