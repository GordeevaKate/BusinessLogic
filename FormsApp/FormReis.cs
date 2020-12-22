using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;

namespace FormsApp
{
    public partial class FormReis : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IReisLogic logic;
        private readonly IRaionLogic logicr;
        private int? id;
        public FormReis(IReisLogic logic, IRaionLogic logicr)
        {
            InitializeComponent();
            this.logic = logic;
            this.logicr = logicr;
            var list = logicr.Read(null);
            textBoxDate.Text = DateTime.Now.ToString("hh:mm tt");
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
            comboBox2.DataSource = list;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Id";
        }
        private void FormComponent_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = logic.Read(new ReisBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.Name;
                        textBoxDate.Text = Convert.ToString(view.Time);
                        textBoxCena.Text = Convert.ToString(view.Cena);
                        var viewr = logicr.Read(new RaionBindingModel { Id = view.OfId })?[0];
                        comboBox1.Text = viewr.Name;
                        viewr = logicr.Read(new RaionBindingModel { Id = view.ToId })?[0];
                        comboBox2.Text = viewr.Name;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                var viewr = logicr.Read(new RaionBindingModel { Name = comboBox1.Text })?[0];
                var viewr1 = logicr.Read(new RaionBindingModel { Name = comboBox2.Text })?[0];

                logic.CreateOrUpdate(new ReisBindingModel
                {
                    Id = id,
                    Name = textBoxName.Text,
                    Time = Convert.ToInt32(textBoxDate.Text),
                    Cena = Convert.ToDouble(textBoxCena.Text),
                    OfId = (int)viewr.Id,
                    ToId = (int)viewr1.Id,
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }
}

