using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ComaxTestWF
{
    public partial class ComaxTest : Form
    {

        private ListSortDirection direction = ListSortDirection.Descending;
        private int oldColumnIndex = -1;

        public ComaxTest()
        {
            InitializeComponent();
        }

        private void ComaxTest_Load(object sender, EventArgs e)
        {
            setXML();
            setHeaderCellsNames();
            setHeadersOrder();
            setHeadersStyles();
            setGridViewSize();
        }
        private void setXML()
        {
            DataSet dataset = new DataSet();
            try
            {
                dataset.ReadXml("xml_ex.xml");
            }
            catch
            {
                MessageBox.Show("No file was found, please load new xml file");

            }
            dataGridView.DataSource = dataset.Tables[0];
        }

        private void setHeaderCellsNames()
        {
            dataGridView.Columns[0].HeaderCell.Value = "קוד";
            dataGridView.Columns[1].HeaderCell.Value = "שם";
            dataGridView.Columns[2].HeaderCell.Value = "ברקוד";
        }

        private void setHeadersOrder()
        {
            dataGridView.Columns[0].DisplayIndex = 2;
            dataGridView.Columns[1].DisplayIndex = 0;
            dataGridView.Columns[2].DisplayIndex = 1;
        }

        private void setHeadersStyles()
        {
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.GridColor = Color.Gray;
        }

        private void setGridViewSize()
        {
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int newColumnIndex = e.ColumnIndex;
            DataGridViewColumn newColumn = dataGridView.Columns[e.ColumnIndex];

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.DefaultCellStyle.BackColor = Color.White;
            }

            
            if (oldColumnIndex == newColumnIndex)
            {
                direction = direction == ListSortDirection.Descending ? 
                    ListSortDirection.Ascending : ListSortDirection.Descending; 
            }
            else
            {
                direction = ListSortDirection.Descending;
            }

            dataGridView.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ?
                SortOrder.Ascending : SortOrder.Descending;

            newColumn.DefaultCellStyle.BackColor = Color.Gray;
            oldColumnIndex = e.ColumnIndex;

        }

        private void nameTxtBox_TextChanged(object sender, EventArgs e)
        {
            if(nameTxtBox.Text != "'")
            {
                try
                {
                    if (codeTxtBox.Text.Equals(""))
                    {
                        searchInTxtBox("Nm LIKE '%{0}%'", nameTxtBox.Text);
                    }
                    else
                    {
                        searchInTxtBox("Kod LIKE '%{0}%' and Nm LIKE '%{1}%'", codeTxtBox.Text, nameTxtBox.Text);
                    }
                }
                catch
                {
                    MessageBox.Show("Illegal char");
                }
            }
            else
            {
                nameTxtBox.Text = "";
                codeTxtBox.Text = "";
            }
        }

        private void codeTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (codeTxtBox.Text != "'")
            {
                try
                {
                    if (nameTxtBox.Text.Equals(""))
                    {
                        searchInTxtBox("Kod LIKE '%{0}%'", codeTxtBox.Text);
                    }
                    else
                    {
                        searchInTxtBox("Kod LIKE '%{0}%' and Nm LIKE '%{1}%'", codeTxtBox.Text, nameTxtBox.Text);
                    }
                }
                catch
                {
                    MessageBox.Show("Illegal char");
                }
            }
            else
            {
                nameTxtBox.Text = "";
                codeTxtBox.Text = "";
            }
        }

        private void searchInTxtBox(string i_SearchStringType, params object[] args)
        {
            (dataGridView.DataSource as DataTable).DefaultView.RowFilter =
                string.Format(i_SearchStringType, args);
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            nameTxtBox.Text = "";
            codeTxtBox.Text = "";
        }
    }
}
