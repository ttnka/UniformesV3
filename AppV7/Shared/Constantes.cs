using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public static class Constantes
    {
        public const string ElDominio = "Zuverworks.com";

        
        // Mail 01

        public const string DeNombreMail01 = "WebMaster";
        public const string DeMail01 = "info@zuverworks.com";
        public const string ServerMail01 = "mail.omnis.com";
        public const int PortMail01 = 587;
        public const int Nivel01 = 1;
        public const string UserNameMail01 = "info@zuverworks.com";
        public const string PasswordMail01 = "2468022Ih.";

        // Mail 02
        public const string DeNombreMail02 = "WebMaster";
        public const string DeMail02 = "@zuverworks.com";
        public const string ServerMail02 = "mail.omnis.com";
        public const int PortMail02 = 587;
        public const string UserNameMail02 = "@zuverworks.com";
        public const string PasswordMail02 = "";

        // Registro Inicial Publico en GENERAL Organizacion
        
        public const string PgRfc = "PGE010101AAA";
        public const string PgRazonSocial = "Publico en General";
        public const int PgEstado = 1;  // En caso de no quere que se utilice poner 2
        public const bool PgStatus = true;
        public const string PgMail = "peg@peg.com";

        // Registro Usuario Publico en GENERAL

        public const string DeNombreMailPublico = "Publico";
        public const string DeMailPublico = "publico@zuverworks.com";
        public const int EstadoPublico = 1;
        public const int NivelPublico = 1;
        public const string UserNameMailPublico = "publico@zuverworks.com";
        public const string PasswordMailPublico = "PublicoLibre1.";


        // Registro de Sistema
        public const string SyRfc = "WEB010101MAS";
        public const string SyRazonSocial = "WEBMASTER";
        public const int SyEstado = 1;
        public const bool SyStatus = true;
        public const string SyMail = "info@zuverworks.com";
        public const string SysPassword = "24680212Ih.";

        // DATOS PARA UNIFORMES
        public const string Tipos = "Entrega,Salida";
        public const string Grupos = "Ropa,Zapatos";
        public const string RopaTallas = "XCh,Ch,Med,Gde,XGde";
        public const string ZapatoTallas = "Z16,Z17,Z18,Z19,Z20";

        public const string MpiosTodos = "Aconchi,Agua Prieta,Alamos,Altar,Arivechi," +
            "Arizpe,Atil,Bacadéhuachi,Bacanora,Bacerac,Bacoachi,Bácum,Banámichi," +
            "Baviácora,Bavispe,Benjamín Hill,Caborca,Cajeme,Cananea,Carbó,La Colorada," +
            "Cucurpe,Cumpas,Divisaderos,Empalme,Etchojoa,Fronteras,Granados,Guaymas," +
            "Hermosillo,Huachinera,Huásabas,Huatabampo,Huépac,Imuris,Magdalena,Mazatán," +
            "Moctezuma,Naco,Nácori Chico,Nacozari de García,Navojoa,Nogales,Ónavas,Opodepe," +
            "Oquitoa,Pitiquito,Puerto Peñasco,Quiriego,Rayón,Rosario,Sahuaripa," +
            "San Felipe de Jesús,San Javier,San Luis Río Colorado,San Miguel de Horcasitas," +
            "San Pedro de la Cueva,Santa Ana,Santa Cruz,Sáric,Soyopa,Suaqui Grande," +
            "Tepache,Trincheras,Tubutama,Ures,Villa Hidalgo,Villa Pesqueira,Yécora," +
            "General Plutarco Elías Calles,Benito Juárez,San Ignacio Río Muerto";
    }
}
