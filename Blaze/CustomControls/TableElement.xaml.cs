using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace Blaze.CustomControls
{
    /// <summary>
    /// Interaction logic for TableElement.xaml
    /// </summary>
    public partial class TableElement : UserControl, INotifyPropertyChanged
    {
        //A reference to the data context of the data grid
        public DataTable data;

        //Constructor
        public TableElement()
        {
            InitializeComponent();

            DataContext = this;

            //Create the data context and add it as the items source
            data = new DataTable();

            dataGrid.ItemsSource = data.DefaultView;
        }

        //Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //The row number property
        private int rowNum  = 1;
        public string RowNum 
        {
            get
            {
                //Returns the string value
                return rowNum.ToString();
            }
            set
            {
                //Add's it if it is a valid number and raises the event
                if (int.TryParse(value, out int num) && num > 0 && num < 101)
                {
                    rowNum = num;

                    RaisePropertyChanged();
                }
            }
        }

        //The column number property
        private int colNum = 1;
        public string ColNum
        {
            get
            {
                //Returns the string value
                return colNum.ToString();
            }
            set
            {
                //Add's it if it is a valid number and raises the event
                if (int.TryParse(value, out int num) && num > 0 && num < 101)
                {
                    colNum = num;
                    RaisePropertyChanged();
                }
            }
        }

        //Add a pre existing column
        public void AddColumn(string columnName)
        {
            //If it is below a max
            if (colNum == 100) return;

            //Need to add a column to the data context and the data grid as
            //data grid doesn't auto update columns
            data.Columns.Add(columnName);
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Binding = new Binding(columnName),
                Header = columnName,
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                MinWidth = ActualWidth / 3.0
            });
        }

        //Add a new column
        public void AddColumn()
        {
            //If it is below a max
            if (colNum == 100) return;

            //Need to add a column to the data context and the data grid as
            //data grid doesn't auto update columns
            data.Columns.Add(new DataColumn());
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Binding = new Binding(data.Columns[data.Columns.Count - 1].ColumnName),
                Header = data.Columns[data.Columns.Count - 1].ColumnName,
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                MinWidth = ActualWidth / 3.0
            });
        }

        //Add an existing row
        public void AddRow(object[] row)
        {
            //If it is below a max
            if (rowNum == 100) return;

            //Only need to add the row
            data.Rows.Add(row);
        }

        //Add a new row
        public void AddRow()
        {
            //If it is below a max
            if (rowNum == 100) return;

            //Only need to add the row
            data.Rows.Add(data.NewRow());
        }

        //Removes a column
        public void RemoveColumn()
        {
            //If it is above 1
            if (colNum == 1) return;

            //Need to remove the column from the data grid as
            //data grid doesn't auto update columns
            dataGrid.Columns.RemoveAt(dataGrid.Columns.Count - 1);
            data.Columns.RemoveAt(data.Columns.Count - 1);
        }

        //Removes a row
        public void RemoveRow()
        {
            //If it is above 1
            if (rowNum == 1) return;

            //Only need to remove the row
            data.Rows.RemoveAt(data.Rows.Count - 1);
        }

        //Method called by add row button
        private void AddRow_Button(object sender, RoutedEventArgs e)
        {
            if (rowNum != 100)
            {
                RowNum = (rowNum + 1).ToString();
            }
        }

        //Method called by remove row button
        private void RemoveRow_Button(object sender, RoutedEventArgs e)
        {
            if (rowNum != 1)
            {
                RowNum = (rowNum - 1).ToString();
            }
        }

        //Method called by add column button
        private void AddColumn_Button(object sender, RoutedEventArgs e)
        {
            if (colNum != 100)
            {
                ColNum = (colNum + 1).ToString();
            }
        }

        //Method called by remove column button
        private void RemoveColumn_Button(object sender, RoutedEventArgs e)
        {
            if (colNum != 1)
            {
                ColNum = (colNum - 1).ToString();
            }
        }

        //Creates the table
        private void CreateTable_Button(object sender, RoutedEventArgs e)
        {
            //Shows the data grid
            dataGrid.Visibility = Visibility.Visible;
            TableSettings.Visibility = Visibility.Collapsed;

            //If the new value is less then the current it deletes them
            if(data.Columns.Count - colNum > 0)
            {
                for (int i = colNum; i < data.Columns.Count; i++)
                {
                    RemoveColumn();
                }
            }
            //Else it adds them
            else
            {
                for (int i = data.Columns.Count; i < colNum; i++)
                {
                    AddColumn();
                }
            }

            //Similarly for rows
            if (data.Rows.Count - rowNum > 0)
            {
                for (int i = rowNum; i < data.Rows.Count; i++)
                {
                    RemoveRow();
                }
            }
            else
            {
                for (int i = data.Rows.Count; i < rowNum; i++)
                {
                    AddRow();
                }
            }
        }

        //Closes the Settings Menu
        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            TableSettings.Visibility = Visibility.Collapsed;
            dataGrid.Visibility = Visibility.Visible;
        }

        //Opens the Settings Menu
        public void OpenSettingsMenu()
        {
            TableSettings.Visibility = Visibility.Visible;
            dataGrid.Visibility = Visibility.Collapsed;
        }

        //Closes the Settings Menu
        public void Cancel()
        {
            TableSettings.Visibility = Visibility.Collapsed;
            dataGrid.Visibility = Visibility.Visible;
        }

        //Loads an existing datatable
        public void Load(DataTable originalData)
        {
            foreach (DataColumn column in originalData.Columns)
            {
                AddColumn(column.ColumnName);
            }

            foreach (DataRow row in originalData.Rows)
            {
                AddRow(row.ItemArray);
            }
        }
    }
}
