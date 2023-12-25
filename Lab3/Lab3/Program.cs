//Задание№1
//Создайте структуру Vector с тремя полями X, Y и Z. 
//Для созданной структуры переопределите операторы сложения векторов, умножения векторов, умножения вектора на число, а также логические операторы.
//Для логических операторов используйте сравнение по длине от начала координат.

//Задание№2
//Создайте класс Car со свойствами Name, Engine, MaxSpeed. Переопределите оператор ToString() таким образом, чтобы он возвращал название машины(Name).
//Реализуйте возможность сравнения объектов Car, реализовав интерфейс IEquatable<Car>. 
//Создайте класс CarsCatalog, содержащий коллекцию машин – элементов типа Car и переопределите для него индексатор таким образом, чтобы он возвращал строку с названием машины и типом двигателя.

//Задание№3
//Создайте базовый класс Currency со свойством Value. Создайте 3 производных от Currency класса – CurrencyUSD, CurrencyEUR и CurrencyRUB со свойствами, соответствующими обменному курсу.\
//В каждом из производных классов переопределите операторы преобразования типов таким образом, чтобы можно было явно или неявно преобразовать одну валюту в другую по курсу, заданному пользователем при запуске программы.



using System;
using System.Collections.Generic;

namespace Lab3
{
    struct Vector
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // Переопределение оператора сложения векторов
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        // Переопределение оператора умножения векторов
        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        // Переопределение оператора умножения вектора на число
        public static Vector operator *(Vector vector, double scalar)
        {
            return new Vector(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
        }

        // Переопределение оператора сравнения по длине
        public static bool operator >(Vector a, Vector b)
        {
            return (a.X * a.X + a.Y * a.Y + a.Z * a.Z) > (b.X * b.X + b.Y * b.Y + b.Z * b.Z);
        }

        // Переопределение оператора сравнения по длине
        public static bool operator <(Vector a, Vector b)
        {
            return (a.X * a.X + a.Y * a.Y + a.Z * a.Z) < (b.X * b.X + b.Y * b.Y + b.Z * b.Z);
        }

        // Переопределение оператора равенства
        public static bool operator ==(Vector a, Vector b)
        {
            return (a.X * a.X + a.Y * a.Y + a.Z * a.Z) == (b.X * b.X + b.Y * b.Y + b.Z * b.Z);
        }

        // Переопределение оператора неравенства
        public static bool operator !=(Vector a, Vector b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector other)
            {
                return this == other;
            }
            return false;
        }

        // Переопределение метода ToString
        public override string ToString()
        {
            return $"Vector (X: {X}, Y: {Y}, Z: {Z})";
        }
    }

