

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Domain_Layer.BusinessRules;
using Domain_Layer.Entities;

namespace WPFPresentation.Models
{
    [Serializable]
    public class ProveedorModel : BaseModel, IDataErrorInfo
    {
        private bool _dirtyNombre;
        private bool _dirtyEmail;
        private bool _dirtyUrl;
        private bool _dirtyDireccion;
        private bool _dirtyTelefono;
        private bool _dirtyNumeroCuenta;


        public int ProveedorId { get; set; }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { if (_nombre != value) { _nombre = value; OnPropertyChanged("Nombre");
                _dirtyNombre = true;
            } }
        }

        private string _direccion;

        public string Direccion
        {
            get { return _direccion; }
            set { if (_direccion != value) { _direccion = value; OnPropertyChanged("Direccion");
                _dirtyDireccion = true;
            } }
        }

        private string _telefono;
        public string Telefono
        {
            get { return _telefono; }
            set { if (_telefono != value) { _telefono = value; OnPropertyChanged("Telefono"); _dirtyTelefono = true; } }
        }


        private string _email;

        public string Email
        {
            get { return _email; }
            set { if (_email != value) { _email = value; OnPropertyChanged("Email"); _dirtyEmail = true; } }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set { if (_url != value) { _url = value; OnPropertyChanged("Url"); _dirtyUrl = true; } }
        }


        private string _numeroCuenta;
        public string NumeroCuenta
        {
            get { return _numeroCuenta; }
            set { if (_numeroCuenta != value) { _numeroCuenta = value; OnPropertyChanged("NumeroCuenta"); _dirtyNumeroCuenta = true; } }
        }

        public  ICollection<Pedido> Pedidos { get; set; }

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
                
                if (columnName == "Direccion")
                {
                    var validationErrors = GetPropertyValidationsError(columnName);
                    return _dirtyDireccion & !string.IsNullOrEmpty(validationErrors) ? validationErrors : null;
                }

                if (columnName == "Telefono")
                {
                    var validationErrors = GetPropertyValidationsError(columnName);
                    return _dirtyTelefono & !string.IsNullOrEmpty(validationErrors) ? validationErrors : null;
                }

                if (columnName == "Email")
                {
                    var validationErrors = GetPropertyValidationsError(columnName);
                    return _dirtyEmail & !string.IsNullOrEmpty(validationErrors) ? validationErrors : null;
                }

                if (columnName == "Url")
                {
                    var validationErrors = GetPropertyValidationsError(columnName);
                    return _dirtyUrl & !string.IsNullOrEmpty(validationErrors) ? validationErrors : null;
                }

                if (columnName == "NumeroCuenta")
                {
                    var validationErrors = GetPropertyValidationsError(columnName);
                    return _dirtyNumeroCuenta & !string.IsNullOrEmpty(validationErrors) ? validationErrors : null;
                }
                return null;
            }

        }

        public ProveedorModel()
        {
            AddRule(new ValidateRequired("Nombre", "Este campo es requerido"));
            AddRule(new ValidateLength("Nombre", "El nombre es muy corto o muy largo", 1, 50));

            AddRule(new ValidateRequired("NumeroCuenta", "Este campo es requerido"));

        }

    }
}
