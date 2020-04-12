namespace CleanFiles.Api
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnCharge = new System.Windows.Forms.Button();
            this.tvData = new System.Windows.Forms.TreeView();
            this.btnRepeat = new System.Windows.Forms.Button();
            this.tvRepeats = new System.Windows.Forms.TreeView();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.fbDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnPath = new System.Windows.Forms.Button();
            this.chkSubFolders = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCharge
            // 
            this.btnCharge.Location = new System.Drawing.Point(364, 117);
            this.btnCharge.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCharge.Name = "btnCharge";
            this.btnCharge.Size = new System.Drawing.Size(109, 26);
            this.btnCharge.TabIndex = 5;
            this.btnCharge.Text = "Cargar";
            this.btnCharge.UseVisualStyleBackColor = true;
            this.btnCharge.Click += new System.EventHandler(this.btnCharge_Click);
            // 
            // tvData
            // 
            this.tvData.Location = new System.Drawing.Point(44, 187);
            this.tvData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tvData.Name = "tvData";
            this.tvData.Size = new System.Drawing.Size(344, 458);
            this.tvData.TabIndex = 6;
            this.tvData.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvData_AfterSelect);
            // 
            // btnRepeat
            // 
            this.btnRepeat.Location = new System.Drawing.Point(479, 117);
            this.btnRepeat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRepeat.Name = "btnRepeat";
            this.btnRepeat.Size = new System.Drawing.Size(165, 26);
            this.btnRepeat.TabIndex = 7;
            this.btnRepeat.Text = "Buscar Repetidos";
            this.btnRepeat.UseVisualStyleBackColor = true;
            this.btnRepeat.Click += new System.EventHandler(this.btnRepeat_Click);
            // 
            // tvRepeats
            // 
            this.tvRepeats.Location = new System.Drawing.Point(422, 187);
            this.tvRepeats.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tvRepeats.Name = "tvRepeats";
            this.tvRepeats.Size = new System.Drawing.Size(344, 458);
            this.tvRepeats.TabIndex = 8;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(650, 117);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(116, 26);
            this.btnEliminar.TabIndex = 9;
            this.btnEliminar.Text = "Eliminar Repetidos";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(41, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 22);
            this.label2.TabIndex = 10;
            this.label2.Text = "Archivos Encontrados";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(419, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 22);
            this.label3.TabIndex = 11;
            this.label3.Text = "Archivos Duplicados";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(136, 36);
            this.txtPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPath.Multiline = true;
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(408, 26);
            this.txtPath.TabIndex = 3;
            // 
            // btnPath
            // 
            this.btnPath.Location = new System.Drawing.Point(54, 34);
            this.btnPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(76, 26);
            this.btnPath.TabIndex = 12;
            this.btnPath.Text = "Ruta";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // chkSubFolders
            // 
            this.chkSubFolders.AutoSize = true;
            this.chkSubFolders.BackColor = System.Drawing.Color.Transparent;
            this.chkSubFolders.Location = new System.Drawing.Point(136, 67);
            this.chkSubFolders.Name = "chkSubFolders";
            this.chkSubFolders.Size = new System.Drawing.Size(158, 21);
            this.chkSubFolders.TabIndex = 13;
            this.chkSubFolders.Text = "Incluir Sub-Capertas";
            this.chkSubFolders.UseVisualStyleBackColor = false;
            this.chkSubFolders.CheckedChanged += new System.EventHandler(this.chkSubFolders_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(828, 730);
            this.Controls.Add(this.chkSubFolders);
            this.Controls.Add(this.btnPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.tvRepeats);
            this.Controls.Add(this.btnRepeat);
            this.Controls.Add(this.tvData);
            this.Controls.Add(this.btnCharge);
            this.Controls.Add(this.txtPath);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "CleanDuplicados";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCharge;
        private System.Windows.Forms.TreeView tvData;
        private System.Windows.Forms.Button btnRepeat;
        private System.Windows.Forms.TreeView tvRepeats;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog fbDialog;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.CheckBox chkSubFolders;
    }
}

