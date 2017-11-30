using System;
using System.Collections.Generic;
using System.Linq;

namespace TestPSL
{
    /// <summary>
    /// Prueba PSL
    /// Ivan Camilo Suarez Castro
    /// </summary>
    public class ImpresorLCD
    {
        int tamanio;
        string espacio;
        string numeros; 
        List<char[,]> matrizLCD;

        int filas;
        int columnas;

        /// <summary>
        /// Caracter que se usa para pintar las lineas horizontales
        /// </summary>
        public char h = '_';

        /// <summary>
        /// Cartacter que se usa para pintar las lineas verticales
        /// </summary>
        public char v = '|';

        public ImpresorLCD(string input, int espacio)
        {
            string[] cadena = input.Split(new char[] { ',' });

            numeros = cadena[1];
            tamanio = int.Parse(cadena[0]);
            this.espacio = string.Empty;

            filas = (tamanio * 2) + 1;
            columnas = tamanio + 2;

            matrizLCD = new List<char[,]>(numeros.Length);

            for (int i = 0; i <= espacio; i++)
                this.espacio += " ";
        }

        /// <summary>
        /// Genera lineas horizontales ya sea en la parte superio, 
        /// inferior o en el medio de la matriz
        /// </summary>
        /// <param name="posicion">Arriba, medio o abajo</param>
        /// <param name="matriz">matriz donde se esta generando el numero</param>
        public void PintarHorizontal(PosicionHorizontal posicion, char[,] matriz)
        {
            int i = 0;
            if (posicion == PosicionHorizontal.medio)
                i = matriz.GetLength(0) / 2;
            else if(posicion == PosicionHorizontal.abajo)
                i = matriz.GetLength(0) - 1;

            for (int j = 1; j < matriz.GetLength(1) - 1; j++)
                matriz[i, j] = h;
        }

        /// <summary>
        /// Genera lineas verticales, ya sean las dos laterales completas, 
        /// o la mitad, sean arriba o abajo
        /// </summary>
        /// <param name="posicionV">Adelante o atras</param>
        /// <param name="posicionH">Arriba, abajo o completo</param>
        /// <param name="matriz">matriz donde se esta generando el numero</param>
        public void PintarVertical(PosicionVertical posicionV, PosicionHorizontal posicionH, char[,] matriz)
        {
            int i = posicionV == PosicionVertical.adelante ? 0 : matriz.GetLength(1) - 1;
            int js = 1;
            int jf = matriz.GetLength(0) - 1;
            if (posicionH != PosicionHorizontal.todo)
            {
                if (posicionH == PosicionHorizontal.arriba)
                {
                    js = 1;
                    jf = matriz.GetLength(0) / 2; 
                }
                else if (posicionH == PosicionHorizontal.abajo)
                {
                    js = matriz.GetLength(0) / 2 + 1;
                    jf = matriz.GetLength(0) - 1;
                }
            }
            for (int j = js; j <= jf; j++)
                matriz[j, i] = v;
        }

        /// <summary>
        /// Ya que mas de un numero tiene todas las lineas horizontales,
        /// se genera este metodo para que las genere todas
        /// </summary>
        /// <param name="matriz">matriz donde se esta generando el numero</param>
        public void PintarTodosHorizontales(char[,] matriz)
        {
            PintarHorizontal(PosicionHorizontal.arriba, matriz);
            PintarHorizontal(PosicionHorizontal.medio, matriz);
            PintarHorizontal(PosicionHorizontal.abajo, matriz);
        }

