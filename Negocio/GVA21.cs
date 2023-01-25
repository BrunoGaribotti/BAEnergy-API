using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class GVA21
    {
        public List<GVA03> cGVA03 = new List<GVA03>();
        public Retorno Retorno = new Retorno();

        public string ID_GVA21 = "";
        public string FILLER = "";
        public string APRUEBA = "";
        public string CIRCUITO = "";
        public string COD_CLIENT = "";
        public string COD_SUCURS = "";
        public string COD_TRANSP = "";
        public string COD_VENDED = "";
        public string COMENTARIO = "";
        public string COMP_STK = "";
        public string COND_VTA = "";
        public string COTIZ = "";
        public string ESTADO = "";
        public string EXPORTADO = "";
        public string FECHA_APRU = "";
        public string FECHA_ENTR = "";
        public string FECHA_PEDI = "";
        public string HORA_APRUE = "";
        public string ID_EXTERNO = "";
        public string LEYENDA_1 = "";
        public string LEYENDA_2 = "";
        public string LEYENDA_3 = "";
        public string LEYENDA_4 = "";
        public string LEYENDA_5 = "";
        public string MON_CTE = "";
        public string N_LISTA = "";
        public string N_REMITO = "";
        public string NRO_O_COMP = "";
        public string NRO_PEDIDO = "";
        public string NRO_SUCURS = "";
        public string ORIGEN = "";
        public string PORC_DESC = "";
        public string REVISO_FAC = "";
        public string REVISO_PRE = "";
        public string REVISO_STK = "";
        public string TALONARIO = "";
        public string TALON_PED = "";
        public string TOTAL_PEDI = "";
        public string TIPO_ASIEN = "";
        public string MOTIVO = "";
        public string HORA = "";
        public string COD_CLASIF = "";
        public string ID_ASIENTO_MODELO_GV = "";
        public string TAL_PE_ORI = "";
        public string NRO_PE_ORI = "";
        public string FECHA_INGRESO = "";
        public string HORA_INGRESO = "";
        public string USUARIO_INGRESO = "";
        public string TERMINAL_INGRESO = "";
        public string FECHA_ULTIMA_MODIFICACION = "";
        public string HORA_ULTIMA_MODIFICACION = "";
        public string USUA_ULTIMA_MODIFICACION = "";
        public string TERM_ULTIMA_MODIFICACION = "";
        public string ID_DIRECCION_ENTREGA = "";
        public string ES_PEDIDO_WEB = "";
        public string WEB_ORDER_ID = "";
        public string FECHA_O_COMP = "";
        public string ACTIVIDAD_COMPROBANTE_AFIP = "";
        public string ID_ACTIVIDAD_EMPRESA_AFIP = "";
        public string TIPO_DOCUMENTO_PAGADOR = "";
        public string NUMERO_DOCUMENTO_PAGADOR = "";
        public string USUARIO_TIENDA = "";
        public string TIENDA = "";
        public string ORDER_ID_TIENDA = "";
        public string NRO_OC_COMP = "";
        public string TIENDA_QUE_VENDE = "";
        public string TOTAL_DESC_TIENDA = "";
        public string PORCEN_DESC_TIENDA = "";
        public string USUARIO_TIENDA_VENDEDOR = "";
        public string ID_NEXO_PEDIDOS_ORDEN = "";
        public string Insert()
        {
            string sql = "";
            sql = @"
            INSERT INTO GVA21 
            (
            FILLER
            ,APRUEBA
            ,CIRCUITO
            ,COD_CLIENT
            ,COD_SUCURS
            ,COD_TRANSP
            ,COD_VENDED
            ,COMENTARIO
            ,COMP_STK
            ,COND_VTA
            ,COTIZ
            ,ESTADO
            ,EXPORTADO
            ,FECHA_APRU
            ,FECHA_ENTR
            ,FECHA_PEDI
            ,HORA_APRUE
            ,ID_EXTERNO
            ,LEYENDA_1
            ,LEYENDA_2
            ,LEYENDA_3
            ,LEYENDA_4
            ,LEYENDA_5
            ,MON_CTE
            ,N_LISTA
            ,N_REMITO
            ,NRO_O_COMP
            ,NRO_PEDIDO
            ,NRO_SUCURS
            ,ORIGEN
            ,PORC_DESC
            ,REVISO_FAC
            ,REVISO_PRE
            ,REVISO_STK
            ,TALONARIO
            ,TALON_PED
            ,TOTAL_PEDI
            ,TIPO_ASIEN
            ,MOTIVO
            ,HORA
            ,COD_CLASIF
            ,ID_ASIENTO_MODELO_GV
            ,TAL_PE_ORI
            ,NRO_PE_ORI
            ,FECHA_INGRESO
            ,HORA_INGRESO
            ,USUARIO_INGRESO
            ,TERMINAL_INGRESO
            ,FECHA_ULTIMA_MODIFICACION
            ,HORA_ULTIMA_MODIFICACION
            ,USUA_ULTIMA_MODIFICACION
            ,TERM_ULTIMA_MODIFICACION
            ,ID_DIRECCION_ENTREGA
            ,ES_PEDIDO_WEB
            ,WEB_ORDER_ID
            ,FECHA_O_COMP
            ,ACTIVIDAD_COMPROBANTE_AFIP
            ,ID_ACTIVIDAD_EMPRESA_AFIP
            ,TIPO_DOCUMENTO_PAGADOR
            ,NUMERO_DOCUMENTO_PAGADOR
            ,USUARIO_TIENDA
            ,TIENDA
            ,ORDER_ID_TIENDA
            ,NRO_OC_COMP
            ,TIENDA_QUE_VENDE
            ,TOTAL_DESC_TIENDA
            ,PORCEN_DESC_TIENDA
            ,USUARIO_TIENDA_VENDEDOR
            ,ID_NEXO_PEDIDOS_ORDEN
            ) VALUES ( 
            '" + FILLER + @"'
            ,'" + APRUEBA + @"'
            ," + CIRCUITO + @" 
           ,'" + COD_CLIENT + @"'
           ,'" + COD_SUCURS + @"'
           ,'" + COD_TRANSP + @"'
           ,'" + COD_VENDED + @"'
           ,'" + COMENTARIO + @"'
            ," + COMP_STK + @" 
            ," + COND_VTA + @" 
            ," + COTIZ + @" 
            ," + ESTADO + @" 
            ," + EXPORTADO + @" 
            ,CONVERT(DATETIME,'" + FECHA_APRU + @"',103)
            ,CONVERT(DATETIME,'" + FECHA_ENTR + @"',103)
            ,CONVERT(DATETIME,'" + FECHA_PEDI + @"',103)
           ,'" + HORA_APRUE + @"'
           ,'" + ID_EXTERNO + @"'
           ,'" + LEYENDA_1 + @"'
           ,'" + LEYENDA_2 + @"'
           ,'" + LEYENDA_3 + @"'
           ,'" + LEYENDA_4 + @"'
           ,'" + LEYENDA_5 + @"'
            ," + MON_CTE + @" 
            ," + N_LISTA + @" 
           ,'" + N_REMITO + @"'
           ,'" + NRO_O_COMP + @"'
           ,'" + NRO_PEDIDO + @"'
            ," + NRO_SUCURS + @" 
           ,'" + ORIGEN + @"'
            ," + PORC_DESC + @" 
           ,'" + REVISO_FAC + @"'
           ,'" + REVISO_PRE + @"'
           ,'" + REVISO_STK + @"'
            ," + TALONARIO + @" 
            ," + TALON_PED + @" 
            ," + TOTAL_PEDI.Replace(",", ".") + @" 
           ,'" + TIPO_ASIEN + @"'
           ,'" + MOTIVO + @"'
           ,'" + HORA + @"'
           ,'" + COD_CLASIF + @"'
            ," + ID_ASIENTO_MODELO_GV + @" 
            ," + TAL_PE_ORI + @" 
           ,'" + NRO_PE_ORI + @"'
            ,CONVERT(DATETIME,'" + FECHA_INGRESO + @"',103)
           ,'" + HORA_INGRESO + @"'
           ,'" + USUARIO_INGRESO + @"'
           ,'" + TERMINAL_INGRESO + @"'
            ,CONVERT(DATETIME,'" + FECHA_ULTIMA_MODIFICACION + @"',103)
           ,'" + HORA_ULTIMA_MODIFICACION + @"'
           ,'" + USUA_ULTIMA_MODIFICACION + @"'
           ,'" + TERM_ULTIMA_MODIFICACION + @"'
            ," + ID_DIRECCION_ENTREGA + @" 
            ," + ES_PEDIDO_WEB + @" 
            ," + WEB_ORDER_ID + @"
            ,CONVERT(DATETIME,'" + FECHA_O_COMP + @"',103)
           ,'" + ACTIVIDAD_COMPROBANTE_AFIP + @"'
            ," + ID_ACTIVIDAD_EMPRESA_AFIP + @" 
            ," + TIPO_DOCUMENTO_PAGADOR + @" 
           ,'" + NUMERO_DOCUMENTO_PAGADOR + @"'
           ," + USUARIO_TIENDA + @"
           ," + TIENDA + @"
           ," + ORDER_ID_TIENDA + @"
           ,'" + NRO_OC_COMP + @"'
           ," + TIENDA_QUE_VENDE + @"
            ," + TOTAL_DESC_TIENDA + @" 
            ," + PORCEN_DESC_TIENDA + @" 
          ,'" + USUARIO_TIENDA_VENDEDOR + @"'
            ," + ID_NEXO_PEDIDOS_ORDEN + @"
            )
            ";
            return sql;
        }
        public GVA21()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString()).Remove(0, 6) + "\\GVA21.xml");
            ID_GVA21 = ds.Tables[0].Rows[0]["ID_GVA21"].ToString();
            FILLER = ds.Tables[0].Rows[0]["FILLER"].ToString();
            APRUEBA = ds.Tables[0].Rows[0]["APRUEBA"].ToString();
            CIRCUITO = ds.Tables[0].Rows[0]["CIRCUITO"].ToString();
            COD_CLIENT = ds.Tables[0].Rows[0]["COD_CLIENT"].ToString();
            COD_SUCURS = ds.Tables[0].Rows[0]["COD_SUCURS"].ToString();
            COD_TRANSP = ds.Tables[0].Rows[0]["COD_TRANSP"].ToString();
            COD_VENDED = ds.Tables[0].Rows[0]["COD_VENDED"].ToString();
            COMENTARIO = ds.Tables[0].Rows[0]["COMENTARIO"].ToString();
            COMP_STK = ds.Tables[0].Rows[0]["COMP_STK"].ToString();
            COND_VTA = ds.Tables[0].Rows[0]["COND_VTA"].ToString();
            COTIZ = ds.Tables[0].Rows[0]["COTIZ"].ToString();
            ESTADO = ds.Tables[0].Rows[0]["ESTADO"].ToString();
            EXPORTADO = ds.Tables[0].Rows[0]["EXPORTADO"].ToString();
            FECHA_APRU = ds.Tables[0].Rows[0]["FECHA_APRU"].ToString();
            FECHA_ENTR = ds.Tables[0].Rows[0]["FECHA_ENTR"].ToString();
            FECHA_PEDI = ds.Tables[0].Rows[0]["FECHA_PEDI"].ToString();
            HORA_APRUE = ds.Tables[0].Rows[0]["HORA_APRUE"].ToString();
            ID_EXTERNO = ds.Tables[0].Rows[0]["ID_EXTERNO"].ToString();
            LEYENDA_1 = ds.Tables[0].Rows[0]["LEYENDA_1"].ToString();
            LEYENDA_2 = ds.Tables[0].Rows[0]["LEYENDA_2"].ToString();
            LEYENDA_3 = ds.Tables[0].Rows[0]["LEYENDA_3"].ToString();
            LEYENDA_4 = ds.Tables[0].Rows[0]["LEYENDA_4"].ToString();
            LEYENDA_5 = ds.Tables[0].Rows[0]["LEYENDA_5"].ToString();
            MON_CTE = ds.Tables[0].Rows[0]["MON_CTE"].ToString();
            N_LISTA = ds.Tables[0].Rows[0]["N_LISTA"].ToString();
            N_REMITO = ds.Tables[0].Rows[0]["N_REMITO"].ToString();
            NRO_O_COMP = ds.Tables[0].Rows[0]["NRO_O_COMP"].ToString();
            NRO_PEDIDO = ds.Tables[0].Rows[0]["NRO_PEDIDO"].ToString();
            NRO_SUCURS = ds.Tables[0].Rows[0]["NRO_SUCURS"].ToString();
            ORIGEN = ds.Tables[0].Rows[0]["ORIGEN"].ToString();
            PORC_DESC = ds.Tables[0].Rows[0]["PORC_DESC"].ToString();
            REVISO_FAC = ds.Tables[0].Rows[0]["REVISO_FAC"].ToString();
            REVISO_PRE = ds.Tables[0].Rows[0]["REVISO_PRE"].ToString();
            REVISO_STK = ds.Tables[0].Rows[0]["REVISO_STK"].ToString();
            TALONARIO = ds.Tables[0].Rows[0]["TALONARIO"].ToString();
            TALON_PED = ds.Tables[0].Rows[0]["TALON_PED"].ToString();
            TOTAL_PEDI = ds.Tables[0].Rows[0]["TOTAL_PEDI"].ToString();
            TIPO_ASIEN = ds.Tables[0].Rows[0]["TIPO_ASIEN"].ToString();
            MOTIVO = ds.Tables[0].Rows[0]["MOTIVO"].ToString();
            HORA = ds.Tables[0].Rows[0]["HORA"].ToString();
            COD_CLASIF = ds.Tables[0].Rows[0]["COD_CLASIF"].ToString();
            ID_ASIENTO_MODELO_GV = ds.Tables[0].Rows[0]["ID_ASIENTO_MODELO_GV"].ToString();
            TAL_PE_ORI = ds.Tables[0].Rows[0]["TAL_PE_ORI"].ToString();
            NRO_PE_ORI = ds.Tables[0].Rows[0]["NRO_PE_ORI"].ToString();
            FECHA_INGRESO = ds.Tables[0].Rows[0]["FECHA_INGRESO"].ToString();
            HORA_INGRESO = ds.Tables[0].Rows[0]["HORA_INGRESO"].ToString();
            USUARIO_INGRESO = ds.Tables[0].Rows[0]["USUARIO_INGRESO"].ToString();
            TERMINAL_INGRESO = ds.Tables[0].Rows[0]["TERMINAL_INGRESO"].ToString();
            FECHA_ULTIMA_MODIFICACION = ds.Tables[0].Rows[0]["FECHA_ULTIMA_MODIFICACION"].ToString();
            HORA_ULTIMA_MODIFICACION = ds.Tables[0].Rows[0]["HORA_ULTIMA_MODIFICACION"].ToString();
            USUA_ULTIMA_MODIFICACION = ds.Tables[0].Rows[0]["USUA_ULTIMA_MODIFICACION"].ToString();
            TERM_ULTIMA_MODIFICACION = ds.Tables[0].Rows[0]["TERM_ULTIMA_MODIFICACION"].ToString();
            ID_DIRECCION_ENTREGA = ds.Tables[0].Rows[0]["ID_DIRECCION_ENTREGA"].ToString();
            ES_PEDIDO_WEB = ds.Tables[0].Rows[0]["ES_PEDIDO_WEB"].ToString();
            WEB_ORDER_ID = ds.Tables[0].Rows[0]["WEB_ORDER_ID"].ToString();
            FECHA_O_COMP = ds.Tables[0].Rows[0]["FECHA_O_COMP"].ToString();
            ACTIVIDAD_COMPROBANTE_AFIP = ds.Tables[0].Rows[0]["ACTIVIDAD_COMPROBANTE_AFIP"].ToString();
            ID_ACTIVIDAD_EMPRESA_AFIP = ds.Tables[0].Rows[0]["ID_ACTIVIDAD_EMPRESA_AFIP"].ToString();
            TIPO_DOCUMENTO_PAGADOR = ds.Tables[0].Rows[0]["TIPO_DOCUMENTO_PAGADOR"].ToString();
            NUMERO_DOCUMENTO_PAGADOR = ds.Tables[0].Rows[0]["NUMERO_DOCUMENTO_PAGADOR"].ToString();
            USUARIO_TIENDA = ds.Tables[0].Rows[0]["USUARIO_TIENDA"].ToString();
            TIENDA = ds.Tables[0].Rows[0]["TIENDA"].ToString();
            ORDER_ID_TIENDA = ds.Tables[0].Rows[0]["ORDER_ID_TIENDA"].ToString();
            NRO_OC_COMP = ds.Tables[0].Rows[0]["NRO_OC_COMP"].ToString();
            TIENDA_QUE_VENDE = ds.Tables[0].Rows[0]["TIENDA_QUE_VENDE"].ToString();
            TOTAL_DESC_TIENDA = ds.Tables[0].Rows[0]["TOTAL_DESC_TIENDA"].ToString();
            PORCEN_DESC_TIENDA = ds.Tables[0].Rows[0]["PORCEN_DESC_TIENDA"].ToString();
            USUARIO_TIENDA_VENDEDOR = ds.Tables[0].Rows[0]["USUARIO_TIENDA_VENDEDOR"].ToString();
            ID_NEXO_PEDIDOS_ORDEN = ds.Tables[0].Rows[0]["ID_NEXO_PEDIDOS_ORDEN"].ToString();
            ds.Dispose();
        }
    }

}
