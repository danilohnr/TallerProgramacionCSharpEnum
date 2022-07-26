﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IUFormulario
{
    public partial class FrmMainTaller : Form
    {
        //Variable global que podrá acceder a los métodos definidos en el formulario
        LogicaNegocio.Cajero _objCajero = new LogicaNegocio.Cajero();
        //Variable global de la lista de personas para evitar que se pierda la información
        List<Modelo.Persona> _listaPersonas = new List<Modelo.Persona>();
        //Cadena para usar como separador de los datos en la lista de personas
        private const string SEPARADOR = "--";
        public FrmMainTaller()
        {
            InitializeComponent();
        }

        private void BtnConsulta_Click(object sender, EventArgs e)
        {
            //LogicaNegocio.Cajero objCajero = new LogicaNegocio.Cajero(20000);
            //LogicaNegocio.Cajero objCajero = new LogicaNegocio.Cajero();
            TxtConsulta.Text = _objCajero.DineroActual.ToString();
        }

        private void DividirCero() 
        {
            int cero = 0;
            decimal error = 8 / cero;
        }

        private string RetirarDinero()
        {
            //Recuperar el valor del cuadro de texto TxtRetiro
            string montoRetiro = TxtRetiro.Text;
            //Hacer una instancia de la clase Cajero
            //LogicaNegocio.Cajero objCajero = new LogicaNegocio.Cajero();
            //string mensajeRetornado = "";
            string mensajeRetornado = string.Empty;
            decimal valorRetornado = 0; //Para guardar lo que devuelva el método RetirarDinero
            valorRetornado = _objCajero.RetirarDinero(Convert.ToDecimal(montoRetiro), ref mensajeRetornado);
            //Mostrar en el cuadro de texto TxtConsulta lo que queda disponible
            TxtConsulta.Text = valorRetornado.ToString();
            TxtMensajeCajero.Text = mensajeRetornado;
            return mensajeRetornado;
        }

        private void BtnRetiro_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    DividirCero();
                }
                catch (DivideByZeroException ex)
                {
                    TxtMensajeCajero.Text = ex.Message;
                }
                //Capturar el mensaje del método RetirarDinero()
                string mensajeCajero = string.Empty;
                mensajeCajero = RetirarDinero();
                //1.Recuperar fecha
                DateTime fechaTransaccion = DateTime.Now;
                //2.Crear objeto persona
                //Modelo.Persona objPersona = new Modelo.Persona();
                //objPersona.Identificacion = TxtIdentificacion.Text;
                //Y aquí seguiría la captura del resto de los datos del usuario
                //Pero también se puede hacer así:
                Modelo.Persona objPersona = new Modelo.Persona {
                    Identificacion = TxtIdentificacion.Text,
                    Nombre = TxtNombre.Text,
                    PrimerApellido = TxtApellido1.Text,
                    SegundoApellido = TxtApellido2.Text,
                    FechaTransaccion = fechaTransaccion,
                    MontoTransaccion = Convert.ToDecimal(TxtRetiro.Text),
                    Mensaje = mensajeCajero
                };
                //3.Agregar el objeto Persona a una lista de personas                
                _listaPersonas.Add(objPersona);
                //4.Agregar esa lista de personas al campo de texto TxtListaPersonas
                TxtListaPersonas.Text = "";
                foreach (var item in _listaPersonas)
                {
                    TxtListaPersonas.Text += item.Identificacion + SEPARADOR;
                    TxtListaPersonas.Text += item.FechaTransaccion + SEPARADOR;
                    TxtListaPersonas.Text += item.Nombre + " " + item.PrimerApellido + " " + item.SegundoApellido + SEPARADOR;
                    TxtListaPersonas.Text += item.MontoTransaccion + SEPARADOR;
                    TxtListaPersonas.Text += item.Mensaje + SEPARADOR;
                    TxtListaPersonas.Text += Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                TxtMensajeCajero.Text = ex.Message;
            }
        }

        private void BtnArray_Click(object sender, EventArgs e)
        {
            string[] resultadoLista;
            string mensaje = string.Empty;
            resultadoLista = LogicaNegocio.Lista.ListaArray();
            for (int i = 0; i < resultadoLista.Length; i++)
            {
                mensaje = mensaje + "--" + resultadoLista[i];
            }
            TxtListaArray.Text = mensaje;
        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            List<string> resultadoList = LogicaNegocio.Lista.ListaList();
            string mensaje = string.Empty;
            foreach (var objLista in resultadoList)
            {
                mensaje = mensaje + "--" + objLista.ToString();
            }
            TxtList.Text = mensaje;
        }
    }
}
