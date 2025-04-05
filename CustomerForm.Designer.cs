using System.Windows.Forms;

namespace c9692
{
    public partial class CustomerForm : Form
    {
        private void InitializeComponent()
        {
            this.dataGridViewCustomers = new System.Windows.Forms.DataGridView();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAppointment = new System.Windows.Forms.Button(); // Add this line
            this.buttonCustomerAppointment = new System.Windows.Forms.Button(); // Add this line
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCustomers
            // 
            this.dataGridViewCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCustomers.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewCustomers.Name = "dataGridViewCustomers";
            this.dataGridViewCustomers.Size = new System.Drawing.Size(776, 396);
            this.dataGridViewCustomers.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(12, 414);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(93, 414);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate.TabIndex = 2;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(174, 414);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAppointment
            // 
            this.buttonAppointment.Location = new System.Drawing.Point(255, 414);
            this.buttonAppointment.Name = "buttonAppointment";
            this.buttonAppointment.Size = new System.Drawing.Size(100, 23);
            this.buttonAppointment.TabIndex = 4;
            this.buttonAppointment.Text = "Appointments";
            this.buttonAppointment.UseVisualStyleBackColor = true;
            this.buttonAppointment.Click += new System.EventHandler(this.buttonAppointment_Click);
            // 
            // buttonCustomerAppointment
            // 
            this.buttonCustomerAppointment.Location = new System.Drawing.Point(361, 414);
            this.buttonCustomerAppointment.Name = "buttonCustomerAppointment";
            this.buttonCustomerAppointment.Size = new System.Drawing.Size(150, 23);
            this.buttonCustomerAppointment.TabIndex = 5;
            this.buttonCustomerAppointment.Text = "Customer Appointment";
            this.buttonCustomerAppointment.UseVisualStyleBackColor = true;
            this.buttonCustomerAppointment.Click += new System.EventHandler(this.buttonCustomerAppointment_Click);
            // 
            // CustomerForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonCustomerAppointment);
            this.Controls.Add(this.buttonAppointment);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.dataGridViewCustomers);
            this.Name = "CustomerForm";
            this.Load += new System.EventHandler(this.CustomerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dataGridViewCustomers;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAppointment; // Add this line
        private System.Windows.Forms.Button buttonCustomerAppointment; // Add this line
    }
}