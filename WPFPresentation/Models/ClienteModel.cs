using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Domain_Layer.BusinessRules;

namespace WPFPresentation.Models
{
    public class ClienteModel: BaseModel, IDataErrorInfo
    {
        private bool _dirtyNombre;
        private bool _dirtyApellidos;
        private bool _dirtyFechaNacimiento;
        private bool _dirtyNumeroDocumento;
        private bool _dirtyDireccion;
        private bool _dirtyTelefono;

        public int ClienteId { get; set; }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }

            set { if (_nombre != value) {_nombre = value; OnPropertyChanged("Nombre");_dirtyNombre = true;}}
        }


        private string _apellidos;

        public string Apellidos
        {
            get { return _apellidos; }
            set
            {
                if (_apellidos != value){ _apellidos = value; OnPropertyChanged("Apellidos");_dirtyApellidos = true;}
            }
        }

        private DateTime _fechaNacimiento;

        public DateTime FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set
            {
                if (_fechaNacimiento != value) { _fechaNacimiento = value; OnPropertyChanged("FechaNacimiento"); _dirtyFechaNacimiento = true; }
            }
        }



        private string _numeroDocumento;

        public string NumeroDocumento
        {
            get { return _numeroDocumento; }
            set { if (_numeroDocumento != value) { _numeroDocumento = value; OnPropertyChanged("NumeroDocumento");_dirtyNumeroDocumento = true;}}
        }

        private string _direccion;

        public string Direccion
        {
            get { return _direccion; }
            set { if (_direccion != value) { _direccion = value; OnPropertyChanged("Direccion"); _dirtyDireccion = true; } }
        }



        private string _telefono;

        public string Telefono
        {
            get { return _telefono; }
            set { if (_telefono != value) { _telefono = value; OnPropertyChanged("Telefono"); _dirtyTelefono = true; } }
        }


        private ObservableCollection<VentaModel> _ventas;

        public ObservableCollection<VentaModel> Ventas
        {
            get { return _ventas; }
            set { if (_ventas != value) { _ventas = value; OnPropertyChanged("Ventas"); } }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Nombre")
                {
                    var validationErrors = GetPropertyValidationsError(columnName);
                    return _dirtyNombre & !string.IsNullOrEmpty(validationErrors) ? validationErrors : null;
                }

                if (columnName == "Apellidos")
                {
                    var validationErrors = GetPropertyValidationsError(columnName);
                    return _dirtyApellidos & !string.IsNullOrEmpty(validationErrors) ? validationErrors : null;
                }

                if (columnName == "NumeroDocumento")
                {
                    var validationErrors = GetPropertyValidationsError(columnName);
                    return _dirtyNumeroDocumento & !string.IsNullOrEmpty(validationErrors) ? validationErrors : null;
                }

                if (columnName == "Telefono")
                {
                    var validationErrors = GetPropertyValidationsError(columnName);
                    return _dirtyTelefono & !string.IsNullOrEmpty(validationErrors) ? validationErrors : null;
                }

                if (columnName == "FechaNacimiento")
                {
                    var validationErrors = GetPropertyValidationsError(columnName);
                    return _dirtyFechaNacimiento & !string.IsNullOrEmpty(validationErrors) ? validationErrors : null;
                }
                return null;
            }

        }

        public ClienteModel()
        {
            AddRule(new ValidateRequired("Nombre", "Este campo es requerido"));
            AddRule(new ValidateLength("Nombre", "El nombre es muy corto o muy largo", 5, 50));

            AddRule(new ValidateRequired("Apellidos", "Este campo es requerido"));
            AddRule(new ValidateLength("Apellidos", "El nombre es muy corto o muy largo", 5, 50));

            AddRule(new ValidateRequired("NumeroDocumento", "Este campo es requerido"));
            AddRule(new ValidateId("NumeroDocumento", "El valor debe ser numerico"));

            AddRule(new ValidateRequired("Telefono", "Este campo es requerido"));
            AddRule(new ValidateId("Telefono", "El valor debe ser numerico"));

            AddRule(new ValidateLength("Direccion", "El nombre  muy largo", 0, 100));

            FechaNacimiento = DateTime.Now;
            Direccion = "";
        }
    }
}
