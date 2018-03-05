using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinematic
{
    [Serializable]
    public class Matrix3F:ICloneable
    {
        private double[] data = new double[9];


        public double this[int row, int col]
        {
            get
            {
                if (row < 3 && col < 3 && row >= 0 && col >= 0)
                {
                    return data[3 * row + col];
                }
                else
                {
                    throw new ArgumentOutOfRangeException("row col", "row col must smaller than 3 and larger than 0");
                }

            }

            set
            {
                if (row < 3 && col < 3 && row >= 0 && col >= 0)
                {
                    data[3 * row + col] = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("row col", "row col must smaller than 3 and larger than 0");
                }
            }

        }

        public double this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }
        

        public Vector3F3 Row
        {
            get => new Vector3F3(data);
            
        }

        public Vector3F3 Col
        {
            get => new Vector3F3(data, false);
            
        }

        public Matrix3F(params double[] values)
        {
            
            int itr = values.Length < 9 ? values.Length : 9;
            for (int i =0;i<itr;i++)
            {
                data[i] = values[i];
            }
        }

        public Matrix3F()
        {
            
        }

        public static Matrix3F operator *(Matrix3F mat1,Matrix3F mat2)
        {
            Matrix3F mat =  new Matrix3F();
            mat[0, 0] = mat1[0, 0] * mat2[0, 0] + mat1[0, 1] * mat2[1, 0] + mat1[0, 2] * mat2[2, 0];
            mat[0, 1] = mat1[0, 0] * mat2[0, 1] + mat1[0, 1] * mat2[1, 1] + mat1[0, 2] * mat2[2, 1];
            mat[0, 2] = mat1[0, 0] * mat2[0, 2] + mat1[0, 1] * mat2[1, 2] + mat1[0, 2] * mat2[2, 2];
            mat[1, 0] = mat1[1, 0] * mat2[0, 0] + mat1[1, 1] * mat2[1, 0] + mat1[1, 2] * mat2[2, 0];
            mat[1, 1] = mat1[1, 0] * mat2[0, 1] + mat1[1, 1] * mat2[1, 1] + mat1[1, 2] * mat2[2, 1];
            mat[1, 2] = mat1[1, 0] * mat2[0, 2] + mat1[1, 1] * mat2[1, 2] + mat1[1, 2] * mat2[2, 2];
            mat[2, 0] = mat1[2, 0] * mat2[0, 0] + mat1[2, 1] * mat2[1, 0] + mat1[2, 2] * mat2[2, 0];
            mat[2, 1] = mat1[2, 0] * mat2[0, 1] + mat1[2, 1] * mat2[1, 1] + mat1[2, 2] * mat2[2, 1];
            mat[2, 2] = mat1[2, 0] * mat2[0, 2] + mat1[2, 1] * mat2[1, 2] + mat1[2, 2] * mat2[2, 2];
            return mat;
        }

        public static Matrix3F operator *(double d,Matrix3F mat)
        {
            Matrix3F mr = new Matrix3F();
            for (int i =0;i<9;i++)
            {
                mr.data[i] = d * mat.data[i];
            }
            return mr;
        }

        public static Vector3F operator * (Matrix3F mat, Vector3F vec)
        {
            return new Vector3F(
                mat[0, 0] * vec[0] + mat[0, 1] * vec[1] + mat[0, 2] * vec[2],
                mat[1, 0] * vec[0] + mat[1, 1] * vec[1] + mat[1, 2] * vec[2],
                mat[2, 0] * vec[0] + mat[2, 1] * vec[1] + mat[2, 2] * vec[2]);
        }

        public static Matrix3F operator + (Matrix3F mat1,Matrix3F mat2)
        {
            Matrix3F rs = new Matrix3F();
            for (int i = 0; i < 9; i++)
                rs[i] = mat1[i] + mat2[i];
            return rs;
        }

        public static Matrix3F GetUnitMatrix()
        {
            return new Matrix3F(1, 0, 0, 0, 1, 0, 0, 0, 1);
        }

        public object Clone()
        {
            Matrix3F mat = new Matrix3F();
            Array.Copy(data, mat.data, 9);
            return mat;
        }

        public override string ToString()
        {
            return String.Format("[{0} {1} {2}; {3} {4} {5}; {6} {7} {8}]", data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8]);

        }
    }
}
