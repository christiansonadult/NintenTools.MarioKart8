using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this._cmsGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._cmsGridSet = new System.Windows.Forms.ToolStripMenuItem();
            this._cmsGridSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._cmsGridAdd = new System.Windows.Forms.ToolStripMenuItem();
            this._cmsGridSubtract = new System.Windows.Forms.ToolStripMenuItem();
            this._cmsGridMultiply = new System.Windows.Forms.ToolStripMenuItem();
            this._cmsGridDivide = new System.Windows.Forms.ToolStripMenuItem();
            this._ccMain = new Syroot.NintenTools.MarioKart8.EditorUI.CategoryControl();
            this._ccHandling = new Syroot.NintenTools.MarioKart8.EditorUI.CategoryControl();
            this._ccSpeed = new Syroot.NintenTools.MarioKart8.EditorUI.CategoryControl();
            this._ccPhysics = new Syroot.NintenTools.MarioKart8.EditorUI.CategoryControl();
            this._ccPoints = new Syroot.NintenTools.MarioKart8.EditorUI.CategoryControl();
            this._tlpFile = new System.Windows.Forms.TableLayoutPanel();
            this._btSaveAs = new System.Windows.Forms.Button();
            this._btSave = new System.Windows.Forms.Button();
            this._btOpen = new System.Windows.Forms.Button();
            this._cmsGrid.SuspendLayout();
            this._ccMain.SuspendLayout();
            this._tlpFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // _cmsGrid
            // 
            this._cmsGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cmsGridSet,
            this._cmsGridSeparator1,
            this._cmsGridAdd,
            this._cmsGridSubtract,
            this._cmsGridMultiply,
            this._cmsGridDivide});
            this._cmsGrid.Name = "_cmsGrid";
            this._cmsGrid.Size = new System.Drawing.Size(128, 120);
            // 
            // _cmsGridSet
            // 
            this._cmsGridSet.Image = global::Syroot.NintenTools.MarioKart8.PerformanceEditor.Properties.Resources.Menu_Set;
            this._cmsGridSet.Name = "_cmsGridSet";
            this._cmsGridSet.Size = new System.Drawing.Size(127, 22);
            this._cmsGridSet.Text = "Set...";
            this._cmsGridSet.Click += new System.EventHandler(this._cmsGridSet_Click);
            // 
            // _cmsGridSeparator1
            // 
            this._cmsGridSeparator1.Name = "_cmsGridSeparator1";
            this._cmsGridSeparator1.Size = new System.Drawing.Size(124, 6);
            // 
            // _cmsGridAdd
            // 
            this._cmsGridAdd.Image = global::Syroot.NintenTools.MarioKart8.PerformanceEditor.Properties.Resources.Menu_Add;
            this._cmsGridAdd.Name = "_cmsGridAdd";
            this._cmsGridAdd.Size = new System.Drawing.Size(127, 22);
            this._cmsGridAdd.Text = "Add...";
            this._cmsGridAdd.Click += new System.EventHandler(this._cmsGridAdd_Click);
            // 
            // _cmsGridSubtract
            // 
            this._cmsGridSubtract.Image = global::Syroot.NintenTools.MarioKart8.PerformanceEditor.Properties.Resources.Menu_Subtract;
            this._cmsGridSubtract.Name = "_cmsGridSubtract";
            this._cmsGridSubtract.Size = new System.Drawing.Size(127, 22);
            this._cmsGridSubtract.Text = "Subtract...";
            this._cmsGridSubtract.Click += new System.EventHandler(this._cmsGridSubtract_Click);
            // 
            // _cmsGridMultiply
            // 
            this._cmsGridMultiply.Image = global::Syroot.NintenTools.MarioKart8.PerformanceEditor.Properties.Resources.Menu_Multiply;
            this._cmsGridMultiply.Name = "_cmsGridMultiply";
            this._cmsGridMultiply.Size = new System.Drawing.Size(127, 22);
            this._cmsGridMultiply.Text = "Multiply...";
            this._cmsGridMultiply.Click += new System.EventHandler(this._cmsGridMultiply_Click);
            // 
            // _cmsGridDivide
            // 
            this._cmsGridDivide.Image = global::Syroot.NintenTools.MarioKart8.PerformanceEditor.Properties.Resources.Menu_Divide;
            this._cmsGridDivide.Name = "_cmsGridDivide";
            this._cmsGridDivide.Size = new System.Drawing.Size(127, 22);
            this._cmsGridDivide.Text = "Divide...";
            this._cmsGridDivide.Click += new System.EventHandler(this._cmsGridDivide_Click);
            // 
            // _ccMain
            // 
            this._ccMain.BackColor = System.Drawing.Color.White;
            this._ccMain.Controls.Add(this._tlpFile);
            this._ccMain.Controls.Add(this._ccPoints);
            this._ccMain.Controls.Add(this._ccSpeed);
            this._ccMain.Controls.Add(this._ccHandling);
            this._ccMain.Controls.Add(this._ccPhysics);
            this._ccMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ccMain.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this._ccMain.HeaderDisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._ccMain.HeaderForeColor = System.Drawing.Color.White;
            this._ccMain.HeaderHoveredBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._ccMain.HeaderSelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(66)))), ((int)(((byte)(200)))));
            this._ccMain.Location = new System.Drawing.Point(0, 0);
            this._ccMain.Margin = new System.Windows.Forms.Padding(0);
            this._ccMain.Name = "_ccMain";
            this._ccMain.SelectedControl = this._tlpFile;
            this._ccMain.Size = new System.Drawing.Size(934, 561);
            this._ccMain.TabIndex = 2;
            // 
            // _ccHandling
            // 
            this._ccHandling.Enabled = false;
            this._ccHandling.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._ccHandling.HeaderDisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._ccHandling.HeaderForeColor = System.Drawing.Color.White;
            this._ccHandling.HeaderHoveredBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._ccHandling.HeaderSelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this._ccHandling.Location = new System.Drawing.Point(584, 30);
            this._ccHandling.Margin = new System.Windows.Forms.Padding(0);
            this._ccHandling.Name = "_ccHandling";
            this._ccHandling.SelectedControl = null;
            this._ccHandling.Size = new System.Drawing.Size(200, 531);
            this._ccHandling.TabIndex = 7;
            this._ccMain.SetTitle(this._ccHandling, "Handling");
            // 
            // _ccSpeed
            // 
            this._ccSpeed.Enabled = false;
            this._ccSpeed.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._ccSpeed.HeaderDisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._ccSpeed.HeaderForeColor = System.Drawing.Color.White;
            this._ccSpeed.HeaderHoveredBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._ccSpeed.HeaderSelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this._ccSpeed.Location = new System.Drawing.Point(384, 30);
            this._ccSpeed.Margin = new System.Windows.Forms.Padding(0);
            this._ccSpeed.Name = "_ccSpeed";
            this._ccSpeed.SelectedControl = null;
            this._ccSpeed.Size = new System.Drawing.Size(200, 531);
            this._ccSpeed.TabIndex = 4;
            this._ccMain.SetTitle(this._ccSpeed, "Speed");
            // 
            // _ccPhysics
            // 
            this._ccPhysics.Enabled = false;
            this._ccPhysics.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._ccPhysics.HeaderDisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._ccPhysics.HeaderForeColor = System.Drawing.Color.White;
            this._ccPhysics.HeaderHoveredBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._ccPhysics.HeaderSelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this._ccPhysics.Location = new System.Drawing.Point(184, 30);
            this._ccPhysics.Margin = new System.Windows.Forms.Padding(0);
            this._ccPhysics.Name = "_ccPhysics";
            this._ccPhysics.SelectedControl = null;
            this._ccPhysics.Size = new System.Drawing.Size(200, 531);
            this._ccPhysics.TabIndex = 3;
            this._ccMain.SetTitle(this._ccPhysics, "Physics");
            // 
            // _ccPoints
            // 
            this._ccPoints.Enabled = false;
            this._ccPoints.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._ccPoints.HeaderDisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._ccPoints.HeaderForeColor = System.Drawing.Color.White;
            this._ccPoints.HeaderHoveredBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._ccPoints.HeaderSelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this._ccPoints.Location = new System.Drawing.Point(784, 30);
            this._ccPoints.Margin = new System.Windows.Forms.Padding(0);
            this._ccPoints.Name = "_ccPoints";
            this._ccPoints.SelectedControl = null;
            this._ccPoints.Size = new System.Drawing.Size(200, 531);
            this._ccPoints.TabIndex = 8;
            this._ccMain.SetTitle(this._ccPoints, "Points");
            // 
            // _tlpFile
            // 
            this._tlpFile.ColumnCount = 1;
            this._tlpFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpFile.Controls.Add(this._btSaveAs, 0, 2);
            this._tlpFile.Controls.Add(this._btSave, 0, 1);
            this._tlpFile.Controls.Add(this._btOpen, 0, 0);
            this._tlpFile.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this._tlpFile.Location = new System.Drawing.Point(0, 30);
            this._tlpFile.Margin = new System.Windows.Forms.Padding(0);
            this._tlpFile.Name = "_tlpFile";
            this._tlpFile.RowCount = 3;
            this._tlpFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._tlpFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._tlpFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._tlpFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._tlpFile.Size = new System.Drawing.Size(184, 531);
            this._tlpFile.TabIndex = 0;
            this._ccMain.SetTitle(this._tlpFile, "File");
            // 
            // _btSaveAs
            // 
            this._btSaveAs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._btSaveAs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btSaveAs.FlatAppearance.BorderSize = 0;
            this._btSaveAs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this._btSaveAs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this._btSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btSaveAs.ForeColor = System.Drawing.Color.White;
            this._btSaveAs.Location = new System.Drawing.Point(0, 354);
            this._btSaveAs.Margin = new System.Windows.Forms.Padding(0);
            this._btSaveAs.Name = "_btSaveAs";
            this._btSaveAs.Size = new System.Drawing.Size(184, 177);
            this._btSaveAs.TabIndex = 3;
            this._btSaveAs.Text = "Save As";
            this._btSaveAs.UseVisualStyleBackColor = false;
            this._btSaveAs.Visible = false;
            this._btSaveAs.Click += new System.EventHandler(this._btSaveAs_Click);
            // 
            // _btSave
            // 
            this._btSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._btSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btSave.FlatAppearance.BorderSize = 0;
            this._btSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this._btSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this._btSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btSave.ForeColor = System.Drawing.Color.White;
            this._btSave.Location = new System.Drawing.Point(0, 177);
            this._btSave.Margin = new System.Windows.Forms.Padding(0);
            this._btSave.Name = "_btSave";
            this._btSave.Size = new System.Drawing.Size(184, 177);
            this._btSave.TabIndex = 2;
            this._btSave.Text = "Save";
            this._btSave.UseVisualStyleBackColor = false;
            this._btSave.Visible = false;
            this._btSave.Click += new System.EventHandler(this._btSave_Click);
            // 
            // _btOpen
            // 
            this._btOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._btOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btOpen.FlatAppearance.BorderSize = 0;
            this._btOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this._btOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this._btOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btOpen.ForeColor = System.Drawing.Color.White;
            this._btOpen.Location = new System.Drawing.Point(0, 0);
            this._btOpen.Margin = new System.Windows.Forms.Padding(0);
            this._btOpen.Name = "_btOpen";
            this._btOpen.Size = new System.Drawing.Size(184, 177);
            this._btOpen.TabIndex = 1;
            this._btOpen.Text = "Open";
            this._btOpen.UseVisualStyleBackColor = false;
            this._btOpen.Click += new System.EventHandler(this._btOpen_Click);
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 561);
            this.Controls.Add(this._ccMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mario Kart 8 Performance Editor";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            this._cmsGrid.ResumeLayout(false);
            this._ccMain.ResumeLayout(false);
            this._tlpFile.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private CategoryControl _ccMain;
        private CategoryControl _ccPhysics;
        private CategoryControl _ccSpeed;
        private CategoryControl _ccHandling;
        private CategoryControl _ccPoints;
        private System.Windows.Forms.TableLayoutPanel _tlpFile;
        private System.Windows.Forms.Button _btSaveAs;
        private System.Windows.Forms.Button _btSave;
        private System.Windows.Forms.Button _btOpen;
        private System.Windows.Forms.ContextMenuStrip _cmsGrid;
        private System.Windows.Forms.ToolStripMenuItem _cmsGridAdd;
        private System.Windows.Forms.ToolStripMenuItem _cmsGridSubtract;
        private System.Windows.Forms.ToolStripMenuItem _cmsGridMultiply;
        private System.Windows.Forms.ToolStripMenuItem _cmsGridDivide;
        private System.Windows.Forms.ToolStripMenuItem _cmsGridSet;
        private System.Windows.Forms.ToolStripSeparator _cmsGridSeparator1;
    }
}

