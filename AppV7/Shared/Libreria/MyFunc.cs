﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared.Libreria
{
    public class MyFunc
    {
        public Z190_Bitacora WriteBitacora(string userId,string orgId, string desc,
            bool sistema )
        {
            Z190_Bitacora bitacora = new Z190_Bitacora();
            bitacora.UsuariosId = userId;
            bitacora.OrgId = orgId;
            bitacora.Desc = desc;
            bitacora.Sistema = sistema;
            
            return bitacora;
        }

        public static string DiaTitulo(int dia, int completo=0)
        {
            string valores = "Dom,Lun,Mar,Mie,Jue,Vie,Sab,Domingo,Lunes,Martes,Miercoles,Jueves,Viernes,Sabado";
            var arr = valores.Split(",");
            completo = completo > 1 ? 0 : completo * 7;
            
            return arr[(dia-1+completo)];
        }
        public static string MesTitulo(int mes, int completo = 0)
        {
            string valores = "Ene,Feb,Mar,Abr,May,Jun,Jul,Ago,Sep,Oct,Nov,Dic,Enero,Febrero,Marzo,Abril,Mayo,Junio,Julio,Agosto,Septiembre,Octubre,Noviembre,Diciembre";
            var arr = valores.Split(",");
            completo = completo > 1 ? 0 : completo * 12;

            return arr[(mes - 1 + completo)];
        }
        public static int Ejercicio(DateTime fecha)
        {
            int year = fecha.Year;
            return year < 2000 ? year - 1900 : year - 2000; 
        }
        public static string LaHora(DateTime lahora, string formato)
        {
            switch (formato)
            {
                case "M":
                    string cero = lahora.Minute < 10 ? "0" : "";
                    return $"{lahora.Hour}:{cero}{lahora.Minute}";

                default:
                    string mincero = lahora.Minute < 10 ? "0" : "";
                    string segcero = lahora.Second < 10 ? "0" : "";
                    return $"{lahora.Hour}:{mincero}{lahora.Minute}:{segcero}{lahora.Second}";
            }
        }

        public static string FormatoFecha(string formato, DateTime lafecha)
        {
            string resultado = string.Empty;

            switch (formato)
            {
                case "DD/MMM/AA":
                    resultado = $"{lafecha.Day} / ";
                    resultado += $"{MesTitulo(lafecha.Month, 0)} /";
                    resultado += $"{Ejercicio(lafecha)}";
                    break;
                    
            }
            return resultado;
        }

        public static List<string> WebSites()
        {// Donde se muestra en la pagina
            var resultado = new List<string>();
            resultado.Add("GeneralWeb");
            resultado.Add("ContactanosWeb");
            return resultado;
        }
        public static List<string> Captura()
        { // donde se captura los datos a mostrar
            var resultado = new List<string>();
            resultado.Add("GeneralCapt");
            resultado.Add("Vacio");

            return resultado;
        }

        public static List<string> Componentes()
        { // donde se configura que se mostrara de la pagina
            var resultado = new List<string>();
            resultado.Add("WebConfig");
            resultado.Add("ContactanosConfig");
            return resultado;
        }
    }
}
