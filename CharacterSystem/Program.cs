using Microsoft.Win32.SafeHandles;

System.Console.WriteLine("Система персонажа");

CharacterStats hero = new CharacterStats();

Console.Write("Введите имя своего персонажа: ");
hero.Name = Console.ReadLine()!;
Console.Write("Введите максимальное hp своего персонажа (больше 30): ");
hero.MaxHP = int.Parse(Console.ReadLine()!);
double heroHP = hero.MaxHP;
Console.Write("Введите максимальную ману своего персонажа (больше 20): ");
hero.MaxMana = int.Parse(Console.ReadLine()!);
double heroMana = hero.MaxMana;
Console.Write("Введите урон своего персонажа (больше 10): ");
hero.Damage = int.Parse(Console.ReadLine()!);
Console.Write("Введите броню своего персонажа (больше 5): ");
hero.Armor = int.Parse(Console.ReadLine()!);
Console.Write("Введите урон оружия своего персонажа: ");
hero.WeaponLevel = int.Parse(Console.ReadLine()!);

CharacterStats ogre = new CharacterStats();
ogre.Name = "Огр Мэджай";
ogre.MaxHP = 50;
double ogreHP = ogre.MaxHP;
ogre.MaxMana = 0;
double ogreMana = ogre.MaxMana;
ogre.Damage = 20;
ogre.Armor = 15;
ogre.WeaponLevel = 1;

double ogreAttack = TotalDamage(ogre.Damage, ogre.WeaponLevel, hero.Armor);
double heroAttack = TotalDamage(hero.Damage, hero.WeaponLevel, ogre.Armor);

System.Console.WriteLine();
System.Console.WriteLine("Вы встретили Огра!");
PrintStats(ogre, ogre.MaxHP, ogre.MaxMana, false);

System.Console.WriteLine();
System.Console.WriteLine($"{ogre.Name} вас атакует. Он нанёс {ogreAttack}");
MinusHP(ref heroHP, ogreAttack);

System.Console.WriteLine();
PrintStats(hero, heroHP, heroMana, false);

System.Console.WriteLine();
bool canCastFireball = CanCastSpell("Огненный шар", ref heroMana, 99, out double fireballDamage);
if (canCastFireball) {
    System.Console.WriteLine($"Враг потерял {fireballDamage} hp");
    MinusHP(ref ogreHP, fireballDamage);
}
else {
    System.Console.WriteLine("Недостаточно маны");
}

System.Console.WriteLine();
bool canCastFrostArrow = CanCastSpell("Ледяная стрела", ref heroMana, 3, out double frostArrowDamage);
if (canCastFrostArrow) {
    System.Console.WriteLine($"Враг потерял {frostArrowDamage} hp");
    MinusHP(ref ogreHP, frostArrowDamage);
}
else {
    System.Console.WriteLine("Недостаточно маны");
}

System.Console.WriteLine();
PrintStats(ogre, ogreHP, ogreMana, false);

System.Console.WriteLine();
PrintStats(hero, heroHP, heroMana, longVariant: true);


/* 
ref выбран для hp, потому что здоровье должно уменьшаться при получении урона (ЛОГИЧНО)
damage не меняется, а просто передаётся в метод для изменения hp
*/
void MinusHP(ref double hp, double damage) {
    hp -= damage;
}

/*
для вычисления итогового урона в метод передаются damage, weaponLevel и enemyArmorLevel, которые никак не изменяются сами
сам же метод возвращает результат вычисления - итоговый урон
*/
double TotalDamage(double damage, int weaponLevel, int enemyArmorLevel) {
    double result = damage + (weaponLevel * 2) - enemyArmorLevel;

    if (result <= 0) {
        return 0;
    }
    return result;
}

/*
spellName никак не меняется, т.к. используется лишь для вывода текста; spellLevel также не меняется, т.к. нужен только для вычислений
mana передаётся через ref, потому что после использования заклинания она должна уменьшится
spellDamage передаётся через out, т.к.персонаж сможет использовать заклинание только, если у него будет хватать на него маны
*/
bool CanCastSpell(string spellName, ref double mana, int spellLevel, out double spellDamage) {
    System.Console.WriteLine($"Вы используете {spellName}");
    double manaCost = spellLevel * 2.71;
    if (mana >= manaCost) {
        spellDamage = manaCost + (spellLevel ^ 2);
        mana -= manaCost;
        return true;
    }

    spellDamage = 0;
    return false;
}

/*
stats, hp и mana передано через in, т.к. они нужны только для чтения (вывода характеристик)
longVariant - это необязательный параметр для вывода сокращённой или полной версии характеристик
*/
void PrintStats(in CharacterStats stats, in double hp, in double mana, bool longVariant = true) {
    System.Console.WriteLine($"\t{stats.Name}");

    if (longVariant) {
        System.Console.WriteLine($"| HP: {hp}/{stats.MaxHP}");
        System.Console.WriteLine($"| Мана: {mana}/{stats.MaxMana}");
        System.Console.WriteLine($"| Урон: {stats.Damage}");
        System.Console.WriteLine($"| Броня: {stats.Armor}");
        System.Console.WriteLine($"| Уровень оружия: {stats.WeaponLevel}");
    }
    else {
        System.Console.WriteLine($"| HP: {hp}");
        System.Console.WriteLine($"| Мана: {mana}");
        System.Console.WriteLine($"| Урон: {stats.Damage}");
        System.Console.WriteLine($"| Броня: {stats.Armor}");
    }
}

struct CharacterStats {
    public string Name;
    public int MaxHP;
    public int MaxMana;
    public int Damage;
    public int WeaponLevel;
    public int Armor;
}

