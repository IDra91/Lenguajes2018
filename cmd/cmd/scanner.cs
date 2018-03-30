using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmd
{
    class scanner
    {
        char caracter;
        int estadoActual = 0;
        int total = 0;
        int cantidad = 0;
        String auxiliar = "";
        List<token> ListaT = new List<token>();
        List<error> ListaER = new List<error>();


        //Aquí está programado todo el autómata
        public void Analizador(string cadena)
        {

            for (int i = 0; i < cadena.Length; i++)
            {
                caracter = cadena[i];
                // Esto lo que hace es que separa la cadena que necesitás en caracteres
                switch (estadoActual)
                {
                    case 0:
                        if (CaracterMayuscula(caracter))
                        {
                            estadoActual = 1;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena ----> " + auxiliar);
                        }
                        else if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 111;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena ----> " + auxiliar);
                        }
                        else
                        {
                            //ERRORES DE RECEPCION
                            estadoActual = 9999;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena ----> " + auxiliar);
                        }
                        break;
                    case 1:
                        //AQUÍ VAN LOS COMANDOS QUE COMIENZAN CON MAYÚSCULAS
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 1;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena ----> " + auxiliar);
                        }
                        else if (Espacio(caracter))
                        {
                            //SE HACE LA COMPARATIVA DE LOS COMANDOS QUE USAN MAYÚSCULAS O MINÚSCULAS Y SE VA A UN RESPECTIVO ESTADO
                            if (auxiliar == "CrearCarpeta")
                            {
                                //SE VA A UN ESTADO PARA RECIVIR EL NOMBRE DE LA CARPETA
                                AgregarALista(auxiliar, "Reservada CrearCarpeta", 11);
                            }
                            else if (auxiliar == "AbrirC")
                            {
                                //SE VA A UN ESTADO PARA MOSTRAR EL CONTENIDO DE LA CARPETA
                                AgregarALista(auxiliar, "Reservada AbrirCarpeta", 22);
                            }
                            else if (auxiliar == "CrearArchivo")
                            {
                                //SE VA A UN ESTADO PARA RECIBIR EL NOMBRE DEL ARCHIVO
                                AgregarALista(auxiliar, "Reservada CrearArchivo", 33);
                            }
                            else if (auxiliar == "Ejecutar")
                            {
                                //SE VA A UN ESTADO PARA EJECUTAR EL ARCHIVO CREADO
                                AgregarALista(auxiliar, "Reservada EJECUTAR", 44);
                            }
                            else
                            {
                                AgregarAErrores(auxiliar, "Reservada inexistente");
                            }
                        }
                        else if (Saltos(caracter))
                        {
                             if (auxiliar == "ManualUsuario")
                        {
                            // SE REGRESA AL INICIO Y ABRE EL MANUAL DE USUARIO
                            AgregarALista(auxiliar, "Reservada MANUAL_DE_USUARIO", 0);
                        }
                        else if (auxiliar == "ManualTecnico")
                        {
                            // SE REGRESA AL INICIO Y ABRE EL MANUAL TÉCNICO
                            AgregarALista(auxiliar, "Reservada Manual_Tecnico", 0);
                        }
                        else if (auxiliar == "Reportes")
                        {
                            // SE REGRESA AL INICIO Y ABRE EL MANUAL TÉCNICO
                            AgregarALista(auxiliar, "Reportes", 0);

                        }
                        }
                        else
                        {
                            estadoActual = 111111;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena ----> " + auxiliar);
                        }
                        break;
                    case 2: 
                        //AQUÍ VAN LOS COMANDOS QUE COMIENZAN CON MINÚSCULAS
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 2;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena ----> " + auxiliar);
                        }
                        else if (Espacio(caracter))
                        {
                            if (auxiliar == "renombrarC")
                            {
                                //AQUÍ SE VA A UN ESTADO, EN EL QUE SE RECIBIRÁ EL NOMBRE VIEJO DE LA CARPETA Y EL NUEVO
                                AgregarALista(auxiliar, "Reservada RenombrarCarpeta", 55);
                            }
                            else if (auxiliar == "regresar")
                            {
                                //AQUÍ SE REGRESA AL INICIO Y SE REDIRECCIONA A LA CARPETA C:
                                AgregarALista(auxiliar, "Reservada Regresar", 0);
                            }
                            else if (auxiliar == "eliminarC")
                            {
                                //AQUÍ SE VA A UN ESTADO, EN EL QUE SE RECIBIRÁ EL NOMBRE DE UNA CARPETA Y SE ELIMINARÁ
                                AgregarALista(auxiliar, "Reservada C", 66);
                            }
                            else if (auxiliar == "abrirArchivo")
                            {
                                //AQUÍ SE VA A UN ESTADO, EN EL QUE RECIBIRÁ EL NOMBRE DE UN ARCHIVO Y SE ABRIRÁ
                                AgregarALista(auxiliar, "Reservada AbrirArchivo", 77);
                            }
                            else if (auxiliar == "renombrarA")
                            {
                                //AQUÍ SE VA A UN ESTADO, RECIBIRÁ EL NOMBRE VIEJO DEL ARCHIVO Y SE RECIBIRÁ SU NUEVO NOMBRE
                                AgregarALista(auxiliar, "Reservada renombrarA", 88);
                            }
                            else if (auxiliar == "mover")
                            {
                                //AQUÍ SE VA A UN ESTADO, RECIBIRÁ EL ARCHIVO Y EL NOMBRE DEL ARCHIVO
                                AgregarALista(auxiliar, "Reservada mover", 99);
                            }
                            else if (auxiliar == "copiar")
                            {
                                //AQUÍ SE VA A UN ESTADO, RECIBIRA EL COMANDO ARCHIVO Y RECIBIRÁ SU NUEVA RUTA
                                AgregarALista(auxiliar, "Reservada copiar", 111);
                            }
                            else if (auxiliar == "eliminarA")
                            {
                                //AQUÍ SE VA A UN ESTADO, RECIBIRÁ EL NOMBRE DEL ARCHIVO Y LO ELIMINARÁ
                                AgregarALista(auxiliar, "Reservada Eliminar_Archivo", 222);
                            }
                            else if (auxiliar == "cargar")
                            {
                                //AQUÍ SE VA A UN ESTADO, RECIBIRÁ LA DIRECCIÓN Y EL NOMBRE DEL ARCHIVO
                                AgregarALista(auxiliar, "Reservada Cargar", 333);
                            }
                            else
                            {
                                AgregarAErrores(auxiliar, "Reservada inexistente");
                            }
                        }
                        else if (Saltos(caracter))
                        {
                            if (auxiliar == "acercaDe")
                            {
                                Console.WriteLine("Hecho por Manuel Rivera, 201212747, Lenguajes Formales");
                                AgregarALista(auxiliar, "Reservada Acerca_De", 0);
                            }
                            else
                            {
                                AgregarAErrores(auxiliar, "Reservada, inexistente");
                            }
                        }
                        else
                        {
                            estadoActual = 222222;
                            auxiliar += caracter; 
                        }
                        break;
                    case 11:


                        break;
                    case 22:

                        break;
                    case 33:

                        break;
                    case 44:

                        break;
                    case 55:

                        break;
                    case 66:

                        break;
                    case 77:

                        break;
                    case 88:

                        break;
                    case 99: 
                        
                        break;
                    case 111:

                        break;
                    case 222:

                        break;
                    case 333:

                        break; 

                }
            }

        }



     // METDODO DE AGREGAR CARPETAS




        //Agregar a Lista de errores, limpiar y luego regresar al estado 0
        public void AgregarAErrores(string lexema, string tipo)
        {

            ListaER.Add(new error(lexema, tipo));

            auxiliar = "";
            estadoActual = 0;
        }

        //Agregar a Lista, limpiar y luego regresar al estado 0
        public void AgregarALista(string lexema, string tipo, int estado)
        {

            ListaT.Add(new token(lexema, tipo));
            auxiliar = "";
            estadoActual = estado;
            Console.WriteLine(ListaT[0].Lexema);

        }



        //Todos estos métodos son para reconocer un caracter en específico, valuándolos con su valor en ASCII
        public Boolean CaracterMinuscula(char valor)
        {
            if ((valor > 96) & (valor < 123))
            {
                return true;
            }
            else
                return false;
        }
        public Boolean CaracterMayuscula(char valor)
        {
            if ((valor > 64) & (valor < 123))
            {
                return true;
            }
            else
                return false;
        }

        public Boolean CaracterNumerico(char valor)
        {
            if ((valor > 47) & (valor < 58))
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Espacio(char valor)
        {
            if (valor == 32)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Saltos(char valor)
        {
            if (valor == 13)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean MenorQue(char valor)
        {
            if (valor == 60)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean MayorQue(char valor)
        {
            if (valor == 62)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Slash(char valor)
        {
            if (valor == 47)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Punto(char valor)
        {
            if (valor == 46)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Arroba(char valor)
        {
            if (valor == 64)
            {
                return true;
            }
            else
                return false;
        }
        public Boolean CierraI(char valor)
        {
            if (valor == 63)
            {
                return true;
            }
            else
                return false;
        }
        public Boolean Coma(char valor)
        {
            if (valor == 44)
            {
                return true;
            }
            else
                return false;
        }
        public Boolean SubRaya(char valor)
        {
            if (valor == 95)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Suma(char valor)
        {
            if (valor == 43)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Resta(char valor)
        {
            if (valor == 45)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Asterisco(char valor)
        {
            if (valor == 42)
            {
                return true;
            }
            else
                return false;
        }
    }
}
