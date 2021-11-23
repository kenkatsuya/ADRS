using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADRS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool fDirty = false;

        private void adresTableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.adresTableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.adresDataSet);
            fDirty = false; //データを保存した

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: このコード行はデータを 'adresDataSet.AdresTable' テーブルに読み込みます。必要に応じて移動、または削除をしてください。
            this.adresTableTableAdapter.Fill(this.adresDataSet.AdresTable);

        }

        private void adresTableDataGridView_cellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //セルの値が変更されたかどうか覚えておくための変数
            fDirty = true;

        }

        private void adresTableDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //行が追加された
            fDirty = true;
        }

        private void adresTableDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            //行が削除された
            fDirty = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(fDirty)
            {
                DialogResult result;
                result = MessageBox.Show("変更を保存しますか？", "保存の確認", MessageBoxButtons.YesNoCancel);
                if(result == DialogResult.Yes)　//保存する
                {
                    adresTableBindingNavigatorSaveItem_Click(sender, e);
                }
                if (result == DialogResult.No)　//保存しない
                {
                    e.Cancel = false;
                }
                if (result == DialogResult.Cancel)　//保存を中止する
                {
                    e.Cancel = true;
                }
            }
        }

        private void adresTableDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "エラー!");
        }
    }
}
