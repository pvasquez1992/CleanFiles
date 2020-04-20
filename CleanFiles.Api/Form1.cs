using CleanFiles.Tools;
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

namespace CleanFiles.Api
{
    public partial class Form1 : Form
    {
        private string OriginPath;
        private List<string> KillFiles;
        private bool SubDir;
        private bool InceptionMode; //este modo en TRUE no solo examina duplicidad en carpetas , si no en todo el arbol de archivos. 
        private string LastPath;
        private string currentPath;
        public Form1()
        {
            OriginPath = string.Empty;
            KillFiles = new List<string>();
            InitializeComponent();
            SubDir = chkSubFolders.Checked;
            InceptionMode = false;
            LastPath = string.Empty;
            currentPath = string.Empty;
        }

        private void btnCharge_Click(object sender, EventArgs e)
        {
            ClearMe();

            if (!string.IsNullOrEmpty(txtPath.Text))
            {
                OriginPath = txtPath.Text.Trim();

                Killer kill = new Killer(OriginPath);
                var listaResult = kill.GetFilesInfo(SubDir);
                LoadTreeViewData(listaResult, tvData);

            }
        }


        private void ClearMe()
        {

            tvData.Nodes.Clear();
            tvRepeats.Nodes.Clear();
            OriginPath = string.Empty;
            btnModeIncepcion.Image = Properties.Resources.red;
            InceptionMode = false;

        }

        private void LoadTreeViewData(List<FileInfo> lista, TreeView tv, int mode = 0)
        {
            var path = txtPath.Text.TrimEnd('\\');
            var mainFolder = path.Split('\\').Select(x => x).LastOrDefault();
            currentPath = mainFolder;

            TreeNode treeNode = new TreeNode(mainFolder);

            if (lista == null)
            {
                // lista = new Killer(path).GetFiles(SubDir);
            }


            LoadSubNodes(lista,  treeNode, mode);
            tv.Nodes.Add(treeNode);
        }




        private void LoadSubNodes(List<FileInfo> sources, TreeNode treeNode, int mode)
        {
            //var lista = sources.Select(x => x.Replace(path, "").Remove(0, 1).TrimEnd().Split('\\')).ToList().GroupBy(x => x[0]).Select(y => y.ToList()).ToList();

            var agregados = sources.Where(o => o.Directory.Name.Equals(treeNode.Text)).ToList();


            agregados.ForEach((item) => treeNode.Nodes.Add(item.Name));

            var repetidos = new Killer().EvaluateListDuplicated(agregados.Select(x => x.FullName).ToList());

            var listaClear = sources.Where(i => !agregados.Select(x => x.FullName).ToList().Contains(i.FullName)).OrderByDescending(x => x.FullName).ToList();

            var lista = listaClear.GroupBy(u => u.DirectoryName).OrderBy(x => x.Key).ToList();


            foreach (var file in lista)
            {
                var temPath = file.Key;
                var items = file.Select(x => x).ToList();


                if (items.Count >= 1)
                {
                    var txtNode = items[0].Directory.Name;
                    var nd = new TreeNode(txtNode);
                    treeNode.Nodes.Add(nd);

                   LoadSubNodes(listaClear, nd, mode);

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


                    //  LoadTreeViewData(KillFiles, tvRepeats);
                }
                else
                {
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

        private void btnCrazy_Click(object sender, EventArgs e)
        {
            if (InceptionMode)
            {
                InceptionMode = false;
                btnModeIncepcion.Image = Properties.Resources.red;

            }
            else
            {
                DialogResult dialogResult = MessageBox.Show($"¿Estas seguro de activar MODO INCEPCION?, \n  *se buscara duplicidad en cada carpeta en el arbol seleccionado.", "Eliminacion Modo Incepción(!)", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    InceptionMode = true;

                    btnModeIncepcion.Image = Properties.Resources.redturn;
                }

            }
        }
    }
}
