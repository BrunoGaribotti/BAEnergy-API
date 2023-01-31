using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    /// <summary>
    /// Objeto de GVA03 - Renglones del pedido.
    /// </summary>
    public class GVA03
    {
        public string ID_GVA03 = "";
        public string FILLER = "";
        public string CAN_EQUI_V = "";
        public string CANT_A_DES = "";
        public string CANT_A_FAC = "";
        public string CANT_PEDID = "";
        public string CANT_PEN_D = "";
        public string CANT_PEN_F = "";
        public string COD_ARTICU = "";
        public string DESCUENTO = "";
        public string N_RENGLON = "";
        public string NRO_PEDIDO = "";
        public string PEN_REM_FC = "";
        public string PEN_FAC_RE = "";
        public string PRECIO = "";
        public string TALON_PED = "";
        public string COD_CLASIF = "";
        public string CANT_A_DES_2 = "";
        public string CANT_A_FAC_2 = "";
        public string CANT_PEDID_2 = "";
        public string CANT_PEN_D_2 = "";
        public string CANT_PEN_F_2 = "";
        public string PEN_REM_FC_2 = "";
        public string PEN_FAC_RE_2 = "";
        public string ID_MEDIDA_VENTAS = "";
        public string ID_MEDIDA_STOCK_2 = "";
        public string ID_MEDIDA_STOCK = "";
        public string UNIDAD_MEDIDA_SELECCIONADA = "";
        public string COD_ARTICU_KIT = "";
        public string RENGL_PADR = "";
        public string PROMOCION = "";
        public string PRECIO_ADICIONAL_KIT = "";
        public string KIT_COMPLETO = "";
        public string INSUMO_KIT_SEPARADO = "";
        public string PRECIO_LISTA = "";
        public string PRECIO_BONIF = "";
        public string DESCUENTO_PARAM = "";
        public string PRECIO_FECHA = "";
        public string FECHA_MODIFICACION_PRECIO = "";
        public string USUARIO_MODIFICACION_PRECIO = "";
        public string TERMINAL_MODIFICACION_PRECIO = "";
        public string ID_NEXO_PEDIDOS_RENGLON_ORDEN = "";
        public string Insert()
        {
            string sql = "";
            sql = @"
                INSERT INTO GVA03 
                (
                FILLER
                ,CAN_EQUI_V
                ,CANT_A_DES
                ,CANT_A_FAC
                ,CANT_PEDID
                ,CANT_PEN_D
                ,CANT_PEN_F
                ,COD_ARTICU
                ,DESCUENTO
                ,N_RENGLON
                ,NRO_PEDIDO
                ,PEN_REM_FC
                ,PEN_FAC_RE
                ,PRECIO
                ,TALON_PED
                ,COD_CLASIF
                ,CANT_A_DES_2
                ,CANT_A_FAC_2
                ,CANT_PEDID_2
                ,CANT_PEN_D_2
                ,CANT_PEN_F_2
                ,PEN_REM_FC_2
                ,PEN_FAC_RE_2
                ,ID_MEDIDA_VENTAS
                ,ID_MEDIDA_STOCK_2
                ,ID_MEDIDA_STOCK
                ,UNIDAD_MEDIDA_SELECCIONADA
                ,COD_ARTICU_KIT
                ,RENGL_PADR
                ,PROMOCION
                ,PRECIO_ADICIONAL_KIT
                ,KIT_COMPLETO
                ,INSUMO_KIT_SEPARADO
                ,PRECIO_LISTA
                ,PRECIO_BONIF
                ,DESCUENTO_PARAM
                ,PRECIO_FECHA
                ,FECHA_MODIFICACION_PRECIO
                ,USUARIO_MODIFICACION_PRECIO
                ,TERMINAL_MODIFICACION_PRECIO
                ,ID_NEXO_PEDIDOS_RENGLON_ORDEN
                ) VALUES ( 
               '" + FILLER + @"'
                ," + CAN_EQUI_V + @" 
                ," + CANT_A_DES + @" 
                ," + CANT_A_FAC + @" 
                ," + CANT_PEDID + @" 
                ," + CANT_PEN_D + @" 
                ," + CANT_PEN_F + @" 
               ,'" + COD_ARTICU + @"'
                ," + DESCUENTO + @" 
                ," + N_RENGLON + @" 
               ,'" + NRO_PEDIDO + @"'
                ," + PEN_REM_FC + @" 
                ," + PEN_FAC_RE + @" 
                ," + PRECIO.Replace(",", ".") + @"
                ," + TALON_PED + @" 
               ,'" + COD_CLASIF + @"'
                ," + CANT_A_DES_2 + @" 
                ," + CANT_A_FAC_2 + @" 
                ," + CANT_PEDID_2 + @" 
                ," + CANT_PEN_D_2 + @" 
                ," + CANT_PEN_F_2 + @" 
                ," + PEN_REM_FC_2 + @" 
                ," + PEN_FAC_RE_2 + @" 
                ," + ID_MEDIDA_VENTAS + @" 
                ," + ID_MEDIDA_STOCK_2 + @" 
                ," + ID_MEDIDA_STOCK + @" 
                ,'" + UNIDAD_MEDIDA_SELECCIONADA + @"'
               ,'" + COD_ARTICU_KIT + @"'
                ," + RENGL_PADR + @" 
                ," + PROMOCION + @" 
                ," + PRECIO_ADICIONAL_KIT + @" 
                ," + KIT_COMPLETO + @" 
                ," + INSUMO_KIT_SEPARADO + @" 
                ," + PRECIO_LISTA.Replace(",",".") + @" 
                ,null 
                ," + DESCUENTO_PARAM + @" 
                ," + PRECIO_FECHA + @"
                ," + FECHA_MODIFICACION_PRECIO + @"
               ," + USUARIO_MODIFICACION_PRECIO + @"
               ," + TERMINAL_MODIFICACION_PRECIO + @"
                ," + ID_NEXO_PEDIDOS_RENGLON_ORDEN + @"
                )
                ";
            return sql;
        }
        public GVA03()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString()).Remove(0, 6) + "\\GVA03.xml");
            ID_GVA03 = ds.Tables[0].Rows[0]["ID_GVA03"].ToString();
            FILLER = ds.Tables[0].Rows[0]["FILLER"].ToString();
            CAN_EQUI_V = ds.Tables[0].Rows[0]["CAN_EQUI_V"].ToString();
            CANT_A_DES = ds.Tables[0].Rows[0]["CANT_A_DES"].ToString();
            CANT_A_FAC = ds.Tables[0].Rows[0]["CANT_A_FAC"].ToString();
            CANT_PEDID = ds.Tables[0].Rows[0]["CANT_PEDID"].ToString();
            CANT_PEN_D = ds.Tables[0].Rows[0]["CANT_PEN_D"].ToString();
            CANT_PEN_F = ds.Tables[0].Rows[0]["CANT_PEN_F"].ToString();
            COD_ARTICU = ds.Tables[0].Rows[0]["COD_ARTICU"].ToString();
            DESCUENTO = ds.Tables[0].Rows[0]["DESCUENTO"].ToString();
            N_RENGLON = ds.Tables[0].Rows[0]["N_RENGLON"].ToString();
            NRO_PEDIDO = ds.Tables[0].Rows[0]["NRO_PEDIDO"].ToString();
            PEN_REM_FC = ds.Tables[0].Rows[0]["PEN_REM_FC"].ToString();
            PEN_FAC_RE = ds.Tables[0].Rows[0]["PEN_FAC_RE"].ToString();
            PRECIO = ds.Tables[0].Rows[0]["PRECIO"].ToString();
            TALON_PED = ds.Tables[0].Rows[0]["TALON_PED"].ToString();
            COD_CLASIF = ds.Tables[0].Rows[0]["COD_CLASIF"].ToString();
            CANT_A_DES_2 = ds.Tables[0].Rows[0]["CANT_A_DES_2"].ToString();
            CANT_A_FAC_2 = ds.Tables[0].Rows[0]["CANT_A_FAC_2"].ToString();
            CANT_PEDID_2 = ds.Tables[0].Rows[0]["CANT_PEDID_2"].ToString();
            CANT_PEN_D_2 = ds.Tables[0].Rows[0]["CANT_PEN_D_2"].ToString();
            CANT_PEN_F_2 = ds.Tables[0].Rows[0]["CANT_PEN_F_2"].ToString();
            PEN_REM_FC_2 = ds.Tables[0].Rows[0]["PEN_REM_FC_2"].ToString();
            PEN_FAC_RE_2 = ds.Tables[0].Rows[0]["PEN_FAC_RE_2"].ToString();
            ID_MEDIDA_VENTAS = ds.Tables[0].Rows[0]["ID_MEDIDA_VENTAS"].ToString();
            ID_MEDIDA_STOCK_2 = ds.Tables[0].Rows[0]["ID_MEDIDA_STOCK_2"].ToString();
            ID_MEDIDA_STOCK = ds.Tables[0].Rows[0]["ID_MEDIDA_STOCK"].ToString();
            UNIDAD_MEDIDA_SELECCIONADA = ds.Tables[0].Rows[0]["UNIDAD_MEDIDA_SELECCIONADA"].ToString();
            COD_ARTICU_KIT = ds.Tables[0].Rows[0]["COD_ARTICU_KIT"].ToString();
            RENGL_PADR = ds.Tables[0].Rows[0]["RENGL_PADR"].ToString();
            PROMOCION = ds.Tables[0].Rows[0]["PROMOCION"].ToString();
            PRECIO_ADICIONAL_KIT = ds.Tables[0].Rows[0]["PRECIO_ADICIONAL_KIT"].ToString();
            KIT_COMPLETO = ds.Tables[0].Rows[0]["KIT_COMPLETO"].ToString();
            INSUMO_KIT_SEPARADO = ds.Tables[0].Rows[0]["INSUMO_KIT_SEPARADO"].ToString();
            PRECIO_LISTA = ds.Tables[0].Rows[0]["PRECIO_LISTA"].ToString();
            PRECIO_BONIF = ds.Tables[0].Rows[0]["PRECIO_BONIF"].ToString();
            DESCUENTO_PARAM = ds.Tables[0].Rows[0]["DESCUENTO_PARAM"].ToString();
            PRECIO_FECHA = ds.Tables[0].Rows[0]["PRECIO_FECHA"].ToString();
            FECHA_MODIFICACION_PRECIO = ds.Tables[0].Rows[0]["FECHA_MODIFICACION_PRECIO"].ToString();
            USUARIO_MODIFICACION_PRECIO = ds.Tables[0].Rows[0]["USUARIO_MODIFICACION_PRECIO"].ToString();
            TERMINAL_MODIFICACION_PRECIO = ds.Tables[0].Rows[0]["TERMINAL_MODIFICACION_PRECIO"].ToString();
            ID_NEXO_PEDIDOS_RENGLON_ORDEN = ds.Tables[0].Rows[0]["ID_NEXO_PEDIDOS_RENGLON_ORDEN"].ToString();
            ds.Dispose();
        }
    }
}