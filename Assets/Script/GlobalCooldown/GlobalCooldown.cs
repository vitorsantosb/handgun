using System;
using System.Collections.Generic;
using UnityEngine;

public static class CooldownManager
{
    public static Dictionary<string, Dictionary<string, long>> cooldowns = new Dictionary<string, Dictionary<string, long>>();

    /*
        key = "Chave" ou um valor único para cada usuário ou generalizado
        type = "Tipo" uma definição para encontrar um cooldown específico dentro do usuário
        expires = "Expira" tempo que irá expirar esse cooldown (1 segundo = 1000 | 10 segundos = 10000) sendo então segundo * 1000
        AddCooldown("shadomal", "cooldownUseStormMagic", 3000);
        AddCooldown("shadomal", "cooldownUseFireMagic", 5000);

        Adiciona um cooldown ao usuário
    */
    public static void AddCooldown(string key, string type, long expires)
    {
        Dictionary<string, long> cooldown = null;
        cooldowns.TryGetValue(key, out cooldown);

        if (cooldown == null)
        {
            cooldown = new Dictionary<string, long>();
        }

        cooldown.Remove(type);
        cooldown.Add(type, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + expires);

        cooldowns.Remove(key);
        cooldowns.Add(key, cooldown);
    }

    /*
        key = "Chave" ou um valor único para cada usuário ou generalizado
        type = "Tipo" uma definição para encontrar um cooldown específico dentro do usuário
        RemoveCooldown("shadomal", "cooldownUseStormMagic");

        irá remover um cooldown previamente adicionada ao usuário
    */
    public static void RemoveCooldown(string key, string type)
    {
        Dictionary<string, long> cooldown = null;
        cooldowns.TryGetValue(key, out cooldown);

        if (cooldown != null && cooldown.ContainsKey(type))
        {
            cooldown.Remove(type);

            cooldowns.Remove(key);
            cooldowns.Add(key, cooldown);
        }
    }

    /*
        key = "Chave" ou um valor único para cada usuário ou generalizado
        type = "Tipo" uma definição para encontrar um cooldown específico dentro do usuário
        IsExpired("shadomal", "cooldownUseStormMagic");

        Retorna um valor booleano (true/false) se o cooldown expirou (true) se não (false)
    */
    public static bool IsExpired(string key, string type)
    {
        Dictionary<string, long> cooldown = null;
        cooldowns.TryGetValue(key, out cooldown);

        if (cooldown != null)
        {

            long expires = 0;
            cooldown.TryGetValue(type, out expires);

            return expires <= DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        return true;
    }
}

