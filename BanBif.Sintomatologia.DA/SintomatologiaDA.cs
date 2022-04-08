using BanBif.Sintomatologia.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.Sintomatologia.DA
{
    public class SintomatologiaDA
    {
        #region FORMULARIO
        public ObtenerFormResult ObtenerFormulario(ObtenerFormRequest request)
        {
            using (var db = new panelEntities())
            {
                var Formulario = db.tbl_mSINTO_FORMULARIO.Where(p => p.ESTADO == request.Estado).FirstOrDefault();


                var Resultado = new ObtenerFormResult();

                if (Formulario != null)
                {
                    Resultado.CodigoFormulario = Formulario.CODIGOFORMULARIO;
                    Resultado.Titulo = Formulario.TITULO;
                    Resultado.DescripcionTitulo = Formulario.DESCRIPCIONTITULO;
                    Resultado.Subtitulo = Formulario.DESCRIPCIONSUBTITULO;
                    Resultado.DescripcionSubtitulo = Formulario.DESCRIPCIONSUBTITULO;
                    Resultado.TerminosCondiciones = Formulario.TERMINOSCONDICIONES;

                    var ListaGrupos = new List<ObtenerGrupoPreguntaResult>();
                    foreach (var item in Formulario.tbl_aSINTO_GRUPOPREGUNTA.Where(p => p.ESTADO == 1).ToList())
                    {
                        var GrupoPregunta = new ObtenerGrupoPreguntaResult();
                        GrupoPregunta.CodigoGrupo = item.CODIGOGRUPO;
                        GrupoPregunta.Grupo = item.GRUPO;
                        GrupoPregunta.Orden = item.ORDEN.Value;

                        var ListaPreguntas = new List<ObtenerPreguntaResult>();
                        foreach (var itemP in item.tbl_aSINTO_PREGUNTA.Where(p => p.ESTADO == 1).ToList())
                        {
                            var Pregunta = new ObtenerPreguntaResult();
                            Pregunta.CodigoPregunta = itemP.CODIGOPREGUNTA;
                            Pregunta.Pregunta = itemP.PREGUNTA;
                            Pregunta.TextoApoyo = itemP.TEXTO_AYUDA;
                            Pregunta.Orden = itemP.ORDEN.Value;
                            Pregunta.CodigoTipoPregunta = itemP.CODIGOTIPOPREGUNTA.Value;
                            ListaPreguntas.Add(Pregunta);

                            var ListaOpciones = new List<ObtenerOpcionesResult>();

                            foreach (var itemO in itemP.tbl_aSINTO_OPCIONES.Where(p => p.ESTADO == 1).ToList())
                            {
                                var Opciones = new ObtenerOpcionesResult();
                                Opciones.CodigoOpcion = itemO.CODIGOOPCION;
                                Opciones.Opcion = itemO.OPCION;
                                Opciones.Imagen = itemO.IMAGEN;
                                Opciones.Orden = itemO.ORDEN.Value;

                                Pregunta.Opciones = ListaOpciones;
                                ListaOpciones.Add(Opciones);


                            }

                          

                        }

                        GrupoPregunta.Preguntas = ListaPreguntas;
                        ListaGrupos.Add(GrupoPregunta);


                        Resultado.GrupoPregunta = ListaGrupos;
                    }
                }

                return Resultado;
            }
        }
        #endregion

        #region LOGIN
        public ValidarLoginResult ValidarLogin(ValidarLoginRequest request)
        {
            using (var db = new panelEntities())
            {
                var cliente = db.tbl_mSINTO_COLABORADOR.Where(p => p.DOCUMENTO == request.Documento).FirstOrDefault(); //.ToList()
                var result = new ValidarLoginResult();

                /*DNI EXISTE EN BD*/
                if (cliente != null)
                {
                    result.CodigoAuto = cliente.CODAUTOGENERADO;
                }

                db.SaveChanges();

                return result;
            }
        }

        #endregion

        #region GRUPOPREGUNTA
        public ObtenerGrupoPreguntaResult ObtenerGrupoPregunta(ObtenerGrupoPreguntaRequest request)
        {
            using (var db = new panelEntities())
            {
                var GrupoPregunta = db.tbl_aSINTO_GRUPOPREGUNTA.Where(p => p.CODIGOFORMULARIO == request.CodigoFormulario).FirstOrDefault();
                var result = new ObtenerGrupoPreguntaResult();

                if (GrupoPregunta != null)
                {
                    result.CodigoGrupo = GrupoPregunta.CODIGOGRUPO;
                    result.Grupo = GrupoPregunta.GRUPO;
                    result.Flagsino = GrupoPregunta.FLAGSINO.Value;
                    result.Orden = GrupoPregunta.ORDEN.Value;
                }

                return result;
            }

        }

        #endregion

        #region TIPOPREGUNTA
        public ObtenerTipoPreguntaResult ObtenerTipoPregunta(ObtenerTipoPreguntaRequest request)
        {
            using (var db = new panelEntities())
            {
                var TipoPregunta = db.tbl_aSINTO_TIPOPREGUNTA.Where(p => p.ESTADO == request.Estado).FirstOrDefault();
                var Resultado = new ObtenerTipoPreguntaResult();

                if (TipoPregunta != null)
                {
                    Resultado.CodigoTipoPregunta = TipoPregunta.CODIGOTIPOPREGUNTA;
                    Resultado.DescripcionPregunta = TipoPregunta.DESCRIPCION;
                }

                return Resultado;
            }
        }
        #endregion

        #region PREGUNTA
        public ObtenerPreguntaResult ObtenerPregunta(ObtenerPreguntaRequest request)
        {
            using (var db = new panelEntities())
            {
                var Pregunta = db.tbl_aSINTO_PREGUNTA.Where(p => p.CODIGOGRUPO == request.CodigoGrupoPregunta && p.CODIGOTIPOPREGUNTA == request.CodigoTipoPregunta).FirstOrDefault();
                var result = new ObtenerPreguntaResult();

                if (Pregunta != null)
                {
                    result.CodigoPregunta = Pregunta.CODIGOPREGUNTA;
                    result.Pregunta = Pregunta.PREGUNTA;
                    result.Estado = Pregunta.ESTADO;
                    result.Orden = Pregunta.ORDEN.Value;
                    result.Obligatorio = Pregunta.OBLIGATORIO.Value;
                    result.Dependiente = Pregunta.DEPENDIENTE.Value;

                }

                return result;
            }

        }
        #endregion

        #region OPCION
        public ObtenerOpcionesResult ObtenerOpciones(ObtenerOpcionesRequest request)
        {
            using (var db = new panelEntities())
            {
                var Opcion = db.tbl_aSINTO_OPCIONES.Where(p => p.CODIGOPREGUNTA == request.CodigoPregunta).FirstOrDefault();
                var result = new ObtenerOpcionesResult();

                if (Opcion != null)
                {
                    result.CodigoOpcion = Opcion.CODIGOOPCION;
                    result.Opcion = Opcion.OPCION;
                    result.Estado = Opcion.ESTADO;
                    result.Imagen = Opcion.IMAGEN;
                    result.Orden = Opcion.ORDEN.Value;
                    result.Flagotro = Opcion.FLAGOTRO.Value;
                    result.CodigoPreguntaMostrar = Opcion.CODIGOPREGUNTA_MOSTRAR.Value;
                }

                return result;
            }

        }
        #endregion

        #region RESPUESTA
        public ObtenerRespuestaResult ObtenerRespuesta(ObtenerRespuestaRequest request)
        {
            using (var db = new panelEntities())
            {
                var Respuesta = db.tbl_aSINTO_RESPUESTA.Where(p => p.CODREGISTRO == request.CodigoRegistro && p.CODIGOGRUPO == request.CodigoOpcion && p.CODIGOPREGUNTA == request.CodigoPregunta).FirstOrDefault();
                var result = new ObtenerRespuestaResult();

                if (Respuesta != null)
                {
                    result.CodigoRespuesta = Respuesta.CODGIORESPUESTA;
                }

                return result;
            }

        }

        public GuardarRespuestasResponse GuardarRespuesta(GuardarRespuestasRequest request)
        {
            var result = new GuardarRespuestasResponse();

            var data = new GuardarRespuestasResult();

            using (var db = new panelEntities())
            {
                try
                {
                    /*GUARDAR COLABORADOR*/
                    var colaborador = db.tbl_mSINTO_COLABORADOR.Where(p => p.DOCUMENTO == request.Documento).FirstOrDefault();
                    var codigoColaborador = 0;
                    if (colaborador == null)
                    {

                        var nuevoColaborador = new tbl_mSINTO_COLABORADOR
                        {
                            CODCOLABORADOR = 0,
                            NOMBRESAPELLIDOS = request.Nombres,
                            DOCUMENTO = request.Documento,
                            CELULAR = request.Celular,
                            ESCOLABORADOR = false,
                            ESTADO = 1,
                            ANEXO = request.Anexo,
                            PUESTO = "",
                            AREA = "",
                            CORREO = "",
                            USUARIOWINDOWS = ""
                        };

                        db.tbl_mSINTO_COLABORADOR.Add(nuevoColaborador);
                        db.SaveChanges();

                        codigoColaborador = nuevoColaborador.CODAUTOGENERADO;
                    }
                    else {
                        colaborador.CELULAR = request.Celular;
                        db.SaveChanges();
                        codigoColaborador = colaborador.CODAUTOGENERADO;
                    }


                    var nuevo = new tbl_aSINTO_REGISTRO
                    {
                        CELULAR = request.Celular,
                        ANEXO = request.Anexo,
                        CHECKTERMINOS = request.ChckTerminos,
                        ESTADO = 1,
                        FECHAREGISTRO = DateTime.Now,
                        FECHA = DateTime.Now,
                        CODAUTOGENERADO = codigoColaborador,
                        CODIGOFORMULARIO = 1,
                        ESCOLABORADOR = true ,
                        PROCESADO = 0,
                        CODIGOMENSAJE = request.CodigoMensaje
                    };

                    db.tbl_aSINTO_REGISTRO.Add(nuevo);
                    db.SaveChanges();



                    foreach (var item in request.ListarPreguntas)
                    {
                        
                            var nuevo2 = new tbl_aSINTO_RESPUESTA
                            {
                                CODREGISTRO = nuevo.CODREGISTRO,
                                CODIGOGRUPO = item.CodigoGrupo,
                                CODIGOPREGUNTA = item.CodigoPregunta,
                                TEXTO_OTRO = item.TextoOtro,
                                ESTADO = item.Estado,
                                OPCION = item.Opcion,
                                OPCION2 = item.Opcion2
                            };

                            db.tbl_aSINTO_RESPUESTA.Add(nuevo2);
                            db.SaveChanges();
                        
                        
                    }


                    var nuevoProceo = new tbl_aSINTO_PROCESO
                    {
                        CODIGOREGISTRO = nuevo.CODREGISTRO,
                        FECHAREGISTRO = DateTime.Now,
                        PROCESADO = true
                    };

                    db.tbl_aSINTO_PROCESO.Add(nuevoProceo);
                    db.SaveChanges();


                    //db.SP_PROCESAR_SINTOMATOLOGIA(nuevo.CODREGISTRO);
                    data.CodigoRegistro = nuevoProceo.CODIGOREGISTRO;
                    data.MensajeHtml = db.tbl_aSINTO_MENSAJES.Where(p => p.CODIGOMENSAJE == request.CodigoMensaje).FirstOrDefault().MENSAJEHTML;

                    result.Result = true;
                    result.Data = data.CodigoRegistro.ToString();
                    result.MensajeHtml = data.MensajeHtml;

                    return result;
                }

                catch (Exception ex)
                {
                    result.Mensaje = ex.StackTrace.ToString() + "===============================" + ex.InnerException.ToString();
                    return result;
                }

            }

        }

        #endregion

        #region REGISTRO
        public ObtenerRegistroResult ObtenerRegistro(ObtenerRegistroRequest request)
        {
            using (var db = new panelEntities())
            {
                var Registro = db.tbl_aSINTO_REGISTRO.Where(p => p.CODAUTOGENERADO == request.CodigoAuto && p.CODIGOFORMULARIO == request.CodigoFormulario).FirstOrDefault();
                var result = new ObtenerRegistroResult();

                if (Registro != null)
                {
                    result.CodigoRegistro = Registro.CODREGISTRO;
                    result.Puesto = Registro.PUESTO;
                    result.Area = Registro.AREA;
                    result.Celular = Registro.CELULAR;                   
                    result.CheckTerminos = Registro.CHECKTERMINOS.Value;
                    result.FechaRegistro = Registro.FECHAREGISTRO.Value;
                    result.Escolaborador = Registro.ESCOLABORADOR.Value;
                    result.Estado = Registro.ESTADO.Value;

                }

                return result;
            }

        }
        #endregion

        #region COLABORADOR
        public ObtenerColaboradorResult ObtenerColaborador(ObtenerColaboradorRequest request)
        {
            using (var db = new panelEntities())
            {
                var Colaborador = db.tbl_mSINTO_COLABORADOR.Where(p => p.CODAUTOGENERADO == request.CodigoAuto).FirstOrDefault();
                var result = new ObtenerColaboradorResult();

                if (Colaborador != null)
                {
                    result.CodColaborador = Colaborador.CODCOLABORADOR;
                    result.NombresApellidos = Colaborador.NOMBRESAPELLIDOS;
                    result.Documento = Colaborador.DOCUMENTO;
                    result.Puesto = Colaborador.PUESTO;
                    result.Area = Colaborador.AREA;
                    result.Celular = Colaborador.CELULAR;
                    result.Correo = Colaborador.CORREO;
                    result.UsuarioWindows = Colaborador.USUARIOWINDOWS;
                    result.EsColaborador = Colaborador.ESCOLABORADOR.Value;
                    result.Estado = Colaborador.ESTADO.Value;
                    result.Anexo = Colaborador.ANEXO.Value;

                }

                return result;
            }

        }

        public RegistrarColaboradorResult RegistrarColaborador(RegistrarColaboradorRequest request)
        {
            using (var db = new panelEntities())
            {
                var result = new RegistrarColaboradorResult();
                var Colaborador = new tbl_mSINTO_COLABORADOR();
                try
                {


                    Colaborador.CODCOLABORADOR = request.CodColaborador;
                    Colaborador.NOMBRESAPELLIDOS = request.NombresApellidos;
                    Colaborador.DOCUMENTO = request.Documento;
                    Colaborador.PUESTO = request.Puesto;
                    Colaborador.AREA = request.Area;
                    Colaborador.CELULAR = request.Celular;
                    Colaborador.CORREO = request.Correo;
                    Colaborador.USUARIOWINDOWS = request.UsuarioWindows;
                    Colaborador.ESCOLABORADOR = request.EsColaborador;
                    Colaborador.ESTADO = request.Estado;

                    db.tbl_mSINTO_COLABORADOR.Add(Colaborador);
                    db.SaveChanges();

                    result.CodAutogenerado = Colaborador.CODAUTOGENERADO;
                }
                catch (Exception ex)
                {
                    result.CodAutogenerado = 0;
                }

                return result;
            }
        }

        public ObtenerNombreResult ObtenerNombre(ObtenerNombreRequest request)
        {
            using (var db = new panelEntities())
            {
                var cliente = db.tbl_mSINTO_COLABORADOR.Where(p => p.CODAUTOGENERADO == request.CodigoAuto).FirstOrDefault();
                var result = new ObtenerNombreResult();

                if (cliente != null)
                {
                    result.NombresApellidos = cliente.NOMBRESAPELLIDOS;
                }

                return result;
            }

        }

        #endregion

        #region REPORTE
        public int ProcesarRegistros() {
            using (var db = new panelEntities()) {
                var registros = db.tbl_aSINTO_REGISTRO.Where(p => p.PROCESADO == 0).ToList();
                foreach (var item in registros) {
                    db.SP_PROCESAR_SINTOMATOLOGIA(item.CODREGISTRO);
                    item.PROCESADO = 1;
                    db.SaveChanges();
                }
                return registros.Count;
            }
        }
        #endregion

        #region FECHAREGISTRO
        public ObtenerFechaRegistroResult ObtenerFechaRegistro(ObtenerFechaRegistroRequest request)
        {
            using (var db = new panelEntities())
            {
                var f1 = DateTime.Now.Date.ToString("yyyy-MM-dd");

                var result = new ObtenerFechaRegistroResult();

                var colaborador = db.tbl_mSINTO_COLABORADOR.Where(p => p.CODAUTOGENERADO == request.CodigoAuto).FirstOrDefault();


                if (colaborador != null)
                {
                    result.NombreColaborador = colaborador.NOMBRESAPELLIDOS;
                }

                var listaRegistro = db.tbl_aSINTO_REGISTRO.Where(p => p.CODAUTOGENERADO == request.CodigoAuto && p.FECHA.ToString() == f1).ToList();

                if (listaRegistro.Count > 0)
                {
                    result.CodigoRegistro = listaRegistro.OrderByDescending(p => p.CODREGISTRO).First().CODREGISTRO;
                    var CODMENSAJE= listaRegistro.Where(p => p.CODREGISTRO== result.CodigoRegistro).First().CODIGOMENSAJE;
                    result.MensajeHtml = db.tbl_aSINTO_MENSAJES.Where(x => x.CODIGOMENSAJE == CODMENSAJE).First().MENSAJEHTML;
                }

               
               result.ContadorFechaRegistro = listaRegistro.Count();
                


                return result;
            }

        }

        #endregion

    }
}
