using CleanFiles.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanFiles.Api
{
    public partial class Form1 : Form
    {
        private string OriginPath;
        private List<string> KillFiles;
        private bool SubDir;
        public Form1()
        {
            OriginPath = string.Empty;
            KillFiles = new List<string>();            
            InitializeComponent();
            SubDir = chkSubFolders.Checked;
        }

        private void btnCharge_Click(object sender, EventArgs e)
        {
            ClearMe();

            if (!string.IsNullOrEmpty(txtPath.Text))
            {
                OriginPath = txtPath.Text.Trim();

                Killer kill = new Killer(OriginPath);
                var listaResult = kill.GetFiles(SubDir);
                LoadTreeViewData(listaResult, tvData);
            }
        }


        private void ClearMe()
        {

            tvData.Nodes.Clear();
            tvRepeats.Nodes.Clear();
            OriginPath = string.Empty;

        }

        private void LoadTreeViewData(List<string> lista, TreeView tv)
        {
            var path = txtPath.Text.TrimEnd('\\');
            var mainFolder = path.Split('\\').Select(x => x).LastOrDefault();

            TreeNode treeNode = new TreeNode(mainFolder);

            if (lista == null)
            {
                lista = new Killer(path).GetFiles(SubDir);
            }


            LoadSubNodes(lista, path, treeNode);
            tv.Nodes.Add(treeNode);
        }



        private void LoadSubNodes(List<string> sources, string path, TreeNode treeNode)
        {

            var lista = sources.Select(x => x.Replace(path, "").Remove(0, 1).TrimEnd().Split('\\')).ToList().GroupBy(x => x[0]).Select(y => y.ToList()).ToList();

            var agregados = lista.Where(x => x.Count.Equals(1)).Select(j => new TreeNode(j[0][0])).ToList();

            agregados.ForEach((item) => treeNode.Nodes.Add(item));

            foreach (var sublist in lista)
            {
                var item = sublist.ToList();

                if (item.Count > 1)
                {
                    var tempath = '\\' + item[0][0];
                    var newpath = path + tempath;

                    List<string> newList = new List<string>();

                    item.ForEach((val) =>
                    {
                        var temp = val.ToList();
                        string dir = "";
                        temp.ForEach((cad) => dir += '\\' + cad);
                        newList.Add(dir);
                    }
                    );
                    var nd = new TreeNode(tempath.Replace('\\', ' ').TrimStart());
                    treeNode.Nodes.Add(nd);
                    LoadSubNodes(newList, tempath, nd);


                }




            }







        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnRepeat_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPath.Text))
            {
                OriginPath = txtPath.Text.Trim();
                tvRepeats.Nodes.Clear();
                Killer kill = new Killer(OriginPath);
                var rptList = kill.GetFiles(SubDir);


                Cursor = Cursors.WaitCursor;
                KillFiles = kill.EvaluateListDuplicated(rptList);
                Cursor = Cursors.Default;

                if (KillFiles.Count > 0)
                {


                    LoadTreeViewData(KillFiles, tvRepeats);
                }
                else {
                    MessageBox.Show($"No hay archivos repetidos en ningunta ruta");
                
                }             

            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"¿Estas segurdo de elimnar estos {KillFiles.Count} archivo(s) ?", "Eliminacion(!)", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                Killer kill = new Killer();
                var result = kill.DeleteFileList(KillFiles);

                MessageBox.Show($"Se eliminaron {result.Hash} archivos");
                if (!string.IsNullOrEmpty(result.Value))
                {
                    MessageBox.Show($"Se Presentarion los siguientes errores {result.Value}");
                }

                tvRepeats.Nodes.Clear();
                KillFiles = new List<string>();
            }

        }

        private void tvData_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            fbDialog.ShowDialog();
            var path = fbDialog.SelectedPath;
            if (!string.IsNullOrEmpty(path))
            {
                txtPath.Text = path;
            }


        }

        private void chkSubFolders_CheckedChanged(object sender, EventArgs e)
        {
            SubDir = chkSubFolders.Checked;
        }
    }
}
