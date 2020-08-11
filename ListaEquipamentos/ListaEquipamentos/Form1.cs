using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpaceItem;

namespace ListaEquipamentos
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void btn_criar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(texDesc.Text) && string.IsNullOrEmpty(texName.Text)) return;
            else
            {
                int auxiliar = 0;
                foreach (ListViewItem id in Lista.Items)
                {
                    if (Convert.ToInt32(id.SubItems[0].Text) > auxiliar)
                    {
                        auxiliar = Convert.ToInt32(id.SubItems[0].Text);
                    }
                }
                RepositoryItem.geraid = auxiliar; 

                Item it = new Item(texName.Text, texDesc.Text);
                ListViewItem item = new ListViewItem(it.id.ToString());
                item.SubItems.Add(it.name);
                item.SubItems.Add(it.description);
                Lista.Items.Add(item);
                texName.Clear();
                texDesc.Clear();
                texName.Focus();
            }
            
        }

        

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Lista.Items.Count > 0)
            {
                try { Lista.Items.Remove(Lista.SelectedItems[0]); } catch { return; }
            }
                
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Item it = new Item(texName.Text, texDesc.Text);
                ListViewItem item = new ListViewItem(Lista.SelectedItems[0].Text);

                if (string.IsNullOrEmpty(texName.Text))
                {
                    item.SubItems.Add(Lista.SelectedItems[0].SubItems[1]);
                }
                else item.SubItems.Add(it.name);


                if (string.IsNullOrEmpty(texDesc.Text))
                {
                    item.SubItems.Add(Lista.SelectedItems[0].SubItems[2]);
                }
                else item.SubItems.Add(it.description);


                Lista.Items.Insert(Lista.SelectedIndices[0], item);
                Lista.Items.Remove(Lista.SelectedItems[0]);
                texName.Clear();
                texDesc.Clear();
                texName.Focus();
            }
            catch { return; }
        }

        private void Lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { TexCanto.Text = (Lista.SelectedItems[0].SubItems[2].Text); }
            catch { return; }
        }

        private async void salvarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog salva = new SaveFileDialog() { Filter = "Documentos de Texto|*.txt", ValidateNames = true})
            {
                if (salva.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter arquivo = File.CreateText(salva.FileName))
                    {
                        foreach (ListViewItem item in Lista.Items)
                        {
                            await arquivo.WriteLineAsync(item.SubItems[0].Text + "\n" + item.SubItems[1].Text + "\n" + item.SubItems[2].Text + "\n===");
                        }
                    }
                }
            }
        }

        private void carregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog abrir = new OpenFileDialog() { Filter = "Documentos de Texto|*.txt", ValidateNames = true, Multiselect = false})
            {
                if (abrir.ShowDialog() == DialogResult.OK)
                {
                    Lista.Items.Clear();
                    using (StreamReader arquivo = new StreamReader(abrir.FileName))
                    {
                        while (arquivo.Peek() >= 0)
                        {
                            
                            Item it = new Item(texName.Text, texDesc.Text);
                            ListViewItem item = new ListViewItem(arquivo.ReadLine());
                            item.SubItems.Add(arquivo.ReadLine());
                            item.SubItems.Add(arquivo.ReadLine());
                            Lista.Items.Add(item);
                            arquivo.ReadLine();
                            texName.Clear();
                            texDesc.Clear();
                            texName.Focus();

                        }
                    }
                }
            }
        }

        private void btn_pesquisa_Click(object sender, EventArgs e)
        {
            string pesquisa = texPesquisa.Text;
            System.Diagnostics.Process.Start("https://google.com/search?q=" + pesquisa);
        }
    }
}
