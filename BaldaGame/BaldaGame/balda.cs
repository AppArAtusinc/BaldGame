using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaldaGame
{
    class balda
    {

        matrix data;            //матрица с словами
        int countFirst, countsecond;//очкии игроков
        bool playerFirst;///кто ходит из игроков

        balda(int size = 5);//Загружает слова, инициализирует матрицу и счетчеки 

        public void NewGame();//Если нужно начать игру заново
                              //стираем матрицу, 
                              //выбирает рандомное слово, 
                              //записует его в матрицу, ходит первый игрок.

        public void Move(matrix Words, int[] cells);//Игрок ходит
                                                    //Проверяем порядое ячеек
                                                    //Составляет слово из матрицы и ячеек
                                                    //Проверяем слово в словаре
                                                    //Если солова нет, то ошибка "Слова нет"
                                                    //елсе
                                                    //data = Words
                                                    //след. игрок
                                                   
    }
}
