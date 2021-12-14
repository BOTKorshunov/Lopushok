using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSUniversalLib;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Проверка на корректных данных
        /// </summary>
        [TestMethod]
        public void GetQuantityForProduct_CorrectData()
        {
            int expected = 114148;
            int actual = Calculation.GetQuantityForProduct(3, 1, 15, 20, 45);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Проверка на некорректный типа продукта
        /// </summary>
        [TestMethod]
        public void GetQuantityForProduct_IncorrectProductType()
        {
            int expected = -1;
            int actual = Calculation.GetQuantityForProduct(0, 4, 11, 15, 20);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Проверка на некорректный тип материала
        /// </summary>
        [TestMethod]
        public void GetQuantityForProduct_IncorrectMaterialType()
        {
            int expected = -1;
            int actual = Calculation.GetQuantityForProduct(2, -5, 14, 11, 9);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Проверка на отрицательное количество
        /// </summary>
        [TestMethod]
        public void GetQuantityForProduct_IncorrectNegativeCount()
        {
            int expected = -1;
            int actual = Calculation.GetQuantityForProduct(2, 2, -16, 25, 16);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Проверка на нулевое количество
        /// </summary>
        [TestMethod]
        public void GetQuantityForProduct_IncorrectZeroCount()
        {
            int expected = -1;
            int actual = Calculation.GetQuantityForProduct(2, 2, 0, 25, 16);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Проверка на отрицательную ширину
        /// </summary>
        [TestMethod]
        public void GetQuantityForProduct_IncorrectNegativeWidth()
        {
            int expected = -1;
            int actual = Calculation.GetQuantityForProduct(3, 1, 13, -11, 10);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Проверка на нулевую ширину
        /// </summary>
        [TestMethod]
        public void GetQuantityForProduct_IncorrectZeroWidth()
        {
            int expected = -1;
            int actual = Calculation.GetQuantityForProduct(3, 1, 13, 0, 10);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Проверка на отрицательную длину
        /// </summary>
        [TestMethod]
        public void GetQuantityForProduct_IncorrectNegativeLength()
        {
            int expected = -1;
            int actual = Calculation.GetQuantityForProduct(3, 1, 13, 40, -4);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Проверка на нулевую длину
        /// </summary>
        [TestMethod]
        public void GetQuantityForProduct_IncorrectZeroLength()
        {
            int expected = -1;
            int actual = Calculation.GetQuantityForProduct(3, 1, 13, 40, 0);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Проверка на некорректных данных
        /// </summary>
        [TestMethod]
        public void GetQuantityForProduct_IncorrectDataAll()
        {
            int expected = -1;
            int actual = Calculation.GetQuantityForProduct(-1, -19, -14, -10, -0);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Проверка на ширину с плавающей точкой
        /// </summary>
        [TestMethod]
        public void GetQuantityForProduct_FloatWidth()
        {
            int expected = 13309;
            int actual = Calculation.GetQuantityForProduct(2, 2, 11, 20.14f, 24);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Проверка на длину с плавающей точкой
        /// </summary>
        [TestMethod]
        public void GetQuantityForProduct_FloatLength()
        {
            int expected = 2983;
            int actual = Calculation.GetQuantityForProduct(1, 1, 8, 33, 10.24f);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Проверка на длину и ширину с плавающей точкой №1
        /// </summary>
        [TestMethod]
        public void GetQuantityForProduct_FloatWidthAndLength_1()
        {
            int expected = 12671;
            int actual = Calculation.GetQuantityForProduct(3, 1, 8, 12.396f, 15.111f);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Проверка на длину и ширину с плавающей точкой №2
        /// </summary>
        [TestMethod]
        public void GetQuantityForProduct_FloatWidthAndLength_2()
        {
            int expected = 1493;
            int actual = Calculation.GetQuantityForProduct(1, 2, 8, 9.444f, 17.94f);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Проверка на длину и ширину с плавающей точкой №3
        /// </summary>
        [TestMethod]
        public void GetQuantityForProduct_FloatWidthAndLength_3()
        {
            int expected = 3751;
            int actual = Calculation.GetQuantityForProduct(2, 2, 8, 12.396f, 15.111f);
            Assert.AreEqual(expected, actual);
        }
    }
}
