using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace FotoViewerEF2
{
    /// <summary>
    /// Interaction logic for UserControlSearch.xaml
    /// </summary>
    public partial class UserControlComboBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Делегат события "Выбрано значение"
        /// </summary>
        /// <param name="selectedItem"></param>
        public delegate void SelectionChangedHandler(Object selectedItem);
        /// <summary>
        /// Событие срабатывает по выбору значения из списка combobox
        /// </summary>
        public event SelectionChangedHandler SelectionChangedEvent;

        /// <summary>
        /// Массив значений Combobox в string формате
        /// </summary>
        List<string> _itemsStrings;
        /// <summary>
        /// Выбранное значение в combobox
        /// </summary>
        Object _selectedItem;
        /// <summary>
        /// Была ли передача значения через ивент
        /// </summary>
        bool _itemSend;

        /// <summary>
        /// Массив значений в object формате
        /// </summary>
        public List<Object> Items { get; set; }

        /// <summary>
        /// Выбранное значение в combobox
        /// </summary>
        public Object SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {

                _selectedItem = value;

                if (_selectedItem != null)
                {
                    _itemSend = true;

                    if (SelectionChangedEvent != null)
                        SelectionChangedEvent(_selectedItem);
                }
            }
        }
        /// <summary>
        /// Массив объектов - результат поиска
        /// </summary>
        public ObservableCollection<Object> SearchResult { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public UserControlComboBox()
        {
            //Resources = ResourceWPFManager.Resources;

            InitializeComponent();

            SearchResult = new ObservableCollection<object>();

            Items = new List<object>();

            DataContext = this;

            _itemSend = true;
        }

        private void AutoCompleteComboBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;

            _itemSend = false;

            if (!string.IsNullOrEmpty(cmb.Text))
            {
                string fullText = cmb.Text;

                if (!IsSpecialChar(e.Text))
                    fullText = cmb.Text.Insert(GetChildOfType<TextBox>(cmb).CaretIndex, e.Text);

                SearchResult.Clear();

                for (int i = 0; i < _itemsStrings.Count; i++)
                {
                    if (_itemsStrings[i].IndexOf(fullText, StringComparison.CurrentCultureIgnoreCase) != -1)
                        SearchResult.Add(Items.ElementAt(i));
                }
            }
            else if (!string.IsNullOrEmpty(e.Text))
            {
                if (IsSpecialChar(e.Text))
                    SearchResult = new ObservableCollection<Object>(Items);
                else
                {
                    _selectedItem = null;
                    SearchResult.Clear();

                    for (int i = 0; i < _itemsStrings.Count; i++)
                    {
                        if (_itemsStrings[i].IndexOf(e.Text, StringComparison.CurrentCultureIgnoreCase) != -1)
                            SearchResult.Add(Items.ElementAt(i));
                    }
                }
            }
            else
                SearchResult = new ObservableCollection<Object>(Items);

            cmb.ItemsSource = SearchResult;
            cmb.IsDropDownOpen = true;
        }

        private void AutoCompleteComboBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                ComboBox cmb = (ComboBox)sender;

                _itemSend = false;

                if (!string.IsNullOrEmpty(cmb.Text))
                {
                    SearchResult.Clear();

                    for (int i = 0; i < _itemsStrings.Count; i++)
                    {
                        if (_itemsStrings[i].IndexOf(cmb.Text, StringComparison.CurrentCultureIgnoreCase) != -1)
                            SearchResult.Add(Items.ElementAt(i));
                    }
                }
                else
                    SearchResult = new ObservableCollection<Object>(Items);

                cmb.ItemsSource = SearchResult;
                cmb.IsDropDownOpen = true;
            }
        }

        private void AutoCompleteComboBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;

            _itemSend = false;

            string pastedText = (string)e.DataObject.GetData(typeof(string));

            string fullText;

            if (!string.IsNullOrEmpty(cmb.Text))
                fullText = cmb.Text.Insert(GetChildOfType<TextBox>(cmb).CaretIndex, pastedText);
            else
                fullText = pastedText;

            if (!string.IsNullOrEmpty(fullText))
            {
                SearchResult.Clear();

                for (int i = 0; i < _itemsStrings.Count; i++)
                {
                    if (_itemsStrings[i].IndexOf(fullText, StringComparison.CurrentCultureIgnoreCase) != -1)
                        SearchResult.Add(Items.ElementAt(i));
                }

                cmb.ItemsSource = SearchResult;
            }
            else
            {
                SearchResult = new ObservableCollection<Object>(Items);
                cmb.ItemsSource = SearchResult;
            }

            cmb.ItemsSource = SearchResult;
            cmb.IsDropDownOpen = true;
        }

        #region Старая версия
        //TextBoxBase.TextChanged="AutoCompleteComboBox_TextChanged"
        //private void AutoCompleteComboBox_TextChanged(object sender, TextChangedEventArgs args)
        //{
        //    ComboBox cmb = (ComboBox)sender;
        //    TextBox tbCmb = GetChildOfType<TextBox>(cmb);

        //    _itemSend = false;

        //    string fullText = tbCmb.Text;
        //    int carIndex = tbCmb.CaretIndex;

        //    if (!string.IsNullOrEmpty(fullText))          
        //    {
        //        SelectedItem = null;
        //        SearchResult.Clear();

        //        for (int i = 0; i < _itemsStrings.Count; i++)
        //        {
        //            if (_itemsStrings[i].IndexOf(fullText, StringComparison.CurrentCultureIgnoreCase) != -1)
        //                SearchResult.Add(Items.ElementAt(i));
        //        }
        //    }
        //    else
        //        SearchResult = new ObservableCollection<Object>(Items);

        //    AutoCompleteComboBox.ItemsSource = SearchResult;
        //    AutoCompleteComboBox.IsDropDownOpen = true;

        //    tbCmb.Text = fullText;
        //    tbCmb.CaretIndex = carIndex;
        //}
        #endregion

        private void AutoCompleteComboBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;

            cmb.IsDropDownOpen = true;
        }

        private void AutoCompleteComboBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (_itemSend)
                return;

            ComboBox cmb = (ComboBox)sender;

            if (string.IsNullOrEmpty(cmb.Text))
                return;

            int itemIndex = _itemsStrings.FindIndex(s => s.IndexOf(cmb.Text, StringComparison.CurrentCultureIgnoreCase) != -1);

            if (itemIndex != -1)
            {
                Object item = Items[itemIndex];

                cmb.Text = item.ToString();
                cmb.IsDropDownOpen = false;

                _itemSend = true;

                if (SelectionChangedEvent != null)
                    SelectionChangedEvent(item);
            }
        }

        /// <summary>
        /// Получение дочернего контрола визуального дерева
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        private static T GetChildOfType<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }

        /// <summary>
        /// Получен ли спец символ
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool IsSpecialChar(string text)
        {
            if (text.Length == 1)
            {
                char escChar = Convert.ToChar(027);
                return text[0] == escChar;
            }
            return false;
        }

        /// <summary>
        /// Инициализация UserControlSearch
        /// </summary>
        /// <param name="items"></param>
        public void Init(List<Object> items)
        {
            Items = items;
            _itemsStrings = Items.Select(i => i.ToString()).ToList();

            SearchResult = new ObservableCollection<Object>(Items);
            AutoCompleteComboBox.ItemsSource = SearchResult;
        }

        /// <summary>
        /// Очистка Combobox
        /// </summary>
        public void ClearText()
        {
            AutoCompleteComboBox.Text = "";
            SearchResult = new ObservableCollection<Object>(Items);
            AutoCompleteComboBox.IsDropDownOpen = false;
        }
    }
}
