//
//  Form1.cs - CS file to search a word in a large size text file using MultiThreading.
//  Project Name: TextSearch
//
//  Created by Prashuk Ajmera on 3/20/2019.
//  Last Modified by Prashuk Ajmera on 3/27/2019.
//  Copyright © 2019 Prashuk Ajmera. All rights reserved.
//

using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace TextSearch
{
    public partial class Search : Form
    {
        BackgroundWorker background = new BackgroundWorker();
        OpenFileDialog openFileDialog = new OpenFileDialog();

        string fileName;
        int size = 0;

        public Search()
        {
            InitializeComponent();

            InitializeBackgroundWorker();
        }

        // Initializing components for Background Worker
        private void InitializeBackgroundWorker()
        {
            background.DoWork += new DoWorkEventHandler(background_DoWork);
            background.ProgressChanged += new ProgressChangedEventHandler(background_ProgressChanged);
            background.RunWorkerCompleted += new RunWorkerCompletedEventHandler(background_RunWorkerCompleted);
            background.WorkerReportsProgress = true;
            background.WorkerSupportsCancellation = true;
        }

        // Calls when Browse Button CLicked
        private void browseBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                try
                {
                    fileNameTxtBox.Text = fileName;
                    FileInfo fileNameInfo = new FileInfo(fileName);
                    size = (int)fileNameInfo.Length;
                }
                catch (IOException)
                {
                }
            }
        }

        // Calls when Search Button CLicked
        private void searchBtn_Click(object sender, EventArgs e)
        {
            if(searchWordTxtBox.Text.Equals(""))
            {
                toolStripStatusLabel1.Text = "Enter search word.";
            }
            else
            {
                if(fileNameTxtBox.Text.Equals(""))
                {
                    toolStripStatusLabel1.Text = "Please browse the file.";
                }
                else
                {
                    toolStripStatusLabel1.Text = "";
                    if (searchBtn.Text.Equals(Constant.cancel))
                    {
                        if (background.IsBusy)
                        {
                            searchBtn.Text = Constant.search;
                            browseBtn.Enabled = true;
                            fileNameTxtBox.Enabled = true;
                            searchWordTxtBox.Enabled = true;
                            background.CancelAsync();
                        }
                    }
                    else if (searchBtn.Text.Equals(Constant.search))
                    {
                        searchBtn.Text = Constant.cancel;
                        browseBtn.Enabled = false;
                        fileNameTxtBox.Enabled = false;
                        searchWordTxtBox.Enabled = false;
                        progressLbl.Text = String.Empty;
                        background.RunWorkerAsync();
                        textLine.Items.Clear();
                    }
                }
            }
        }

        // Background Worker Component - DoWork
        private void background_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            searchWord(sender, e);
        }

        // Main logic for searching
        private void searchWord(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                using (StreamReader file = new StreamReader(fileName))
                {
                    int counter = 1;
                    string ln;
                    int sizeTemp = 0;
                    while ((ln = file.ReadLine()) != null)
                    {
                        Thread.Sleep(100);
                        counter++;
                        sizeTemp += System.Text.ASCIIEncoding.ASCII.GetByteCount(ln);
                        float percentTemp = (float)sizeTemp * 100 / (float)size;
                        double ceilPerTemp = Math.Ceiling(percentTemp);
                        background.ReportProgress((int)ceilPerTemp);
                        if (background.CancellationPending)
                        {
                            e.Cancel = true;
                            background.ReportProgress(0);
                            return;
                        }
                        if (ln.Contains(searchWordTxtBox.Text))
                        {
                            if (InvokeRequired)
                            {
                                this.Invoke(new MethodInvoker(delegate {
                                    ListViewItem textValue = new ListViewItem(counter.ToString());
                                    textValue.SubItems.Add(ln);
                                    textLine.Items.Add(textValue);
                                }));
                            }
                        }
                    }
                    background.ReportProgress(100);
                    file.Close();
                }
            }
            catch (IOException e1)
            {
                Console.WriteLine(e1.Message);
            }
        }

        // Background Worker Component - ProgressChanged
        public void background_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            progressLbl.Text = Constant.process + progressBar.Value.ToString() + Constant.percent;
        }

        // Background Worker Component - RunWorkerCompleted
        public void background_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                progressLbl.Text = Constant.searchCancel;
            }
            else if (e.Error != null)
            {
                progressLbl.Text = Constant.error;
            }
            else
            {
                progressLbl.Text = Constant.searchComplete;
            }

            searchBtn.Text = Constant.search;
            browseBtn.Enabled = true;
            fileNameTxtBox.Enabled = true;
            searchWordTxtBox.Enabled = true;
        }
    }
}
