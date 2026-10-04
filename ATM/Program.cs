using System.ComponentModel;
using System.IO.Pipelines;

System.Console.WriteLine("Добро пожаловать в банк \"VSB\"!");
System.Console.WriteLine("Банкомат");

CashbackCategories categories = new CashbackCategories();
categories.Category1 = "Аптеки";
categories.Category2 = "Продуктовые магазины";
categories.Category3 = "Кафе";

double balance = 23643.53;

string menuChoice = "0";
bool flag = true;

do {
    if (flag) {
        System.Console.WriteLine();
        System.Console.WriteLine("Главное меню.");

        System.Console.WriteLine("1 - Категории кэшбэка");
        System.Console.WriteLine("2 - Внести/вывести средства");
        System.Console.WriteLine("0 - Выход");
        Console.Write("Выберите номер раздела: ");
        menuChoice = Console.ReadLine()!;
    }

    switch (menuChoice) {
        case "1":
            flag = false;

            System.Console.WriteLine();
            System.Console.WriteLine("Главное меню. Кэшбэк.");

            System.Console.WriteLine("1 - Просмотреть категории кэшбэка");
            System.Console.WriteLine("2 - Просмотреть категории кэшбэка и вывести справку");
            System.Console.WriteLine("3 - Поменять местами категории кэшбэка");
            System.Console.WriteLine("0 - Назад");
            Console.Write("Введите номер действия: ");
            string cashbackChoice = Console.ReadLine()!;

            switch (cashbackChoice) {
                case "1":
                    System.Console.WriteLine();
                    PrintCategories(categories);
                    Console.ReadLine();
                    break;
                case "2":
                    System.Console.WriteLine();
                    PrintCategories(categories, true);
                    Console.ReadLine();
                    break;
                case "3":
                    System.Console.WriteLine();
                    System.Console.WriteLine("Главное меню. Кэшбэк. Поменять местами категории кэшбэка.");

                    Console.Write("Введите номер первой категории: ");
                    string swapNum1 = Console.ReadLine()!;
                    Console.Write("введите номер второй категории: ");
                    string swapNum2 = Console.ReadLine()!;

                    PrintAnswerOnSwap(swapNum1, swapNum2, ref categories);
                    Console.ReadLine();
                    break;
                case "0": flag = true; break;
                default:
                    System.Console.WriteLine();
                    System.Console.WriteLine("Такого действия не существует. Попробуйте снова.");
                    Console.ReadLine();
                    break;
            }
            break;
        case "2":
            flag = false;

            System.Console.WriteLine();
            System.Console.WriteLine("Главное меню. Внести/вывести средства.");

            System.Console.WriteLine($"Текущий баланс: {balance} руб.");
            Console.ReadLine();

            System.Console.WriteLine("1 - Внести средства на карту");
            System.Console.WriteLine("2 - Вывести средства с карты");
            System.Console.WriteLine("0 - Назад");
            Console.Write("Введите номер действия: ");
            string operationChoice = Console.ReadLine()!;

            switch (operationChoice) {
                case "1":
                case "2":
                    bool isValid;
                    int cash;

                    do {
                        Console.Write("Введите сумму: ");
                        string cashInput = Console.ReadLine()!;
                        isValid = int.TryParse(cashInput, out cash);

                        if (!isValid || cash <= 0) {
                            System.Console.WriteLine("Неверный ввод. Попробуйте снова.");
                            Console.ReadLine();
                        }
                    } while (!isValid || cash <= 0);

                    System.Console.WriteLine();
                    PrintAnswerOnOperation(cash, operationChoice, ref balance);
                    Console.ReadLine();
                    break;
                case "0": flag = true; break;
                default:
                    System.Console.WriteLine();
                    System.Console.WriteLine("Такого действия не существует. Попробуйте снова.");
                    Console.ReadLine();
                    break;
            }
            break;
        case "0":
            System.Console.WriteLine();
            System.Console.WriteLine("Благодарим Вас за использование нашего VSB-банкомата.");
            System.Console.WriteLine("До свидания!");
            break;
        default:
            System.Console.WriteLine();
            System.Console.WriteLine("Такого пункта не существует. Попробуйте снова.");
            Console.ReadLine();
            break;
    }
} while (menuChoice != "0");


void SwapCategories(ref string category1, ref string category2) {
    string temp = category1;
    category1 = category2;
    category2 = temp;
}

/*
В случае с categories я использую ref, т.к. нужно изменить категории
*/
void PrintAnswerOnSwap(string swapNum1, string swapNum2, ref CashbackCategories categories) {
    System.Console.WriteLine();
    if (!"123".Contains(swapNum1) || !"123".Contains(swapNum2)) {
        System.Console.WriteLine("Введённых категорий не существует.");
    }
    else if (swapNum1 == swapNum2) {
        System.Console.WriteLine("Нельзя поменять местами одну и ту же категорию.");
    }
    else if (swapNum1 == "1" || swapNum2 == "1") {
        if (swapNum1 == "2" || swapNum2 == "2") {
            SwapCategories(ref categories.Category1, ref categories.Category2);
        }
        else {
            SwapCategories(ref categories.Category1, ref categories.Category3);
        }
        System.Console.WriteLine($"Категории {swapNum1} и {swapNum2} успешно изменены.");
    }
    else {
        SwapCategories(ref categories.Category2, ref categories.Category3);
        System.Console.WriteLine($"Категории {swapNum1} и {swapNum2} успешно изменены.");
    }
}

/*
categories передаётся с помощью in, т.к. он нужен только для чтения
printRef - необязательный параметр для вывода справки
*/
void PrintCategories(in CashbackCategories categories, bool printRef = false) {
    if (printRef) {
        System.Console.WriteLine("В нашем банке реализована система кэшбэка \"Три категории\"");
        System.Console.WriteLine("\nКак это работает?");
        System.Console.WriteLine("У вас есть три категории кэшбэка. В зависимости от того, под каким номером они находятся, такой процент с покупок они возвращают Вам на карту.");
        System.Console.WriteLine("(Новые категории начинают действовать только со следующего месяца после их смены)");
    }

    System.Console.WriteLine("Категории кэшбэка:");
    System.Console.WriteLine($"1. {categories.Category1} - 15 %");
    System.Console.WriteLine($"2. {categories.Category2} - 10 %");
    System.Console.WriteLine($"3. {categories.Category3} - 5 %");
}

bool OperateCard(int cash, double balance, out double result) {
    if (-cash <= balance) {
        result = cash;
        return true;
    }
    result = 0;
    return false;
}


/*
balance передаётся через ref, т.к. уже существующая переменная должна изменить значение
*/
void PrintAnswerOnOperation(int cash, string operationChoice, ref double balance) {
    if (operationChoice == "2") {
        cash = -cash;
    }

    bool canOperate = OperateCard(cash, balance, out double result);

    if (canOperate) {
        System.Console.WriteLine("Операция выполнена успешно.");
        System.Console.WriteLine($"| {result} руб. |");
        balance += result;
    }
    else {
        System.Console.WriteLine("Недостаточно средств.");
    }
}

struct CashbackCategories {
    public string Category1;
    public string Category2;
    public string Category3;
}
