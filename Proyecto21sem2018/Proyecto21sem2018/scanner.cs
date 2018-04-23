using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics; 
namespace Proyecto21sem2018
{
    class scanner
    {
        char caracter;
        int estadoActual = 0;
        int estado = 0; 
        String auxiliar = "";
        String nombre = "";
        String tipo = "";
        String valor = "";
        String nomba = "";
        String color = "";
        String alt = "";
        String anch = "";
        String inix = "";
        String iniy = "";
        String finx = "";
        String finy = "";
        String tipA = "";
        String lon = "";
        String aso = "";
        List<tokens> ListaT = new List<tokens>();
        List<errores> ListaER = new List<errores>();
        List<sTokens> ListasT = new List<sTokens>();
        List<sErrores> ListasE = new List<sErrores>();
        List<variables> ListaVar = new List<variables>();

        public void Analizador_Lexico(String cadena)
        {
            for (int i = 0; i < cadena.Length; i++)
            {
                caracter = cadena[i];
                // Esto lo que hace es que separa la cadena que necesitás en caracteres
                switch (estadoActual)
                {
                    case 0:
                        if (MenorQue(caracter))
                        {
                            estadoActual = 1;
                            auxiliar += caracter;
                        }
                        else
                        {
                            estadoActual = 11111; 
                        }
                        break; 
                    case 1:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 2;
                            auxiliar += caracter;
                        }
                        else if (Slash(caracter))
                        {
                            estadoActual = 3;
                            auxiliar += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 1; 
                        }
                        else
                        {
                            estadoActual = 22222;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 2:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 2;
                            auxiliar += caracter;
                        }
                        else if (MayorQue(caracter))
                        {
                            auxiliar += caracter;
                            if ((auxiliar == "<diseño>") || (auxiliar == "<DISEÑO>") || (auxiliar == "<Diseño>"))
                            {
                                auxiliar += caracter;
                                AgregarAListaTokens(auxiliar, "Token_Etiqueta_Diseño_Abierto", 0);
                            }
                            else if ((auxiliar == "<variables>") || (auxiliar == "<VARIABLES>") || (auxiliar == "<Variables>"))
                            {

                                auxiliar += caracter;
                                //ESTO SE VA A LA SECCION DE VARIABLES
                                AgregarAListaTokens(auxiliar, "Token_Etiqueta_Variables_Abierto", 10);
                            }
                            else if ((auxiliar == "<construccion>") || (auxiliar == "<CONSTRUCCION>") || (auxiliar == "<Construccion>"))
                            {

                                auxiliar += caracter;
                                //ESTO SE VA A LA SECCION DE CONSTRUCCION
                                AgregarAListaTokens(auxiliar, "Token_Etiqueta_Construccion_Abierto", 30);
                            }
                            else
                            {
                                auxiliar += caracter;
                                AgregarAListaErroresLexicos(auxiliar, "Etiqueta_Abierto_No_Valida", 0);
                            }
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 2;
                        }
                        else
                        {
                            estadoActual = 98989;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 3:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 4;
                            auxiliar += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 3;
                        }
                        else
                        {
                            estadoActual = 989898;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 4:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 4;
                            auxiliar += caracter;
                        }
                        else if (MayorQue(caracter))
                        {
                            if ((auxiliar == "</diseño>") || (auxiliar == "</DISEÑO>") || (auxiliar == "</Diseño>"))
                            {
                                auxiliar += caracter;
                                AgregarAListaTokens(auxiliar, "Token_Etiqueta_Diseño_Cerrado", 9999);
                            }
                            else if ((auxiliar == "</variables>") || (auxiliar == "</VARIABLES>") || (auxiliar == "</Variables>"))
                            {
                                auxiliar += caracter;
                                AgregarAListaTokens(auxiliar, "Token_Etiqueta_Variables_Cerrado", 0);
                            }
                            else if ((auxiliar == "</construccion>") || (auxiliar == "</CONSTRUCCION>") || (auxiliar == "</Construccion>"))
                            {
                                auxiliar += caracter;
                                AgregarAListaTokens(auxiliar, "Token_Etiqueta_Construccion_Cerrado", 0);
                            }
                            else
                            {
                                auxiliar += caracter;
                                AgregarAListaErroresLexicos(auxiliar, "Etiqueta_Cerrado_No_Valida", 0);
                            }
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 4;
                        }
                        else
                        {
                            estadoActual = 9090909;
                            auxiliar += caracter;
                        }
                        break; 
                    case 10:
                        if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 11;
                            auxiliar += caracter;
                        }
                        else if (MenorQue(caracter)) {
                            estadoActual = 1; 
                            auxiliar += caracter;

                        }else if (PuntoComa(caracter)){
                            estadoActual = 22; 
                            auxiliar += caracter; 

                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 10;
                        }
                        else
                        {
                            estadoActual = 676767;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 11:
                        if(CaracterMinuscula(caracter)){
                            estadoActual = 11;
                            auxiliar += caracter;
                        }
                        else if (DosPuntos(caracter))
                        {
                            if (auxiliar == "nombre")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Nombre", 12);
                            }
                            else if (auxiliar == "tipo")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Tipo", 14);
                            }
                            else if (auxiliar == "valor")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Valor", 16);
                            }
                            else
                            {
                                AgregarAListaErroresLexicos(auxiliar, "Palabra_Reservada_No_Valida", 10);
                            }
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 11;
                        }
                        else
                        {
                            estadoActual = 98989;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 12:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 13; 
                            nombre += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 12;
                        }
                        else
                        {
                            estadoActual = 12;
                            nombre += caracter; 
                        }
                        break; 
                    case 13:
                        if (Saltos(caracter))
                        {
                            estadoActual = 10;
                        }
                        else if (PuntoComa(caracter))
                        {
                            estadoActual = 22;
                        }
                        else if (Espacio(caracter))
                        {
                            estadoActual = 13;
                        }
                        else
                        {
                            estadoActual = 13;
                            nombre += caracter;
                        }
                        break; 
                    case 14:
                        if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 15;
                            tipo += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 14;
                        }
                        else
                        {
                            estadoActual = 7878787;
                            auxiliar += caracter; 
                        } break; 
                    case 15:
                        if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 15;
                            tipo += caracter;
                        }
                        else if (Saltos(caracter))
                        {
                            if (auxiliar == "entero")
                            {
                                AgregarAListaTokens(tipo, "Palabra_Reservada_Entero", 18);
                            }
                            else if (auxiliar == "doble")
                            {
                                AgregarAListaTokens(tipo, "Palabra_Reservada_Doble", 18);
                            }
                            else if (auxiliar == "cadena")
                            {
                                AgregarAListaTokens(tipo, "Palabra_Reservada_Cadena", 16);
                            }
                            else
                            {
                                AgregarAListaErroresLexicos(tipo, "Tipo_De_Variable_Invalido", 10);
                            }
                        }
                        else if (PuntoComa(caracter))
                        {
                            estadoActual = 22; 
                        }
                        else
                        {
                            estadoActual = 989898; 
                            auxiliar += caracter; 
                        }
                        break; 
                    case 16:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 17;
                            valor += caracter;
                        }
                        else if (CaracterNumerico(caracter))
                        {
                            estadoActual = 743;
                            valor += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 16;
                        }
                        else
                        {
                            estadoActual = 923823;
                            auxiliar += caracter; 
                        }break;
                    case 17:
                        if (Saltos(caracter))
                        {
                            estadoActual = 10;
                        }
                        else if (PuntoComa(caracter))
                        {
                            estadoActual = 22;
                        }
                        else
                        {
                            estadoActual = 17; 
                            valor += caracter; 
                        }
                        break; 
                    case 18:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 19;
                            valor += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 18;
                        }
                        else
                        {
                            estadoActual = 98219;
                            auxiliar += caracter;
                        }
                        break; 
                    case 19:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 19;
                            valor += caracter;
                        }
                        else if (Saltos(caracter))
                        {
                            estadoActual = 10;
                        }
                        else if (PuntoComa(caracter))
                        {
                            estadoActual = 22;
                        }
                        else if (Espacio(caracter))
                        {
                            estadoActual = 19;
                        }
                        else if (Punto(caracter))
                        {
                            estadoActual = 20;
                            valor += caracter; 
                        }
                        else
                        {
                            estadoActual = 9182;
                            valor += caracter; 
                        } break; 
                    case 20:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 21;
                            valor += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 200;
                        }
                        else
                        {
                            estadoActual = 1921;
                                valor += caracter;
                        }
                        break; 
                    case 21:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 21;
                            valor += caracter;

                        }
                        else if (Saltos(caracter))
                        {
                            estadoActual = 10;
                        }
                        else if (PuntoComa(caracter))
                        {
                            estadoActual = 22;
                        }
                        else
                        {
                            estadoActual = 12981;
                            valor += caracter; 
                        } break; 
                    case 22:
                        AgregarAListaVariables(nombre, tipo, valor, 10);
                        break; 
                    case 30:
                        if (MenorQue(caracter))
                        {
                            estadoActual = 31;
                            auxiliar += caracter;
                        }
                        else
                        {
                            estadoActual = 30; 
                        }
                        break; 
                    case 31:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 32;
                            auxiliar += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 31;
                        }
                        else if (Slash(caracter))
                        {
                            estadoActual = 33;
                            auxiliar += caracter; 
                        }
                        else
                        {
                            estadoActual = 9898;
                            auxiliar += caracter; 
                        } break; 
                    case 32:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 32;
                            auxiliar += caracter;
                        }
                        else if (MayorQue(caracter))
                        {
                            if ((auxiliar == "<terreno>") || (auxiliar == "<TERRENO>") || (auxiliar == "<Terreno>"))
                            {
                                auxiliar += caracter; 
                                AgregarAListaTokens(auxiliar, "Reservada_Etiqueta_Terreno_Abierto", 40);
                                // ESTADO 40 
                            }
                            else if ((auxiliar == "<pared>") || (auxiliar == "<PARED>") || (auxiliar == "<Pared>"))
                            {
                                auxiliar += caracter; 
                                AgregarAListaTokens(auxiliar, "Reservada_Etiqueta_Pared_Abierto", 50);
                                // ESTADO 50
                            }
                            else if ((auxiliar == "<ventana>") || (auxiliar == "<VENTANA>") || (auxiliar == "<Ventana>"))
                            {
                                auxiliar += caracter; 
                                AgregarAListaTokens(auxiliar, "Reservada_Etiqueta_Ventana_Abierto", 44);
                            }
                            else if ((auxiliar == "<puerta>") || (auxiliar == "<PUERTA>") || (auxiliar == "<Puerta>"))
                            {
                                auxiliar += caracter; 
                                AgregarAListaTokens(auxiliar, "Reservada_Etiqueta_Suelo_Abierto", 44);
                            }
                            else if ((auxiliar == "<suelo>") || (auxiliar == "<SUELO>") || (auxiliar == "<Suelo>"))
                            {
                                auxiliar += caracter; 
                                AgregarAListaTokens(auxiliar, "Reservada_Etiqueta_Suelo_Abierto", 44);
                            }
                            else
                            {
                                auxiliar += caracter; 
                                AgregarAListaErroresLexicos(auxiliar, "Etiqueta_No_Valida", 30);
                            }
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 31;
                        }
                        else {
                            estadoActual = 2983;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 33:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 34;
                            auxiliar += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 31;
                        }
                        else
                        {
                            estadoActual = 2938;
                            auxiliar += caracter; 
                        }                        
                        break; 
                    case 34:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 34;
                            auxiliar += caracter;
                        }
                        else if (MayorQue(caracter))
                        {
                            if ((auxiliar == "</terreno>") || (auxiliar == "</TERRENO>") || (auxiliar == "</Terreno>"))
                            {
                                auxiliar += caracter;
                                AgregarAListaTokens(auxiliar, "Reservada_Etiqueta_Terreno_Cerrado", 30);
                            }
                            else if ((auxiliar == "</pared>") || (auxiliar == "</PARED>") || (auxiliar == "</Pared>"))
                            {

                                auxiliar += caracter;
                                AgregarAListaTokens(auxiliar, "Reservada_Etiqueta_Pared_Cerrado", 30);
                            }
                            else if ((auxiliar == "</ventana>") || (auxiliar == "</VENTANA>") || (auxiliar == "</Ventana>"))
                            {
                                auxiliar += caracter;
                                AgregarAListaTokens(auxiliar, "Reservada_Etiqueta_Ventana_Cerrado", 30);
                            }
                            else if ((auxiliar == "</puerta>") || (auxiliar == "</PUERTA>") || (auxiliar == "</Puerta>"))
                            {
                                auxiliar += caracter;
                                AgregarAListaTokens(auxiliar, "Reservada_Etiqueta_Puerta_Cerrado", 30);
                            }
                            else if ((auxiliar == "</suelo>") || (auxiliar == "</SUELO>") || (auxiliar == "</Suelo>"))
                            {
                                auxiliar += caracter;
                                AgregarAListaTokens(auxiliar, "Reservada_Etiqueta_Suelo_Cerrado", 30);
                            }
                            else if ((auxiliar == "</construccion>") || (auxiliar == "</CONSTRUCCION>") || (auxiliar == "</Construccion>"))
                            {
                                auxiliar += caracter;
                                AgregarAListaTokens(auxiliar, "Reservada_Etiqueta_Construccion_Cerrado", 0);
                            }
                            else
                            {
                                auxiliar += caracter;
                                AgregarAListaErroresLexicos(auxiliar, "Etiqueta_No_Valida", 30);
                            }
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 34;
                        }
                        else
                        {
                            estadoActual = 9283;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 40:
                        if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 41;
                            auxiliar += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 40;
                        }
                        else if (MenorQue(caracter))
                        {
                            estadoActual = 31;
                            auxiliar += caracter; 
                        }
                        else
                        {
                            estadoActual = 92382;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 41:
                        if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 41;
                            auxiliar += caracter;
                        }
                        else if (Igual(caracter))
                        {
                            if ((auxiliar == "ancho"))
                            {

                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Ancho", 42);
                            }
                            else if (auxiliar == "largo")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Largo", 44);
                            }
                            else
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Invalida", 40);
                            }
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 40;
                        }
                        else
                        {
                            estadoActual = 2938;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 42:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 43;
                            anch += caracter; 
                        }
                        else
                        {
                            estadoActual = 42; 
                        }
                        break; 
                    case 43:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 43; 
                                anch += caracter;

                        }
                        else if (Saltos(caracter))
                        {
                            AgregarAListaTokens(anch, "ID_Numerico", 40);
                            //HACER ALGO CON ESTE VALOR!!!! >:V
                        }
                        else if (Espacio(caracter))
                        {
                            estadoActual = 43;
                        }
                        else
                        {
                            estadoActual = 9238;
                                anch += caracter; 
                        }
                            break; 
                    case 44:
                         if (CaracterNumerico(caracter))
                        {
                            estadoActual = 45;
                            alt += caracter; 
                        }
                        else
                        {
                            estadoActual = 44; 
                        }
                        break; 
                    case 45:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 45;
                            alt += caracter;

                        }
                        else if (Saltos(caracter))
                        {
                            AgregarAListaTokens(alt, "ID_Numerico", 40);
                            //HACER ALGO CON ESTE VALOR!!!! >:V
                        }
                        else if (Espacio(caracter))
                        {
                            estadoActual = 45;
                        }
                        else
                        {
                            estadoActual = 9238;
                            alt += caracter;
                        }
                        break; 
                    case 50:
                        if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 51;
                            auxiliar += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 50;
                        }
                        else if (MenorQue(caracter))
                        {
                            estadoActual = 31;
                            auxiliar += caracter; 
                        }
                        else
                        {
                            estadoActual = 92382;
                            auxiliar += caracter; 
                        }
                        break;  
                    case 51: 
                         if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 51;
                            auxiliar += caracter;
                        }
                        else if (DosPuntos(caracter))
                        {
                            if ((auxiliar == "nombre"))
                            {

                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Nombre", 52);
                            }
                            else if (auxiliar == "color")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Color", 54);
                            }
                            else if (auxiliar == "alto")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Alto", 56);
                            }
                            else if (auxiliar == "inicio")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Inicio", 58);
                            }
                            else if (auxiliar == "fin")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Fin", 62);
                            }
                            else
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Invalida", 40);
                            }
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 51;
                        }
                        else
                        {
                            estadoActual = 2938;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 52:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 53;
                            nomba += caracter;
                        }
                        else
                        {
                            estadoActual = 52; 
                        } break; 
                    case 53:
                        if (Saltos(caracter))
                        {
                            AgregarAListaTokens(nomba, "Token_ID", 50);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (PuntoComa(caracter))
                        {
                            AgregarAListaTokens(nomba, "Token_ID", 98);
                            // HACER ALGO CON ESTO >:V
                        }
                        
                        else if (Espacio(caracter))
                        {
                            estadoActual = 53;
                        }
                        else
                        {
                            estadoActual = 53;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 54:
                        if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 55;
                            color += caracter;
                        }
                        else
                        {
                            estadoActual = 54; 
                        }
                        break; 
                    case 55:
                        if(CaracterMinuscula(caracter)){
                            estadoActual = 55;
                            color += caracter;
                        }
                        else if (PuntoComa(caracter))
                        {
                            AgregarAListaTokens(color, "Token_ID_Color", 98);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (Saltos(caracter))
                        {
                            AgregarAListaTokens(color, "Token_ID", 50);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (Espacio(caracter))
                        {
                            estadoActual = 55;
                        }
                        else
                        {
                            estadoActual = 98897;
                            color += caracter; 
                        }
                        break; 
                    case 56:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 57;
                            alt += caracter;
                        }
                        else
                        {
                            estadoActual = 56; 
                        }
                        break; 
                    case 57:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 57;
                            alt += caracter;
                        }
                        else if (Saltos(caracter))
                        {
                            AgregarAListaTokens(alt, "ID_Numerico", 50);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (PuntoComa(caracter))
                        {
                            AgregarAListaTokens(alt, "Id_Numerico", 98);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (Espacio(caracter))
                        {
                            estadoActual = 57;
                        }
                        else
                        {
                            estadoActual = 9823;
                            alt += caracter; 
                        }
                        break; 
                    case 58:
                          if (CaracterNumerico(caracter))
                        {
                            estadoActual = 59;
                            inix += caracter;
                        }
                        else
                        {
                            estadoActual = 58; 
                        }
                        break; 
                    case 59: 
                          if (CaracterNumerico(caracter))
                        {
                            estadoActual = 59;
                            inix += caracter;
                        }
                        else if(Coma(caracter)){
                            AgregarAListaTokens(inix, "ID_Numerico", 60);

                        } else
                        {
                            estadoActual = 59; 
                        }
                        break; 
                    case 60: 
                          if (CaracterNumerico(caracter))
                        {
                            estadoActual = 61;
                            iniy += caracter;
                        }
                        else
                        {
                            estadoActual = 60; 
                        }
                        break; 
                    case 61: 
                          if (CaracterNumerico(caracter))
                        {
                            estadoActual = 61;
                            iniy += caracter;
                        }
                          else if (Espacio(caracter))
                          {
                              estadoActual = 61; 
                          }
                          else if (PuntoComa(caracter))
                          {
                              AgregarAListaTokens(iniy, "ID_Numerico", 98);
                              //HACER ALGO CON ESTO >:V. 
                          }
                          else if (Saltos(caracter))
                          {
                              AgregarAListaTokens(iniy, "ID_Numerico", 50);
                              //HACER ALGO CON ESTO >:V. 
                          }
                        else
                        {
                            estadoActual = 9853;
                            iniy += caracter; 
                        }
                        break; 
                    case 62:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 63;
                            finx += caracter;
                        }
                        else
                        {
                            estadoActual = 62;
                        }
                        break; 
                    case 63:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 63;
                            finx += caracter;
                        }
                        else if (Coma(caracter))
                        {
                            AgregarAListaTokens(finx, "ID_Numerico", 64);

                        }
                        else
                        {
                            estadoActual = 63;
                        }
                        break; 
                    case 64:

                        break; 
                }
            }

        }

        public void Analizador_Sintactico()
        {
            for (int i = 0; i <= ListaT.Count; i++)
            {
                switch (estado)
                {
                    case 0:
                        if (ListaT[i].Tipo == "Token_Etiqueta_Diseño_Abierto")
                        {
                           
                            AgregarAListaSintactico(ListaT[i].Lexema, "Etiqueta_De_Diseño_Abierta", 1);
                        }
                        else
                        {
                            AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba Etiqueta de Diseño", 1);

                        }
                        break; 
                    case 1:
                        if ((ListaT[i].Tipo == "Token_Etiqueta_Variables_Abierto") || (ListaT[i].Tipo == "Token_Etiqueta_Construccion_Abierto"))
                        {
                            AgregarAListaSintactico(ListaT[i].Lexema, "Etiqueta_Abierto", 2);
                        }
                        else
                            AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba etiqueta de Variables o de Construcción", 2);
                         break; 
                    case 2:
                    
                         break; 
                }
            }
        }

        //MÉTODOS PARA AGREGAR A LISTAS CORRESPONDIENTES
        public void AgregarAListaTokens(string lexema, string tipo, int estado)
        {

            ListaT.Add(new tokens(lexema, tipo));
            auxiliar = "";
            estadoActual = estado;

        }

        public void AgregarAListaSintactico(string lexema, string tipo, int estado)
        {

            ListasT.Add(new sTokens(lexema, tipo));
            auxiliar = "";
            estadoActual = estado;

        }

        public void AgregarAListaErroresLexicos(string lexema, string tipo, int estado)
        {

            ListaER.Add(new errores(lexema, tipo));
            auxiliar = "";
            estadoActual = estado;

        }

        public void AgregarAListaErroresSintacticos(string lexema, string tipo, int estado)
        {

            ListasE.Add(new sErrores(lexema, tipo));
            auxiliar = "";
            estadoActual = estado;
            Console.WriteLine(ListaT[0].Lexema);

        }

        public void AgregarAListaVariables(string nombre, string tipo, string valor,  int estado)
        {

            ListaVar.Add(new variables(nombre, tipo, valor));
            auxiliar = "";
            nombre = "";
            tipo = "";
            valor = "";
            estadoActual = estado;
            

        }






        //MÉTODOS PARA EL REPORTE DE TOKENS Y EL DE ERRORES
        public void generarHTMLTokens()
        {
            StreamWriter html = new StreamWriter("salidaTokens.html");
            html.Write("<html>");
            html.Write("<head>");
            html.Write("Archivo de Salida, bienvenidos sean, UNIVERSIDAD DE SAN CARLOS DE GUATEMALA");
            html.Write("</head>");
            html.Write("<body>");
            html.Write("<p> Tabla de Tokens Léxicos</p>");
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
            html.WriteLine("</table>");
            html.Write("<p> Tabla de Tokens Sintácticos </p>");
            html.Write("<table>");
            html.Write("<tr>");
            html.Write("<th> Token </th>");
            html.Write("<th> Lexema </th>");
            html.Write("</tr>");
            for (int j = 0; j <= ListasT.Count - 1; j++)
            {
                html.Write("<tr>");
                html.Write("<td>" + this.ListasT[j].Lexema + "</td>");

                html.Write("<td>" + this.ListasT[j].Tipo + "</td>");
                html.Write("</tr>");
            }
            html.WriteLine("</table>");
            html.Write("</body>");
            html.Write("</html>");
            html.Close();
            MessageBox.Show("Se ha creado la lista de Tokens :D");

        }
        public void generarHTMLErrores()
        {
            StreamWriter html = new StreamWriter("salidaErrores.html");
            html.Write("<html>");
            html.Write("<head>");
            html.Write("Archivo de Salida, bienvenidos sean, UNIVERSIDAD DE SAN CARLOS DE GUATEMALA");
            html.Write("</head>");
            html.Write("<body>");
            html.Write("<p> Tabla de Errores Léxicos </p>");
            html.Write("<table>");
            html.Write("<tr>");
            html.Write("<th> Token </th>");
            html.Write("<th> Lexema </th>");
            html.Write("</tr>");
            for (int i = 0; i <= ListaER.Count - 1; i++)
            {
                html.Write("<tr>");
                html.Write("<td>" + ListaER[i].Lexema + "</td>");

                html.Write("<td>" + ListaER[i].Tipo + "</td>");
                html.Write("</tr>");
            }
            html.WriteLine("</table>");
            html.Write("<p> Tabla de Errores Sintácticos</p>");
            html.Write("<table>");
            html.Write("<tr>");
            html.Write("<th> Token </th>");
            html.Write("<th> Lexema </th>");
            html.Write("</tr>");
            for (int j = 0; j <= ListasE.Count - 1; j++)
            {
                html.Write("<tr>");
                html.Write("<td>" + this.ListasE[j].Lexema + "</td>");

                html.Write("<td>" + this.ListasE[j].Tipo + "</td>");
                html.Write("</tr>");
            }
            html.WriteLine("</table>");
            html.Write("</body>");
            html.Write("</html>");
            html.Close();
            MessageBox.Show("Se ha creado la lista de Errores :D");

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
            if ((valor > 64) & (valor < 90))
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
        public Boolean PuntoComa(char valor)
        {
            if (valor == 59)
            {
                return true;
            }
            else
                return false;
        }
        public Boolean Igual(char valor)
        {
            if (valor == 61)
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
