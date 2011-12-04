using System;
using System.Windows.Forms;
using ValveFormat;


namespace TF2Items.Dialogs
{
	public partial class AttributesWindow : Form
	{
		public MainWindow inst;

		public DataNode attribsNode;

		public string _lastTip;

		public AttributesWindow()
		{
			InitializeComponent();
		}

		public void AttributesWindow_Load(object sender, System.EventArgs e)
		{
			if (MainWindow.itemsParser == null)
			{
				MessageBox.Show("items_game.txt hasn't been opened yet!",
								"TF2 Items Editor",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				Hide();
				return;
			}
			attribsNode = MainWindow.itemsParser.RootNode.SubNodes.Find(n => n.Key == "attributes");
			listAttribs.Items.Clear();
			foreach (DataNode node in attribsNode.SubNodes)
			{
				listAttribs.Items.Add(node.SubNodes.Find(n => n.Key == "name").Value);
			}
		}

		private void AttributesWindow_Shown(object sender, System.EventArgs e)
		{
			if (MainWindow.itemsParser == null)
			{
				MessageBox.Show("items_game.txt hasn't been opened yet!",
								"TF2 Items Editor",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				Hide();
				return;
			}
			attribsNode = MainWindow.itemsParser.RootNode.SubNodes.Find(n => n.Key == "attributes");
			listAttribs.Items.Clear();
			foreach (DataNode node in attribsNode.SubNodes)
			{
				listAttribs.Items.Add(node.SubNodes.Find(n => n.Key == "name").Value);
			}
		}

		private void listAttribs_MouseDown(object sender, MouseEventArgs e)
		{
			int index = listAttribs.IndexFromPoint(e.Location);
			if (index >= 0 && index < listAttribs.Items.Count)
			{
				listAttribs.DoDragDrop(listAttribs.Items[index], DragDropEffects.Copy);
			}
		}

		private const int SnapDist = 20;
		private bool DoSnap(int pos, int edge)
		{
			int delta = Math.Abs(pos - edge);
			return delta <= SnapDist;
		}

		private void AttributesWindow_Move(object sender, System.EventArgs e)
		{
			if (MainWindow.Moving) return;
			int width = inst.Width - inst.ClientSize.Width;
			int height = (inst.Height - inst.ClientSize.Height);

			if (DoSnap(Left, inst.Right + width)) Left = inst.Right + width;
			if (DoSnap(Right, inst.Left - width)) Left = inst.Left - width - Width;

			if (DoSnap(inst.Top - inst.Height, Top)) Top = inst.Top - Height;
			if (DoSnap(inst.Bottom, Top)) Top = inst.Bottom;

			if (DoSnap(inst.Top, Top)) Top = inst.Top;
			if (DoSnap(inst.Bottom, Bottom)) Top = inst.Bottom - Height;

			MainWindow.AttribOffsetX = Left - inst.Right;
			MainWindow.AttribOffsetY = Top - inst.Top;
		}

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
			listAttribs.Items.Clear();
			if (String.IsNullOrEmpty(textBox1.Text))
			{
				foreach (DataNode node in attribsNode.SubNodes)
				{
					listAttribs.Items.Add(node.SubNodes.Find(n => n.Key == "name").Value);
				}
				return;
			}
			foreach (DataNode n in attribsNode.SubNodes)
			{
				string txt = n.SubNodes.Find(no => no.Key == "name").Value;
				if (textBox1.Text != null && txt.ToLower().Contains(textBox1.Text.ToLower()))
				{
					listAttribs.Items.Add(txt);
				}
			}
		}
		private void listAttribs_MouseMove(object sender, MouseEventArgs e)
		{
			var listBox = (ListBox)sender;
			try
			{
				int index = listBox.IndexFromPoint(e.Location);
				if (index > -1 && index < listBox.Items.Count)
				{
					string tipp = listBox.Items[index].ToString();
					//int idd = GetAttribId(tipp);
					string tip = ToolTips.FindToolTip(tipp);
					if (tipp != _lastTip)
					{
						toolTip1.SetToolTip(listBox, tip);
						_lastTip = tipp;
					}
				}
			}
			catch
			{
				toolTip1.Hide(listBox);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			textBox1.Clear();
		}
	}
}
