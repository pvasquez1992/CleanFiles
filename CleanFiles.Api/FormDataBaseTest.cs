using FormDB.BussinesLogic;
using System;
using System.Windows.Forms;

namespace CleanFiles.Api
{
    public partial class FormDataBaseTest : Form
    {
        public FormDataBaseTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                int id = 0;

                int.TryParse(txtId.Text, out id);

                var customer = new CustomerQfBol().GetCustomerById(id);

                var msj = $"Id: {customer.CustomerId}, nombres: {string.Concat(customer.FirstName, customer.LastName)}";
                MessageBox.Show(msj);
            }
            catch (Exception ex)
            {
                var msj = $"error -> {ex.Message} , track -> {ex.StackTrace}";
                MessageBox.Show(msj);

            }


        }
    }
}
