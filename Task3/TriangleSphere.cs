using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafica
{
    public class TriangleSphere
    {
        public static void Draw()
        {
            var x = 50.525731112119133606;
            var z = 50.850650808352039932;
            var first = new[] {-x, 0.0, z};
            var second = new[] {x, 0.0, z};
            var third = new[] {-x, 0.0, -z};
            var fourth = new[] {x, 0.0, -z};
            var five = new[] {0.0, z, x};
            var six = new[] {0.0, z, -x};
            var seven = new[] {0.0, -z, x};
            var eight = new[] {0.0, -z, -x};
            var nine = new[] {z, x, 0.0};
            var ten = new[] {-z, x, 0.0};
            var eleven = new[] {z, -x, 0.0};
            var twelth = new[] {-z, -x, 0.0};
            double[][] vdata = {first, second, third, fourth, five, six, seven, eight, nine, ten, eleven, twelth};
            var first1 = new[] {0, 4, 1};
            var second1 = new[] {0, 9, 4};
            var third1 = new[] {9, 5, 4};
            var fourth1 = new[] {4, 5, 8};
            var five1 = new[] {4, 8, 1};
            var six1 = new[] {8, 10, 1};
            var seven1 = new[] {8, 3, 10};
            var eight1 = new[] {5, 3, 8};
            var nine1 = new[] {5, 2, 3};
            var ten1 = new[] {2, 7, 3};
            var eleven1 = new[] {7, 10, 3};
            var twelth1 = new[] {7, 6, 10};
            var first2 = new[] {7,11,6};
            var second2 = new[] {11,0,6};
            var third2 = new[] {0,1,6};
            var fourth2 = new[] {6,1,10};
            var five2 = new[] {9,0,11};
            var six2 = new[] {9,11,2};
            var seven2 = new[] {9,2,5};
            var eight2 = new[] {7,2,11};
            int[][] tindices =
            {
                first1, second1, third1, fourth1, five1, six1, seven1, eight1, nine1, ten1, eleven1, twelth1,
                first2, second2, third2, fourth2, five2, six2, seven2, eight2,
            };

            int i;
            double nx, ny, nz;
            GL.Begin(BeginMode.LineStrip);
            for (i = 0; i < 20; i++)
            {
                nx = vdata[tindices[i][0]][0];
                ny = vdata[tindices[i][0]][1];
                nz = vdata[tindices[i][0]][2];
                nx += vdata[tindices[i][1]][0];
                ny += vdata[tindices[i][1]][1];
                nz += vdata[tindices[i][1]][2];
                nx += vdata[tindices[i][2]][0];
                nx /= 3.0;
                ny += vdata[tindices[i][2]][1];
                ny /= 3.0;
                nz += vdata[tindices[i][2]][2];
                nz /= 3.0;
                GL.Normal3(nx, ny, nz);
                GL.Vertex3(vdata[tindices[i][0]]);
                GL.Vertex3(vdata[tindices[i][1]]);
                GL.Vertex3(vdata[tindices[i][2]]);
            }

            GL.End();
        }
    }
}