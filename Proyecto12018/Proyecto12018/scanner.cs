using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Proyecto12018
{
    class scanner
    {
        char caracter;
        int estadoActual = 0;
        int total = 0;
        int cantidad = 0;
        String auxiliar = "";
        String nombreV = "";
        String nombreN = "";
        String direccion = "";
        List<token> ListaT = new List<token>();
        List<error> ListaER = new List<error>();
        string raiz = @"C:\Users\manu9\Desktop";

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
                            //ERRORES DE RECEPCION ✓✓
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
                                //SE VA A UN ESTADO PARA RECIVIR EL NOMBRE DE LA CARPETA ✓✓
                                AgregarALista(auxiliar, "Reservada CrearCarpeta", 11);
                            }
                            else if (auxiliar == "AbrirC")
                            {

                                AgregarALista(auxiliar, "Reservada AbrirCarpeta", 0);
                                //EJECUTAR COMANDO PARA MOSTRAR EN PANTALLA LO QUE TIENE DICHA CARPETA (BUSCAR)
                            }
                            else if (auxiliar == "CrearArchivo")
                            {
                                //SE VA A UN ESTADO PARA RECIBIR EL NOMBRE DEL ARCHIVO  ✓✓
                                AgregarALista(auxiliar, "Reservada CrearArchivo", 22);
                            }
                            else if (auxiliar == "Ejecutar")
                            {
                                //ABRE EL ARCHIVO Y LO MUESTRA EN PANTALLA (BUSCAR)
                                AgregarALista(auxiliar, "Reservada EJECUTAR", 0);
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
                                // SE REGRESA AL INICIO Y ABRE EL MANUAL DE USUARIO (ABRIR ARCHIVO POR APARTE)
                                AgregarALista(auxiliar, "Reservada MANUAL_DE_USUARIO", 0);
                            }
                            else if (auxiliar == "ManualTecnico")
                            {
                                // SE REGRESA AL INICIO Y ABRE EL MANUAL TÉCNICO (ABRIR ARCHIVO POR APARTE)
                                AgregarALista(auxiliar, "Reservada Manual_Tecnico", 0);
                            }
                            else if (auxiliar == "Reportes")
                            {
                                // SE REGRESA AL INICIO Y ABRE LOS REPORTES ✓✓
                                AgregarALista(auxiliar, "Reportes", 0);
                                this.generarHTML();
                            }
                        }
                        else
                        {
                            // ERRORES DE RECEPCIÓN ✓✓
                            estadoActual = 9999;
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
                                //AQUÍ SE VA A UN ESTADO, EN EL QUE SE RECIBIRÁ EL NOMBRE VIEJO DE LA CARPETA Y EL NUEVO ✓✓
                                AgregarALista(auxiliar, "Reservada RenombrarCarpeta", 33);
                            }
                            else if (auxiliar == "regresar")
                            {
                                //AQUÍ SE REGRESA AL INICIO Y SE REDIRECCIONA A LA CARPETA C:
                                AgregarALista(auxiliar, "Reservada Regresar", 0);
                            }
                            else if (auxiliar == "eliminarC")
                            {
                                //AQUÍ SE VA A UN ESTADO, EN EL QUE SE RECIBIRÁ EL NOMBRE DE UNA CARPETA Y SE ELIMINARÁ ✓✓
                                AgregarALista(auxiliar, "Reservada C", 44);
                            }
                            else if (auxiliar == "abrirArchivo")
                            {
                                //AQUÍ SE VA A UN ESTADO, EN EL QUE RECIBIRÁ EL NOMBRE DE UN ARCHIVO Y SE ABRIRÁ ✓
                                AgregarALista(auxiliar, "Reservada AbrirArchivo", 55);
                            }
                            else if (auxiliar == "renombrarA")
                            {
                                //AQUÍ SE VA A UN ESTADO, RECIBIRÁ EL NOMBRE VIEJO DEL ARCHIVO Y SE RECIBIRÁ SU NUEVO NOMBRE ✓
                                AgregarALista(auxiliar, "Reservada renombrarA", 66);
                            }
                            else if (auxiliar == "mover")
                            {
                                //AQUÍ SE VA A UN ESTADO, RECIBIRÁ EL ARCHIVO Y EL NOMBRE DEL ARCHIVO ✓
                                AgregarALista(auxiliar, "Reservada mover", 77);
                            }
                            else if (auxiliar == "copiar")
                            {
                                //AQUÍ SE VA A UN ESTADO, RECIBIRA EL COMANDO ARCHIVO Y RECIBIRÁ SU NUEVA RUTA ✓
                                AgregarALista(auxiliar, "Reservada copiar", 88);
                            }
                            else if (auxiliar == "eliminarA")
                            {
                                //AQUÍ SE VA A UN ESTADO, RECIBIRÁ EL NOMBRE DEL ARCHIVO Y LO ELIMINARÁ ✓
                                AgregarALista(auxiliar, "Reservada Eliminar_Archivo", 222);
                            }
                            else if (auxiliar == "cargar")
                            {
                                //AQUÍ SE VA A UN ESTADO, RECIBIRÁ LA DIRECCIÓN Y EL NOMBRE DEL ARCHIVO ✓
                                AgregarALista(auxiliar, "Reservada Cargar", 111);
                            }
                            else
                            {
                                AgregarAErrores(auxiliar, "Reservada Desconocida");
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
                            // ERRORES DE RESERVADAS ✓✓
                            estadoActual = 9999;
                            auxiliar += caracter;
                        }
                        break;
                    case 11:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 12;
                            auxiliar += caracter;
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 11;
                        }
                        else
                        {
                            // ERRORES DE NOMBRES ✓✓
                            estadoActual = 8888;
                            auxiliar += caracter;
                        }

                        break;
                    case 12:
                        if (Saltos(caracter))
                        {
                            CrearCarpeta(auxiliar);
                            AgregarALista(auxiliar, "Nombre", 0);
                            //MÉTODO PARA PODER CREAR CARPETAS, BUSCARLO
                           
                        }
                        else
                        {
                            estadoActual = 12;
                            auxiliar += caracter;
                        }
                        break;
                    case 22:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 23;
                            auxiliar += caracter;
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 22;
                        }
                        else
                        {
                            estadoActual = 22;
                            auxiliar += caracter;
                        }
                        break;
                    case 23:
                        if (Saltos(caracter))
                        {
                            CrearArchivo(auxiliar);
                            AgregarALista(auxiliar, "Nombre", 0);
                            //MÉTODO PARA PODER CREAR ARCHIVOS, BUSCARLO
                            MessageBox.Show("Se ha creado el archivo :D");
                        }
                        else
                        {
                            estadoActual = 23;
                            auxiliar += caracter;
                        }
                        break;
                    case 33:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 34;
                            nombreV += caracter;
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 33;
                        }
                        else
                        {
                            // ERRORES DE NOMBRES ✓✓
                            estadoActual = 8888;
                            nombreV += caracter;
                        }
                        break;
                    case 34:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 34;
                            nombreV += caracter;
                        }
                        else if (Espacio(caracter))
                        {
                            AgregarALista(nombreV, "Nombre", 35);
                        }
                        else
                        {
                            estadoActual = 34;
                            nombreV += caracter;
                        }
                        break;
                    case 35:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 36;
                            nombreN += caracter;
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 35;
                        }
                        else
                        {
                            estadoActual = 35;
                            nombreN += caracter;
                        }
                        break;
                    case 36:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 36;
                            nombreN += caracter;
                        }
                        else if (Espacio(caracter))
                        {
                            AgregarALista(nombreN, "Nombre", 0);
                            //EJECUTAR EL MÈTODO DE RENOMBRE DE CARPETAS
                            RenombrarCarpeta(nombreV, nombreN);
                            MessageBox.Show("Se ha renombrado :D");
                            nombreN = "";
                            nombreV = "";
                        }
                        else
                        {
                            estadoActual = 36;
                            nombreN += caracter;
                        }
                        break;
                    case 44:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 45;
                            auxiliar += caracter;
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 44;
                        }
                        else
                        {
                            estadoActual = 44;
                            auxiliar += caracter;
                        }
                        break;
                    case 45:
                        if (Saltos(caracter))
                        {
                            EliminarCarpeta(auxiliar);
                            AgregarALista(auxiliar, "Nombre", 0);
                            //MÉTODO PARA PODER ELIMINAR CARPETAS, BUSCARLO
                            MessageBox.Show("Se ha eliminado :D");
                        }
                        else
                        {
                            estadoActual = 45;
                            auxiliar += caracter;
                        }
                        break;
                    case 55:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 56;
                            auxiliar += caracter;
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 55;
                        }
                        else
                        {
                            estadoActual = 8888;
                            auxiliar += caracter;
                        }
                        break;
                    case 56:
                        if (Saltos(caracter))
                        {
                            AgregarALista(auxiliar, "Nombre", 0);
                            //MÉTODO PARA PODER ABRIR ARCHIVOS Y DESPLEGAR SU CONTENIDO EN CONSOLA, BUSCARLO
                        }
                        else
                        {
                            estadoActual = 56;
                            auxiliar += caracter;
                        }
                        break;
                    case 66:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 67;
                            nombreV += caracter;
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 66;
                        }
                        else
                        {
                            estadoActual = 66;
                            nombreV += caracter;
                        }
                        break;
                    case 67:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 67;
                            nombreV += caracter;
                        }
                        else if (Espacio(caracter))
                        {
                            AgregarALista(nombreV, "Nombre", 68);
                        }
                        else
                        {
                            estadoActual = 67;
                            nombreV += caracter;
                        }
                        break;
                    case 68:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 69;
                            nombreN += caracter;
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 68;
                        }
                        else
                        {
                            estadoActual = 68;
                            nombreN += caracter;
                        }
                        break;
                    case 69:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 69;
                            nombreN += caracter;
                        }
                        else if (Espacio(caracter))
                        {
                            AgregarALista(nombreN, "Nombre", 0);
                            //EJECUTAR EL MÈTODO DE RENOMBRE DE ARCHIVOS
                            MessageBox.Show("Se ha renombrado :D");
                            nombreN = "";
                            nombreV = "";
                        }
                        else
                        {
                            estadoActual = 69;
                            nombreN += caracter;
                        }
                        break;
                    case 77:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 78;
                            nombreV += caracter;
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 77;
                        }
                        else
                        {
                            estadoActual = 77;
                            nombreV += caracter;
                        }
                        break;
                    case 78:
                        if (Saltos(caracter))
                        {
                            AgregarALista(nombreV, "Nombre", 79);
                        }
                        else
                        {
                            estadoActual = 78;
                            auxiliar += caracter;
                        }
                        break;
                    case 79:
                        if (Comillas(caracter))
                        {
                            estadoActual = 80;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 79;

                        }
                        else
                        {
                            estadoActual = 222;
                            direccion += caracter;
                        } break;

                    case 80:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 81;
                            direccion += caracter;
                        }

                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 80;
                        }
                        else
                        {
                            estadoActual = 222;
                            direccion += caracter;
                        }
                        break;
                    case 81:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 81;
                            direccion += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 81;
                        }
                        else if (Slash(caracter))
                        {
                            estadoActual = 81;
                            direccion += "\\";
                        }
                        else if (Comillas(caracter))
                        {
                            estadoActual = 82;
                        }
                        else
                        {
                            estadoActual = 222;
                            direccion += caracter;
                        }
                        break;
                    case 82:
                        if (Saltos(caracter))
                        {
                            AgregarALista(direccion, "Direccion", 0);
                            //AGREGAR METODO PARA MOVER UN ARCHIVO 
                            MessageBox.Show("Se ha movido :D");
                            nombreV = "";
                            direccion = "";
                        }
                        else
                        {
                            estadoActual = 1111;
                            direccion += caracter;
                        }
                        break;
                    case 88:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 89;
                            nombreV += caracter;
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 88;
                        }
                        else
                        {
                            estadoActual = 88;
                            nombreV += caracter;
                        }
                        break;
                    case 89:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 89;
                            nombreV += caracter;
                        }
                        else if (Espacio(caracter))
                        {
                            AgregarALista(nombreV, "Nombre", 90);
                        }
                        else if (Saltos(caracter))
                        {
                            estadoActual = 89;
                        }
                        else
                        {
                            estadoActual = 89;
                            nombreV += caracter;
                        }
                        break;
                    case 90:
                        if (Comillas(caracter))
                        {
                            estadoActual = 91;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 90;

                        }
                        else
                        {
                            estadoActual = 222;
                            direccion += caracter;
                        } break;
                    case 91:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 92;
                            direccion += caracter;
                        }

                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 91;
                        }
                        else
                        {
                            estadoActual = 222;
                            direccion += caracter;
                        }
                        break;
                    case 92:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 92;
                            direccion += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 92;
                        }
                        else if (Slash(caracter))
                        {
                            estadoActual = 92;
                            direccion += "\\";
                        }
                        else if (Comillas(caracter))
                        {
                            estadoActual = 93;
                        }
                        else
                        {
                            estadoActual = 222;
                            direccion += caracter;
                        }
                        break;
                    case 93:
                        if (Saltos(caracter))
                        {
                            AgregarALista(direccion, "Direccion", 0);
                            //AGREGAR METODO PARA COPIAR UN ARCHIVO 
                            MessageBox.Show("Se ha copiado :D");
                            nombreV = "";
                            direccion = "";
                        }
                        else
                        {
                            estadoActual = 93; 
                        }
                        break;
                    case 99:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 100;
                            auxiliar += caracter;
                        }

                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 99;
                        }
                        else
                        {
                            estadoActual = 8888;
                            auxiliar += caracter;
                        }
                        break;
                    case 100:
                        if (Saltos(caracter))
                        {
                            AgregarALista(auxiliar, "Nombre", 0);
                            // AGREGAR MÉTODO PARA ELIMINAR ARCHIVOS

                        }
                        else
                        {
                            estadoActual = 100;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 111:
                         if (Comillas(caracter))
                        {
                            estadoActual = 112;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 111;

                        }
                        else
                        {
                            estadoActual = 222;
                            direccion += caracter;
                        }
                        break;

                    case 112:
                         if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 113;
                            direccion += caracter;
                        }

                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 112;
                        }
                        else
                        {
                            estadoActual = 222;
                            direccion += caracter;
                        }
                        break;
                    case 113: 
                         if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 113;
                            direccion += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 113;
                        }
                        else if (Slash(caracter))
                        {
                            estadoActual = 113;
                            direccion += "\\";
                        }
                        else if (Comillas(caracter))
                        {
                            estadoActual = 114;
                        }
                        else
                        {
                            estadoActual = 222;
                            direccion += caracter;
                        }
                        break;
                    case 114: 
                         if (Espacio(caracter))
                        {
                            AgregarALista(direccion, "Direccion", 115);
                        }
                        else
                        {
                            estadoActual = 114; 
                        }
                        break;
                    case 115:
                         if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 116;
                            nombreN += caracter;
                        }

                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 99;
                        }
                        else
                        {
                            estadoActual = 1111;
                            nombreN += caracter;
                        }
                        break;
                    case 116: 
                        if (Saltos(caracter))
                        {
                            AgregarALista(nombreN, "Nombre", 0);
                            // AGREGAR MÉTODO PARA CARGAR ARCHIVO Y PRESENTARLO EN PANTALLA

                            direccion = "";
                            nombreN = "";
                        }
                        else
                        {
                            estadoActual = 116;
                            nombreN += caracter;
                        }
                        break;
                    case 9999:
                        if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            AgregarAErrores(auxiliar, "Valor no reconocido");
                            MessageBox.Show("Valor no reconocido");

                        }
                        else
                        {
                            estadoActual = 9999;
                            auxiliar += caracter;
                        }
                        break;
                    case 8888: 
                          if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            AgregarAErrores(auxiliar, "Nombre no válido");
                            MessageBox.Show("Nombre no válido");

                            nombreN = "";
                            nombreV = "";

                        }
                        else
                        {
                            estadoActual = 8888;
                            auxiliar += caracter;
                        }
                        break;
                    case 222:
                         if ((Saltos(caracter)))
                        {
                            AgregarAErrores(direccion, "Dirección no válida");
                            MessageBox.Show("Dirección no válida");
                        }
                        else
                        {
                            estadoActual = 222;
                            direccion += caracter;
                        }
                        break;


                }
            }

        }

        // MÉTODO PARA GENERAR LOS REPORTES
        public void generarHTML()
        {
            StreamWriter html = new StreamWriter("salida.html");
            html.Write("<html>");
            html.Write("<head>");
            html.Write("Archivo de Salida, bienvenidos sean, UNIVERSIDAD DE SAN CARLOS DE GUATEMALA");
            html.Write("</head>");
            html.Write("<body>");
            html.Write("<p> Tabla de Tokens </p>");
            html.Write("<table>");
            html.Write("<tr>");
            html.Write("<th> Token </th>");
            html.Write("<th> Lexema </th>");
            html.Write("</tr>");
            for (int i = 0; i <= ListaT.Count - 1; i++)
            {
                html.Write("<tr>");
                html.Write("<td>" + ListaT[i].Lexema + "</td>");

                html.Write("<td>" + ListaT[i].Tipo + "</td>");
                html.Write("</tr>");
            }

            html.Write("</body>");
            html.Write("</html>");
            html.Close();
            Console.WriteLine("Cantidad en la lista" + ListaT.Count);

        }

        // Método para renombrar archivos
        public void RenombrarArchivo(string nombreviejo, string nombrenuevo)
        {

        }

        // Método para eliminar carpeta
        public void EliminarCarpeta(string nombre)
        {
            if (Directory.Exists(raiz + nombre))
            {
                Directory.Delete(raiz + nombre);
            }
            else
            {
                MessageBox.Show("La carpeta no existe");
            }
        }

        // Método de renombrar carpetas
        public void RenombrarCarpeta(string nombreviejo, string nombrenuevo)
        {
            for (int i = ListaT.Count - 1; i >= 0; i--)
            {
                if (ListaT[i].Tipo == "Direccion")
                {
                    if (Directory.Exists(raiz + ListaT[i].Lexema + nombreviejo))
                    {
                        Directory.Move(raiz + ListaT[i].Lexema + nombreviejo, raiz + ListaT[i].Lexema + nombrenuevo);
                        i = 0; 
                    }
                    else
                    {
                        MessageBox.Show("No existe la carpeta");
                    }
                }
                else
                {
                    i--; 
                }
            }
        }

        // METDODO DE AGREGAR CARPETAS
        public void CrearCarpeta(string nombre)
        {
            if (Directory.Exists(raiz + nombre))
            {
                MessageBox.Show("Ya existe la carpeta");
            }
            else
            {
                Directory.CreateDirectory(raiz + nombre);
            }

        }
        // Método de crear archivo. 
        public void CrearArchivo(string nombre)
        {
            for (int i = ListaT.Count - 1; i >= 0; i--)
            {
                if (ListaT[i].Tipo == "Direccion")
                {
                    if (System.IO.File.Exists(raiz + ListaT[i].Lexema))
                    {
                        MessageBox.Show("El fichero ya existe");
                    }
                    else
                    {
                        File.Create(raiz + ListaT[i].Lexema + nombre + ".lfp");
                        i = 0;
                    }
                }
                else
                {
                    i--;
                }
            }
        }

        public void ObtenerArchivos()
        {
            string ThePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            string[] files = Directory.GetFiles(ThePath);
            foreach (string s in files)
            {
                Console.WriteLine("Archivo encontrado: " + s);

            } Console.WriteLine();

        }
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
            if (valor == 10)
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
        public Boolean DosPuntos(char valor)
        {
            if (valor == 58)
            {
                return true;
            }
            else
                return false;
        }
        public Boolean Comillas(char valor)
        {
            if (valor == 34)
            {
                return true;
            }
            else
                return false;
        }
        public Boolean SlashI(char valor)
        {
            if (valor == 92)
            {
                return true;
            }
            else
                return false; 
        }
    }
}
