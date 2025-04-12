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
        private System.Windows.Forms.Button buttonAdjustToLocal;
        private System.Windows.Forms.Button buttonRevertToOriginal;
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
            this.buttonAdjustToLocal = new System.Windows.Forms.Button();
            this.buttonRevertToOriginal = new System.Windows.Forms.Button();
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
            this.textBoxInstruction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxInstruction.Location = new System.Drawing.Point(12, 415);
            this.textBoxInstruction.Name = "textBoxInstruction";
            this.textBoxInstruction.ReadOnly = true;
            this.textBoxInstruction.Size = new System.Drawing.Size(776, 15);
            this.textBoxInstruction.TabIndex = 1;
            this.textBoxInstruction.Text = "Please use the customer form to add an appointment by selecting a customer and th" +
    "en clicking \"Add customer Appointment\". Times are displayed in local time.";
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
            // buttonAdjustToLocal
            // 
            this.buttonAdjustToLocal.Location = new System.Drawing.Point(336, 450);
            this.buttonAdjustToLocal.Name = "buttonAdjustToLocal";
            this.buttonAdjustToLocal.Size = new System.Drawing.Size(194, 23);
            this.buttonAdjustToLocal.TabIndex = 5;
            this.buttonAdjustToLocal.Text = "Adjust to Local Timezone";
            this.buttonAdjustToLocal.UseVisualStyleBackColor = true;
            this.buttonAdjustToLocal.Click += new System.EventHandler(this.buttonAdjustToLocal_Click);
            // 
            // buttonRevertToOriginal
            // 
            this.buttonRevertToOriginal.Location = new System.Drawing.Point(536, 450);
            this.buttonRevertToOriginal.Name = "buttonRevertToOriginal";
            this.buttonRevertToOriginal.Size = new System.Drawing.Size(150, 23);
            this.buttonRevertToOriginal.TabIndex = 6;
            this.buttonRevertToOriginal.Text = "Revert to EST";
            this.buttonRevertToOriginal.UseVisualStyleBackColor = true;
            this.buttonRevertToOriginal.Click += new System.EventHandler(this.buttonRevertToOriginal_Click);
            // 
            // AppointmentForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.buttonRevertToOriginal);
            this.Controls.Add(this.buttonAdjustToLocal);
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