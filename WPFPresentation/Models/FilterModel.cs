using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPresentation.Models
{
    public class FilterModel: BaseModel
    {
        private ClienteModel _cliente;
        public ClienteModel Cliente
        {
            get { return _cliente; }
            set
            {
                if (_cliente != value)
                {
                    _cliente = value;
                    OnPropertyChanged("Cliente");
                }
            }
        }

        private DateTime _desde;
        public DateTime Desde
        {
            get { return _desde; }
            set
            {
                if (_desde != value)
                {
                    _desde = value;
                    OnPropertyChanged("Desde");
                }
            }
        }

        private DateTime _hasta;
        public DateTime Hasta
        {
            get { return _hasta; }
            set
            {
                if (_hasta != value)
                {
                    _hasta = value;
                    OnPropertyChanged("Hasta");
                }
            }
        }

        private int _ventaId;
        public int  VentaId
        {
            get { return _ventaId; }
            set {
                if (_ventaId != value)
                {
                    _ventaId = value;
                    OnPropertyChanged("VentaId");
                }
            }
        }

        private ProveedorModel _proveedor;
        public ProveedorModel Proveedor
        {
            get { return _proveedor; }
            set {
                if (_proveedor != value)
                {
                    _proveedor = value;
                    OnPropertyChanged("Proveedor");
                }
            }
        }

        private string _identificador;
        public string Identificador
        {
            get { return _identificador; }
            set {
                if (_identificador != value)
                {
                    _identificador = value;
                    OnPropertyChanged("Identificador");
                }
            }
        }

        private bool _usarFecha;
        public bool UsarFecha
        {
            get { return _usarFecha; }
            set
            {
                if (_usarFecha != value)
                {
                    _usarFecha = value;
                    OnPropertyChanged("UsarFecha");
                }
            }
        }

        public FilterModel()
        {
            Desde = DateTime.Now.AddMonths(-1);
            Hasta = DateTime.Now;
            UsarFecha = true;
        }

    }
}
