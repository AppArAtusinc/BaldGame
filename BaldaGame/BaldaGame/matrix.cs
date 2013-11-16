using System;

namespace BaldaGame
{
    public class matrix//v1.0
    {
        int[][] data;

        public matrix(int size = 0)
        {
            data = new int[size][];
            for (int i = 0; i < size; i++)
                data[i] = new int[size];
 
        }
 
        public matrix(int[] MFI_VECTOR, int[] MFI_SEPARATORS)
        {
            Array.Resize<int[]>(ref data, MFI_SEPARATORS.Length);

            for (int i = 0; i < MFI_SEPARATORS.Length; i++)
                Array.Resize<int>(ref data[i], MFI_SEPARATORS.Length);//size is ready

            int count = 0;
            for (int i = 0; i < MFI_VECTOR.Length; i++)
            {
                while(i == MFI_SEPARATORS[count])
                    count++;
                data[count][MFI_VECTOR[i]-1] = 1;
            }
        }
        public matrix(matrix e)
        {
            data = new int[e.Length][];
            for (int i = 0; i < e.Length; i++)
                data[i] = new int[e[i].Length];

            for (int i = 0; i < e.Length; i++)
                for (int j = 0; j < e[i].Length; j++)
                    data[i][j] = e[i][j];

        }
        public int Length
        {
            get
            {
                return data.Length;
            }
        }       
        public void clear()
        {
            data = new int[0][];

        }
        public int[] this[int index]
        {
            get
            {
                return data[index];
            }
        }
        public int GetSize()
        {
            return data.Length;
        }
        public void Delete(int I, int J = -1)
        {
            int newsize = data.Length - 1, spCount = 0; ;
            int[][] newdata = new int[newsize][];
            for (int i = 0; i < newsize; i++)
            {
                if (i == I)
                    spCount++;

                newdata[i] = data[i + spCount];
            }
            if (J != -1)
            {
                for (int i = 0; i < newsize; i++)
                    DeleteE(ref newdata[i], J);
            }

            data = newdata;
        }
        void DeleteE(ref int[] mass, int J)
        {
            int newsize = data.Length - 1, spCount = 0; ;
            int[] newdata = new int[mass.Length - 1];
            for (int i = 0; i < newsize; i++)
            {
                if (i == J)
                    spCount++;
                newdata[i] = mass[i + spCount];
            }
            mass = newdata;
        }
        
        public static matrix operator * (matrix a, matrix b)
        {
            matrix buf = new matrix();
            buf.Copy(a);

            for (int i = 0; i < a.Length; i++)

                for (int j = 0; j < a.Length; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < a.Length; k++)
                        sum += a[i][k] * b[k][j];

                    buf[i][j] = sum;
                }

            return buf;
        }

        public void Copy(matrix e)
        {
            int[][] bdata = new int[e.Length][];
            for (int i = 0; i < e.Length; i++)
                bdata[i] = new int[e[i].Length];

            for (int i = 0; i < e.Length; i++)
                for (int j = 0; j < e[i].Length; j++)
                    bdata[i][j] = e[i][j];
            data = bdata;
        }

    }
}