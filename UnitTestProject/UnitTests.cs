using Microsoft.VisualStudio.TestTools.UnitTesting;
using static PracticeTask8.Program;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            string path = "C:/Users/Aleksandr/source/repos/PracticeTask8/PracticeTask8/Graph.txt";
            int rows = 7;
            int columns = 8;
            byte[,] matrix = new byte[,]
            {
                {1, 0, 0, 1, 0, 0, 0, 0 },
                {1, 1, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 1, 1, 0, 0, 0 },
                {0, 1, 1, 0, 0, 0, 0, 0 },
                {0, 0, 1, 0, 1, 1, 1, 0 },
                {0, 0, 0, 0, 0, 0, 1, 1 },
                {0, 0, 0, 0, 0, 1, 0, 1 }
            };
            Graph graph1 = Graph.ReadGraph(path);
            Graph graph2 = new Graph(rows, columns, matrix);
            Assert.AreEqual(graph1.ToString(), graph2.ToString());
        }

        [TestMethod]
        public void TestMethod2()
        {
            string path = "C:/Users/Aleksandr/source/repos/PracticeTask8/PracticeTask8/Graph2.txt";
            Graph graph = Graph.ReadGraph(path);
            Assert.AreEqual(null, graph);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string path = "C:/Users/Aleksandr/source/repos/PracticeTask8/PracticeTask8/Graph771.txt";
            Graph graph = Graph.ReadGraph(path);
            Assert.AreEqual(null, graph);
        }

        [TestMethod]
        public void TestMethod4()
        {
            string path = "C:/Users/Aleksandr/source/repos/PracticeTask8/PracticeTask8/Graph3.txt";
            int rows = 7;
            int columns = 8;
            byte[,] matrix = new byte[,]
            {
                {1, 0, 0, 1, 0, 0, 0, 0 },
                {1, 1, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 1, 1, 0, 0, 0 },
                {0, 1, 1, 0, 0, 0, 0, 0 },
                {0, 0, 1, 0, 1, 1, 1, 0 },
                {0, 0, 0, 0, 0, 0, 1, 1 },
                {0, 0, 0, 0, 0, 1, 0, 1 }
            };
            Graph graph1 = Graph.ReadGraph(path);
            Graph graph2 = new Graph(rows, columns, matrix);
            Assert.AreEqual(graph1.ToString(), graph2.ToString());
        }

        [TestMethod]
        public void TestMethod5()
        {
            int rows = 7;
            int columns = 8;
            byte[,] matrix = new byte[,]
            {
                {1, 0, 0, 1, 0, 0, 0, 0 },
                {1, 1, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 1, 1, 0, 0, 0 },
                {0, 1, 1, 0, 0, 0, 0, 0 },
                {0, 0, 1, 0, 1, 1, 1, 0 },
                {0, 0, 0, 0, 0, 0, 1, 1 },
                {0, 0, 0, 0, 0, 1, 0, 1 }
            };
            Graph graph = new Graph(rows, columns, matrix);
            int i = graph.DeepSearch(0, -1);
            Assert.AreEqual(1, i);
        }
    }
}
