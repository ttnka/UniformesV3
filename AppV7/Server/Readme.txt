This code includes a dependency on Duende IdentityServer.
This is an open source product with a reciprocal license agreement. If you plan to use Duende IdentityServer in production this may require a license fee.
To see how to use Azure Active Directory for your identity please see https://aka.ms/aspnetidentityserver
To see if you require a commercial license for Duende IdentityServer please see https://aka.ms/identityserverlicense







Procedimientos guardados en MySQL

DELIMITER $$
CREATE DEFINER=`lafam002_ulises`@`%` PROCEDURE `DetFiltroGeneral`(IN `FoliosF` VARCHAR(200), IN `EstadoF` INT, IN `AlmacenF` VARCHAR(200), IN `TipoEntradaF` VARCHAR(200), IN `ComercioF` VARCHAR(200), IN `ProductoF` VARCHAR(200), IN `CiudadF` VARCHAR(200), IN `CantidadF` INT)
Select Det.*
    FROM 
    	Usuarios As Users 
        	INNER Join 
        Solicitudes As Sol On Sol.Usuario = Users.UsuariosId 
        	Inner Join 
        DetSolicitud as Det On Sol.SolicitudId = Det.SolicitudId
    Where
    	If (FoliosF != 'Alla', Sol.Folio = FoliosF, 1=1) AND
        if (EstadoF <> 0, Sol.Estado = EstadoF, 1=1) AND
    	if (AlmacenF != 'Alla', Sol.Almacen = AlmacenF, 1=1) AND
        If (TipoEntradaF != 'Alla', Sol.Tipo = TipoEntradaF, 1=1) AND
        If (ComercioF != 'Alla', Sol.Usuario = ComercioF, 1=1) AND
        If (ProductoF != 'Alla', Det.Producto = ProductoF, 1=1) AND
        If (CiudadF != 'Alla', Users.Municipio = CiudadF, 1=1) AND
        If (CantidadF <> 0, Det.Cantidad = CantidadF, 1=1) AND
        Sol.Status = '1' AND 
        Det.Status = '1'$$
DELIMITER ;