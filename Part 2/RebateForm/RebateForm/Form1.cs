using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;

namespace RebateForm
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        //variables
        int flag = 0;
        int newflag = 0;
        int bsCount = 0;
        int dateFlag = 0;
        int emailFlag = 0;
        int zipFlag = 0;
        int phoneFlag = 0;
        int addCount = 0;

        //email regex
        public const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

        DateTime dateTime;        
        DateTime dt2 = DateTime.Now;

        string startTime;
        string endTime;
        List<string> startTimeForAddingEachRecord = new List<string>();
        List<string> endTimeForAddingEachRecord = new List<string>();
        List<TimeSpan> timeTakenForAddingEachRecord = new List<TimeSpan>();
        TimeSpan tempForDiff;
        List<TimeSpan> diffOfRecordsInTime = new List<TimeSpan>();

        int totalBSCount = 0;

        //date formate
        string format = "MM-dd-yyyy";

        //Dropdown box
        internal System.Windows.Forms.ComboBox ComboBox1;
        internal System.Windows.Forms.ComboBox ComboBox2;

        //dummy var
        string modifyFName = "";
        string modifyMName = "";
        string modifyLName = "";
        string modifyphone = "";

        public Form1()
        {
            InitializeComponent();

            hideAllReqLabels();
            hideCountLabels();
            requiredLbl.Hide();

            dateTextBox.Text = DateTime.Now.ToString("MM-dd-yyyy");

            //initialising gender drop down
            EventHandler handler1 = new EventHandler(genderTextBox_Click);
            this.ComboBox1 = new ComboBox();
            this.ComboBox1.Location = new System.Drawing.Point(116, 225);
            this.ComboBox1.Name = "genderTextBox";
            this.ComboBox1.Size = new System.Drawing.Size(60, 20);
            this.ComboBox1.TabIndex = 27;
            string[] items1 = new string[] { "Male", "Female", "Other" };
            ComboBox1.Items.AddRange(items1);
            this.Controls.Add(this.ComboBox1);

            //initialising Proof of purchase drop down
            EventHandler handler2 = new EventHandler(purchaseTextBox_Click);
            this.ComboBox2 = new ComboBox();
            this.ComboBox2.Location = new System.Drawing.Point(116, 307);
            this.ComboBox2.Name = "purchaseTextBox";
            this.ComboBox2.Size = new System.Drawing.Size(50, 20);
            this.ComboBox2.TabIndex = 27;
            string[] items2 = new string[] { "Yes", "No" };
            ComboBox2.Items.AddRange(items2);
            this.Controls.Add(this.ComboBox2);
        }

        //First function to call when app launches
        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(@"CS6326Asg2.txt"))
            {
                File.Delete(@"CS6326Asg2.txt");
            }
            
            //file creation
            try
            {
                using (System.IO.StreamWriter txt = new System.IO.StreamWriter(@"CS6326Asg2.txt", true))
                {
                    txt.Write("First Name" + "\t");
                    txt.Write("Middle Initials" + "\t");
                    txt.Write("Last Name" + "\t");
                    txt.Write("Address 1" + "\t");
                    txt.Write("Address 2" + "\t");
                    txt.Write("City" + "\t");
                    txt.Write("State" + "\t");
                    txt.Write("Zip Code" + "\t");
                    txt.Write("Gender" + "\t");
                    txt.Write("Phone" + "\t");
                    txt.Write("Email" + "\t");
                    txt.Write("Purchased" + "\t");
                    txt.Write("Date" + "\t");
                    txt.Write("Start Time" + "\t");
                    txt.Write("End Time" + "\t");
                    txt.Write("Backspace Count" + Environment.NewLine);
                }
            }
            catch (Exception e1)
            {
                Console.WriteLine("Exception: " + e1.Message);
            }
            string[] lines = System.IO.File.ReadAllLines(@"CS6326Asg2.txt");
            for(int i = 1; i < lines.Length; i++)
            {
                String[] lineWords = lines[i].Split('\t');
                String[] row = { lineWords[0] + " " + lineWords[1] + " " + lineWords[2], lineWords[9] };
                ListViewItem item = new ListViewItem(row);
                listView1.Items.Add(item);
            }
        }    

        //Call when click on Add Button
        private void addButton_Click(object sender, EventArgs e)
        {
            if (firstNameTextBox.Text == "" || lastNameTextBox.Text == "" || address1TextBox.Text == "" || 
                cityTextBox.Text == "" || stateTextBox.Text == "" || zipTextBox.Text == "" || zipTextBox.Text.Length != 5 || 
                ComboBox1.Text == "" || phoneNumberTextBox.Text == "" || phoneNumberTextBox.Text.Length != 10 || 
                emailAddressTextBox.Text == "" || ComboBox2.Text == "" || 
                dateTextBox.Text == "" || dateFlag == 0 || emailFlag == 0 || zipFlag == 0 || phoneFlag == 0)
            {
                //validation check
                if (firstNameTextBox.Text == "")
                {
                    firstNameReq.Show();
                }
                if (lastNameTextBox.Text == "")
                {
                    lastReq.Show();
                }
                if (address1TextBox.Text == "")
                {
                    address1Req.Show();
                }
                if (cityTextBox.Text == "")
                {
                    cityReq.Show();
                }
                if (stateTextBox.Text == "")
                {
                    stateReq.Show();
                }
                if (zipTextBox.Text == "")
                {
                    zipCodeReq.Show();
                }
                if (zipTextBox.Text.Length != 5)
                {
                    zipCountReq.Show();
                }
                if (ComboBox1.Text == "")
                {
                    genderReq.Show();
                    genderReqCount.Show();
                }
                else
                {
                    if(ComboBox1.Text == "Male" || ComboBox1.Text == "Female" || ComboBox1.Text == "Other")
                    {
                        genderReqCount.Hide();
                    }
                    else
                    {
                        genderReqCount.Text = "Enter only Male/Female/Other";
                        genderReqCount.Show();
                    }
                    genderReq.Hide();
                }
                if (phoneNumberTextBox.Text == "")
                {
                    phoneReq.Show();
                }
                if (phoneNumberTextBox.Text.Length != 10)
                {
                    phoneCountReq.Show();
                }
                if (emailAddressTextBox.Text == "")
                {
                    emailReq.Show();
                }
                if (ComboBox2.Text == "")
                {
                    purchasedReq.Show();
                    purchaseReqCount.Show();
                }
                else
                {
                    if (ComboBox2.Text == "Yes" || ComboBox2.Text == "No")
                    {
                        purchaseReqCount.Hide();
                    }
                    else
                    {
                        purchaseReqCount.Text = "Enter only Yes/No";
                        purchaseReqCount.Show();
                    }
                    purchasedReq.Hide();
                }
                if (dateTextBox.Text == "")
                {
                    dateReq.Show();
                }
                requiredLbl.Show();
                toolStripStatusLabel1.Text = "   Enter required and valid fields.";
            }
            else
            {
                //validation check
                if(ComboBox1.Text == "Male" || ComboBox1.Text == "Female" || ComboBox1.Text == "Other")
                {
                    if (ComboBox2.Text.Equals("Yes") || ComboBox2.Text.Equals("No"))
                    {
                        flag = 0;
                        string[] lines = System.IO.File.ReadAllLines(@"CS6326Asg2.txt");
                        for (int i = 1; i < lines.Length; i++)
                        {
                            String[] lineWords = lines[i].Split('\t');
                            if (firstNameTextBox.Text.Equals(lineWords[0]) && lastNameTextBox.Text.Equals(lineWords[2]) && phoneNumberTextBox.Text.Equals(lineWords[9]))
                            {
                                flag = 1;
                            }
                        }
                        if (flag == 0)
                        {
                            endTime = DateTime.Now.ToString("HH:mm:ss");
                            endTimeForAddingEachRecord.Add(endTime);

                            DateTime endTimer = DateTime.ParseExact(endTime, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                            DateTime startTimer = DateTime.ParseExact(startTime, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                            TimeSpan ts = (endTimer - startTimer);
                            timeTakenForAddingEachRecord.Add(ts);

                            totalBSCount = totalBSCount + bsCount;

                            //row added
                            add(firstNameTextBox.Text, middleInitialsTextBox.Text, lastNameTextBox.Text, address1TextBox.Text, address2TextBox.Text, cityTextBox.Text, stateTextBox.Text, zipTextBox.Text, ComboBox1.Text, phoneNumberTextBox.Text, emailAddressTextBox.Text, ComboBox2.Text, dateTextBox.Text, startTime, endTime, bsCount);
                            label15.Text = "Start Time: " + startTime;
                            label16.Text = "End Time: " + endTime;
                            label17.Text = "Time Taken: " + ts;
                            label18.Text = "Backspace count: " + bsCount;

                            addCount++;
                            bsCount = 0;
                            newflag = 0;
                            clearAllFields();

                            hideAllReqLabels();
                            hideCountLabels();
                            requiredLbl.Hide();

                            toolStripStatusLabel1.Text = "   Data saved.";
                            toolStripStatusLabel2.Text = "Start Time: " + startTime + ", End Time: " + endTime + ", Time Taken: " + ts;
                        }
                        else
                        {
                            toolStripStatusLabel1.Text = "   Data already exist.";
                        }
                        purchaseReqCount.Hide();
                    }
                    else
                    {
                        purchaseReqCount.Show();
                    }
                    genderReqCount.Hide();
                }
                else
                {
                    genderReqCount.Show();
                }
            }

            //analysis for 10 ros added
            if (addCount == 10)
            {
                DateTime initialTime = DateTime.ParseExact(startTimeForAddingEachRecord.First(), "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime exitTime = DateTime.ParseExact(endTimeForAddingEachRecord.Last(), "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                TimeSpan timeDifffor10Rows = exitTime - initialTime;

                for (int a = 0; a < startTimeForAddingEachRecord.Count-1; a++)
                {
                    DateTime endTimerofDiff = DateTime.ParseExact(endTimeForAddingEachRecord[a], "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime startTimerofDiff = DateTime.ParseExact(startTimeForAddingEachRecord[a+1], "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                    tempForDiff = startTimerofDiff - endTimerofDiff;
                    diffOfRecordsInTime.Add(tempForDiff);
                }

                try
                {
                    //file creation
                    using (System.IO.StreamWriter txt = new System.IO.StreamWriter(@"temp.txt", true))
                    {
                        txt.Write("Number of records: " + addCount + Environment.NewLine);
                        txt.Write("Minimum entry time: " + timeTakenForAddingEachRecord.Min().ToString().Split(':')[1] + ":" + timeTakenForAddingEachRecord.Min().ToString().Split(':')[2] + Environment.NewLine);
                        txt.Write("Maximum entry time: " + timeTakenForAddingEachRecord.Max().ToString().Split(':')[1] + ":" + timeTakenForAddingEachRecord.Max().ToString().Split(':')[2] + Environment.NewLine);
                        txt.Write("Average entry time: " + getAverageTime(timeTakenForAddingEachRecord) + Environment.NewLine);

                        txt.Write("Minimum inter-record time: " + diffOfRecordsInTime.Min().ToString().Split(':')[1] + ":" + diffOfRecordsInTime.Min().ToString().Split(':')[2] + Environment.NewLine);
                        txt.Write("Maximum inter-record time: " + diffOfRecordsInTime.Max().ToString().Split(':')[1] + ":" + diffOfRecordsInTime.Max().ToString().Split(':')[2] + Environment.NewLine);
                        txt.Write("Average inter-record time: " + getAverageTime(diffOfRecordsInTime) + Environment.NewLine);

                        txt.Write("Total time: " + timeDifffor10Rows.ToString().Split(':')[1] + ":" + timeDifffor10Rows.ToString().Split(':')[2] + Environment.NewLine);
                        txt.Write("Backspace count: " + totalBSCount);
                    }
                }
                catch (Exception e1)
                {
                    Console.WriteLine("Exception: " + e1.Message);
                }

                label20.Text = "Number of records: " + addCount;
                label21.Text = "Minimum entry time: " + timeTakenForAddingEachRecord.Min().ToString().Split(':')[1] + ":" + timeTakenForAddingEachRecord.Min().ToString().Split(':')[2];
                label22.Text = "Maximum entry time: " + timeTakenForAddingEachRecord.Max().ToString().Split(':')[1] + ":" + timeTakenForAddingEachRecord.Max().ToString().Split(':')[2];
                label23.Text = "Average entry time: " + getAverageTime(timeTakenForAddingEachRecord);
                label24.Text = "Minimum inter-record time: " + diffOfRecordsInTime.Min().ToString().Split(':')[1] + ":" + diffOfRecordsInTime.Min().ToString().Split(':')[2];
                label25.Text = "Maximum inter-record time: " + diffOfRecordsInTime.Max().ToString().Split(':')[1] + ":" + diffOfRecordsInTime.Max().ToString().Split(':')[2];
                label26.Text = "Average inter-record time: " + getAverageTime(diffOfRecordsInTime);
                label27.Text = "Total time: " + timeDifffor10Rows.ToString().Split(':')[1] + ":" + timeDifffor10Rows.ToString().Split(':')[2];
                label28.Text = "Backspace count: " + totalBSCount;
            }
        }

        //Function for adding new row into the database
        private void add(string fname, string mname, string lname, string address1, string address2, string city, string state, string zipcode, string gender, string phone, string email, string purchase, string date, string strTime, string endTime, int backspaceC)
        {
            string[] row = { fname + " " + mname + " " + lname, phone };
            ListViewItem item = new ListViewItem(row);
            listView1.Items.Add(item);

            try
            {
                using (System.IO.StreamWriter txt = new System.IO.StreamWriter(@"CS6326Asg2.txt", true))
                {
                    txt.Write(fname + "\t");
                    txt.Write(mname + "\t");
                    txt.Write(lname + "\t");
                    txt.Write(address1 + "\t");
                    txt.Write(address2 + "\t");
                    txt.Write(city + "\t");
                    txt.Write(state + "\t");
                    txt.Write(zipcode + "\t");
                    txt.Write(gender + "\t");
                    txt.Write(phone + "\t");
                    txt.Write(email + "\t");
                    txt.Write(purchase + "\t");
                    txt.Write(date + "\t");
                    txt.Write(strTime + "\t");
                    txt.Write(endTime + "\t");
                    txt.Write(backspaceC + "\t" + Environment.NewLine);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            newflag = 0;
        }

        //function for getingt the average of times which are passed as a list
        private string getAverageTime(List<TimeSpan> times)
        {
            int count = times.Count;
            int[] timesInSeconds = new int[count];
            // loop through times
            for (var i = 0; i < count; i++)
            {
                // parse
                var pieces = times[i].ToString().Split(':');
                int hrs = Int32.Parse(pieces[0]);
                int mins = Int32.Parse(pieces[1]);
                int secs = Int32.Parse(pieces[2]);
                // convert to 24 hr format (military time)
                
                // find value in seconds of time
                int totalSecs = hrs * 60 * 60;
                totalSecs += mins * 60;
                totalSecs += secs;
                // add to array
                timesInSeconds[i] = totalSecs;
            }
            // find average timesInSeconds
            int total = 0;
            for (int j = 0; j < count; j++)
            {
                total = total + timesInSeconds[j];
            }
            double x = total / count;
            int avg = (int)Math.Round(x);
            // turn seconds back into a time
            double y = avg / 60;
            int avgMins = (int)Math.Floor(y);
            int avgSecs = avg - (60 * avgMins);
            double z = avgMins / 60;
            int avgHrs = (int)Math.Floor(z);
            avgMins = avgMins - (60 * avgHrs);

            return avgMins.ToString().PadLeft(2, '0') + ':' + avgSecs.ToString().PadLeft(2, '0');
        }

        //Function Call when you click Modify Button
        private void modifyButton_Click(object sender, EventArgs e)
        {
            if (firstNameTextBox.Text == "" || lastNameTextBox.Text == "" || address1TextBox.Text == "" ||
                cityTextBox.Text == "" || stateTextBox.Text == "" || zipTextBox.Text == "" || zipTextBox.Text.Length != 5 ||
                ComboBox1.Text == "" || phoneNumberTextBox.Text == "" || phoneNumberTextBox.Text.Length != 10 ||
                emailAddressTextBox.Text == "" || ComboBox2.Text == "" ||
                dateTextBox.Text == "" || dateFlag == 0 || emailFlag == 0 || zipFlag == 0 || phoneFlag == 0)
            {
                if (firstNameTextBox.Text == "")
                {
                    firstNameReq.Show();
                }
                if (lastNameTextBox.Text == "")
                {
                    lastReq.Show();
                }
                if (address1TextBox.Text == "")
                {
                    address1Req.Show();
                }
                if (cityTextBox.Text == "")
                {
                    cityReq.Show();
                }
                if (stateTextBox.Text == "")
                {
                    stateReq.Show();
                }
                if (zipTextBox.Text == "")
                {
                    zipCodeReq.Show();
                }
                if (zipTextBox.Text.Length != 5)
                {
                    zipCountReq.Show();
                }
                if (ComboBox1.Text == "")
                {
                    genderReq.Show();
                    genderReqCount.Show();
                }
                else
                {
                    if (ComboBox1.Text == "Male" || ComboBox1.Text == "Female" || ComboBox1.Text == "Other")
                    {
                        genderReqCount.Hide();
                    }
                    else
                    {
                        genderReqCount.Text = "Enter only Male/Female/Other";
                        genderReqCount.Show();
                    }
                    genderReq.Hide();
                }
                if (phoneNumberTextBox.Text == "")
                {
                    phoneReq.Show();
                }
                if (phoneNumberTextBox.Text.Length != 10)
                {
                    phoneCountReq.Show();
                }
                if (emailAddressTextBox.Text == "")
                {
                    emailReq.Show();
                }
                if (ComboBox2.Text == "")
                {
                    purchasedReq.Show();
                    purchaseReqCount.Show();
                }
                else
                {
                    if (ComboBox2.Text == "Yes" || ComboBox2.Text == "No")
                    {
                        purchaseReqCount.Hide();
                    }
                    else
                    {
                        purchaseReqCount.Text = "Enter only Yes/No";
                        purchaseReqCount.Show();
                    }
                    purchasedReq.Hide();
                }
                if (dateTextBox.Text == "")
                {
                    dateReq.Show();
                }
                requiredLbl.Show();
                toolStripStatusLabel1.Text = "   Enter required and valid fields.";
            }
            else
            {
                if (ComboBox1.Text == "Male" || ComboBox1.Text == "Female" || ComboBox1.Text == "Other")
                {
                    if (ComboBox2.Text.Equals("Yes") || ComboBox2.Text.Equals("No"))
                    {
                        flag = 0;
                        string[] lines = System.IO.File.ReadAllLines(@"CS6326Asg2.txt");
                        for (int i = 1; i < lines.Length; i++)
                        {
                            String[] lineWords = lines[i].Split('\t');

                            if (firstNameTextBox.Text.Equals(lineWords[0]) && lastNameTextBox.Text.Equals(lineWords[2]) && phoneNumberTextBox.Text.Equals(lineWords[9]))
                            {
                                flag = 1;
                            }

                        }
                        if (flag == 0)
                        {
                            try
                            {
                                using (System.IO.StreamWriter txt = new System.IO.StreamWriter(@"CS6326Asg2_backup.txt", true))
                                {
                                    txt.Write("First Name" + "\t");
                                    txt.Write("Middle Initials" + "\t");
                                    txt.Write("Last Name" + "\t");
                                    txt.Write("Address 1" + "\t");
                                    txt.Write("Address 2" + "\t");
                                    txt.Write("City" + "\t");
                                    txt.Write("State" + "\t");
                                    txt.Write("Zip Code" + "\t");
                                    txt.Write("Gender" + "\t");
                                    txt.Write("Phone" + "\t");
                                    txt.Write("Email" + "\t");
                                    txt.Write("Purchased" + "\t");
                                    txt.Write("Date" + "\t");
                                    txt.Write("Start Time" + "\t");
                                    txt.Write("End Time");
                                    txt.Write("Backspace Count" + Environment.NewLine);
                                }
                            }
                            catch (Exception e1)
                            {
                                Console.WriteLine("Exception: " + e1.Message);
                            }
                            string[] editlines = System.IO.File.ReadAllLines(@"CS6326Asg2.txt");
                            for (int l = 1; l < editlines.Length; l++)
                            {
                                String[] editlineWords = editlines[l].Split('\t');
                                if (modifyFName.Equals(editlineWords[0]) && modifyLName.Equals(editlineWords[2]) && modifyphone.Equals(editlineWords[9]))
                                {
                                    try
                                    {
                                        using (System.IO.StreamWriter txt = new System.IO.StreamWriter(@"CS6326Asg2_backup.txt", true))
                                        {
                                            txt.Write(firstNameTextBox.Text + "\t");
                                            txt.Write(middleInitialsTextBox.Text + "\t");
                                            txt.Write(lastNameTextBox.Text + "\t");
                                            txt.Write(address1TextBox.Text + "\t");
                                            txt.Write(address2TextBox.Text + "\t");
                                            txt.Write(cityTextBox.Text + "\t");
                                            txt.Write(stateTextBox.Text + "\t");
                                            txt.Write(zipTextBox.Text + "\t");
                                            txt.Write(ComboBox1.Text + "\t");
                                            txt.Write(phoneNumberTextBox.Text + "\t");
                                            txt.Write(emailAddressTextBox.Text + "\t");
                                            txt.Write(ComboBox2.Text + "\t");
                                            txt.Write(dateTextBox.Text + "\t");
                                            txt.Write(editlineWords[13] + "\t");
                                            txt.Write(editlineWords[14] + "\t");
                                            txt.Write(editlineWords[15] + "\t" + Environment.NewLine);
                                        }
                                    }
                                    catch (Exception e1)
                                    {
                                        Console.WriteLine("Exception: " + e1.Message);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        using (System.IO.StreamWriter txt = new System.IO.StreamWriter(@"CS6326Asg2_backup.txt", true))
                                        {
                                            txt.Write(lines[l] + Environment.NewLine);
                                        }
                                    }
                                    catch (Exception e1)
                                    {
                                        Console.WriteLine("Exception: " + e1.Message);
                                    }
                                }
                            }
                            System.IO.File.Move(@"CS6326Asg2.txt", @"CS6326Asg2_temp.txt");
                            System.IO.File.Move(@"CS6326Asg2_backup.txt", @"CS6326Asg2.txt");
                            System.IO.File.Delete(@"CS6326Asg2_temp.txt");

                            clearAllFields();
                            hideAllReqLabels();
                            hideCountLabels();
                            requiredLbl.Hide();
                            toolStripStatusLabel1.Text = "   Data modified.";

                            string[] newlines = System.IO.File.ReadAllLines(@"CS6326Asg2.txt");

                            for (int i = 0; i < listView1.Items.Count; i++)
                            {
                                listView1.Items[i].Remove();
                                i--;
                            }
                            for (int j = 1; j < newlines.Length; j++)
                            {
                                string[] newArr = newlines[j].Split('\t');
                                string[] newrow = { newArr[0] + " " + newArr[1] + " " + newArr[2], newArr[9] };
                                ListViewItem newitem = new ListViewItem(newrow);
                                listView1.Items.Add(newitem);
                            }
                        }
                        else
                        {
                            toolStripStatusLabel1.Text = "   Modified data already exist.";
                        }
                        purchaseReqCount.Hide();
                    }
                    else
                    {
                        purchaseReqCount.Show();
                    }
                    genderReqCount.Hide();
                }
                else
                {
                    genderReqCount.Show();
                }
            }
        }

        //Call when you click Delete Button
        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (System.IO.StreamWriter txt = new System.IO.StreamWriter(@"CS6326Asg2_backup.txt", true))
                {
                    txt.Write("First Name" + "\t");
                    txt.Write("Middle Initials" + "\t");
                    txt.Write("Last Name" + "\t");
                    txt.Write("Address 1" + "\t");
                    txt.Write("Address 2" + "\t");
                    txt.Write("City" + "\t");
                    txt.Write("State" + "\t");
                    txt.Write("Zip Code" + "\t");
                    txt.Write("Gender" + "\t");
                    txt.Write("Phone" + "\t");
                    txt.Write("Email" + "\t");
                    txt.Write("Purchased" + "\t");
                    txt.Write("Date" + "\t");
                    txt.Write("Start Time" + "\t");
                    txt.Write("End Time" + "\t");
                    txt.Write("Backspace Count" + Environment.NewLine);
                }
            }
            catch (Exception e1)
            {
                Console.WriteLine("Exception: " + e1.Message);
            }
            flag = 0;
            string[] lines = System.IO.File.ReadAllLines(@"CS6326Asg2.txt");
            for (int i = 1; i < lines.Length; i++)
            {
                String[] lineWords = lines[i].Split('\t');
                if (firstNameTextBox.Text.Equals(lineWords[0]) && lastNameTextBox.Text.Equals(lineWords[2]) && phoneNumberTextBox.Text.Equals(lineWords[9]))
                {

                }
                else
                {
                    try
                    {
                        using (System.IO.StreamWriter txt = new System.IO.StreamWriter(@"CS6326Asg2_backup.txt", true))
                        {
                            txt.Write(lines[i] + Environment.NewLine);
                        }
                    }
                    catch (Exception e1)
                    {
                        Console.WriteLine("Exception: " + e1.Message);
                    }
                }
            }
            System.IO.File.Move(@"CS6326Asg2.txt", @"CS6326Asg2_temp.txt");
            System.IO.File.Move(@"CS6326Asg2_backup.txt", @"CS6326Asg2.txt");
            System.IO.File.Delete(@"CS6326Asg2_temp.txt");
            System.IO.File.Delete(@"CS6326Asg2_backup.tx");
            clearAllFields();
            hideAllReqLabels();
            hideCountLabels();
            requiredLbl.Hide();
            toolStripStatusLabel1.Text = "   Data deleted.";

            string[] newlines = System.IO.File.ReadAllLines(@"CS6326Asg2.txt");

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].Remove();
                i--;
            }

            for (int j = 1; j < newlines.Length; j++)
            {
                string[] newArr = newlines[j].Split('\t');
                string[] newrow = { newArr[0] + " " + newArr[1] + " " + newArr[2], newArr[9] };
                ListViewItem newitem = new ListViewItem(newrow);
                listView1.Items.Add(newitem);
            }

        }

        //Call when click Reset Button
        private void resetButton_Click(object sender, EventArgs e)
        {
            clearAllFields();
            toolStripStatusLabel1.Text = "   All fields reseted.";
        }

        //Call when you click on a row of listView
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel2.Text = "";
            String name = listView1.SelectedItems[0].SubItems[0].Text;
            String[] nameArr = name.Split(' ');
            String phone = listView1.SelectedItems[0].SubItems[1].Text;

            string[] lines = System.IO.File.ReadAllLines(@"CS6326Asg2.txt");
            foreach (string line in lines)
            {
                String[] lineWords = line.Split('\t');

                if (lineWords[0].Equals(nameArr[0]) && lineWords[1].Equals(nameArr[1]) && lineWords[2].Equals(nameArr[2]) && lineWords[9].Equals(phone))
                {
                    firstNameTextBox.Text = lineWords[0];
                    modifyFName = lineWords[0];
                    middleInitialsTextBox.Text = lineWords[1];
                    modifyMName = lineWords[1];
                    lastNameTextBox.Text = lineWords[2];
                    modifyLName = lineWords[2];
                    address1TextBox.Text = lineWords[3];
                    address2TextBox.Text = lineWords[4];
                    cityTextBox.Text = lineWords[5];
                    stateTextBox.Text = lineWords[6];
                    zipTextBox.Text = lineWords[7];
                    ComboBox1.Text = lineWords[8];
                    phoneNumberTextBox.Text = lineWords[9];
                    modifyphone = lineWords[9];
                    emailAddressTextBox.Text = lineWords[10];
                    ComboBox2.Text = lineWords[11];
                    dateTextBox.Text = lineWords[12];
                }
            }
        }

        private void s(object sender, EventArgs e)
        {
            
        }

        // TextBox Events
        private void firstNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (newflag == 0 && firstNameTextBox.Text.Length > 0)
            {
                startTime = DateTime.Now.ToString("HH:mm:ss");
                startTimeForAddingEachRecord.Add(startTime);
                newflag = 1;
            }
            if (firstNameTextBox.Text == "")
            {
                firstNameReq.Show();
                requiredLbl.Show();
            }
            else
            {
                firstNameReq.Hide();
                requiredLbl.Hide();
            }
        }

        private void lastNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (lastNameTextBox.Text == "")
            {
                lastReq.Show();
                requiredLbl.Show();
            }
            else
            {
                lastReq.Hide();
                requiredLbl.Hide();
            }
        }

        private void address1TextBox_TextChanged(object sender, EventArgs e)
        {
            if (address1TextBox.Text == "")
            {
                address1Req.Show();
                requiredLbl.Show();
            }
            else
            {
                address1Req.Hide();
                requiredLbl.Hide();
            }
        }

        private void cityTextBox_TextChanged(object sender, EventArgs e)
        {
            if (cityTextBox.Text == "")
            {
                cityReq.Show();
                requiredLbl.Show();
            }
            else
            {
                cityReq.Hide();
                requiredLbl.Hide();
            }
        }

        private void stateTextBox_TextChanged(object sender, EventArgs e)
        {
            if (stateTextBox.Text == "")
            {
                stateReq.Show();
                requiredLbl.Show();
            }
            else
            {
                stateReq.Hide();
                requiredLbl.Hide();
            }
        }

        private void zipTextBox_TextChanged(object sender, EventArgs e)
        {
            if (zipTextBox.Text == "")
            {
                zipCodeReq.Show();
                requiredLbl.Show();
                zipCountReq.Hide();
                zipFlag = 0;
            }
            else
            {
                if (zipTextBox.Text.Length == 5)
                {
                    if (zipTextBox.Text.All(char.IsDigit))
                    {
                        zipCountReq.Hide();
                        zipFlag = 1;
                    }
                    else
                    {
                        zipCountReq.Text = "Enter numeric digit only";
                        zipCountReq.Show();
                        zipFlag = 0;
                    }
                }
                else
                {
                    zipCountReq.Text = "Enter 5 digit only";
                    zipCountReq.Show();
                    zipFlag = 0;
                }
                zipCodeReq.Hide();
                requiredLbl.Hide();
            }
        }

        private void genderTextBox_Click(object sender, EventArgs e)
        {
            if (ComboBox1.Text == "")
            {
                genderReq.Show();
                requiredLbl.Show();
            }
            else
            {
                genderReq.Hide();
                requiredLbl.Hide();
            }
        }

        private void phoneNumberTextBox_TextChanged(object sender, EventArgs e)
        {

            if (phoneNumberTextBox.Text == "")
            {
                phoneReq.Show();
                requiredLbl.Show();
                phoneCountReq.Hide();
                phoneFlag = 0;
            }
            else
            {
                if (phoneNumberTextBox.Text.Length == 10)
                {
                    if (phoneNumberTextBox.Text.All(char.IsDigit))
                    {
                        phoneCountReq.Hide();
                        phoneFlag = 1;
                    }
                    else
                    {
                        phoneCountReq.Text = "Enter numeric digit only";
                        phoneCountReq.Show();
                        phoneFlag = 0;
                    }
                }
                else
                {
                    phoneCountReq.Text = "Enter 10 digit only";
                    phoneCountReq.Show();
                    phoneFlag = 0;
                }
                phoneReq.Hide();
                requiredLbl.Hide();
            }
        }

        private void emailAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            if (emailAddressTextBox.Text == "")
            {
                emailReq.Show();
                requiredLbl.Show();
                emailValidReq.Hide();
                emailFlag = 0;
            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(emailAddressTextBox.Text, MatchEmailPattern))
                {
                    emailValidReq.Hide();
                    emailFlag = 1;
                }
                else
                {
                    emailValidReq.Text = "Enter valid email id.";
                    emailValidReq.Show();
                    emailFlag = 0;
                }
                emailReq.Hide();
                requiredLbl.Hide();
            }
        }

        private void purchaseTextBox_Click(object sender, EventArgs e)
        {
            if (ComboBox2.Text == "")
            {
                purchasedReq.Show();
                requiredLbl.Show();
            }
            else
            {
                purchasedReq.Hide();
                requiredLbl.Hide();
            }
        }

        private void dateTextBox_TextChanged(object sender, EventArgs e)
        {
            if (dateTextBox.Text == "")
            {
                dateReq.Show();
                requiredLbl.Show();
                dateCountReq.Hide();
                dateFlag = 0;
            }
            else
            {
                if (DateTime.TryParseExact(dateTextBox.Text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {
                    DateTime dt1 = DateTime.ParseExact(dateTextBox.Text, format, CultureInfo.InvariantCulture);
                    if (dt1.Date > dt2.Date)
                    {
                        dateCountReq.Text = "Eenter today's or past date";
                        dateCountReq.Show();
                        dateFlag = 0;
                    }
                    else
                    {
                        dateCountReq.Hide();
                        dateFlag = 1;
                    }
                }
                else
                {
                    dateCountReq.Text = "Enter valid date in MM-dd-yyyy formate";
                    dateCountReq.Show();
                    dateFlag = 0;
                }
                dateReq.Hide();
                requiredLbl.Hide();
            }
        }

        private void firstNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            countForBackSpace(e);
        }

        private void middleInitialsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            countForBackSpace(e);
        }

        private void lastNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            countForBackSpace(e);
        }

        private void address1TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            countForBackSpace(e);
        }

        private void address2TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            countForBackSpace(e);
        }

        private void cityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            countForBackSpace(e);
        }

        private void stateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            countForBackSpace(e);
        }

        private void zipTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            countForBackSpace(e);
        }

        private void genderTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            countForBackSpace(e);
        }

        private void phoneNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            countForBackSpace(e);
        }

        private void emailAddressTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            countForBackSpace(e);
        }

        private void purchaserTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            countForBackSpace(e);
        }

        private void dateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            countForBackSpace(e);
        }

        //Function for hide all the * required labels
        private void hideAllReqLabels()
        {
            firstNameReq.Hide();
            lastReq.Hide();
            address1Req.Hide();
            cityReq.Hide();
            stateReq.Hide();
            zipCodeReq.Hide();
            genderReq.Hide();
            phoneReq.Hide();
            emailReq.Hide();
            emailValidReq.Hide();
            purchasedReq.Hide();
            dateReq.Hide();
            dateCountReq.Hide();
        }

        //Function for hide all the count labels
        private void hideCountLabels()
        {
            zipCountReq.Hide();
            phoneCountReq.Hide();
            genderReqCount.Hide();
            purchaseReqCount.Hide();
        }

        //Function for clear all the TextBox fields
        private void clearAllFields()
        {
            firstNameTextBox.Text = "";
            middleInitialsTextBox.Text = "";
            lastNameTextBox.Text = "";
            address1TextBox.Text = "";
            address2TextBox.Text = "";
            cityTextBox.Text = "";
            stateTextBox.Text = "";
            zipTextBox.Text = "";
            ComboBox1.Text = "";
            phoneNumberTextBox.Text = "";
            emailAddressTextBox.Text = "";
            ComboBox2.Text = "";
            dateTextBox.Text = "";
        }

        //Function for counting backspace button press
        private void countForBackSpace(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                bsCount++;
            }
        }

        //Function call when import button click
        private void button1_Click(object sender, EventArgs e)
        {
            if(filenameTextBox.Text != "")
            {
                if(addCount >= 10)
                {
                    System.IO.File.Move(@"temp.txt", filenameTextBox.Text + ".txt");
                    System.IO.File.Delete(@"CS6326Asg2_temp.txt");
                    toolStripStatusLabel1.Text = "   Data imported in " + filenameTextBox.Text + ".txt file. Please check Debug folder.";
                    toolStripStatusLabel2.Text = "";
                } 
                else
                {
                    toolStripStatusLabel1.Text = "   Add atleat 10 rows.";
                    toolStripStatusLabel2.Text = "";
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "   Enter the file name.";
                toolStripStatusLabel2.Text = "";
            }
        }
    }
}
