﻿using Casgem.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IMongoEstateService
    {
        List<Estate> Get();
        Estate Get(string id);
        List<Estate> GetByFilter(string? city, string? type, int? room, string? title, int? price, string? buildYear);
        Estate Create(Estate estate);
        void Update(string id, Estate estate);
        void Remove(string id);
    }
}