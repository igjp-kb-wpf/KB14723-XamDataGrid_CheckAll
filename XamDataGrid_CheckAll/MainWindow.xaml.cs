using Infragistics.Windows;
using Infragistics.Windows.DataPresenter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace XamDataGrid_CheckAll
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainModel();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (DataRecord rc in xamDataGrid1.Records)
            {
                if (!(rc.IsFilteredOut != null && (bool)rc.IsFilteredOut))
                {
                    (rc.DataItem as TestData).Test1 = true;
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsFocused)
            {
                foreach (DataRecord rc in xamDataGrid1.Records)
                {
                    if (!(rc.IsFilteredOut != null && (bool)rc.IsFilteredOut))
                    {
                        (rc.DataItem as TestData).Test1 = false;
                    }
                }
            }
        }

        private void xamDataGrid1_CellChanged(object sender, Infragistics.Windows.DataPresenter.Events.CellChangedEventArgs e)
        {
            if (e.Cell.Field.Name == "Test1" && (bool)e.Cell.Value)
            {
                CheckBox cb = Utilities.GetDescendantFromName(xamDataGrid1, "checkAll") as CheckBox;
                cb.IsChecked = false;
            }
        }
    }

    public class MainModel : INotifyPropertyChanged
    {
        public MainModel()
        {
            m_gridData = new ObservableCollection<TestData>();
            for (int i = 0; i < 20; i++)
            {
                m_gridData.Add(new TestData { Id = "test" + i, Test1 = false, Test2 = "Test" + i });
            }
        }

        private ObservableCollection<TestData> m_gridData;
        public ObservableCollection<TestData> GridData
        {
            get { return m_gridData; }
            set
            {
                m_gridData = value;
                NotifyPropertyChanged("GridData");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

    public class TestData : INotifyPropertyChanged
    {
        private String m_id;
        public String Id
        {
            get { return m_id; }
            set
            {
                m_id = value;
                NotifyPropertyChanged("Id");
            }
        }

        private bool m_test1;
        public bool Test1
        {
            get { return m_test1; }
            set
            {
                m_test1 = value;
                NotifyPropertyChanged("Test1");
            }
        }

        private String m_test2;
        public String Test2
        {
            get { return m_test2; }
            set
            {
                m_test2 = value;
                NotifyPropertyChanged("Test2");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
