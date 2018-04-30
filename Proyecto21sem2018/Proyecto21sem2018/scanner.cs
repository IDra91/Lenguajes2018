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
        int estadoAC = 0; 
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
        String rad = "";
        List<tokens> ListaT = new List<tokens>();
        List<errores> ListaER = new List<errores>();
        List<sTokens> ListasT = new List<sTokens>();
        List<sErrores> ListasE = new List<sErrores>();
        List<variables> ListaVar = new List<variables>();
        List<pared> Pared = new List<pared>();
        List<puerta> Puerta = new List<puerta>();
        List<suelo> Suelo = new List<suelo>();
        List<terreno> Terreno = new List<terreno>();
        List<ventana> Ventana = new List<ventana>();

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
                            ImprimirEnConsola();
                        }
                        else
                        {
                            estadoActual = 0; 
                        }
                        break; 
                    case 1:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 2;
                            auxiliar += caracter;
                            ImprimirEnConsola();
                        }
                        else if (Slash(caracter))
                        {
                            estadoActual = 3;
                            auxiliar += caracter;
                            ImprimirEnConsola();
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 1; 
                        }
                        else
                        {
                            estadoActual = 1111;
                            auxiliar += caracter;
                            ImprimirEnConsola();
                        }
                        break; 
                    case 2:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)) || (EñeMinuscula(caracter)) || (EñeMayuscula(caracter)))
                        {
                            estadoActual = 2;
                            auxiliar += caracter;
                            ImprimirEnConsola();
                        }
                        else if (MayorQue(caracter))
                        {
                            
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
                            estadoActual = 1111;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 3:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)) || (EñeMinuscula(caracter)) || (EñeMayuscula(caracter)))
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
                            estadoActual = 1111;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 4:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)) || (EñeMinuscula(caracter)) || (EñeMayuscula(caracter)))
                        {
                            estadoActual = 4;
                            auxiliar += caracter;
                        }
                        else if (MayorQue(caracter))
                        {
                            if ((auxiliar == "</diseño>") || (auxiliar == "</DISEÑO>") || (auxiliar == "</Diseño>"))
                            {
                                auxiliar += caracter;
                                AgregarAListaTokens(auxiliar, "Token_Etiqueta_Diseño_Cerrado", 120);
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
                            estadoActual = 1111;
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
                            estadoActual = 2222;
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
                            AgregarAListaTokens(nombre, "Token_ID_Nombre", 10);
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
                            estadoActual = 2222;
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
                            estadoActual = 2222; 
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
                            estadoActual = 16;
                            valor += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 16;
                        }
                        else
                        {
                            estadoActual = 2222;
                            auxiliar += caracter; 
                        }break;
                    case 17:
                        if (Saltos(caracter))
                        {
                            AgregarAListaTokens(valor, "Token_ID_Cadena", 10);
                        }
                        else if (PuntoComa(caracter))
                        {
                            AgregarAListaTokens(valor, "Token_ID_Cadena", 22);
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
                            estadoActual = 2222;
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
                            AgregarAListaTokens(valor, "Token_ID_Entero", 10);
                        }
                        else if (PuntoComa(caracter))
                        {
                            AgregarAListaTokens(valor, "Token_ID_Entero", 22);
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
                            estadoActual = 2222;
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
                            estadoActual = 20;
                        }
                        else
                        {
                            estadoActual = 2222;
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
                            AgregarAListaTokens(valor, "Token_ID_Decimal", 10);
                        }
                        else if (PuntoComa(caracter))
                        {
                            AgregarAListaTokens(valor, "Token_ID_Decimal", 22);
                        }
                        else
                        {
                            estadoActual = 2222;
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
                            estadoActual = 3333;
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
                                AgregarAListaTokens(auxiliar, "Reservada_Etiqueta_Ventana_Abierto", 70);
                            }
                            else if ((auxiliar == "<puerta>") || (auxiliar == "<PUERTA>") || (auxiliar == "<Puerta>"))
                            {
                                auxiliar += caracter; 
                                AgregarAListaTokens(auxiliar, "Reservada_Etiqueta_Puerta_Abierto", 90);
                            }
                            else if ((auxiliar == "<suelo>") || (auxiliar == "<SUELO>") || (auxiliar == "<Suelo>"))
                            {
                                auxiliar += caracter; 
                                AgregarAListaTokens(auxiliar, "Reservada_Etiqueta_Suelo_Abierto", 110);
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
                            estadoActual = 3333;
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
                            estadoActual = 3333;
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
                            estadoActual = 3333;
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
                            estadoActual = 3333;
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
                            estadoActual = 3333;
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
                            estadoActual = 3333;
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
                            estadoActual = 3333;
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
                            estadoActual = 3333;
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
                            estadoActual = 3333;
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
                            AgregarAListaTokens(nomba, "Token_ID_Nombre", 50);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (PuntoComa(caracter))
                        {
                            AgregarAListaTokens(nomba, "Token_ID_Nombre", 50);
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
                            AgregarAListaTokens(color, "Token_ID_Color", 50);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (Saltos(caracter))
                        {
                            AgregarAListaTokens(color, "Token_ID_Color", 50);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (Espacio(caracter))
                        {
                            estadoActual = 55;
                        }
                        else
                        {
                            estadoActual = 3333;
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
                            AgregarAListaTokens(alt, "Id_Numerico", 50);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (Espacio(caracter))
                        {
                            estadoActual = 57;
                        }
                        else
                        {
                            estadoActual = 3333;
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
                              AgregarAListaTokens(iniy, "ID_Numerico", 50);
                              //HACER ALGO CON ESTO >:V. 
                          }
                          else if (Saltos(caracter))
                          {
                              AgregarAListaTokens(iniy, "ID_Numerico", 50);
                              //HACER ALGO CON ESTO >:V. 
                          }
                        else
                        {
                            estadoActual = 3333;
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
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 65;
                            finy += caracter;
                        } else
                        {
                            estadoActual = 64;
                        }                  
                        break; 
                    case 65:
                         if (CaracterNumerico(caracter))
                        {
                            estadoActual = 65;
                            finy += caracter;
                        }
                          else if (Espacio(caracter))
                          {
                              estadoActual = 65; 
                          }
                          else if (PuntoComa(caracter))
                          {
                              AgregarAListaTokens(finy, "ID_Numerico", 50);
                              //HACER ALGO CON ESTO >:V. 
                          }
                          else if (Saltos(caracter))
                          {
                              AgregarAListaTokens(finy, "ID_Numerico", 50);
                              //HACER ALGO CON ESTO >:V. 
                          }
                        else
                        {
                            estadoActual = 3333;
                            finy += caracter; 
                        }
                        break; 
                    case 70:
                        if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 71;
                            auxiliar += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 70;
                        }
                        else if (MenorQue(caracter))
                        {
                            estadoActual = 31;
                            auxiliar += caracter; 
                        }
                        else
                        {
                            estadoActual = 3333;
                            auxiliar += caracter; 
                        }
                        break;  
                    case 71: 
                          if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 71;
                            auxiliar += caracter;
                        }
                        else if (DosPuntos(caracter))
                        {
                            if ((auxiliar == "nombre"))
                            {

                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Nombre", 72);
                            }
                            else if (auxiliar == "tipo")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Tipo", 74);
                            }
                            else if (auxiliar == "longitud")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Longitud", 76);
                            }
                            else if (auxiliar == "radio")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Radio", 78);
                            }
                            else if (auxiliar == "pared_asociada")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_ParedAsociada", 80);
                            }
                            else
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Invalida", 40);
                            }
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 71;
                        }
                        else if (SubRaya(caracter))
                          {
                              estadoActual = 71;
                              auxiliar += caracter;
                          }
                        else
                        {
                            estadoActual = 3333;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 72:
                          if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 73;
                            nomba += caracter;
                        }
                        else
                        {
                            estadoActual = 72; 
                        } break; 
                    case 73: 
                         if (Saltos(caracter))
                        {
                            AgregarAListaTokens(nomba, "Token_ID_Nombre", 70);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (PuntoComa(caracter))
                        {
                            AgregarAListaTokens(nomba, "Token_ID_Nombre", 70);
                            // HACER ALGO CON ESTO >:V
                        }
                        
                        else if (Espacio(caracter))
                        {
                            estadoActual = 73;
                        }
                        else
                        {
                            estadoActual = 73;
                            nomba += caracter; 
                        }
                        break; 
                    case 74: 
                            if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 75;
                            tipo += caracter;
                        }
                        else
                        {
                            estadoActual = 74; 
                        } break; 
                    case 75:
                            if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 75;
                            tipo += caracter;
                        }
                        else if (Saltos(caracter))
                            {
                                AgregarAListaTokens(tipo, "Token_Tipo", 70);
                            }
                        else if (PuntoComa(caracter))
                            {
                                AgregarAListaTokens(nomba, "Token_Tipo", 70);
                                // HACER ALGO CON ESTO >:V
                            }
                        else
                        {
                            estadoActual = 72; 
                        } break; 
                    case 76:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 77;
                            lon += caracter;
                        }
                        else
                        {
                            estadoActual = 76;
                        }

                        break; 
                    case 77:
                          if (CaracterNumerico(caracter))
                        {
                            estadoActual = 77;
                            lon += caracter;
                        }
                        else if (Saltos(caracter))
                            {
                                AgregarAListaTokens(lon, "Token_Longitud", 70);
                            }
                        else if (PuntoComa(caracter))
                            {
                                AgregarAListaTokens(lon, "Token_Longitud", 70);
                                // HACER ALGO CON ESTO >:V
                            }
                        else
                        {
                            estadoActual = 3333;
                            lon += caracter; 
                        } break; 
                    case 78:
                           if (CaracterNumerico(caracter))
                        {
                            estadoActual = 79;
                            rad += caracter;
                        }
                        else
                        {
                            estadoActual = 78;
                        }

                        break;
                    case 79: 
                           if (CaracterNumerico(caracter))
                        {
                            estadoActual = 79;
                            rad += caracter;
                        }
                        else if (Saltos(caracter))
                            {
                                AgregarAListaTokens(rad, "Token_ID_Radio", 70);
                            }
                        else if (PuntoComa(caracter))
                            {
                                AgregarAListaTokens(rad, "Token_ID_Radio", 70);
                                // HACER ALGO CON ESTO >:V
                            }
                           else if (Punto(caracter))
                           {
                               estadoActual = 79;
                               rad += caracter;
                           }
                        else
                        {
                            estadoActual = 3333;
                            rad += caracter; 
                        } break; 
                    case 80: 
                           if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 81;
                            aso += caracter;
                        }
                        else
                        {
                            estadoActual = 80; 
                        } break; 
                    case 81: 
                          if (Saltos(caracter))
                        {
                            AgregarAListaTokens(aso, "Token_ID_NombAsoc", 70);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (PuntoComa(caracter))
                        {
                            AgregarAListaTokens(aso, "Token_ID_NombAsoc", 70);
                            // HACER ALGO CON ESTO >:V
                        }
                        
                        else if (Espacio(caracter))
                        {
                            estadoActual = 73;
                        }
                        else
                        {
                            estadoActual = 73;
                            aso += caracter; 
                        }
                        break; 
                    case 90:
                        if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 91;
                            auxiliar += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 90;
                        }
                        else if (MenorQue(caracter))
                        {
                            estadoActual = 31;
                            auxiliar += caracter;
                        }
                        else
                        {
                            estadoActual = 3333;
                            auxiliar += caracter;
                        }
                        break;
                    case 91:
                          if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 91;
                            auxiliar += caracter;
                        }
                        else if (DosPuntos(caracter))
                        {
                            if ((auxiliar == "nombre"))
                            {

                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Nombre", 92);
                            }
                            else if (auxiliar == "alto")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Alto", 94);
                            }
                            else if (auxiliar == "ancho")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Ancho", 96);
                            }
                            else if (auxiliar == "pared_asociada")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_ParedAsociada", 98);
                            }
                            else if (auxiliar == "color")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Color", 100);
                            }
                            else
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Invalida", 40);
                            }
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 91;
                        }
                        else if (SubRaya(caracter))
                          {
                              estadoActual = 91;
                              auxiliar += caracter;
                          }
                        else
                        {
                            estadoActual = 3333;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 92: 
                            if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 93;
                            nomba += caracter;
                        }
                        else
                        {
                            estadoActual = 92; 
                        } break; 
                    case 93: 
                         if (Saltos(caracter))
                        {
                            AgregarAListaTokens(nomba, "Token_ID_Nombre", 90);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (PuntoComa(caracter))
                        {
                            AgregarAListaTokens(nomba, "Token_ID_Nombre", 90);
                            // HACER ALGO CON ESTO >:V
                        }
                        
                        else if (Espacio(caracter))
                        {
                            estadoActual = 93;
                        }
                        else
                        {
                            estadoActual = 93;
                            nomba += caracter; 
                        }
                        break; 
                    case 94:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 95;
                            alt += caracter;
                        }
                        else
                        {
                            estadoActual = 94; 
                        }
                        break; 
                    case 95:
                         if (CaracterNumerico(caracter))
                        {
                            estadoActual = 95;
                            lon += caracter;
                        }
                        else if (Saltos(caracter))
                            {
                                AgregarAListaTokens(alt, "Token_ID_Altura", 90);
                            }
                        else if (PuntoComa(caracter))
                            {
                                AgregarAListaTokens(alt, "Token_ID_Altura", 90);
                                // HACER ALGO CON ESTO >:V
                            }
                        else
                        {
                            estadoActual = 9898;
                            alt += caracter; 
                        } break; 
                    case 96:
                          if (CaracterNumerico(caracter))
                        {
                            estadoActual = 97;
                            anch += caracter;
                        }
                        else
                        {
                            estadoActual = 96; 
                        }
                        break; 
                    case 97: 
                          if (CaracterNumerico(caracter))
                        {
                            estadoActual = 97;
                            anch += caracter;
                        }
                        else if (Saltos(caracter))
                            {
                                AgregarAListaTokens(anch, "Token_ID_Anchura", 90);
                            }
                        else if (PuntoComa(caracter))
                            {
                                AgregarAListaTokens(anch, "Token_ID_Anchura", 90);
                                // HACER ALGO CON ESTO >:V
                            }
                          else if (Punto(caracter))
                          {
                              estadoActual = 97;
                              anch += caracter; 
                          }
                        else
                        {
                            estadoActual = 3333;
                            anch += caracter; 
                        } break; 
                    case 98: 
                              if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 99;
                            aso += caracter;
                        }
                        else
                        {
                            estadoActual = 98; 
                        } break; 
                    case 99: 
                          if (Saltos(caracter))
                        {
                            AgregarAListaTokens(aso, "Token_ID", 90);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (PuntoComa(caracter))
                        {
                            AgregarAListaTokens(aso, "Token_ID", 90);
                            // HACER ALGO CON ESTO >:V
                        }
                        
                        else if (Espacio(caracter))
                        {
                            estadoActual = 93;
                        }
                        else
                        {
                            estadoActual = 93;
                            aso += caracter; 
                        }
                        break; 
                    case 100: 
                                if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 101;
                            color += caracter;
                        }
                        else
                        {
                            estadoActual = 100; 
                        } break; 
                    case 101: 
                         if(CaracterMinuscula(caracter)){
                            estadoActual = 101;
                            color += caracter;
                        }
                        else if (PuntoComa(caracter))
                        {
                            AgregarAListaTokens(color, "Token_ID_Color", 90);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (Saltos(caracter))
                        {
                            AgregarAListaTokens(color, "Token_ID_Color", 90);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (Espacio(caracter))
                        {
                            estadoActual = 101;
                        }
                        else
                        {
                            estadoActual = 3333;
                            color += caracter; 
                        }
                        break; 
                    case 110: 
                         if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 111;
                            auxiliar += caracter;
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 110;
                        }
                        else if (MenorQue(caracter))
                        {
                            estadoActual = 31;
                            auxiliar += caracter;
                        }
                        else
                        {
                            estadoActual = 3333;
                            auxiliar += caracter;
                        }
                        break;
                    case 111: 
                              if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 111;
                            auxiliar += caracter;
                        }
                        else if (DosPuntos(caracter))
                        {
                            if ((auxiliar == "nombre"))
                            {

                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Nombre", 112);
                            }
                            
                            else if (auxiliar == "color")
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Color", 114);
                            }
                            else
                            {
                                AgregarAListaTokens(auxiliar, "Palabra_Reservada_Invalida", 40);
                            }
                        }
                        else if ((Saltos(caracter)) || (Espacio(caracter)))
                        {
                            estadoActual = 111;
                        }
                        else if (SubRaya(caracter))
                          {
                              estadoActual = 111;
                              auxiliar += caracter;
                          }
                        else
                        {
                            estadoActual = 3333;
                            auxiliar += caracter; 
                        }
                        break; 
                    case 112:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 113;
                            nomba += caracter;
                        }
                        else
                        {
                            estadoActual = 112;
                        } break;
                    case 113:
                        if (Saltos(caracter))
                        {
                            AgregarAListaTokens(nomba, "Token_ID_Nombre", 110);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (PuntoComa(caracter))
                        {
                            AgregarAListaTokens(nomba, "Token_ID_Nombre", 110);
                            // HACER ALGO CON ESTO >:V
                        }

                        else if (Espacio(caracter))
                        {
                            estadoActual = 113;
                        }
                        else
                        {
                            estadoActual = 113;
                            nomba += caracter;
                        }
                        break; 
                    case 114:
                        if ((CaracterMinuscula(caracter)) || (CaracterMayuscula(caracter)))
                        {
                            estadoActual = 115;
                            color += caracter;
                        }
                        else
                        {
                            estadoActual = 114;
                        } break;
                    case 115:
                        if (CaracterMinuscula(caracter))
                        {
                            estadoActual = 115;
                            color += caracter;
                        }
                        else if (PuntoComa(caracter))
                        {
                            AgregarAListaTokens(color, "Token_ID_Color", 110);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (Saltos(caracter))
                        {
                            AgregarAListaTokens(color, "Token_ID_Color", 110);
                            // HACER ALGO CON ESTO >:V
                        }
                        else if (Espacio(caracter))
                        {
                            estadoActual = 115;
                        }
                        else
                        {
                            estadoActual = 3333;
                            color += caracter;
                        }
                        break; 
                    case 120:
                        Analizador_Sintactico();
                        break; 
                    case 1111: 
                         if (MayorQue(caracter))
                        {
                            AgregarAListaErroresLexicos(auxiliar, "Etiqueta_No_Valida", 0);
                            

                        }
                        else
                        {
                            estadoActual = 1111;
                            auxiliar += caracter;
                        }
                        break;
                    case 2222:
                         if ((Saltos(caracter)))
                        {
                            AgregarAListaErroresLexicos(auxiliar, "Valor no reconocido", 10);

                        }
                        else
                        {
                            estadoActual = 2222;
                            auxiliar += caracter;
                        }
                        break;
                    case 3333:
                        if ((Saltos(caracter)))
                        {
                            AgregarAListaErroresLexicos(auxiliar, "Valor no reconocido", 30);

                        }
                        else
                        {
                            estadoActual = 2222;
                            auxiliar += caracter;
                        }
                        break; 
                }
            }

        }

        public void Analizador_Sintactico()
        {
            for (int i = 0; i < ListaT.Count; i++)
            {
                switch (estadoAC)
                {
                    case 0:
                        if (ListaT[i].Tipo == "Token_Etiqueta_Diseño_Abierto")
                        {
                           
                            AgregarAListaSintactico(ListaT[i].Lexema, "Etiqueta_De_Diseño_Abierta", 1);
                        }
                        else
                        {
                            AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba Etiqueta de Diseño", 0);

                        }
                        break; 
                    case 1:
                        if ((ListaT[i].Tipo == "Token_Etiqueta_Variables_Abierto"))
                        {
                            AgregarAListaSintactico(ListaT[i].Lexema, "Etiqueta_Abierto", 2);
                        }
                        else if (ListaT[i].Tipo == "Token_Etiqueta_Construccion_Abierto")
                        {
                            AgregarAListaSintactico(ListaT[i].Lexema, "Etiqueta_Construccion_Abierto", 6);
                        }
                        else if (ListaT[i].Tipo == "Token_Etiqueta_Diseño_Cerrado")
                        {
                            AgregarAListaSintactico(ListaT[i].Lexema, "Etiqueta_Diseño_Cerrado", 99);
                        }
                        else
                            AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba etiqueta de Variables o de Construcción", 1);
                         break; 
                    case 2:
                         if (ListaT[i].Tipo == "Palabra_Reservada_Nombre")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Palabra_Reservada_Nombre", 3);
                         }
                         else if (ListaT[i].Tipo == "Palabra_Reservada_Tipo")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Palabra_Reservada_Tipo", 4);
                         }
                         else if (ListaT[i].Tipo == "Palabra_Reservada_Valor"){
                             AgregarAListaSintactico(ListaT[i].Lexema, "Palabra_Reservada_Valor", 5);
                         }
                         else if (ListaT[i].Tipo == "Token_Etiqueta_Variables_Cerrado")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Etiqueta_Variables_Cerrado", 1);
                         }
                         else
                         {
                             AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba Palabra Reservada o etiqueta de cerrado", 1);
                         }
                         break; 
                    case 3:
                         if (ListaT[i].Tipo == "Palabra_Reservada_Tipo")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_ID_Nombre", 2);
                         }
                         else
                         {
                             AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba valor del NOMBRE", 2);
                         }
                         break;
                    case 4:
                         if ((ListaT[i].Tipo == "Palabra_Reservada_Entero") || (ListaT[i].Tipo == "Palabra_Reservada_Doble") || (ListaT[i].Tipo == "Palabra_Reservada_Cadena"))
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_ID_Tipo", 2);
                         }
                         else
                         {
                             AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba Palabra Reservada de Tipo", 2);
                         }
                         break; 
                    case 5:
                         if ((ListaT[i].Tipo == "Palabra_ID_Cadena") || (ListaT[i].Tipo == "Palabra_ID_Entero") || (ListaT[i].Tipo == "Palabra_ID_Decimal"))
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_ID_Valor", 2);
                         }
                         else
                         {
                             AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba el valor de la variable", 2);
                         }
                         break; 
                    case 6:
                         if (ListaT[i].Tipo == "Reservada_Etiqueta_Terreno_Abierto")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_Etiqueta_Terreno_Abierta", 7);
                         }
                         else if (ListaT[i].Tipo == "Reservada_Etiqueta_Pared_Abierto")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_Etiqueta_Pared_Abierta", 9);
                         }
                         else if (ListaT[i].Tipo == "Reservada_Etiqueta_Ventana_Abierto")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_Etiqueta_Ventana_Abierta", 13);
                         }
                         else if (ListaT[i].Tipo == "Reservada_Etiqueta_Puerta_Abierto")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_Etiqueta_Puerta_Abierta", 15);
                         }
                         else if (ListaT[i].Tipo == "Reservada_Etiqueta_Suelo_Abierto")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_Etiqueta_Suelo_Abierta", 17);
                         }
                         else if (ListaT[i].Tipo == "Reservada_Etiqueta_Construccion_Cerrado")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_Etiqueta_Construccion_Cerrado", 1);
                         }
                         else
                         {
                             AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba una palabra reservada o una etiqueta de cierre", 6);
                         }
                         
                         break; 
                    case 7:
                         if ((ListaT[i].Tipo == "Palabra_Reservada_Ancho") || (ListaT[i].Tipo == "Palabra_Reservada_Largo"))
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_Palabra_Reservada_Extension", 8);
                         }
                         else if (ListaT[i].Tipo == "Reservada_Etiqueta_Terreno_Cerrado")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_Etiqueta_Terreno_Cerrado", 6);
                         }
                         else
                         {
                             AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba una palabra reservada de extension", 7);
                         }
                         break; 
                    case 8:
                         if (ListaT[i].Tipo == "ID_Numerico")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_ID_Numero", 7);
                         }
                         else
                         {
                             AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba un valor numerico", 7);
                         }
                         break; 
                    case 9:
                         if ((ListaT[i].Tipo == "Palabra_Reservada_Nombre") || (ListaT[i].Tipo == "Palabra_Reservada_Color") || (ListaT[i].Tipo == "Palabra_Reservada_Alto"))
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Palabra_Reservada", 10);
                         }
                         else if ((ListaT[i].Tipo == "Palabra_Reservada_Inicio") || (ListaT[i].Tipo == "Palabra_Reservada_Fin"))
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Palabra_Reservada", 11);
                         }
                         else if (ListaT[i].Tipo == "Etiqueta_Pared_Cerrado")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Etiqueta_Pared_Cerrado", 6);
                         }
                         else
                         {
                             AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba una palabra reservada o una etiqueta", 6);
                         }
                         break; 
                    case 10:
                         if ((ListaT[i].Tipo == "Token_ID_Nombre") || (ListaT[i].Tipo == "Palabra_ID_Color") || (ListaT[i].Tipo == "ID_Numerico"))
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_ID_Valor", 9);
                         }
                         else
                         {
                             AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba el valor de la variable", 9);
                         }
                         break; 
                    case 11:
                         if (ListaT[i].Tipo == "ID_Numerico")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_ID_Valor", 12);
                         }
                         else
                         {
                             AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba el valor de la variable", 9);
                         }
                         break;
                    case 12:
                        if (ListaT[i].Tipo == "ID_Numerico")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_ID_Valor", 9);
                         }
                         else
                         {
                             AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba el valor de la variable", 9);
                         }
                         break;
                    case 13:
                         if ((ListaT[i].Tipo == "Palabra_Reservada_Nombre") || (ListaT[i].Tipo == "Palabra_Reservada_Tipo") || (ListaT[i].Tipo == "Palabra_Reservada_Longitud") || (ListaT[i].Tipo == "Palabra_Reservada_Radio") || (ListaT[i].Tipo == "Palabra_Reservada_ParedAsociada"))
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_Palabra_Reservada", 14);
                        }
                         else if (ListaT[i].Tipo == "Reservada_Etiqueta_Ventana_Cerrado")
                        {
                            AgregarAListaSintactico(ListaT[i].Lexema, "Token_ID_Valor", 6);
                        } else {
                            AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba una palabra reservada o una etiqueta de cierre", 6);
                        }

                         break; 
                    case 14:
                         if ((ListaT[i].Tipo == "Token_ID_Nombre") || (ListaT[i].Tipo == "Token_Tipo") || (ListaT[i].Tipo == "Token_Longitud") || (ListaT[i].Tipo == "Token_ID_Radio") || (ListaT[i].Tipo == "Token_ID_NombAsoc"))
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_Palabra_Reservada", 13);
                        }
                          else {
                            AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba un valor de variable", 13);
                        }

                         break; 
                    case 15: 
                          if ((ListaT[i].Tipo == "Palabra_Reservada_Nombre") || (ListaT[i].Tipo == "Palabra_Reservada_Alto") || (ListaT[i].Tipo == "Palabra_Reservada_Ancho") || (ListaT[i].Tipo == "Palabra_Reservada_ParedAsociada") || (ListaT[i].Tipo == "Palabra_Reservada_Color"))
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_Palabra_Reservada", 16);
                        }
                         else if (ListaT[i].Tipo == "Reservada_Etiqueta_Puerta_Cerrado")
                        {
                            AgregarAListaSintactico(ListaT[i].Lexema, "Token_ID_Valor", 6);
                        } else {
                            AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba una palabra reservada o una etiqueta de cierre", 6);
                        }

                         break; 
                    case 16: 
                         if ((ListaT[i].Tipo == "Token_ID_Nombre") || (ListaT[i].Tipo == "Token_ID_Altura") || (ListaT[i].Tipo == "Token_ID_Anchura") || (ListaT[i].Tipo == "Token_ID") || (ListaT[i].Tipo == "Token_ID_Color"))
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_Palabra_Reservada", 13);
                        }
                          else {
                            AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba un valor de variable", 13);
                        }

                         break; 
                    case 17: 
                         if ((ListaT[i].Tipo == "Palabra_Reservada_Nombre") || (ListaT[i].Tipo == "Palabra_Reservada_Color"))
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_Palabra_Reservada_Atributos", 18);
                         }
                         else if (ListaT[i].Tipo == "Reservada_Etiqueta_Suelo_Cerrado")
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_Etiqueta_Suelo_Cerrado", 6);
                         }
                         else
                         {
                             AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba una palabra reservada del suelo", 7);
                         }
                         break; 
                    case 18:
                         if ((ListaT[i].Tipo == "Token_ID_Nombre") || (ListaT[i].Tipo == "Token_ID_Color"))
                         {
                             AgregarAListaSintactico(ListaT[i].Lexema, "Token_ID_Valor", 17);
                         }
                         
                         else
                         {
                             AgregarAListaErroresSintacticos(ListaT[i].Lexema, "Se esperaba el valor del atributo", 7);
                         }
                         break; 
                    case 99:
                         MessageBox.Show("Se ha terminado la compilación :D");
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
            estadoAC = estado;

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
            estadoAC = estado;
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

        public void AgregarVentana(string nombre, string tipo, int longitud, double radio, string pared){
            Ventana.Add(new ventana(nombre, tipo, longitud, radio, pared));
        }

        public void AgregarPared(string nombre, string color, int alto, int inicioX, int inicioY, int finX, int finY)
        {
            Pared.Add(new pared(nombre, color, alto, inicioX, inicioY, finX, finY));
        }

        public void AgregarSuelo(string nombre, string color)
        {
            Suelo.Add(new suelo(nombre, color));
        }

        public void AgregarPuerta(string nombre, int alto, double ancho, string pared_asoc, string color)
        {
            Puerta.Add(new puerta(nombre, alto, ancho, pared_asoc, color));
        }

        public void AgregarTerreno(int ancho, int largo)
        {
            Terreno.Add(new terreno(ancho, largo));
        }

        public void ImprimirEnConsola()
        {
            Console.WriteLine("Así va la cadena --->  " + auxiliar);
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
        public Boolean EñeMinuscula(char valor)
        {
            if (valor == 164)
            {
                return true;
            }
            else
            {
                return false; 
            }
        }

        public Boolean EñeMayuscula(char valor)
        {
            if (valor == 165)
            {
                return true;
            }
            else
            {
                return false;
            }
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
    
       
    }
}
