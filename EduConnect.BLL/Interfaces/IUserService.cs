﻿using EduConnect.DAL.Interface;
using EduConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IQueryable<User>> GetAll();
        Task<bool> CreateUser(User entityModel);
        Task<bool> Delete(int id);
        Task<bool> Update(User entityModel);
        Task<User> GetById(string id);
    }
}
