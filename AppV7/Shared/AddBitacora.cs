using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class AddBitacora : Z190_Bitacora
    {
        public Z190_Bitacora WriteBitacora(string userId, string desc, 
            bool sistema, string orgId)
        {
            Z190_Bitacora bitacora = new Z190_Bitacora();
            bitacora.UsuariosId = userId;
            bitacora.Desc = desc;
            bitacora.Sistema = sistema;
            bitacora.OrgId = orgId;
            
            return bitacora;
        }
    }
}
