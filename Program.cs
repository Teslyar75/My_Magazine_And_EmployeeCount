/*Завдання 1
В одному з попередніх практичних завдань ви створювали клас «Журнал». 
Додайте до вже створеного класу інформацію про кількість працівників. 
Виконайте навантаження 
+ (для збільшення кількості працівників на вказану кількість),
— (для зменшення кількості працівників на вказану кількість), 
== (перевірка на рівність кількості 
працівників), 
< і > (перевірка на меншу чи більшу кількість працівників), 
!= і Equals. Використовуйте механізм властивостей полів класу.*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Magazine_And_EmployeeCount
{
    class Magazine
    {
        private string name;
        private int yearFounded;
        private string description;
        private string contactPhone;
        private string email;
        private int employeeCount;

        public Magazine(string name, int yearFounded, string description, string contactPhone, string email, int employeeCount)
        {
            this.name = name;
            this.yearFounded = yearFounded;
            this.description = description;
            this.contactPhone = contactPhone;
            this.email = email;
            this.employeeCount = employeeCount;
        }

        public void IncreaseEmployeeCount(int count)
        {
            if (count > 0)
            {
                employeeCount += count;
            }
        }

        public void DecreaseEmployeeCount(int count)
        {
            if (count > 0 && count <= employeeCount)
            {
                employeeCount -= count;
            }
        }

        public bool Equals(Magazine otherMagazine)
        {
            return this.employeeCount == otherMagazine.employeeCount;
        }

        public static bool operator ==(Magazine magazine1, Magazine magazine2)
        {
            return magazine1.employeeCount == magazine2.employeeCount;
        }

        public static bool operator !=(Magazine magazine1, Magazine magazine2)
        {
            return magazine1.employeeCount != magazine2.employeeCount;
        }

        public static bool operator <(Magazine magazine1, Magazine magazine2)
        {
            return magazine1.employeeCount < magazine2.employeeCount;
        }

        public static bool operator >(Magazine magazine1, Magazine magazine2)
        {
            return magazine1.employeeCount > magazine2.employeeCount;
        }

        public static Magazine operator +(Magazine magazine, int count)
        {
            magazine.IncreaseEmployeeCount(count);
            return magazine;
        }

        public static Magazine operator -(Magazine magazine, int count)
        {
            magazine.DecreaseEmployeeCount(count);
            return magazine;
        }

        public string GetName()
        {
            return name;
        }

        public int GetYearFounded()
        {
            return yearFounded;
        }

        public string GetDescription()
        {
            return description;
        }

        public string GetContactPhone()
        {
            return contactPhone;
        }

        public string GetEmail()
        {
            return email;
        }

        public int GetEmployeeCount()
        {
            return employeeCount;
        }

        public void DisplayData()
        {
            Console.WriteLine("Название журнала: " + GetName());
            Console.WriteLine("Год основания: " + GetYearFounded());
            Console.WriteLine("Описание журнала: " + GetDescription());
            Console.WriteLine("Контактный телефон: " + GetContactPhone());
            Console.WriteLine("Email: " + GetEmail());
            Console.WriteLine("Количество работников: " + GetEmployeeCount());
        }

        public void InputData()
        {
            Console.Write("Введите год основания журнала: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                yearFounded = year;
            }
            else
            {
                Console.WriteLine("Некорректный ввод для года.");
                yearFounded = 0;
            }

            Console.Write("Введите описание журнала: ");
            description = Console.ReadLine();

           /* Console.Write("Введите контактный телефон: ");
            contactPhone = Console.ReadLine();*/

            /*Console.Write("Введите email: ");
            email = Console.ReadLine();*/

            Console.Write("Введите количество работников: ");
            if (int.TryParse(Console.ReadLine(), out int count))
            {
                employeeCount = count;
            }
        }
    }

    class Program
    {
        static List<Magazine> magazines = new List<Magazine>();

        static void Main()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Создать журнал.");
                Console.WriteLine("2. Изменить количество работников в журналах.");
                Console.WriteLine("3. Вывести информацию о журналах.");
                Console.WriteLine("4. Сравнить количество работников в журналах.");
                Console.WriteLine("5. Выйти из программы.");
                Console.Write("Выберите опцию (1-5): ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            CreateMagazine();
                            break;
                        case 2:
                            ChangeEmployeeCounts();
                            break;
                        case 3:
                            DisplayMagazinesInfo();
                            break;
                        case 4:
                            CompareMagazines();
                            break;
                        case 5:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Нет такого пункта. Пожалуйста, выберите пункт от 1 до 5.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Нет такого пункта. Пожалуйста, выберите опцию от 1 до 5.");
                }
            }
        }

        static void CreateMagazine()
        {
            Console.Write("Введите название журнала: ");
            string name = Console.ReadLine();

            if (magazines.Exists(magazine1 => magazine1.GetName() == name))
            {
                Console.WriteLine("Журнал с таким названием уже существует. Введите другое название.");
                return;
            }

            Console.Write("Введите email: ");
            string email = Console.ReadLine();

            if (magazines.Exists(magazine1 => magazine1.GetEmail() == email))
            {
                Console.WriteLine("Журнал с таким email уже существует. Введите другой email.");
                return;
            }

            Console.Write("Введите контактный телефон: ");
            string contactPhone = Console.ReadLine();

            if (magazines.Exists(magazine1 => magazine1.GetContactPhone() == contactPhone))
            {
                Console.WriteLine("Журнал с таким номером телефона уже существует. Введите другой номер телефона.");
                return;
            }

            Magazine magazine = new Magazine(name, 0, "", contactPhone, email, 0);
            magazine.InputData();
            magazines.Add(magazine);
            Console.WriteLine("Журнал создан успешно.");
        }

        static void ChangeEmployeeCounts()
        {
            if (magazines.Count < 2)
            {
                Console.WriteLine("Сначала создайте два журнала для сравнения.");
                return;
            }

            // Выбираем первый и второй журнал из списка
            Magazine magazine1 = magazines[0];
            Magazine magazine2 = magazines[1];

            Console.Write("Введите изменение количества работников для первого журнала (+/-): ");
            if (int.TryParse(Console.ReadLine(), out int change1))
            {
                magazine1 += change1;
                Console.WriteLine($"В {magazine1.GetName()} на данный момент работает: {magazine1.GetEmployeeCount()}");
            }
            else
            {
                Console.WriteLine("Некорректное изменение количества работников.");
            }

            Console.Write("Введите изменение количества работников для второго журнала (+/-): ");
            if (int.TryParse(Console.ReadLine(), out int change2))
            {
                magazine2 += change2;
                Console.WriteLine($"В {magazine2.GetName()} на данный момент работает: {magazine2.GetEmployeeCount()}");
            }
            else
            {
                Console.WriteLine("Некорректное изменение количества работников.");
            }
        }


        static void DisplayMagazinesInfo()
        {
            if (magazines.Count == 0)
            {
                Console.WriteLine("Нет созданных журналов для отображения.");
                return;
            }

            foreach (var magazine in magazines)
            {
                magazine.DisplayData();
                Console.WriteLine();
            }
        }

        static void CompareMagazines()
        {
            if (magazines.Count < 2)
            {
                Console.WriteLine("Сначала создайте два журнала для сравнения.");
                return;
            }

            Magazine magazine1 = magazines[0];
            Magazine magazine2 = magazines[1];

            if (magazine1 == magazine2)
            {
                Console.WriteLine("Журналы имеют одинаковое количество работников.");
            }
            else if (magazine1 < magazine2)
            {
                Console.WriteLine($"{magazine1.GetName()} имеет меньше работников, чем {magazine2.GetName()}.");
            }
            else
            {
                Console.WriteLine($"{magazine1.GetName()} имеет больше работников, чем {magazine2.GetName()}.");
            }
        }
    }
}

