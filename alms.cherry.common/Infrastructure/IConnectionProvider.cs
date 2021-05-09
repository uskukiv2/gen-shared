using System;
using System.Collections.Generic;
using System.Text;
using alms.cherry.common.Models;

namespace alms.cherry.common.Infrastructure
{
    public interface IConnectionProvider : IDisposable
    {
        void SetConfiguration(Configuration configuration);

        Configuration CurrentConfiguration { get; }
    }
}
