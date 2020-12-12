using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;




namespace Administracion_Alumnos
{
    public class AVL
    {
        public Registro valor;
        public AVL NodoIzquierdo;
        public AVL NodoDerecho;
        public AVL NodoPadre;
        public int altura;
        public Rectangle prueba;
        private DibujaAVL arbol;
        public AVL()
        {
        }

        public DibujaAVL Arbol
        {
            get { return arbol; }
            set { arbol = value; }
        }
        // Constructor.
        public AVL(Registro valorNuevo, AVL izquierdo, AVL derecho, AVL padre)
        {
            valor = valorNuevo;
            NodoIzquierdo = izquierdo;
            NodoDerecho = derecho;
            NodoPadre = padre;
            altura = 0;
        }



        //Funcion recursiva para recorrido en preorden
        public String RecorridoPreOrden()
        {
            String salida = "";
            salida += valor;
            if (NodoIzquierdo != null) salida += "    " + NodoIzquierdo.RecorridoPreOrden();

            if (NodoDerecho != null) salida += "    " + NodoDerecho.RecorridoPreOrden();
            return salida;
        }

        //Funcion recursiva para recorrido enorden
        public String RecorridoInOrden()
        {
            String salida = "";
            if (NodoIzquierdo != null) salida += NodoIzquierdo.RecorridoInOrden();
            salida += "    " + valor;
            if (NodoDerecho != null) salida += "    " + NodoDerecho.RecorridoInOrden();
            return salida.Trim();
        }

        //Funcion recurvisa para recorrido en postorden
        public String RecorridoPostOrden()
        {


            String salida = "";
            if (NodoIzquierdo != null) salida += NodoIzquierdo.RecorridoPostOrden();
            if (NodoDerecho != null) salida += "    " + NodoDerecho.RecorridoPostOrden();
            salida += "    " + valor;
            return salida.Trim();
        }

        //Funcion para insertar un nuevo valor en el arbol AVL
        public AVL Insertar(Registro valorNuevo, AVL Raiz)
        {
            if (Raiz == null)
            {

                Raiz = new AVL(valorNuevo, null, null, null);

            }
            else if (valorNuevo.id < Raiz.valor.id)
            {
                Raiz.NodoIzquierdo = Insertar(valorNuevo, Raiz.NodoIzquierdo);

            }
            else if (valorNuevo.id > Raiz.valor.id)
            {
                Raiz.NodoDerecho = Insertar(valorNuevo, Raiz.NodoDerecho);

            }
            else
            {
                MessageBox.Show("Valor Existente en el Arbol", "Error", MessageBoxButtons.OK);
            }




            //Realiza las rotaciones simples o dobles segun el caso
            if (Alturas(Raiz.NodoIzquierdo) - Alturas(Raiz.NodoDerecho) == 2)
            {
                if (valorNuevo.id < Raiz.NodoIzquierdo.valor.id)
                    Raiz = RotacionIzquierdaSimple(Raiz);
                else
                    Raiz = RotacionIzquierdaDoble(Raiz);
            }

            if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) == 2)
            {
                if (valorNuevo.id > Raiz.NodoDerecho.valor.id)
                    Raiz = RotacionDerechaSimple(Raiz);
                else
                    Raiz = RotacionDerechaDoble(Raiz);
            }


