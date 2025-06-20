using System.ComponentModel;

namespace AiUoVsix.Command.EntityFrameworkCore.Models
{
    public class TableInfo : INotifyPropertyChanged
    {
        private bool _isSelected;
        
        public string TableName { get; set; }
        public string Schema { get; set; }
        public string TableType { get; set; }
        public string Comment { get; set; }
        
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}