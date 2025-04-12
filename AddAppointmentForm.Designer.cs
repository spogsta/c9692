using System.Windows.Forms;

namespace c9692
{
    public partial class AddAppointmentForm : Form
    {
        private void InitializeComponent()
        {
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.textBoxContact = new System.Windows.Forms.TextBox();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.numericUpDownStartHour = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownStartMinute = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEndHour = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEndMinute = new System.Windows.Forms.NumericUpDown();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelLocation = new System.Windows.Forms.Label();
            this.labelContact = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.labelUrl = new System.Windows.Forms.Label();
            this.labelStart = new System.Windows.Forms.Label();
            this.labelEnd = new System.Windows.Forms.Label();
            this.labelStartHour = new System.Windows.Forms.Label();
            this.labelStartMinute = new System.Windows.Forms.Label();
            this.labelEndHour = new System.Windows.Forms.Label();
            this.labelEndMinute = new System.Windows.Forms.Label();
            this.labelTimeInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndMinute)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(120, 12);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(200, 22);
            this.textBoxTitle.TabIndex = 0;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(120, 38);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(200, 22);
            this.textBoxDescription.TabIndex = 1;
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Location = new System.Drawing.Point(120, 64);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(200, 22);
            this.textBoxLocation.TabIndex = 2;
            // 
            // textBoxContact
            // 
            this.textBoxContact.Location = new System.Drawing.Point(120, 90);
            this.textBoxContact.Name = "textBoxContact";
            this.textBoxContact.Size = new System.Drawing.Size(200, 22);
            this.textBoxContact.TabIndex = 3;
            // 
            // textBoxType
            // 
            this.textBoxType.Location = new System.Drawing.Point(120, 116);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.Size = new System.Drawing.Size(200, 22);
            this.textBoxType.TabIndex = 4;
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Location = new System.Drawing.Point(120, 142);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(200, 22);
            this.textBoxUrl.TabIndex = 5;
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Location = new System.Drawing.Point(120, 168);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(200, 22);
            this.dateTimePickerStart.TabIndex = 6;
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.Location = new System.Drawing.Point(120, 208);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(200, 22);
            this.dateTimePickerEnd.TabIndex = 7;
            // 
            // numericUpDownStartHour
            // 
            this.numericUpDownStartHour.Location = new System.Drawing.Point(330, 168);
            this.numericUpDownStartHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownStartHour.Name = "numericUpDownStartHour";
            this.numericUpDownStartHour.Size = new System.Drawing.Size(40, 22);
            this.numericUpDownStartHour.TabIndex = 8;
            // 
            // numericUpDownStartMinute
            // 
            this.numericUpDownStartMinute.Location = new System.Drawing.Point(380, 168);
            this.numericUpDownStartMinute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownStartMinute.Name = "numericUpDownStartMinute";
            this.numericUpDownStartMinute.Size = new System.Drawing.Size(40, 22);
            this.numericUpDownStartMinute.TabIndex = 9;
            // 
            // numericUpDownEndHour
            // 
            this.numericUpDownEndHour.Location = new System.Drawing.Point(330, 208);
            this.numericUpDownEndHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownEndHour.Name = "numericUpDownEndHour";
            this.numericUpDownEndHour.Size = new System.Drawing.Size(40, 22);
            this.numericUpDownEndHour.TabIndex = 10;
            // 
            // numericUpDownEndMinute
            // 
            this.numericUpDownEndMinute.Location = new System.Drawing.Point(380, 208);
            this.numericUpDownEndMinute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownEndMinute.Name = "numericUpDownEndMinute";
            this.numericUpDownEndMinute.Size = new System.Drawing.Size(40, 22);
            this.numericUpDownEndMinute.TabIndex = 11;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(120, 234);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(12, 15);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(33, 16);
            this.labelTitle.TabIndex = 13;
            this.labelTitle.Text = "Title";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(12, 41);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(75, 16);
            this.labelDescription.TabIndex = 14;
            this.labelDescription.Text = "Description";
            // 
            // labelLocation
            // 
            this.labelLocation.AutoSize = true;
            this.labelLocation.Location = new System.Drawing.Point(12, 67);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(58, 16);
            this.labelLocation.TabIndex = 15;
            this.labelLocation.Text = "Location";
            // 
            // labelContact
            // 
            this.labelContact.AutoSize = true;
            this.labelContact.Location = new System.Drawing.Point(12, 93);
            this.labelContact.Name = "labelContact";
            this.labelContact.Size = new System.Drawing.Size(52, 16);
            this.labelContact.TabIndex = 16;
            this.labelContact.Text = "Contact";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(12, 119);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(39, 16);
            this.labelType.TabIndex = 17;
            this.labelType.Text = "Type";
            // 
            // labelUrl
            // 
            this.labelUrl.AutoSize = true;
            this.labelUrl.Location = new System.Drawing.Point(12, 145);
            this.labelUrl.Name = "labelUrl";
            this.labelUrl.Size = new System.Drawing.Size(24, 16);
            this.labelUrl.TabIndex = 18;
            this.labelUrl.Text = "Url";
            // 
            // labelStart
            // 
            this.labelStart.AutoSize = true;
            this.labelStart.Location = new System.Drawing.Point(12, 174);
            this.labelStart.Name = "labelStart";
            this.labelStart.Size = new System.Drawing.Size(34, 16);
            this.labelStart.TabIndex = 19;
            this.labelStart.Text = "Start";
            // 
            // labelEnd
            // 
            this.labelEnd.AutoSize = true;
            this.labelEnd.Location = new System.Drawing.Point(12, 208);
            this.labelEnd.Name = "labelEnd";
            this.labelEnd.Size = new System.Drawing.Size(31, 16);
            this.labelEnd.TabIndex = 20;
            this.labelEnd.Text = "End";
            // 
            // labelStartHour
            // 
            this.labelStartHour.AutoSize = true;
            this.labelStartHour.Location = new System.Drawing.Point(330, 152);
            this.labelStartHour.Name = "labelStartHour";
            this.labelStartHour.Size = new System.Drawing.Size(36, 16);
            this.labelStartHour.TabIndex = 21;
            this.labelStartHour.Text = "Hour";
            // 
            // labelStartMinute
            // 
            this.labelStartMinute.AutoSize = true;
            this.labelStartMinute.Location = new System.Drawing.Point(380, 152);
            this.labelStartMinute.Name = "labelStartMinute";
            this.labelStartMinute.Size = new System.Drawing.Size(46, 16);
            this.labelStartMinute.TabIndex = 22;
            this.labelStartMinute.Text = "Minute";
            // 
            // labelEndHour
            // 
            this.labelEndHour.AutoSize = true;
            this.labelEndHour.Location = new System.Drawing.Point(330, 237);
            this.labelEndHour.Name = "labelEndHour";
            this.labelEndHour.Size = new System.Drawing.Size(36, 16);
            this.labelEndHour.TabIndex = 23;
            this.labelEndHour.Text = "Hour";
            // 
            // labelEndMinute
            // 
            this.labelEndMinute.AutoSize = true;
            this.labelEndMinute.Location = new System.Drawing.Point(377, 237);
            this.labelEndMinute.Name = "labelEndMinute";
            this.labelEndMinute.Size = new System.Drawing.Size(46, 16);
            this.labelEndMinute.TabIndex = 24;
            this.labelEndMinute.Text = "Minute";
            // 
            // labelTimeInfo
            // 
            this.labelTimeInfo.AutoSize = true;
            this.labelTimeInfo.Location = new System.Drawing.Point(12, 270);
            this.labelTimeInfo.Name = "labelTimeInfo";
            this.labelTimeInfo.Size = new System.Drawing.Size(435, 16);
            this.labelTimeInfo.TabIndex = 25;
            this.labelTimeInfo.Text = "Times are in 24 hour format entered in your local time - converted to EST.";
            // 
            // AddAppointmentForm
            // 
            this.ClientSize = new System.Drawing.Size(546, 319);
            this.Controls.Add(this.labelTimeInfo);
            this.Controls.Add(this.labelEndMinute);
            this.Controls.Add(this.labelEndHour);
            this.Controls.Add(this.labelStartMinute);
            this.Controls.Add(this.labelStartHour);
            this.Controls.Add(this.labelEnd);
            this.Controls.Add(this.labelStart);
            this.Controls.Add(this.labelUrl);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.labelContact);
            this.Controls.Add(this.labelLocation);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.numericUpDownEndMinute);
            this.Controls.Add(this.numericUpDownEndHour);
            this.Controls.Add(this.numericUpDownStartMinute);
            this.Controls.Add(this.numericUpDownStartHour);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.textBoxUrl);
            this.Controls.Add(this.textBoxType);
            this.Controls.Add(this.textBoxContact);
            this.Controls.Add(this.textBoxLocation);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxTitle);
            this.Name = "AddAppointmentForm";
            this.Text = "Add Appointment";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndMinute)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.TextBox textBoxContact;
        private System.Windows.Forms.TextBox textBoxType;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.NumericUpDown numericUpDownStartHour;
        private System.Windows.Forms.NumericUpDown numericUpDownStartMinute;
        private System.Windows.Forms.NumericUpDown numericUpDownEndHour;
        private System.Windows.Forms.NumericUpDown numericUpDownEndMinute;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.Label labelContact;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label labelUrl;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label labelEnd;
        private System.Windows.Forms.Label labelStartHour;
        private System.Windows.Forms.Label labelStartMinute;
        private System.Windows.Forms.Label labelEndHour;
        private System.Windows.Forms.Label labelEndMinute;
        private System.Windows.Forms.Label labelTimeInfo;
    }
}