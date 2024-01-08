using DLL.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
   public class TestService
    {
        private readonly TestRepository _repository;

        public TestService(TestRepository repository)
        {
            _repository = repository;
        }
        public async Task<OperationDetails> CreateAsync(Test test) 
        { 
          return await  _repository.CreateAsync(test);
        }
    }
}
