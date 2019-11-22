using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Bafp.Logic.Database
{
    public interface IConnectionFactory
    {
        DbConnection Create();
    }
}
