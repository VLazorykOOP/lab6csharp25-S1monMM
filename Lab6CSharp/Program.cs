using System;
using System.Collections;

namespace Lab6Charp
{
    interface Person
    {
        string Name { get; set; }
        int Age { get; set; }
        int Salary { get; set; }
        void Show();
    }

    class Student : Person, ICloneable
    {
        private string name;
        private int age;
        private int salary;
        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }
        public int Age
        {
            get { return this.age; }
            set { this.age = value; }
        }
        public int Salary
        {
            get { return this.salary; }
            set { this.salary = value; }
        }

        public Student(string n, int a, int s)
        {
            name = n;
            age = a;
            salary = s;
        }

        public void Show()
        {
            Console.WriteLine("Student: {0}, Age: {1}, Salary: {2}", name, age, salary);
        }

        public object Clone()
        {
            Console.WriteLine("Cloning Student!");
            return new Student(name, age, salary);
        }
    }

    class Teacher : Person, ICloneable
    {
        private string name;
        private int age;
        private int salary;
        private string subject;
        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }
        public int Age
        {
            get { return this.age; }
            set { this.age = value; }
        }
        public int Salary
        {
            get { return this.salary; }
            set { this.salary = value; }
        }
        public Teacher(string n, int a, int s, string sub)
        {
            name = n;
            age = a;
            salary = s;
            subject = sub;
        }

        public void Show()
        {
            Console.WriteLine("Teacher: {0}, Age: {1}, Salary: {2}", name, age, salary);
        }

        public object Clone()
        {
            Console.WriteLine("Cloning Teacher!");
            return new Teacher(name, age, salary, subject);
        }
    }

    interface Figure
    {
        void Show();
        double GetPerimeter();
        double GetArea();
    }

    class Circle : Figure, ICloneable
    {
        private double radius;
        public Circle(double radius) { this.radius = radius; }
        public double GetPerimeter() {return 2 * Math.PI * radius;}
        public double GetArea() {return Math.PI * radius * radius;}

        public void Show()
        {
            Console.WriteLine("Circle: Radius = {0}, Perimeter = {1}, Area = {2}", radius, GetPerimeter(), GetArea());
        }

        public object Clone()
        {
            Console.WriteLine("Cloning Circle!");
            return new Circle(radius);
        }
    }

    class Rectangle : Figure, ICloneable
    {
        protected double width;
        protected double height;

        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        public double GetPerimeter() {return 2 * (width + height);}
        public double GetArea() { return width * height;}

        public void Show()
        {
            Console.WriteLine("Width:{0}\nHeight:{1}\nPerimeter:{2}\nArea:{3}", width, height, GetPerimeter(), GetArea());
        }

        public object Clone()
        {
            Console.WriteLine("Cloning Rectangle!");
            return new Rectangle(width, height);
        }
    }

    class Triangle : Figure, ICloneable
    {
        private double a, b, c;
        public Triangle(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public double GetPerimeter() {return a + b + c;}

        public double GetArea()
        {
            double p = GetPerimeter() / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public void Show()
        {
            Console.WriteLine("Triangle: Sides = {0}, {1}, {2}, Perimeter = {3}, Area = {4}", a, b, c, GetPerimeter(), GetArea());
        }

        public object Clone()
        {
            Console.WriteLine("Cloning Triangle!");
            return new Triangle(a, b, c);
        }
    }

    public class MyVectorException : ApplicationException
    {
        public MyVectorException() { }
        public MyVectorException(string message) : base(message) { }
    }

    class VectorInt : IEnumerable
    {
        protected int[] IntArray;
        protected uint size;

        public VectorInt(uint size = 1, int defaultValue = 0)
        {
            this.size = size;
            IntArray = new int[size];
            for (int i = 0; i < size; i++)
                IntArray[i] = defaultValue;
        }

        public int this[int i]
        {
            get
            {
                if (i >= 0 && i < size)
                    return IntArray[i];
                throw new MyVectorException($"Index {i} is out of bounds (0 to {size - 1})");
            }
            set
            {
                if (i >= 0 && i < size)
                    IntArray[i] = value;
                else
                    throw new MyVectorException($"Cannot set value — index {i} is invalid.");
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < size; i++)
                yield return IntArray[i];
        }
    }

    class Program
    {
        static void task1()
        {
            Student st = new Student("Ivan", 20, 1000);
            st.Show();
            Student st2 = (Student)st.Clone();
            st2.Show();
            Teacher tc = new Teacher("Petro", 45, 3000, "CS");
            tc.Show();
            Teacher tc2 = (Teacher)tc.Clone();
            tc2.Show();
        }

        static void task2()
        {
            Circle circle = new Circle(5);
            circle.Show();
            Circle circle2 = (Circle)circle.Clone();
            circle2.Show();

            Rectangle rect = new Rectangle(4, 6);
            rect.Show();
            Rectangle rect2 = (Rectangle)rect.Clone();
            rect2.Show();

            Triangle triangle = new Triangle(3, 4, 5);
            triangle.Show();
            Triangle triangle2 = (Triangle)triangle.Clone();
            triangle2.Show();
        }

        static void task3()
        {
            try
            {
                VectorInt vec = new VectorInt(3, 7);
                // Console.WriteLine("Accessing element at index 10...");
                // Console.WriteLine(vec[10]);

                Console.WriteLine("Attempting to assign VectorInt to string array...");
                object[] array = new string[1];
                array[0] = vec;
            }
            catch (MyVectorException ex)
            {
                Console.WriteLine("MyVectorException caught: " + ex.Message);
            }
            catch (ArrayTypeMismatchException ex)
            {
                Console.WriteLine("ArrayTypeMismatchException caught: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General exception caught: " + ex.Message);
            }

            Console.WriteLine("Program finished.");
            Console.ReadLine();
        }

        static void task4()
        {
            Console.Write("Enter vector size: ");
            uint size = Convert.ToUInt32(Console.ReadLine());

            Console.Write("Enter value to fill: ");
            int value = Convert.ToUInt16(Console.ReadLine());

            VectorInt vec = new VectorInt(size, value);

            Console.WriteLine("Vector elements using foreach:");
            foreach (int num in vec)
            {
                Console.Write(num + " ");
            }

            Console.WriteLine();
        }

        static void choose_task()
        {
            Console.Write("1. Person\n2. Figure\n3. Array exception\n4. Foreach in Array\nYour choice: ");
            int answer = Convert.ToInt16(Console.ReadLine());

            switch (answer)
            {
                case 1:
                    task1();
                    break;
                case 2:
                    task2();
                    break;
                case 3:
                    task3();
                    break;
                case 4:
                    task4();
                    break;
            }

            Console.Write("\n\n\n");
            choose_task();
        }

        static void Main(string[] args)
        {
            choose_task();
        }
    }
}
