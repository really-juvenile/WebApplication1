using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using WebApplication1.Models;

namespace WebApplication1.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Username);
            Map(x => x.Password);
        }
    }
}