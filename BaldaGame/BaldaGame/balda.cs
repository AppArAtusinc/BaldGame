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
        string[] allwords;
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

        public void NewGame()
        //Если нужно начать игру заново
        //стираем матрицу, 
        //выбирает рандомное слово, 
        //записует его в матрицу, ходит первый игрок.
        {
            for (int i = 0; i < data.Length; i++)
                for (int j = 0; j < data.Length; j++)
                    data[i][j] = 0;
            string  s = GetRandWord();

            for (int i = 0; i<s.Length; i++)
                data[(data.Length)/2][i] = s[i];
            allwords = new string[1];
            allwords[0] = s;
            playerFirst = true;
        }

        public string Move(matrix Words, int[] cells)
        //Игрок ходит
        //Проверяем порядое ячеек
        //Составляет слово из матрицы и ячеек
        //Проверяем слово в словаре
        //Если солова нет, то ошибка "Слова нет"
        //елсе
        //data = Words
        //след. игрок
        {
            string maybeWord="";

            for(int i =0;i<cells.Length;i+=2)
                maybeWord+= (char)Words[cells[i]][cells[i+1]];

           // maybeWord = new string(maybeWord.Reverse().ToArray<char>());

            if (CheckWord(maybeWord))
            {
                data = Words;

                Array.Resize(ref allwords, allwords.Length + 1);
                allwords[allwords.Length - 1] = maybeWord;

                if (playerFirst)
                    countFirst += maybeWord.Length;
                else 
                    countSecond += maybeWord.Length;

                playerFirst = !playerFirst;
            }
            else
                throw new Error("Слова " + maybeWord + " не существует.");

            return maybeWord;
        }
        public bool CurentPlayer()
        {
            return playerFirst;
        }
        public int[] Rating()

        {
            int[] d = new int[2];
            d[0] = countFirst;
            d[1] = countSecond;
            return d;
        }
        public matrix CurrentMatrix()
        {
            return data;
        }
        public bool CheckCells(int[] cells)//пока не проверена
        {
            for (int i = 0; i < cells.Length - 2; i += 2)
                for (int j = i+2; j < cells.Length - 2; j += 2)
                    if (cells[i] == cells[j + 2] && cells[i + 1] == cells[j + 3])
                        return false;

            for (int i = 0; i < cells.Length - 2; i += 2)
            {
                if(cells[i] == cells[i+2] && cells[i+1] == cells[i+3])
                    return false;

                if (cells[i] == cells[i + 2] && cells[i + 1] != cells[i + 3])
                    if (cells[i + 1] == cells[i + 3] + 1 || cells[i + 1] == cells[i + 3] - 1)
                        continue;

                if (cells[i] != cells[i + 2] && cells[i + 1] == cells[i + 3])
                    if (cells[i] == cells[i + 2] + 1 || cells[i] == cells[i + 2] - 1)
                        continue;
                return false;
            }
            return true;
        }
        public string GetRandWord(int size = 5)
        {
            Random rand = new Random(); 
            string answer = "";int pos = 0;
            pos = rand.Next(0, words.Length-30);
            do
            {
                answer = "";
                while (words[pos] != '\n') pos++;
                pos++;

                while (words[pos] != '\r')
                {
                    answer += words[pos];
                    pos++;
                }
            } while (answer.Length != size);

            return answer;
        }
        public bool CheckWord(string word)//чи існує слово word?
        {
            for (int i = 0; i < allwords.Length; i++)
                if (allwords[i] == word)
                    throw new Error("Слово "+word+ "уже использовалось.");

                if (words.IndexOf("\r\n" + word + "\r\n") == -1)
                    return false;
            return true;

        }
                    
    }

    class Error : Exception
    {
        string info;

        public Error(string Info = "Error")
        {
            info = Info;
        }

        public Error(Error e)
        {
            info = e.info;
        }

        public string Info()
        {
            return info;
        }

    }
}
