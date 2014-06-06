using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigeret.Models.ModelExtensions
{
    public class ModelEnums
    {
    }

    public enum EstatusSolicitudes
    {
        Aprobada = 2,
        En_Proceso = 3,
        Cancelada = 4,
        Pendiente = 5
    }

    public enum EstatusEquipos
    {
        Disponible = 1,
        Mantenimiento = 2,
        En_Uso = 3,
        Dañado = 4,
        Nuevo_Equipo = 5
    }

    public enum EstatusMatriculas
    {
        Activo = 1,
        Inactivo = 2
    }

    public enum TiposSolicitudes
    {
        Sistema = 1,
        Sms = 2
    }

    public enum EstatusUsuarios
    {
        Activo = 1,
        Suspendido = 2,
        Inactivo = 3
    }

}