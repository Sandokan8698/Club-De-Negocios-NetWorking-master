using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.BusinessRules;
using System.ComponentModel;

namespace WPFPresentation.Models
{
    public class TrabajadorModel: BaseModel, IDataErrorInfo
    {
        private bool _dirtyNombre, _dirtyApellidos, _dirtyAcceso, _dirtyUsuario, _dirtyPassword;

        public int TrabajadorId { get; set; }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (_nombre != value) _nombre = value; OnPropertyChanged("Nombre");
                _dirtyNombre = true;
            }
        }

        private string _apellidos;

        public string Apellidos
        {
            get { return _apellidos; }
            set
            {
                if (_apellidos != value) _apellidos = value; OnPropertyChanged("Apellidos");
                _dirtyApellidos = true;
            }
        }


        private string _acceso;

        public string Acceso
        {
            get { return _acceso; }
            set
            {
                if (_acceso != value) _acceso = value; OnPropertyChanged("Acceso");
                _dirtyAcceso = true;
            }
        }


        private string _usuario;

        public string Usuario
        {
            get { return _usuario; }
            set
            {
                if (_usuario != value) _usuario = value; OnPropertyChanged("Usuario");
                _dirtyUsuario = true;
            }
        }


        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value) _password = value; OnPropertyChanged("Password");
                _dirtyPassword = true;
            }
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

                if (columnName == "Acceso")
                {
                    var validationErrors = GetPropertyValidationsError(columnName);
                    return _dirtyAcceso & !string.IsNullOrEmpty(validationErrors) ? validationErrors : null;
                }

                if (columnName == "Usuario")
                {
                    var validationErrors = GetPropertyValidationsError(columnName);
                    return _dirtyUsuario & !string.IsNullOrEmpty(validationErrors) ? validationErrors : null;
                }

                if (columnName == "Password")
                {
                    var validationErrors = GetPropertyValidationsError(columnName);
                    return _dirtyPassword & !string.IsNullOrEmpty(validationErrors) ? validationErrors : null;
                }
                return null;
            }

        }


        public TrabajadorModel()
        {
            AddRule(new ValidateRequired("Nombre", "Este campo es requerido"));
            AddRule(new ValidateLength("Nombre", "El nombre es muy corto o muy largo", 5, 20));

            AddRule(new ValidateRequired("Apellidos", "Este campo es requerido"));
            AddRule(new ValidateLength("Apellidos", "El nombre es muy corto o muy largo", 5, 40));

            AddRule(new ValidateRequired("Acceso", "Este campo es requerido"));
            AddRule(new ValidateLength("Acceso", "El nombre es muy corto o muy largo", 5, 20));

            AddRule(new ValidateRequired("Usuario", "Este campo es requerido"));
            AddRule(new ValidateLength("Usuario", "El nombre es muy corto o muy largo", 5, 20));

            AddRule(new ValidateRequired("Password", "Este campo es requerido"));
            AddRule(new ValidateLength("Password", "El nombre  muy largo", 0, 20));
           
           
        }
    }
}