            Raiz.altura = max(Alturas(Raiz.NodoIzquierdo), Alturas(Raiz.NodoDerecho)) + 1;
            return Raiz;
        }


        //FUNCION DE PRUEBA PARA REALIZAR LAS ROTACIONES



        //Funcio para obtener que rama es mayor 
        private static int max(int lhs, int rhs)
        {
            return lhs > rhs ? lhs : rhs;
        }
        private static int Alturas(AVL Raiz)
        {
            return Raiz == null ? -1 : Raiz.altura;
        }

        AVL nodoE, nodoP;
        public AVL Eliminar(Registro valorEliminar, ref AVL Raiz)
        {

            if (Raiz != null)
            {

                if (valorEliminar.id < Raiz.valor.id)
                {

                    nodoE = Raiz;
                    Eliminar(valorEliminar, ref Raiz.NodoIzquierdo);
                }
                else
                {
                    if (valorEliminar.id > Raiz.valor.id)
                    {


                        nodoE = Raiz;
                        Eliminar(valorEliminar, ref Raiz.NodoDerecho);
                    }
                    else
                    {
                        //Posicionado sobre el elemento a eliminar

                        AVL NodoEliminar = Raiz;
                        if (NodoEliminar.NodoDerecho == null)
                        {
                            Raiz = NodoEliminar.NodoIzquierdo;

                            if (Alturas(nodoE.NodoIzquierdo) - Alturas(nodoE.NodoDerecho) == 2)
                            {
                                //MessageBox.Show("nodoE" + nodoE.valor.ToString());
                                if (valorEliminar.id < nodoE.valor.id)
                                    nodoP = RotacionIzquierdaSimple(nodoE);
                                else
                                    nodoE = RotacionDerechaSimple(nodoE);
                            }

                            if (Alturas(nodoE.NodoDerecho) - Alturas(nodoE.NodoIzquierdo) == 2)
                            {
                                if (valorEliminar.id > nodoE.NodoDerecho.valor.id)
                                    nodoE = RotacionDerechaSimple(nodoE);
                                else
                                    nodoE = RotacionDerechaDoble(nodoE);
                                nodoP = RotacionDerechaSimple(nodoE);
                            }


                        }
                        else
                        {
                            if (NodoEliminar.NodoIzquierdo == null)
                            {
                                Raiz = NodoEliminar.NodoDerecho;
                            }
                            else
                            {
                                if (Alturas(Raiz.NodoIzquierdo) - Alturas(Raiz.NodoDerecho) > 0)
                                {
                                    AVL AuxiliarNodo = null;
                                    AVL Auxiliar = Raiz.NodoIzquierdo;
                                    bool Bandera = false;
                                    while (Auxiliar.NodoDerecho != null)
                                    {
                                        AuxiliarNodo = Auxiliar;
                                        Auxiliar = Auxiliar.NodoDerecho;
                                        Bandera = true;
                                    }
                                    Raiz.valor = Auxiliar.valor;
                                    NodoEliminar = Auxiliar;
                                    if (Bandera == true)
                                    {
                                        AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                    }
                                    else
                                    {
                                        Raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;
                                    }
                                    //Realiza las rotaciones simples o dobles segun el caso


                                }

                                else
                                {

                                    if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) > 0)
                                    {

                                        AVL AuxiliarNodo = null;
                                        AVL Auxiliar = Raiz.NodoDerecho;
                                        bool Bandera = false;
                                        while (Auxiliar.NodoIzquierdo != null)
                                        {
                                            AuxiliarNodo = Auxiliar;
                                            Auxiliar = Auxiliar.NodoIzquierdo;
                                            Bandera = true;
                                        }
                                        Raiz.valor = Auxiliar.valor;
                                        NodoEliminar = Auxiliar;
                                        if (Bandera == true)
                                        {
                                            AuxiliarNodo.NodoIzquierdo = Auxiliar.NodoDerecho;
                                        }
                                        else
                                        {
                                            Raiz.NodoDerecho = Auxiliar.NodoDerecho;
                                        }

                                    }

                                    else
                                    {
                                        if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) == 0)
                                        {
                                            AVL AuxiliarNodo = null;
                                            AVL Auxiliar = Raiz.NodoIzquierdo;
                                            bool Bandera = false;
                                            while (Auxiliar.NodoDerecho != null)
                                            {
                                                AuxiliarNodo = Auxiliar;
                                                Auxiliar = Auxiliar.NodoDerecho;
                                                Bandera = true;
                                            }
                                            Raiz.valor = Auxiliar.valor;
                                            NodoEliminar = Auxiliar;
                                            if (Bandera == true)
                                            {
                                                AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                            }
                                            else
                                            {
                                                Raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;
                                            }
                                        }
                                    }

                                }





                            }
                        }

                    }
                }
            }
            else
            {

                MessageBox.Show("Nodo inexistente en el arbol", "Error", MessageBoxButtons.OK);

            }



            //nodoE.altura = max(Alturas(nodoE.NodoIzquierdo), Alturas(nodoE.NodoDerecho)) + 1;
            //MessageBox.Show("Nodo Resultante Balanceado; Recorrido PreOrden\r\n"+nodoP.RecorridoPreOrden().ToString());
            return nodoP;

        }








        //Seccion de funciones de rotaciones

        //Rotacion Izquierda Simple
        private static AVL RotacionIzquierdaSimple(AVL k2)
        {
            AVL k1 = k2.NodoIzquierdo;
            k2.NodoIzquierdo = k1.NodoDerecho;
            k1.NodoDerecho = k2;
            k2.altura = max(Alturas(k2.NodoIzquierdo), Alturas(k2.NodoDerecho)) + 1;
            k1.altura = max(Alturas(k1.NodoIzquierdo), k2.altura) + 1;
            // MessageBox.Show("Paso realizado: Rotacion Izquierda Simple","Pasos",MessageBoxButtons.OK,MessageBoxIcon.Information);
            // MessageBox.Show(k1.RecorridoPreOrden().ToString());
            return k1;
        }

        //Rotacion Derecha Simple
        private static AVL RotacionDerechaSimple(AVL k1)
        {
            AVL k2 = k1.NodoDerecho;
            k1.NodoDerecho = k2.NodoIzquierdo;
            k2.NodoIzquierdo = k1;
            k1.altura = max(Alturas(k1.NodoIzquierdo), Alturas(k1.NodoDerecho)) + 1;
            k2.altura = max(Alturas(k2.NodoDerecho), k1.altura) + 1;
            // MessageBox.Show("Paso realizado: Rotacion Derecha Simple", "Pasos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return k2;
        }

        //Doble Rotacion Izquierda
        private static AVL RotacionIzquierdaDoble(AVL k3)
        {
            // MessageBox.Show("Paso realizado: Rotacion Izquierda Doble", "Pasos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            k3.NodoIzquierdo = RotacionDerechaSimple(k3.NodoIzquierdo);
            return RotacionIzquierdaSimple(k3);
        }

        //Doble Rotacion Derecha
        private static AVL RotacionDerechaDoble(AVL k1)
        {
            // MessageBox.Show("Paso realizado: Rotacion Derecha Doble", "Pasos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            k1.NodoDerecho = RotacionIzquierdaSimple(k1.NodoDerecho);
            return RotacionDerechaSimple(k1);
        }




        //Funcion para obtener la altura del arbol
        public int getAltura(AVL nodoActual)
        {
            if (nodoActual == null)
                return 0;
            else
                return 1 + Math.Max(getAltura(nodoActual.NodoIzquierdo), getAltura(nodoActual.NodoDerecho));
        }




        //Buscar un valor en el arbol
        public void buscar(Registro valorBuscar, AVL Raiz)
        {
            if (Raiz != null)
            {

                if (valorBuscar.id < Raiz.valor.id)
                {
                    buscar(valorBuscar, Raiz.NodoIzquierdo);
                }
                else
                {
                    if (valorBuscar.id > Raiz.valor.id)
                    {
                        buscar(valorBuscar, Raiz.NodoDerecho);
                    }

                }
            }
            else
                MessageBox.Show("Valor no encontrado", "Error", MessageBoxButtons.OK);
        }





        //Funciones para dibujar el arbol.




        private const int Radio = 30;
        private const int DistanciaH = 40;
        private const int DistanciaV = 10;

        private int CoordenadaX;
        private int CoordenadaY;


        //Encuentra la posicion en donde debe crearse el nodo.
        public void PosicionNodo(ref int xmin, int ymin)
        {
            int aux1, aux2;

            CoordenadaY = (int)(ymin + Radio / 2);


            //obtiene la posicion del Sub-Arbol izquierdo.
            if (NodoIzquierdo != null)
            {
                NodoIzquierdo.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);
            }
            if ((NodoIzquierdo != null) && (NodoDerecho != null))
            {
                xmin += DistanciaH;
            }


            //Si existe el nodo derecho e izquierdo deja un espacion entre ellos.

            if (NodoDerecho != null)
            {
                NodoDerecho.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);
            }




            // Posicion de nodos dercho e izquierdo.
            if (NodoIzquierdo != null)
            {
                if (NodoDerecho != null)
                {

                    //centro entre los nodos.


                    CoordenadaX = (int)((NodoIzquierdo.CoordenadaX + NodoDerecho.CoordenadaX) / 2);


                }
                else
                {

                    // no hay nodo derecho. centrar al nodo izquierdo.
                    aux1 = NodoIzquierdo.CoordenadaX;
                    NodoIzquierdo.CoordenadaX = CoordenadaX - 40;
                    CoordenadaX = aux1;
                }
            }
            else if (NodoDerecho != null)
            {
                aux2 = NodoDerecho.CoordenadaX;
                //no hay nodo izquierdo.centrar al nodo derecho.
                NodoDerecho.CoordenadaX = CoordenadaX + 40;
                CoordenadaX = aux2;
            }
            else
            {
                // Nodo hoja
                CoordenadaX = (int)(xmin + Radio / 2);
                xmin += Radio;
            }
        }

        // Dibuja las ramas de los nodos izquierdo y derecho
        public void DibujarRamas(Graphics grafo, Pen Lapiz)
        {
            if (NodoIzquierdo != null)
            {
                grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY, NodoIzquierdo.CoordenadaX, NodoIzquierdo.CoordenadaY);
                NodoIzquierdo.DibujarRamas(grafo, Lapiz);
            }
            if (NodoDerecho != null)
            {
                grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY, NodoDerecho.CoordenadaX, NodoDerecho.CoordenadaY);
                NodoDerecho.DibujarRamas(grafo, Lapiz);
            }

        }


        //Dibuja el nodo en la posicion especificada.
        public void DibujarNodo(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, int dato, Brush encuentro)
        {

            //Dibuja el contorno del nodo.



            Rectangle rect = new Rectangle(
            (int)(CoordenadaX - Radio / 2),
            (int)(CoordenadaY - Radio / 2),
            Radio, Radio);


            if (valor.id == dato)
            {
                grafo.FillEllipse(encuentro, rect);

            }
            else
            {

                grafo.FillEllipse(encuentro, rect);

                grafo.FillEllipse(Relleno, rect);

            }

            grafo.DrawEllipse(Lapiz, rect);

            //Dibuja el valor del nodo.
            StringFormat formato = new StringFormat();

            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;
            grafo.DrawString(valor.ToString(), fuente, Brushes.Black, CoordenadaX, CoordenadaY, formato);



            //Dibuja los nodos hijos derecho e izquierdo.

            if (NodoIzquierdo != null)
            {
                NodoIzquierdo.DibujarNodo(grafo, fuente, Brushes.YellowGreen, RellenoFuente, Lapiz, dato, encuentro);
            }

            if (NodoDerecho != null)
            {
                NodoDerecho.DibujarNodo(grafo, fuente, Brushes.Yellow, RellenoFuente, Lapiz, dato, encuentro);
            }


        }

        public void colorear(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz)
        {

            //Dibuja el contorno del nodo.

            Rectangle rect = new Rectangle(
            (int)(CoordenadaX - Radio / 2),
            (int)(CoordenadaY - Radio / 2), Radio, Radio);
            prueba = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2),
            Radio, Radio);

            //Dibuja el nombre.
            StringFormat formato = new StringFormat();

            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;

            grafo.DrawEllipse(Lapiz, rect);
            grafo.FillEllipse(Brushes.PaleVioletRed, rect);
            grafo.DrawString(valor.ToString(), fuente, Brushes.Black, CoordenadaX, CoordenadaY, formato);







        }





    }
}
