using System;
using System.Collections.Generic;
using System.Text;

namespace alms.cherry.common.Infrastructure
{
    public interface ISecureHelper
    {
        string GetInstanceName(Type type);
        string GetStage(Type type, string stage);
    }
}
