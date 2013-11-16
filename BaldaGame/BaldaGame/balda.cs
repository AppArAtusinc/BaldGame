using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BaldaGame
{
    class balda
    {

        matrix data;            //матрица с словами
        string words;           //загруженые из словаря слова
        int countFirst, countSecond;//очкии игроков
        bool playerFirst;///кто ходит из игроков

        public balda(int size = 5/*размер поля для игры*/, string pathToDic = "data.txt"/*путь к словарю*/)//Загружает слова, инициализирует матрицу и счетчеки 
        {
            //откриваем поток і загружаем всі слова
            StreamReader file = new StreamReader(pathToDic, Encoding.UTF8);
            words = file.ReadToEnd();
            file.Close();

            data = new matrix(size);
            countFirst = countSecond = 0;
            playerFirst = true;
        }

        public void NewGame(){}//Если нужно начать игру заново
                              //стираем матрицу, 
                              //выбирает рандомное слово, 
                              //записует его в матрицу, ходит первый игрок.

        public void Move(matrix Words, int[] cells){}//Игрок ходит
                                                    //Проверяем порядое ячеек
                                                    //Составляет слово из матрицы и ячеек
                                                    //Проверяем слово в словаре
                                                    //Если солова нет, то ошибка "Слова нет"
                                                    //елсе
                                                    //data = Words
                                                    //след. игрок

        public string GetRandWord(int size = 5)
        {
            Random rand = new Random(); 
            string answer = "";
            do
            {
                int pos = rand.Next(0, words.Length - 100);


                while (words[pos] != '\n') pos++;
                pos++;

                while (words[pos] != '\r')
                {
                    answer += words[pos];
                    pos++;
                }
            }
            while (answer.Length != size);

            return answer;
        }
                                                   
    }
}
