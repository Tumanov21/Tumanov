using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Hubs
{
    public class CustomUserIdProvider:IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext conn)
        {
            return conn.User?.Identity.Name;
        }
    }
}