        /// <summary>
        /// Genera el numero indicado en una matriz
        /// </summary>
        /// <param name="numero">Numero a generar</param>
        /// <returns>Una matriz con el numero generado</returns>
        public char[,] DibujarNumero(int numero)
        {
            char[,] matriz = new char[filas, columnas];
            switch (numero)
            {
                case 0:
                    PintarHorizontal(PosicionHorizontal.arriba, matriz);
                    PintarHorizontal(PosicionHorizontal.abajo, matriz);
                    PintarVertical(PosicionVertical.adelante, PosicionHorizontal.todo, matriz);
                    PintarVertical(PosicionVertical.atras, PosicionHorizontal.todo, matriz);
                    break;
                case 1:
                    PintarVertical(PosicionVertical.atras, PosicionHorizontal.todo, matriz);
                    break;
                case 2:
                    PintarTodosHorizontales(matriz);
                    PintarVertical(PosicionVertical.atras, PosicionHorizontal.arriba, matriz);
                    PintarVertical(PosicionVertical.adelante, PosicionHorizontal.abajo, matriz);
                    break;
                case 3:
                    PintarTodosHorizontales(matriz);
                    PintarVertical(PosicionVertical.atras, PosicionHorizontal.todo, matriz);
                    break;
                case 4:
                    PintarHorizontal(PosicionHorizontal.medio, matriz);
                    PintarVertical(PosicionVertical.adelante, PosicionHorizontal.arriba, matriz);
                    PintarVertical(PosicionVertical.atras, PosicionHorizontal.todo, matriz);
                    break;
                case 5:
                    PintarTodosHorizontales(matriz);
                    PintarVertical(PosicionVertical.adelante, PosicionHorizontal.arriba, matriz);
                    PintarVertical(PosicionVertical.atras, PosicionHorizontal.abajo, matriz);
                    break;
                case 6:
                    PintarTodosHorizontales(matriz);
                    PintarVertical(PosicionVertical.adelante, PosicionHorizontal.todo, matriz);
                    PintarVertical(PosicionVertical.atras, PosicionHorizontal.abajo, matriz);
                    break;
                case 7:
                    PintarHorizontal(PosicionHorizontal.arriba, matriz);
                    PintarVertical(PosicionVertical.atras, PosicionHorizontal.todo, matriz);
                    break;
                case 8:
                    PintarTodosHorizontales(matriz);
                    PintarVertical(PosicionVertical.adelante, PosicionHorizontal.todo, matriz);
                    PintarVertical(PosicionVertical.atras, PosicionHorizontal.todo, matriz);
                    break;
                case 9:
                    PintarTodosHorizontales(matriz);
                    PintarVertical(PosicionVertical.adelante, PosicionHorizontal.arriba, matriz);
                    PintarVertical(PosicionVertical.atras, PosicionHorizontal.todo, matriz);
                    break;
            }
            RellenaEspacios(matriz);
            return matriz;
        }

        /// <summary>
        /// Encargado de generar espacios en la matriz de numeros,
        /// donde haya un null o vacio se asigna un espacio (' ')
        /// </summary>
        /// <param name="matriz"></param>
        public void RellenaEspacios(char[,] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (string.IsNullOrEmpty(matriz[i, j].ToString()))
                    {
                        matriz[i, j] = ' ';
                    }
                }
            }
        }

        /// <summary>
        /// Dados los numeros ingresados por el constructor,
        /// los genera uno a uno
        /// </summary>
        public void CargarLCD()
        {
            for (int i = 0; i < numeros.Length; i++)
            {
                matrizLCD.Add(DibujarNumero(Int32.Parse(numeros[i].ToString())));
            }
        }

        /// <summary>
        /// Imprime un solo numero
        /// </summary>
        /// <param name="numero">Matriz generada con el numero a pintar</param>
        public void ImprimirNumero(char[,] numero)
        {
            for (int i = 0; i < numero.GetLength(0); i++)
            {
                for (int j = 0; j < numero.GetLength(1); j++)
                {
                    Console.Write(numero[i, j]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Impresion final con la cadena de numeros dada en el constructor
        /// </summary>
        public void Imprimir()
        {
            for (int i = 0; i < filas; i++)
            {
                for (int m = 0; m < matrizLCD.Count(); m++)
                {
                    for (int j = 0; j < matrizLCD[m].GetLength(1); j++)
                    {
                        Console.Write(matrizLCD[m][i, j]);
                    }
                    Console.Write(espacio);
                }
                Console.WriteLine();
            }
        }
    }
}
