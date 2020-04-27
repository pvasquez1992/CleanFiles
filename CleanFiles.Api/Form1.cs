using CleanFiles.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CleanFiles.Api
{
    public partial class Form1 : Form
    {
        private string OriginPath;
        private List<FileInfo> KillList;
        private bool SubDir;
        private bool InceptionMode; //este modo en TRUE no solo examina duplicidad en carpetas , si no en todo el arbol de archivos. 

        public Form1()
        {
            OriginPath = string.Empty;
            InitializeComponent();
            SubDir = chkSubFolders.Checked;
            InceptionMode = false;
            KillList = new List<FileInfo>();
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
            KillList = new List<FileInfo>();
        }

        private void LoadTreeViewData(List<FileInfo> lista, TreeView tv, int mode = 0)
        {
            var path = txtPath.Text.TrimEnd('\\');
            var mainFolder = path.Split('\\').Select(x => x).LastOrDefault();

            TreeNode treeNode = new TreeNode(mainFolder);
            treeNode.Name = path;

            LoadSubNodes(lista, treeNode, mode);
            tv.Nodes.Add(treeNode);
        }
        private void LoadSubNodes(List<FileInfo> sources, TreeNode treeNode, int mode)
        {

            if (sources.Count != 0)
            {
                var agregados = sources.Where(o => o.Directory.FullName.Equals(treeNode.Name)).ToList();

                agregados.ForEach((item) => treeNode.Nodes.Add(item.Name));

                if (mode.Equals(0))
                {
                    var repetidos = new Killer().EvaluateFileInfoListDuplicated(agregados.Select(x => x.FullName).ToList()).ToList();
                    KillList.AddRange(repetidos);
                }

                var listaClear = sources.Where(i => !agregados.Select(x => x.FullName).ToList().Contains(i.FullName)).OrderByDescending(x => x.FullName).ToList();

                var lista = listaClear.GroupBy(u => u.DirectoryName.Replace(treeNode.Name, "").Split('\\')[1]).OrderBy(x => x.Key).ToList();



                foreach (var file in lista)
                {

                    var items = file.Select(x => x).ToList();
                    var vll = items.Where(o => o.Directory.Name.Equals(file.Key)).Select(i => i.DirectoryName).FirstOrDefault();

                    if (vll is null)
                    {
                        var temPath = treeNode.Name + '\\' + file.Key;
                        var txtNode = file.Key;
                        var nd = new TreeNode(txtNode);
                        nd.Name = temPath;
                        treeNode.Nodes.Add(nd);
                        LoadSubNodes(items, nd, mode);
                    }
                    else
                    {

                        if (items.Count >= 1)
                        {
                            var temPath = vll;
                            var txtNode = file.Key;
                            var nd = new TreeNode(txtNode);
                            nd.Name = temPath;
                            treeNode.Nodes.Add(nd);

                            LoadSubNodes(items, nd, mode);
                        }
                    }
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


                if (InceptionMode)
                {
                    KillList = new List<FileInfo>();
                    KillList.AddRange(kill.EvaluateFileInfoListDuplicated(rptList));
                }

                Cursor = Cursors.Default;

                if (KillList.Count > 0)
                {
                    LoadTreeViewData(KillList, tvRepeats, 1);
                }
                else
                {
                    MessageBox.Show($"No hay archivos repetidos en ningunta ruta");
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"¿Estas segurdo de elimnar estos {KillList.Count} archivo(s) ?", "Eliminacion(!)", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                Killer kill = new Killer();
                var result = kill.DeleteFileList(KillList);

                MessageBox.Show($"Se eliminaron {result.Hash} archivos");
                if (!string.IsNullOrEmpty(result.Value))
                {
                    MessageBox.Show($"Se Presentarion los siguientes errores {result.Value}");
                }

                tvRepeats.Nodes.Clear();
                KillList = new List<FileInfo>();
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
