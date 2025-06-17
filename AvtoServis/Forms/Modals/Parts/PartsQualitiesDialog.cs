using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class PartsQualitiesDialog : Form
    {
        private readonly PartsViewModel _viewModel;
        private List<AvtoServis.Model.Entities.PartQuality> _qualities;

        public PartsQualitiesDialog(PartsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _qualities = new List<AvtoServis.Model.Entities.PartQuality>();
            ConfigureDataGridView();
            LoadQualities();
        }

        private void ConfigureDataGridView()
        {
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "QualityID",
                HeaderText = "ID",
                DataPropertyName = "QualityID",
                ReadOnly = true,
                Width = 80
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "Название качества",
                DataPropertyName = "Name",
                ReadOnly = true,
                Width = 200
            });
            dataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Actions",
                HeaderText = "Действия",
                Text = "...",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                Width = 80
            });

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoGenerateColumns = false;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 243, 245);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 220, 255);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
        }

        private void LoadQualities()
        {
            try
            {
                _qualities = _viewModel.LoadQualities();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке качеств: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LoadQualities Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void RefreshDataGridView()
        {
            dataGridView.Rows.Clear();
            foreach (var quality in _qualities)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView);
                row.Cells[0].Value = quality.QualityID;
                row.Cells[1].Value = quality.Name;
                row.Tag = quality.QualityID;
                dataGridView.Rows.Add(row);
            }
            lblCount.Text = $"Качества: {_qualities.Count}";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new QualityEditDialog(_viewModel))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        LoadQualities();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при добавлении: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"BtnAdd_Click Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int id = (int)dataGridView.Rows[e.RowIndex].Tag;

            try
            {
                if (dataGridView.Columns[e.ColumnIndex].Name == "Actions")
                {
                    var menu = new ContextMenuStrip
                    {
                        Renderer = new CustomToolStripRenderer()
                    };

                    var editItem = new ToolStripMenuItem
                    {
                        Text = "Редактировать",
                        Tag = "Edit"
                    };
                    editItem.Click += (s, ev) =>
                    {
                        using (var dialog = new QualityEditDialog(_viewModel, id))
                        {
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                LoadQualities();
                            }
                        }
                    };
                    menu.Items.Add(editItem);

                    var deleteItem = new ToolStripMenuItem
                    {
                        Text = "Удалить",
                        Tag = "Delete"
                    };
                    deleteItem.Click += (s, ev) =>
                    {
                        if (MessageBox.Show("Вы уверены, что хотите удалить это качество?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                _viewModel.DeleteQuality(id);
                                LoadQualities();
                            }
                            catch (Exception ex)
                            {
                                ShowError($"Ошибка при удалении: {ex.Message}");
                                System.Diagnostics.Debug.WriteLine($"DeleteQuality Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                            }
                        }
                    };
                    menu.Items.Add(deleteItem);

                    menu.Show(dataGridView, dataGridView.PointToClient(Cursor.Position));
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при выполнении действия: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"DataGridView_CellClick Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
            timerError.Start();
        }

        private void TimerError_Tick(object sender, EventArgs e)
        {
            lblError.Visible = false;
            timerError.Stop();
        }

        private class CustomToolStripRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                var item = e.Item as ToolStripMenuItem;
                if (item != null)
                {
                    Color backgroundColor = item.Selected ? Color.FromArgb(200, 230, 255) : Color.White;
                    if (item.Tag?.ToString() == "Edit")
                        backgroundColor = item.Selected ? Color.FromArgb(50, 140, 230) : Color.FromArgb(25, 118, 210);
                    else if (item.Tag?.ToString() == "Delete")
                        backgroundColor = item.Selected ? Color.FromArgb(255, 100, 100) : Color.FromArgb(220, 53, 69);

                    using (var brush = new SolidBrush(backgroundColor))
                    {
                        e.Graphics.FillRectangle(brush, new Rectangle(0, 0, e.Item.Width, e.Item.Height));
                    }
                }
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = e.Item.Selected ? Color.Black : Color.White;
                base.OnRenderItemText(e);
            }
        }
    }
}