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



namespace SimpleTextEditor
{

    public partial class TextEditor : Form
    {
        public TextEditor()
        {
            InitializeComponent();
            this.FormClosing += TextEditor_FormClosing;

        }

        bool textChanged = false;
        string currentFile = "";



        //function HandleChanges() handle changes int text

        private void HandleChanges()
        {
            string sokvag = this.Text = Path.GetFileName(currentFile);
            if (sokvag.Length > 0)
            {
                if (textChanged == true)
                {
                    this.Text = "*" + sokvag + "-TextEditor";
                }
                else
                {
                    this.Text = sokvag + "-TextEditor";
                }
            }
            else
            {

                if (textChanged == true)
                {
                    this.Text = "* dok1.txt";
                }
                else
                {
                    this.Text =  "TextEditor";
                }
            }


            string userInput = textBox.Text;

            //Count letters (space included)

            int count = 0;
            foreach (char letter in userInput)
            {
                if (char.IsLetter(letter))
                {
                    if (!char.IsWhiteSpace(letter))
                    {
                        count++;
                    }
                }
            }
            labelTecken.Text = "Antal bokstäver med mellanslag: " + count.ToString();


            //Count letters (space excluded)
            int countchar = 0;
            foreach (char tecken in userInput)
            {
                if (char.IsLetter(tecken))
                { countchar++; }


                if (char.IsWhiteSpace(tecken))
                { countchar++; }


            }
            labelTeckenUtanM.Text = "Antal bokstäver utan mellanslag: " + countchar.ToString();

            //Count words

            char[] mellanslag = { ' ' };
            int wordsCount = userInput.Split(mellanslag, StringSplitOptions.RemoveEmptyEntries).Length;
            ordAntalCount.Text = "Antal ord: " + wordsCount.ToString();

            //Count lines
            int linescount = textBox.Lines.Length;
            labelLinesCount.Text = "Antal rader: " + linescount.ToString();

        }


        private void newMenuItem_Click(object sender, EventArgs e)
        {

            if (textChanged == true)
            {
                switch (MessageBox.Show(
                  "Vill du spara ändringar?",
                  "Spara ändringar",
                  MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        if (currentFile.Length > 0)
                        {
                            File.WriteAllText(currentFile, textBox.Text);
                            textChanged = false;

                        }

                        break;
                    case DialogResult.No:
                        textBox.Text = "";
                        break;
                    case DialogResult.Cancel:

                        break;
                }
            }


            currentFile = "";
            textChanged = false;
            HandleChanges();

        }


        //Open file dialog (Open-file item)

        private void openFileMenuItem_Click(object sender, EventArgs e)
        {

            if (textChanged == true)
            {
                switch (MessageBox.Show(
                  "Vill du spara ändringar?",
                  "Spara ändringar",
                  MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        if (currentFile.Length > 0)
                        {
                            File.WriteAllText(currentFile, textBox.Text);
                            textChanged = false;

                        }

                        break;
                    case DialogResult.No:
                        textBox.Text = "";
                        break;
                    case DialogResult.Cancel:

                        break;
                }
            }


            OpenFileDialog dialogOpen = new OpenFileDialog();

            dialogOpen.Filter = "Text files|*.txt";
            dialogOpen.Title = "Öppna fil";

            if (dialogOpen.ShowDialog(this) == DialogResult.OK)
            {
                string filInnehall = File.ReadAllText(dialogOpen.FileName);

                currentFile = dialogOpen.FileName;
                if (filInnehall.Length > 0)
                {
                    textBox.Text = filInnehall;
                    textChanged = false;
                }
            }
            dialogOpen.Dispose();
            HandleChanges();


        }


        //Function saves text if not saved earlier (Save as- menu item)
        
        private void saveAsFileMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog dialogSave = new SaveFileDialog();
            dialogSave.Filter = "Text files|*.txt";
            dialogSave.Title = "Save as";


            if (dialogSave.ShowDialog(this) == DialogResult.OK)
            {
                currentFile = dialogSave.FileName;

                File.WriteAllText(currentFile, textBox.Text);
                textChanged = false;

            }

            HandleChanges();

        }



       //Saves file that has been saved earlier (Save-menu item)

        private void saveFileMenuItem_Click(object sender, EventArgs e)
        {
            if (textChanged==true)
            {
                if (currentFile.Length > 0)
                {
                    File.WriteAllText(currentFile, textBox.Text);
                    textChanged = false;

                }
                else
                {

                    SaveFileDialog dialogSave = new SaveFileDialog();
                    dialogSave.Filter = "Text files|*.txt";
                    dialogSave.Title = "Save as";


                    if (dialogSave.ShowDialog(this) == DialogResult.OK)
                    {
                        currentFile = dialogSave.FileName;

                        File.WriteAllText(currentFile, textBox.Text);
                        textChanged = false;

                    }

                    HandleChanges();

                }
            }

            HandleChanges();
        }


        private void textBox_TextChanged(object sender, EventArgs e)
        {

            textChanged = true;
            HandleChanges();


        }


        //Close form, user will be asked to save the file if not saved and changes have been made
        
        private void TextEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (textChanged == true)
            {

                if (textChanged == true)
                {
                    switch (MessageBox.Show(
                      "Vill du spara ändringar?",
                      "Spara ändringar",
                      MessageBoxButtons.YesNoCancel))
                    {
                        case DialogResult.Yes:
                            if (currentFile.Length > 0)
                            {
                                File.WriteAllText(currentFile, textBox.Text);
                                textChanged = false;

                            }

                            break;
                        case DialogResult.No:
                            textBox.Text = "";
                            break;
                        case DialogResult.Cancel:

                            break;
                    }
                }

            }


        }

        //Close- menu item, works same as above
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textChanged == true)
            {

                if (textChanged == true)
                {
                    switch (MessageBox.Show(
                      "Vill du spara ändringar?",
                      "Spara ändringar",
                      MessageBoxButtons.YesNoCancel))
                    {
                        case DialogResult.Yes:
                            if (currentFile.Length > 0)
                            {
                                File.WriteAllText(currentFile, textBox.Text);
                                textChanged = false;
                            }

                            break;
                        case DialogResult.No:
                            this.Close();
                            break;
                        case DialogResult.Cancel:

                            break;
                    }
                }

            }

            this.Close();

        }


    }
}

