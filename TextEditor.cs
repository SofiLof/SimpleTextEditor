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



namespace WindowsFormsApp4
{
  
    public partial class TextEditor : Form
    {
        public TextEditor()
        {
            InitializeComponent();
            this.FormClosing += TextEditor_FormClosing;
          
        }

        //behöver boolean för att kolla om textet ät ändrat eller inte
        //currentFile är vår sökväg

        bool textChanged = false;
        string currentFile ="";



        //funktion HandleChanges() hanterar ändringar i texten.
        //T.ex. när användaren skriver nånting i osparad fil då visas "*" och namn dok1.txt
        //HandleChanges() också kollar hur många ord, bokstäver och rader finns i texten
        //När det är sparat fil då visas "*" tillsammans med namn på filen om användare ändrade nåt där
        //Om inget är ändrat i sparat fil då visas bara filnamn,när man ändrar nåt då visas filnamn med "*"
        //Path.GetFileName tar sista delen av sökvägen till fil som är ett filnamn
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

            //Räknar bokstäver med mellanslag

            int count = 0;
           foreach(char letter in userInput) {
                if (char.IsLetter(letter))
                {
                    if (!char.IsWhiteSpace(letter))
                    {
                        count++;
                    }
                }
            }
            labelTecken.Text = "Antal bokstäver med mellanslag: " + count.ToString();


              //Ränkar bokstäver utan mellanslag
            int countchar = 0;
            foreach (char tecken in userInput)
            {
               if(char.IsLetter(tecken))
                { countchar++; }


                if (char.IsWhiteSpace(tecken))
                { countchar++; }


            }
            labelTeckenUtanM.Text = "Antal bokstäver utan mellanslag: " + countchar.ToString();

            //Räknar ord
          
            char[] mellanslag = { ' ' };
            int wordsCount = userInput.Split(mellanslag, StringSplitOptions.RemoveEmptyEntries).Length;
            ordAntalCount.Text = "Antal ord: " + wordsCount.ToString();

            //Räknar rader
            int linescount = textBox.Lines.Length;
            labelLinesCount.Text = "Antal rader: " + linescount.ToString();
            
        }




        //När man trycker på "New" och filen är inte sparat då får man en MessageBox
        //där program frågar om du fill spara ändringar i filen och man har tre valmöjlighet
        //Yes- då sparas filen, No -då textBox sättas till tomt, och Cancel-där ingeting händer,alltså textBox är oförändrat så man kan förtsätta att skriva
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
           
            //Aktuellt sökväg ställs till tomt
            //Ropar på metoden HandleChanges() för att kolla om ändringar skedde
            currentFile = "";
            textChanged = false;
            HandleChanges();

        }




        //Funktion för att öppna fil
        //Om texten ändras i Öpnnad fil och är inte sparade då får man Meddelande (MessageBox)
        //där man frågas om man vill spara aktuella ändringar eller inte
        //Vi skapar ett nytt objekt dialogOpen, för att få dialog fönster "öppna fil"
        //Sätter Filter på filer, vi ska bara ha .txt files
        //Om DialogResultat är OK då 
        //med hjälp av ReadAllText får vi innehåll i våt öppet fil och den visas i tetxBoxen
        //Roppar på funktion HandleChanges() för att se aktuella ändringar i filen
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


        //Funktion som sparar tidigare osparat fil när man trycker på Save as
        //Skapar ett nytt objekt dialogSave för att få dialog fönster för att spara fil
        //Sätter filter för att kunna spara i .txt format 
        //Om DialogResult är OK då
        //då med hjälp WriteAllText sparar vi inehhåll som finns i textbox 
        //inehhåll sparas till currentFile som är sökväg (path)
        //Roppar på funktion HandleChanges() för att se aktuella ändringar i filen
   

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


        //När man trycker på Save och man beffinner sig i tidigare sparat fil och man gjorde ändringar i filen
        //då sparas filen under aktuell filnamn 
        //Om detta är nytt fil och man trycker på Save då filen sparars istället så som i Save as ( saveAsFileMenuItem_Click(object sender, EventArgs e);) , som ett nytt fil
       
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


        //textBox_TextChanged är händelse som kollar om textBoxen ändras
        //Härr roppar vi på HandleChanges() och sätter vår variabel tetxChanged till true, 
        //så kontrollerar man om ändringarna sker
        private void textBox_TextChanged(object sender, EventArgs e)
        {
           
            textChanged = true;
            HandleChanges();

          
        }


        //FormClosing för att stänga aktuellt vy av program, 
        //om man trycker på kryss och filen är osparat då får man ett MessageBox där får man välja vad vill man göra med filen
        //om filen är tomt då frågas ingeting
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

        //När man trycker på Close då får man ett MessageBox där frågas man vad vill man göra med fil
        //om filen är tomt då frågas ingeting
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

