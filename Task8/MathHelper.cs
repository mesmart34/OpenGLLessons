using OpenTK;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public static class MathHelper
    {

        public static bool Any(Vector3 v)
        {
            return v.X != 0 || v.Y != 0 || v.Z != 0;
        }

        public static float Saturate(float value)
        {
            if (value >= 1.0f)
                return 1.0f;
            if (value <= 0.0f)
                return 0.0f;
            return value;
        }

        public static Vector3 Reflect(Vector3 i, Vector3 n)
        {
            return i - 2 * n * Vector3.Dot(i, n);
        }

        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        public static Vector3 Refract(Vector3 I, Vector3 N, float refractiveIndex)
        {
            float cosi = -Math.Max(-1, Math.Min(1, Vector3.Dot(I, N)));
            float etai = 1, etat = refractiveIndex;
            Vector3 n = N;
            if (cosi < 0)
            {
                cosi = -cosi;
                Swap(ref etai, ref etat);
                n = -N;
            }
            float eta = etai / etat;
            float k = 1 - eta * eta * (1 - cosi * cosi);
            return k < 0 ? new Vector3(0, 0, 0) : I * eta + n * (float)(eta * cosi - Math.Sqrt(k));
        }

    }
}
