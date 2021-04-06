using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using NUnit.Framework;

namespace ConsoleApp2
{
    [TestFixture]
    public class TestingSorts
    {
        private static readonly Random Random = new Random();
        private static readonly XmlDocument xmlDoc = new XmlDocument();
        private List<string> orderedWordsCollection;
        private List<string> shuffledWordsCollection;

        [Test]
        public void BubbleSortTest()
        {
            xmlDoc.Load(@"C:\Users\diyll\Laboratory\ConsoleApp2\Path.xml");
            var xmlRoot = xmlDoc.DocumentElement.SelectSingleNode("./BubbleSort");
            var reader = new StreamReader(xmlRoot.InnerText);
            orderedWordsCollection = new List<string>();

            while (!reader.EndOfStream)
            {
                orderedWordsCollection.Add(reader.ReadLine());
            }
            reader.Close();

            shuffledWordsCollection = ShuffleCollection(orderedWordsCollection);
            BubbleSort.MakeBubbleSort(shuffledWordsCollection);

            for (var i = 0; i < orderedWordsCollection.Count; ++i)
            {
                Assert.AreEqual(orderedWordsCollection[i], shuffledWordsCollection[i]);
            }
        }

        [Test]
        public void QuickSortTest()
        {
            xmlDoc.Load(@"C:\Users\diyll\Laboratory\ConsoleApp2\Path.xml");
            var xmlRoot = xmlDoc.DocumentElement.SelectSingleNode("./QSort");
            var reader = new StreamReader(xmlRoot.InnerText);
            orderedWordsCollection = new List<string>();

            while (!reader.EndOfStream)
            {
                orderedWordsCollection.Add(reader.ReadLine());
            }
            reader.Close();

            shuffledWordsCollection = ShuffleCollection(orderedWordsCollection);
            QSort.MakeQuickSort(shuffledWordsCollection);

            for (var i = 0; i < orderedWordsCollection.Count; ++i)
            {
                Assert.AreEqual(orderedWordsCollection[i], shuffledWordsCollection[i]);
            }
        }

        [Test]
        public void TreeSortTest()
        {
            xmlDoc.Load(@"C:\Users\diyll\Laboratory\ConsoleApp2\Path.xml");
            var xmlRoot = xmlDoc.DocumentElement.SelectSingleNode("./TreeSort");
            var reader = new StreamReader(xmlRoot.InnerText);
            orderedWordsCollection = new List<string>();

            while (!reader.EndOfStream)
            {
                orderedWordsCollection.Add(reader.ReadLine());
            }
            reader.Close();

            shuffledWordsCollection = ShuffleCollection(orderedWordsCollection);
            shuffledWordsCollection = TreeSort.MakeTreeSort(shuffledWordsCollection);

            for (var i = 0; i < orderedWordsCollection.Count; ++i)
            {
                Assert.AreEqual(orderedWordsCollection[i], shuffledWordsCollection[i]);
            }
        }

        [Test]
        public void InsertionSortTest()
        {
            xmlDoc.Load(@"C:\Users\diyll\Laboratory\ConsoleApp2\Path.xml");
            var xmlRoot = xmlDoc.DocumentElement.SelectSingleNode("./InsertionSort");
            var reader = new StreamReader(xmlRoot.InnerText);
            orderedWordsCollection = new List<string>();

            while (!reader.EndOfStream)
            {
                orderedWordsCollection.Add(reader.ReadLine());
            }
            reader.Close();

            shuffledWordsCollection = ShuffleCollection(orderedWordsCollection);
            InsertionSort.MakeInsertSort(shuffledWordsCollection);

            for (var i = 0; i < orderedWordsCollection.Count; ++i)
            {
                Assert.AreEqual(orderedWordsCollection[i], shuffledWordsCollection[i]);
            }
        }

        [Test]
        public void MergeSortTest()
        {
            xmlDoc.Load(@"C:\Users\diyll\Laboratory\ConsoleApp2\Path.xml");
            var xmlRoot = xmlDoc.DocumentElement.SelectSingleNode("./MergeSort");
            var reader = new StreamReader(xmlRoot.InnerText);
            orderedWordsCollection = new List<string>();

            while (!reader.EndOfStream)
            {
                orderedWordsCollection.Add(reader.ReadLine());
            }
            reader.Close();

            shuffledWordsCollection = ShuffleCollection(orderedWordsCollection);
            MergeSort.MakeMergeSort(shuffledWordsCollection);

            for (var i = 0; i < orderedWordsCollection.Count; ++i)
            {
                Assert.AreEqual(orderedWordsCollection[i], shuffledWordsCollection[i]);
            }
        }

        [Test]
        public void HeapSortTest()
        {
            xmlDoc.Load(@"C:\Users\diyll\Laboratory\ConsoleApp2\Path.xml");
            var xmlRoot = xmlDoc.DocumentElement.SelectSingleNode("./HeapSort");
            var reader = new StreamReader(xmlRoot.InnerText);
            orderedWordsCollection = new List<string>();

            while (!reader.EndOfStream)
            {
                orderedWordsCollection.Add(reader.ReadLine());
            }
            reader.Close();

            shuffledWordsCollection = ShuffleCollection(orderedWordsCollection);
            HeapSort.MakeHeapSort(shuffledWordsCollection);

            for (var i = 0; i < orderedWordsCollection.Count; ++i)
            {
                Assert.AreEqual(orderedWordsCollection[i], shuffledWordsCollection[i]);
            }
        }

        [Test]
        public void RadixSortTest()
        {
            xmlDoc.Load(@"C:\Users\diyll\Laboratory\ConsoleApp2\Path.xml");
            var xmlRoot = xmlDoc.DocumentElement.SelectSingleNode("./RadixSort");
            var reader = new StreamReader(xmlRoot.InnerText);
            orderedWordsCollection = new List<string>();

            while (!reader.EndOfStream)
            {
                orderedWordsCollection.Add(reader.ReadLine());
            }
            reader.Close();

            shuffledWordsCollection = ShuffleCollection(orderedWordsCollection);
            RadixSort.MakeRadixSort(shuffledWordsCollection);

            for (var i = 0; i < orderedWordsCollection.Count; ++i)
            {
                Assert.AreEqual(orderedWordsCollection[i], shuffledWordsCollection[i]);
            }
        }

        [Test]
        public void RedBlackTreeSortTest()
        {
            xmlDoc.Load(@"C:\Users\diyll\Laboratory\ConsoleApp2\Path.xml");
            var xmlRoot = xmlDoc.DocumentElement.SelectSingleNode("./RedBlackTreeSort");
            var reader = new StreamReader(xmlRoot.InnerText);
            orderedWordsCollection = new List<string>();

            while (!reader.EndOfStream)
            {
                orderedWordsCollection.Add(reader.ReadLine());
            }
            reader.Close();

            shuffledWordsCollection = ShuffleCollection(orderedWordsCollection);
            shuffledWordsCollection = RedBlackTreeSort.MakeRBSort(shuffledWordsCollection);

            for (var i = 0; i < orderedWordsCollection.Count; ++i)
            {
                Assert.AreEqual(orderedWordsCollection[i], shuffledWordsCollection[i]);
            }
        }

        private List<string> ShuffleCollection(List<string> collection)
        {
            var result = collection.ToList();

            for (var i = 0; i < result.Count; ++i)
            {
                var next = Random.Next(0, result.Count);
                var temp = result[i];
                result[i] = result[next];
                result[next] = temp;
            }
            return result;
        }
    }
}
