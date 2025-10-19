using UnityEngine;

public class Logger
{
    public static void Log(string message)
    {
        Log(message, Color.white);
    }

    public static void Log(string message, Color color)
    {
        string hexColor = ColorTag.ColorToHex(color);
        Debug.LogError($"<color={hexColor}>{message}</color>");
    }
}

public static class ColorTag
{
    /// <summary>
    /// Конвертирует UnityEngine.Color в HEX-формат (#RRGGBB).
    /// </summary>
    public static string ColorToHex(Color color)
    {
        // Умножаем на 255 и конвертируем в HEX
        int r = (int)(color.r * 255);
        int g = (int)(color.g * 255);
        int b = (int)(color.b * 255);
        return $"#{r:X2}{g:X2}{b:X2}";
    }
}