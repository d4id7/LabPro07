int x = 5;
DoubleValue(ref x);
System.Console.WriteLine(x);

System.Console.WriteLine();
System.Console.WriteLine("Смена мест переменных (метод Swap)");

Console.Write("Введите первое число: ");
int num1 = int.Parse(Console.ReadLine()!);
Console.Write("Введите второе число: ");
int num2 = int.Parse(Console.ReadLine()!);

Swap(ref num1, ref num2);
System.Console.WriteLine($"num1 = {num1}\nnum2 = {num2}");


System.Console.WriteLine();
System.Console.WriteLine("Проверка деления (метод TryDivide)");

Console.Write("Введите делимое: ");
int num3 = int.Parse(Console.ReadLine()!);
Console.Write("Введите делитель: ");
int num4 = int.Parse(Console.ReadLine()!);

bool canDivide = TryDivide(num3, num4, out double result1);

if (canDivide) {
    System.Console.WriteLine($"Деление прошло успешно. Результат: {result1}");
}
else {
    System.Console.WriteLine("Делить на 0 нельзя!");
}

System.Console.WriteLine();
System.Console.WriteLine("Получения частного и остатка от деления (метод Modulo)");

Console.Write("Введите делимое: ");
int num5 = int.Parse(Console.ReadLine()!);
Console.Write("Введите делитель: ");
int num6 = int.Parse(Console.ReadLine()!);

bool canModulo = TryModulo(num5, num6, out int quotient, out int remainder);

if (canModulo) {
    System.Console.WriteLine($"Деление прошло успешно.");
    System.Console.WriteLine($"Частное: {quotient}");
    System.Console.WriteLine($"Остаток: {remainder}");
} else {
    System.Console.WriteLine("На ноль делить нельзя!");
}


System.Console.WriteLine();
System.Console.WriteLine("Вывод характеристик персонажа (метод PrintStats)");

var stats = new CharacterStats();
Console.Write("Введите HP персонажа: ");
stats.Health = int.Parse(Console.ReadLine()!);
Console.Write("Введите урон персонажа: ");
stats.Damage = int.Parse(Console.ReadLine()!);
Console.Write("Введите броню персонажа: ");
stats.Armor = int.Parse(Console.ReadLine()!);

PrintStats(stats);


System.Console.WriteLine();
System.Console.WriteLine("Форматирование игрового персонажа (метод CharacterFormatting)");

System.Console.WriteLine();
CharacterFormatting("Джерри", 1);

System.Console.WriteLine();
CharacterFormatting("Рик", 10, battleClass: "Изобретатель", height: 1.9);

System.Console.WriteLine();
CharacterFormatting("Морти", 3, showStats: false, battleClass: "Монах");


void DoubleValue(ref int number) {
    number *= 2;
}

void Swap(ref int a, ref int b) {
    int c = a;
    a = b;
    b = c;
}


bool TryDivide(int a, int b, out double result) {
    if (b != 0) {
        result = (double)a / b;
        return true;
    }
    else {
        result = 0;
        return false;
    }
}
    
bool TryModulo(int a, int b, out int quotient, out int remainder) {
    if (b != 0) {
        quotient = a / b;
        remainder = a % b;
        return true;
    }
    else {
        quotient = 0;
        remainder = 0;
        return false;
    }
}


void PrintStats(in CharacterStats stats) {
    System.Console.WriteLine($"HP: {stats.Health}");
    System.Console.WriteLine($"Урон: {stats.Damage}");
    System.Console.WriteLine($"Броня: {stats.Armor}");
}


void CharacterFormatting(string name, int level, string battleClass = "Бард", double height = 1.8, bool showStats = true) {
    System.Console.WriteLine($"{battleClass} {name} {level} уровня");
    if (showStats) {
        System.Console.WriteLine($"Рост: {height}");
        System.Console.WriteLine($"Max HP: {(10 + height) * level}");
        System.Console.WriteLine($"Max Damage: {(10 - height * 2) * level}");
        System.Console.WriteLine($"Max Armor: {(10 - (int) (10 / height)) * level}");
    }
}


struct CharacterStats {
    public int Health;
    public int Damage;
    public int Armor;
}