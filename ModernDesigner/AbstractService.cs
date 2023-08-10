using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernDesigner
{
    public abstract class AbstractService : Disposable
    {
        protected IServiceProvider ServiceProvider { get; private set; }

        public AbstractService()
        {

        }

        public AbstractService(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }
    }
}
