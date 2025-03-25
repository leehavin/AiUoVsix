using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AiUoVsix.Command.SqlSugarGen.Common
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ListView))]
    public class ListViewEx : ListView
    {
        private Color[] _colors = new Color[3] { Color.Black, Color.Red, Color.Blue };
        public HashSet<string> Prefixs = new HashSet<string>();
        private IContainer components = null;
        private ImageList images;

        public ListViewEx() => InitializeComponent();

        public ListViewEx(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            CheckBoxes = true;
            FullRowSelect = true;
            GridLines = true;
            View = View.List;
            HideSelection = true;
        }

        internal void AddItems(List<ListViewObjectItem> list)
        {
            Items.Clear();
            Color color = _colors[0];
            foreach (ListViewObjectItem listViewObjectItem in list)
            {
                string name = listViewObjectItem.Name;
                int num = name.IndexOf('_');
                if (num > 0)
                {
                    string str = name.Substring(0, num + 1);
                    if (!Prefixs.Contains(str))
                    {
                        color = _colors[Prefixs.Count % _colors.Length];
                        Prefixs.Add(str);
                    }
                }
                Items.Add(new ListViewItem(name, GetImageKey(listViewObjectItem.Type))
                {
                    ForeColor = color,
                    Checked = listViewObjectItem.Check
                });
            }
        }

        public void SelectAll()
        {
            foreach (ListViewItem listViewItem in Items)
                listViewItem.Checked = true;
        }

        public void ClearSelect()
        {
            foreach (ListViewItem listViewItem in Items)
                listViewItem.Checked = false;
        }

        public List<string> GetSelectedItems()
        {
            List<string> selectedItems = new List<string>();
            foreach (ListViewItem listViewItem in Items)
            {
                if (listViewItem.Checked)
                    selectedItems.Add(listViewItem.Text);
            }
            return selectedItems;
        }

        private string GetImageKey(DbObjectType objType)
        {
            switch (objType)
            {
                case DbObjectType.Table:
                    return "table.png";
                case DbObjectType.View:
                    return "view.png";
                case DbObjectType.Process:
                    return "sp.png";
                default:
                    return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ListViewEx));
            //images = new ImageList();
            //images.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("images.ImageStream");
            //images.TransparentColor = Color.Transparent;
            //images.Images.SetKeyName(0, "00037.png");
            //images.Images.SetKeyName(1, "00040.png");
            //images.Images.SetKeyName(2, "00048.png");
            //images.Images.SetKeyName(3, "00746.png");
            //images.Images.SetKeyName(4, "01119.png");
            //images.Images.SetKeyName(5, "01331.png");
            //images.Images.SetKeyName(6, "01334.png");
            //images.Images.SetKeyName(7, "01335.png");
            //images.Images.SetKeyName(8, "01347.png");
            //images.Images.SetKeyName(9, "01374.png");
            //images.Images.SetKeyName(10, "01380.png");
            //images.Images.SetKeyName(11, "01572.png");
            //images.Images.SetKeyName(12, "01581.png");
            //images.Images.SetKeyName(13, "01582.png");
            //images.Images.SetKeyName(14, "01583.png");
            //images.Images.SetKeyName(15, "01584.png");
            //images.Images.SetKeyName(16, "01666.png");
            //images.Images.SetKeyName(17, "01832.png");
            //images.Images.SetKeyName(18, "01912.png");
            //images.Images.SetKeyName(19, "01936.png");
            //images.Images.SetKeyName(20, "01964.png");
            //images.Images.SetKeyName(21, "6812.png");
            //images.Images.SetKeyName(22, "db.png");
            //images.Images.SetKeyName(23, "db1.png");
            //images.Images.SetKeyName(24, "dir.png");
            //images.Images.SetKeyName(25, "exit.png");
            //images.Images.SetKeyName(26, "info.png");
            //images.Images.SetKeyName(27, "OrmGen.ico");
            //images.Images.SetKeyName(28, "sp.png");
            //images.Images.SetKeyName(29, "splash.jpeg");
            //images.Images.SetKeyName(30, "table.png");
            //images.Images.SetKeyName(31, "view.png");
        }
    }
}
