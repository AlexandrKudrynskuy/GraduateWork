using DLL.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class TestRepository : BaseRepository<Test>
    {
        public TestRepository(TradeAssistantContext context) : base(context)
        {
            
        }
    }
}
