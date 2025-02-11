using ReaLTaiizor.Controls;
using SoulMemory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutoSplitterCore
{
    public partial class MultiSelectionMode : ReaLTaiizor.Forms.MaterialForm
    {
        private List<ItemList> ListItemList = new List<ItemList>();
        public Dictionary<string, string> ReadyElements { get; private set; } = new Dictionary<string, string>();
        public bool ReadyToRead { get; private set; }

        public MultiSelectionMode(List<string> Items)
        {
            InitializeComponent();
            ConfigurePanel();

            foreach (var item in Items)
            {
                AddItemToList(item);
            }
        }

        private void ConfigurePanel()
        {
            panelItems.FlowDirection = FlowDirection.TopDown;
            panelItems.WrapContents = false;
            panelItems.AutoScroll = true;
            panelItems.Padding = new Padding(10);
            panelItems.Margin = new Padding(5);
            panelItems.Dock = DockStyle.Top;
            panelItems.BorderStyle = BorderStyle.FixedSingle;
        }

        private void btnSearch_Click(object sender, EventArgs e) => ExecuteSearch();

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ExecuteSearch();
                e.SuppressKeyPress = true; // Prevents the beep sound
            }
        }

        private void ExecuteSearch()
        {
            Cursor = Cursors.WaitCursor;
            string searchText = searchTextBox.Text.ToLower();
            foreach (var item in ListItemList)
            {
                item.flowPanel.Visible = item.itemName.Text.ToLower().Contains(searchText);
            }
            Cursor = Cursors.Default;
        }

        private void AddItemToList(string item)
        {
            string itemIndex = ListItemList.Count.ToString();

            var comboBoxMode = CreateComboBox(itemIndex);
            var textBoxItem = CreateTextBox(item);
            var chkItem = CreateCheckBox(itemIndex);

            FlowLayoutPanel flowPanel = new FlowLayoutPanel { AutoSize = true };
            flowPanel.Controls.Add(comboBoxMode);
            flowPanel.Controls.Add(textBoxItem);
            flowPanel.Controls.Add(chkItem);

            panelItems.Controls.Add(flowPanel);

            ListItemList.Add(new ItemList(comboBoxMode, textBoxItem, chkItem, flowPanel));
        }

        private SkyComboBox CreateComboBox(string tag)
        {
            var comboBox = new SkyComboBox
            {
                Font = new Font("Verdana", 9),
                Size = new Size(151, 22),
                BGColorB = Color.Aqua,
                BorderColorA = Color.Black,
                Tag = tag
            };
            comboBox.Items.Add("Inmediatly");
            comboBox.Items.Add("Loading game after");
            comboBox.SelectedIndex = 0;
            comboBox.SelectedIndexChanged += (sender, e) => SyncSelection(sender as SkyComboBox);
            return comboBox;
        }

        private TextBox CreateTextBox(string text)
        {
            return new TextBox
            {
                Text = text,
                Width = 250,
                ReadOnly = true,
                Font = new Font("Verdana", 9)
            };
        }

        private System.Windows.Forms.CheckBox CreateCheckBox(string tag)
        {
            var chkBox = new System.Windows.Forms.CheckBox
            {
                Text = $"Enable {tag}",
                Name = $"chkItem_{tag}",
                Font = new Font("Verdana", 9)
            };
            chkBox.CheckedChanged += (sender, e) => SyncSelection(sender as System.Windows.Forms.CheckBox);
            return chkBox;
        }

        private void SyncSelection(object sender)
        {
            if (sender is System.Windows.Forms.CheckBox chk)
            {
                var item = ListItemList.FirstOrDefault(x => x.chkItem.Name == chk.Name);
                if (item != null)
                {
                    item.chkItem.Checked = chk.Checked;
                }
            }
            else if (sender is SkyComboBox combo)
            {
                var item = ListItemList.FirstOrDefault(x => x.comboBoxMode.Tag.ToString() == combo.Tag.ToString());
                if (item != null)
                {
                    item.comboBoxMode.SelectedIndex = combo.SelectedIndex;
                }
            }
        }

        private void btn_AddMulti_Click(object sender, EventArgs e)
        {
            ReadyElements.Clear();

            foreach (var item in ListItemList.Where(x => x.chkItem.Checked))
            {
                if (!ReadyElements.ContainsKey(item.itemName.Text))
                {
                    ReadyElements[item.itemName.Text] = item.comboBoxMode.SelectedItem.ToString();
                }
            }

            if (ReadyElements.Count > 0)
            {
                ReadyToRead = true;
            }
            else
            {
                MessageBox.Show("Select at least one item to continue", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }

    public class ItemList
    {
        public SkyComboBox comboBoxMode { get; }
        public TextBox itemName { get; }
        public System.Windows.Forms.CheckBox chkItem { get; }
        public FlowLayoutPanel flowPanel { get; }

        public ItemList(SkyComboBox combo, TextBox text, System.Windows.Forms.CheckBox checkBox, FlowLayoutPanel panel)
        {
            comboBoxMode = combo;
            itemName = text;
            chkItem = checkBox;
            flowPanel = panel;
        }
    }
}
