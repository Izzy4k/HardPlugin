using Exiled.API.Interfaces;
using System.ComponentModel;

namespace HardPlugin
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = true;

        [Description("Количество мин. По умолчанию: 2")]
        public int CountMines { get; set; } = 2;
    }
}
