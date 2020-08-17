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
                int auxiliar = 0; // cria variavel auxiliar igual a zero
                foreach (ListViewItem id in Lista.Items) // faz um foreach para percorrer os id de todos elementos da lista
                {
                    if (Convert.ToInt32(id.SubItems[0].Text) > auxiliar)
                    {
                        auxiliar = Convert.ToInt32(id.SubItems[0].Text); // se o elemento for maior que o ultimo ele copia para dentro do auxiliar
                    }
                }
                RepositoryItem.geraid = auxiliar; // fazendo assim ele encontra o ultimo elemento da lista

                Item it = new Item(texName.Text, texDesc.Text); // cria um obejto item com o nome e a descrição das caixas de texto
                ListViewItem item = new ListViewItem(it.id.ToString()); //cria um novo objeto item da lista
                item.SubItems.Add(it.name); // copia o nome do objeto item para o objeto lista
                item.SubItems.Add(it.description); // copia a descrição do objeto item para o objeto lista
                Lista.Items.Add(item); // adiciona o item a lista existente
                texName.Clear(); // limpa os campos 
                texDesc.Clear(); // limpa os campos 
                texName.Focus(); // seleciona a caixa de texto do nome 
            }
            
        }

        

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); // fecha o aplicativo 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Lista.Items.Count > 0) // Se a lista tiver algum objeto dentro
            {
                try { Lista.Items.Remove(Lista.SelectedItems[0]); } catch { return; } // tenta excluir o objeto selecionado 
            }
                
        }

        private void btnUpdate_Click(object sender, EventArgs e) 
        { // a logica do update na verdade é criar um novo item para a lista, mantendo ou não as caracteristicas do antigo, inseri-lo no mesmo lugar e apagar o antigo
            try 
            {
                Item it = new Item(texName.Text, texDesc.Text); // cria um novo objeto da classe item
                ListViewItem item = new ListViewItem(Lista.SelectedItems[0].Text); // cria um novo objeto da classe lista, um item

                if (string.IsNullOrEmpty(texName.Text)) // se não tiver nada escrito na caixa de texto usa o mesmo texto para o nome
                {
                    item.SubItems.Add(Lista.SelectedItems[0].SubItems[1]);
                }
                else item.SubItems.Add(it.name);


                if (string.IsNullOrEmpty(texDesc.Text))// se não tiver nada escrito na caixa de texto usa o mesmo texto para a descrição 
                {
                    item.SubItems.Add(Lista.SelectedItems[0].SubItems[2]);
                }
                else item.SubItems.Add(it.description);


                Lista.Items.Insert(Lista.SelectedIndices[0], item); // cria um copia do item e insere no lugar do item a ser modificado
                Lista.Items.Remove(Lista.SelectedItems[0]); // exclui o item antigo
                texName.Clear(); // limpa o campo
                texDesc.Clear(); // limpa o campo 
                texName.Focus(); // seleciona a caixa do nome 
            }
            catch { return; }
        }

        private void Lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { TexCanto.Text = (Lista.SelectedItems[0].SubItems[2].Text); } // toda vez que o item selecionado muda ele atualiza a caixa de descriçao no painel lateral 
            catch { return; }
        }

        private async void salvarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog salva = new SaveFileDialog() { Filter = "Documentos de Texto|*.txt", ValidateNames = true}) // abre a janela que seleciona o local do arquivo
            {
                if (salva.ShowDialog() == DialogResult.OK) // se estiver tudo ok prossegue 
                {
                    using (StreamWriter arquivo = File.CreateText(salva.FileName)) // instancia o objeto para escrever no arquivo
                    {
                        foreach (ListViewItem item in Lista.Items) // percorre os itens da lista escrevendo no arquivo 
                        {
                            await arquivo.WriteLineAsync(item.SubItems[0].Text + "\n" + item.SubItems[1].Text + "\n" + item.SubItems[2].Text + "\n=========="); //separa as colunas por quebra de linha e separa as linhas com "========="
                        }
                    }
                }
            }
        }

        private void carregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog abrir = new OpenFileDialog() { Filter = "Documentos de Texto|*.txt", ValidateNames = true, Multiselect = false}) // abre a janela pra escolher o local do arquivo
            {
                if (abrir.ShowDialog() == DialogResult.OK) // se estiver tudo ok prossegue
                {
                    Lista.Items.Clear(); // limpa a lista atual
                    using (StreamReader arquivo = new StreamReader(abrir.FileName)) // abre o arquivo escolhido 
                    {
                        while (arquivo.Peek() >= 0) // percorre todas as linhas do arquivo 
                        {
                            
                            Item it = new Item(texName.Text, texDesc.Text); // cria um objeto da classe item
                            ListViewItem item = new ListViewItem(arquivo.ReadLine()); // cria um objeto da classe lista, podemos perceber que escreveremos um lista totalmente nova, ja utilizando a primeira linha do documento para o id do item
                            item.SubItems.Add(arquivo.ReadLine()); // em seguida passamos a proxima linha como o nome 
                            item.SubItems.Add(arquivo.ReadLine()); // em seguida passamos a proxima linha como a descrição 
                            Lista.Items.Add(item); // adiciona os item criado a lista 
                            arquivo.ReadLine(); // joga fora a linha com "========"
                            texName.Clear(); // limpa os campos 
                            texDesc.Clear(); // limpa os campos 
                            texName.Focus(); // seleciona a caixa de texto do nome 

                        }
                    }
                }
            }
        }

        private void btn_pesquisa_Click(object sender, EventArgs e)
        {
            string pesquisa = texPesquisa.Text; // coleta o texto digitado na caixa de pesquisa 
            System.Diagnostics.Process.Start("https://google.com/search?q=" + pesquisa); // cria e redireciona o link para um navegador 
        }
    }
}