//========================================================================================================================================================================
    class Car : IEquatable<Car>
    {
        public string Name { get; set; }
        public string Engine { get; set; }
        public int MaxSpeed { get; set; }

        public Car(string name, string engine, int maxSpeed)
        {
            Name = name;
            Engine = engine;
            MaxSpeed = maxSpeed;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(Car other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Engine == other.Engine && MaxSpeed == other.MaxSpeed;
        }

        public override bool Equals(object obj)
        {
            if (obj is Car otherCar)
            {
                return Equals(otherCar);
            }
            return false;
        }

        
    }

    class CarsCatalog
    {
        private List<Car> cars = new List<Car>();

        public string this[int index]
        {
            get
            {
                if (index >= 0 && index < cars.Count)
                {
                    Car car = cars[index];
                    return $"{car.Name} ({car.Engine})";
                }
                else
                {
                    return "Invalid Index";
                }
            }
        }

        public void AddCar(Car car)
        {
            cars.Add(car);
        }
    }

//========================================================================================================================================================================
    public class Currency
    {
        public double Value { get; set; }
        public static double ExchangeRate { get; set; }
        public static double ExchangeRate1 { get; set; }

        public Currency(double value)
        {
            this.Value = value;
        }
    }

    public class CurrencyUSD : Currency
    {
        public CurrencyUSD(double value) : base(value)
        {
        }

        public static explicit operator CurrencyEUR(CurrencyUSD usd)
        {
            double eurValue = usd.Value * Currency.ExchangeRate1;
            return new CurrencyEUR(eurValue);
        }

        public static explicit operator CurrencyRUB(CurrencyUSD usd)
        {
            double rubValue = usd.Value * Currency.ExchangeRate;
            return new CurrencyRUB(rubValue);
        }
    }

    public class CurrencyEUR : Currency
    {
        public CurrencyEUR(double value) : base(value)
        {
        }

        public static explicit operator CurrencyUSD(CurrencyEUR eur)
        {
            double usdValue = eur.Value * Currency.ExchangeRate;
            return new CurrencyUSD(usdValue);
        }

        public static explicit operator CurrencyRUB(CurrencyEUR eur)
        {
            double rubValue = eur.Value * Currency.ExchangeRate;
            return new CurrencyRUB(rubValue);
        }
    }

    public class CurrencyRUB : Currency
    {
        public CurrencyRUB(double value) : base(value)
        {
        }

        public static implicit operator CurrencyUSD(CurrencyRUB rub)
        {
            double usdValue = rub.Value * Currency.ExchangeRate;
            return new CurrencyUSD(usdValue);
        }

        public static implicit operator CurrencyEUR(CurrencyRUB rub)
        {
            double eurValue = rub.Value * Currency.ExchangeRate;
            return new CurrencyEUR(eurValue);
        }
    }

//========================================================================================================================================================================
internal class Program
    {
        static void Main(string[] args)
        {
            //Задание №1
            Console.WriteLine("Задание №1");
            // Создаем два вектора
            Vector vector1 = new Vector(1.0, 2.0, 3.0);
            Vector vector2 = new Vector(3.0, 2.0, 1.0);

            // Сложение векторов
            Vector sum = vector1 + vector2;
            Console.WriteLine($"Сумма векторов: {sum}");

            // Умножение векторов
            Vector product = vector1 * vector2;
            Console.WriteLine($"Произведение векторов: {product}");

            // Умножение вектора на число
            double scalar = 2.0;
            Vector scaledVector = vector1 * scalar;
            Console.WriteLine($"Умножение вектора на число {scalar}: {scaledVector}");

            // Сравнение векторов
            bool isEqual = vector1 == vector2;
            Console.WriteLine($"Вектор1 равен Вектор2: {isEqual}");

            bool isNotEqual = vector1 != vector2;
            Console.WriteLine($"Вектор1 не равен Вектор2: {isNotEqual}");

            bool isGreaterThan = vector1 > vector2;
            Console.WriteLine($"Вектор1 больше Вектор2 по длине: {isGreaterThan}");

            bool isLessThan = vector1 < vector2;
            Console.WriteLine($"Вектор1 меньше Вектор2 по длине: {isLessThan}");

            Console.WriteLine();
//========================================================================================================================================================================
            //Задание №2
            Console.WriteLine("Задание №2");
            Car car1 = new Car("Toyota Camry", "V6", 200);
            Car car2 = new Car("Honda Civic", "Inline-4", 180);

            Console.WriteLine("Машина №1");
            Console.WriteLine(car1); // Выводит название машины (Name)
            Console.WriteLine("Машина №2");
            Console.WriteLine(car2); // Выводит название машины

            Console.WriteLine("Сравним машины по содержанию:"); // Сравнивает машины по содержанию
            Console.WriteLine(car1.Equals(car2));

            CarsCatalog catalog = new CarsCatalog();
            catalog.AddCar(car1);
            catalog.AddCar(car2);

            Console.WriteLine(catalog[0]); // Выводит строку с названием и типом двигателя машины
            Console.WriteLine(catalog[1]); // Выводит строку с названием и типом двигателя машины
            Console.WriteLine(catalog[2]); // Выводит "Invalid Index", так как индекс за пределами списка

            //========================================================================================================================================================================
            Console.WriteLine();
            Console.WriteLine("Задание №3");
            Currency.ExchangeRate = GetUserExchange();
            Currency.ExchangeRate1 = GetUserExchange1();

            CurrencyUSD rub = new CurrencyUSD(100);
            CurrencyEUR eur = (CurrencyEUR)rub;
            CurrencyRUB usd = (CurrencyRUB)rub;
            CurrencyUSD usd2 = rub;
            
            Console.WriteLine("RUB: " + rub.Value);
            Console.WriteLine("USD: " + usd.Value);
            Console.WriteLine("EUR: " + eur.Value);
            
            Console.ReadLine();
        }
        public static double GetUserExchange()
        {
            Console.WriteLine("Введите курс обмена dol-rub: ");
            double rate = double.Parse(Console.ReadLine());
            return rate;
        }
        public static double GetUserExchange1()
        {
            Console.WriteLine("Введите курс обмена eur-rub: ");
            double rate = double.Parse(Console.ReadLine());
            return rate;
        }
    }
}