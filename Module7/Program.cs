using System;

namespace Module7
{
    struct Coordinates
    {
        public double X;
        public double Y;

        public Coordinates(double x, double y)
        {
            X = x;
            Y = y;
        }
        public static Coordinates operator +(Coordinates a, Coordinates b)
        {
            return new Coordinates
            {
                X = a.X + b.X,
                Y = a.Y + b.Y
            };
        }
        public static Coordinates operator -(Coordinates a, Coordinates b)
        {
            return new Coordinates
            {
                X = a.X - b.X,
                Y = a.Y - b.Y
            };
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
    abstract class Delivery
    {
        public string Address;
    }
    abstract class Employee<TId>
    {
        public string Name 
        {
            get
            {
                return Name;
            }
            set
            {
                if (value != null && value != "")
                {
                    Name = value;
                }
                else
                {
                    Console.WriteLine("Имя не может быть пустым");
                }
            } 
        }
        public int Age 
        {
            get
            {
                return Age;
            }
            set
            {
                if (value > 17)
                {
                    Age = value;
                }
                else
                {
                    Console.WriteLine("Возраст должен быть числом больше 17");
                }
            }
        }
        public int Salary
        {
            get
            {
                return Salary;
            }
            set
            {
                if (value > 0)
                {
                    Salary = value;
                }
                else
                {
                    Console.WriteLine("Зарплата должна быть больше нуля");
                }
            }
        }
        public TId Id { get; set; }
        public Employee(string name, int age, TId id)
        {
            Name = name;
            Age = age;
            Id = id;
            Salary = 60000;
        }
        public Employee(string name, int age, int salary, TId id) : this(name,age,id)
        {
            Salary = salary;
        }
        public virtual void Fire()
        {
            Console.WriteLine("Работник по имени {0} с идентификатором {1} был уволен", Name, Id);
        }
    }
    class DeliveryPerson<TId> : Employee<TId>
    {
        public bool isOccupied { get; set; }
        public DeliveryPerson(string name, int age, TId id) : base(name, age, id) { }
        public DeliveryPerson(string name, int age, int salary, TId id) : base(name, age, salary, id) { }
        public override void Fire()
        {
            base.Fire();
            isOccupied = false;
        }
    }

    class HomeDelivery : Delivery
    {
        public DeliveryPerson<string> DeliveryPerson 
        {
            get
            {
                if (!DeliveryPerson.isOccupied)
                {
                    return null;
                }
                else
                {
                    return DeliveryPerson;
                }
            }
            private set { }
        }
        public void AssignDeliveryPerson(DeliveryPerson<string> deliveryPerson)
        {
            if (deliveryPerson.isOccupied)
            {
                Console.WriteLine("Доставщик уже занят");
            }
            else if (DeliveryPerson.isOccupied)
            {
                Console.WriteLine("На эту доставку уже назначили доставщика");
            }
            else
            {
                DeliveryPerson = deliveryPerson;
                DeliveryPerson.isOccupied = true;
            }
        }
        public void UnassignDeliveryPerson()
        {
            Console.WriteLine("{0} больше не назначен на заказ по причине: доставка выполнена", DeliveryPerson.Id);
            DeliveryPerson.isOccupied = false;
        }
        public void UnassignDeliveryPerson(string reason)
        {
            Console.WriteLine("{0} больше не назначен на заказ по причине: {1}", DeliveryPerson.Id, reason);
            DeliveryPerson.isOccupied = false;
        }
    }

    class PickPointDelivery : Delivery
    {
        public string DeliveryCompanyName { get; set; }
    }

    class ShopDelivery : Delivery
    {
        public string ShopName { get; set; }
    }
    class Product<TId>
    {
        public string Name;

        public string Description;

        public decimal Price;

        public TId ProductId;

        public Product(string name, string description, decimal price, TId productId)
        {
            Name = name;
            Description = description;
            Price = price;
            ProductId = productId;
        }
    }
    class ProductCollection<TId>
    {
        private Product<TId>[] collection;

        public ProductCollection(Product<TId>[] collection)
        {
            this.collection = collection;
        }

        public Product<TId> this[int index]
        {
            get
            {
                if (index >= 0 && index < collection.Length)
                {
                    return collection[index];
                }
                else
                {
                    return null;
                }
            }

            private set
            {
                if (index >= 0 && index < collection.Length)
                {
                    collection[index] = value;
                }
            }
        }
    }

    class Order<TDelivery,
    TStruct> where TDelivery : Delivery
    {
        public TDelivery Delivery;

        public int Number;

        public string Description;

        public ProductCollection<string> products;

        public Order(TDelivery delivery, int number, string description)
        {
            Delivery = delivery;
            Number = number;
            Description = description;
        }
        public void DisplayAddress()
        {
            Console.WriteLine(Delivery.Address);
        }

        
    }
}
