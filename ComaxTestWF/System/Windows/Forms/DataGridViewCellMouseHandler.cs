namespace System.Windows.Forms
{
    internal class DataGridViewCellMouseHandler
    {
        private Action<object, DataGridViewCellMouseEventArgs> dataGridView_ColumnHeaderMouseClick;

        public DataGridViewCellMouseHandler(Action<object, DataGridViewCellMouseEventArgs> dataGridView_ColumnHeaderMouseClick)
        {
            this.dataGridView_ColumnHeaderMouseClick = dataGridView_ColumnHeaderMouseClick;
        }
    }
}