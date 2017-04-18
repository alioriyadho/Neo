namespace Neo
{
    partial class PersonForm
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comments = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mother_firstname = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.interpreter = new System.Windows.Forms.CheckBox();
            this.mother_lastname = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.person_id = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.birthdayPicker = new System.Windows.Forms.DateTimePicker();
            this.child_firstname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.child_lastname = new System.Windows.Forms.TextBox();
            this.savebutton = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comments);
            this.groupBox3.Location = new System.Drawing.Point(12, 250);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(334, 128);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Övriga kommentarer";
            // 
            // comments
            // 
            this.comments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.comments.Location = new System.Drawing.Point(6, 19);
            this.comments.Name = "comments";
            this.comments.Size = new System.Drawing.Size(322, 103);
            this.comments.TabIndex = 14;
            this.comments.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.mother_firstname);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.interpreter);
            this.groupBox2.Controls.Add(this.mother_lastname);
            this.groupBox2.Location = new System.Drawing.Point(12, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(334, 95);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Barents mamma";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Förnamn";
            // 
            // mother_firstname
            // 
            this.mother_firstname.Location = new System.Drawing.Point(121, 19);
            this.mother_firstname.Name = "mother_firstname";
            this.mother_firstname.Size = new System.Drawing.Size(197, 20);
            this.mother_firstname.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Efternamn";
            // 
            // interpreter
            // 
            this.interpreter.AutoSize = true;
            this.interpreter.Location = new System.Drawing.Point(121, 71);
            this.interpreter.Name = "interpreter";
            this.interpreter.Size = new System.Drawing.Size(85, 17);
            this.interpreter.TabIndex = 13;
            this.interpreter.Text = "Tolk behövs";
            this.interpreter.UseVisualStyleBackColor = true;
            // 
            // mother_lastname
            // 
            this.mother_lastname.Location = new System.Drawing.Point(121, 45);
            this.mother_lastname.Name = "mother_lastname";
            this.mother_lastname.Size = new System.Drawing.Size(197, 20);
            this.mother_lastname.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.person_id);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.birthdayPicker);
            this.groupBox1.Controls.Add(this.child_firstname);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.child_lastname);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 131);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Barnet";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(26, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Planerad födsel";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(29, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Personnummer";
            // 
            // person_id
            // 
            this.person_id.Location = new System.Drawing.Point(121, 19);
            this.person_id.Name = "person_id";
            this.person_id.Size = new System.Drawing.Size(197, 20);
            this.person_id.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(58, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Förnamn";
            // 
            // birthdayPicker
            // 
            this.birthdayPicker.Location = new System.Drawing.Point(121, 97);
            this.birthdayPicker.MinDate = new System.DateTime(1960, 1, 1, 0, 0, 0, 0);
            this.birthdayPicker.Name = "birthdayPicker";
            this.birthdayPicker.Size = new System.Drawing.Size(197, 20);
            this.birthdayPicker.TabIndex = 12;
            // 
            // child_firstname
            // 
            this.child_firstname.Location = new System.Drawing.Point(121, 45);
            this.child_firstname.Name = "child_firstname";
            this.child_firstname.Size = new System.Drawing.Size(197, 20);
            this.child_firstname.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(51, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Efternamn";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // child_lastname
            // 
            this.child_lastname.Location = new System.Drawing.Point(121, 71);
            this.child_lastname.Name = "child_lastname";
            this.child_lastname.Size = new System.Drawing.Size(197, 20);
            this.child_lastname.TabIndex = 7;
            // 
            // savebutton
            // 
            this.savebutton.Location = new System.Drawing.Point(12, 385);
            this.savebutton.Name = "savebutton";
            this.savebutton.Size = new System.Drawing.Size(333, 34);
            this.savebutton.TabIndex = 21;
            this.savebutton.Text = "Spara";
            this.savebutton.UseVisualStyleBackColor = true;
            this.savebutton.Click += new System.EventHandler(this.savebutton_Click);
            // 
            // PersonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 432);
            this.Controls.Add(this.savebutton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PersonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PersonForm";
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox comments;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox mother_firstname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox interpreter;
        private System.Windows.Forms.TextBox mother_lastname;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox person_id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker birthdayPicker;
        private System.Windows.Forms.TextBox child_firstname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox child_lastname;
        private System.Windows.Forms.Button savebutton;
    }
}