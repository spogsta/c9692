using System.Windows.Forms;

namespace c9692
{
    public partial class AppointmentForm : Form
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewAppointments;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonCalendar;
        private System.Windows.Forms.TextBox textBoxInstruction;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewAppointments = new System.Windows.Forms.DataGridView();
            this.textBoxInstruction = new System.Windows.Forms.TextBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonCalendar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAppointments)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAppointments
            // 
            this.dataGridViewAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAppointments.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewAppointments.Name = "dataGridViewAppointments";
            this.dataGridViewAppointments.RowHeadersWidth = 51;
            this.dataGridViewAppointments.Size = new System.Drawing.Size(776, 396);
            this.dataGridViewAppointments.TabIndex = 0;
            // 
            // textBoxInstruction
            // 
            this.textBoxInstruction.Location = new System.Drawing.Point(12, 415);
            this.textBoxInstruction.Name = "textBoxInstruction";
            this.textBoxInstruction.Size = new System.Drawing.Size(776, 23);
            this.textBoxInstruction.TabIndex = 1;
            this.textBoxInstruction.Text = "Please use the customer form to add an appointment by selecting a customer and then clicking \"Add customer Appointment\".";
            this.textBoxInstruction.ReadOnly = true;
            this.textBoxInstruction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(93, 450);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate.TabIndex = 2;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(174, 450);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonCalendar
            // 
            this.buttonCalendar.Location = new System.Drawing.Point(255, 450);
            this.buttonCalendar.Name = "buttonCalendar";
            this.buttonCalendar.Size = new System.Drawing.Size(75, 23);
            this.buttonCalendar.TabIndex = 4;
            this.buttonCalendar.Text = "Calendar";
            this.buttonCalendar.UseVisualStyleBackColor = true;
            this.buttonCalendar.Click += new System.EventHandler(this.buttonCalendar_Click);
            // 
            // AppointmentForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.textBoxInstruction);
            this.Controls.Add(this.buttonCalendar);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.dataGridViewAppointments);
            this.Name = "AppointmentForm";
            this.Text = "Appointment Form";
            this.Load += new System.EventHandler(this.AppointmentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAppointments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}