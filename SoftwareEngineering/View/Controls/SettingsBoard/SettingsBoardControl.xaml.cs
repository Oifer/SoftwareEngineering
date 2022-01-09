using System;
using System.Windows.Controls;

using SoftwareEngineering.Models.Enums;

namespace SoftwareEngineering.View.Controls.SettingsBoard
{
    /// <summary>
    /// Логика взаимодействия для SettingsBoardControl.xaml
    /// </summary>
    public partial class SettingsBoardControl : UserControl
    {
        public readonly SettingsBoardViewModel ViewModel;

        public SettingsBoardControl()
        {
            InitializeComponent();
            ViewModel = new SettingsBoardViewModel(
                () => MapWidthBox.Text,
                () => MapHeightBox.Text,
                () => LengthToWinBox.Text,
                () => (Mark)Enum.Parse(typeof(Mark), FirstMarkBox.Text));
        }
    }
}
