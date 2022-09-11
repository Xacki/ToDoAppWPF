using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoAppWPF.Models;
using ToDoAppWPF.Services;

namespace ToDoAppWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoDataList.Json";
        private BindingList<ToDoModel> _toDoDataList;
        private FileIOService _fileIOService;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);
            try
            {
                _toDoDataList = _fileIOService.LoadData();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Close();
            }
            
            dgTodoList.ItemsSource = _toDoDataList;

            _toDoDataList.ListChanged += _toDoDataList_ListChanged;
        }

        private void _toDoDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged || e.ListChangedType == ListChangedType.ItemAdded)
            {
                try
                {
                    _fileIOService.SaveData(sender);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                    this.Close();
                }

                dgTodoList.ItemsSource = _toDoDataList;

                _toDoDataList.ListChanged += _toDoDataList_ListChanged;
            }
            
        }
    }
}
