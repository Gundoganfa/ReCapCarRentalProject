using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IResult Update(User user);
        IResult DeleteById(int userId);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int userId);
        IResult Delete(User user);
    }
}
