using System.Windows;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using buisnessBudget;
using System.Collections.Generic;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace BudgetTracker.View
{
    public partial class MainWindow : Window
    {
        private List<string> _budgetTypes = new List<string>();
        private ObservableCollection<BudgetRecord> _budgetRecords = new ObservableCollection<BudgetRecord>();//это как лист ток на приколе, зачем? потому что могу)
        private DateTime currentDate = new DateTime();
        public MainWindow()
        {
            InitializeComponent();
            startWindow();

        }
        private void startWindow()
        {
            _budgetRecords = Serialization.Deserialize<ObservableCollection<BudgetRecord>>();          
            dgRecords.ItemsSource = _budgetRecords;
            for (int i = 0; i < _budgetRecords.Count; i++) 
            {
                _budgetTypes.Add(_budgetRecords[i].Type);
                Sum.Text = Convert.ToString(_budgetRecords[i].Amount + Convert.ToDecimal(Sum.Text));
            }
            cbType.ItemsSource = _budgetTypes;
        }

        public MainWindow(string textFromFirstWindow) : this()
        {
            _budgetTypes.Add(textFromFirstWindow);
        }        

        private void btnAddType_Click(object sender, RoutedEventArgs e)
        {
            NewTypeWindow typeWindow = new NewTypeWindow();
            typeWindow.Show();
            this.Close();
            
        }

        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                string name = tbName.Text;
                string selectedDate = dpDate.Text;
                decimal.TryParse(tbAmount.Text, out decimal amount);
                string selectedType = cbType.SelectedItem.ToString();
                if (selectedType != null)
                {
                    BudgetRecord record = new BudgetRecord(name, selectedType, amount, selectedDate );
                    _budgetRecords.Add(record);
                    dgRecords.ItemsSource = _budgetRecords;
                }
                else
                {
                    MessageBox.Show("Данные неверны или не полны");
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Данные неверны или не полны");          
            }
        }

        private void btnSaveRecord_Click(object sender, RoutedEventArgs e)
        {
            Serialization.Serialize<ObservableCollection<BudgetRecord>>(_budgetRecords);
            for (int i = 0;  i < _budgetRecords.Count; i++) 
            { 
                Sum.Text = Convert.ToString(_budgetRecords[i].Amount+Convert.ToDecimal(Sum.Text)); 
            }
            MessageBox.Show("Данные сохранены!");
        }

        private void btnDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            if (dgRecords.SelectedIndex != -1)
            {
                int selectedIndex = dgRecords.SelectedIndex;
                _budgetRecords.RemoveAt(selectedIndex);
                Sum.Text = Convert.ToString(Convert.ToDecimal(Sum.Text) - _budgetRecords[selectedIndex-1].Amount);
                Serialization.Serialize<ObservableCollection<BudgetRecord>>(_budgetRecords);
                dgRecords.ItemsSource = _budgetRecords;
            }

        }

        

        private void dpDate_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ObservableCollection<BudgetRecord> _selectedDateRecords = new ObservableCollection<BudgetRecord>();

            for (int i = 0; i < _budgetRecords.Count; i++)
            {
                if (_budgetRecords[i].RecordTime == dpDate.Text) 
                {
                    _selectedDateRecords.Add(_budgetRecords[i]);
                }
            }
            dgRecords.ItemsSource = _selectedDateRecords;
        }


        private void dgRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                if (dgRecords.SelectedIndex != -1)
                {
                    cbType.SelectedValue = _budgetRecords[dgRecords.SelectedIndex].Type;
                    dpDate.Text = _budgetRecords[dgRecords.SelectedIndex].RecordTime;
                    tbName.Text = _budgetRecords[dgRecords.SelectedIndex].Name;
                    tbAmount.Text = Convert.ToString(_budgetRecords[dgRecords.SelectedIndex].Amount);
                }
            } catch (ArgumentOutOfRangeException ) { return; }
        }
    }
}
