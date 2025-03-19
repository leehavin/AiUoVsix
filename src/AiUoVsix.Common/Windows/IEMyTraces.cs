namespace AiUoVsix.Common.Windows
{ 
    public enum IEMyTraces
    {
        History = 1,
        Cookies = 2,
        TemporaryFiles = 8,
        FormData = 16, // 0x00000010
        Passwords = 32, // 0x00000020
        DeleteAll = 255, // 0x000000FF
        DeleteAllAndAddOns = 4351, // 0x000010FF
    }
}
