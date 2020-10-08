using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfLaba1.View
{
    public class AdvancedDataGrid:DataGrid //нужно для захвата нескольких элементов при удалении, вырезании и копипасты
    {
        public AdvancedDataGrid()
        {
            this.SelectionChanged += (s, e) => { this.SelectedItemsList = this.SelectedItems; };
        }

        #region
        public IList SelectedItemsList
        {
            get { return (IList)GetValue(SelectedItemsListProperty); }
            set { SetValue(SelectedItemsListProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsListProperty = DependencyProperty.Register("SelectedItemsList", typeof(IList), typeof(AdvancedDataGrid), new PropertyMetadata(null));
        #endregion
    }
}
