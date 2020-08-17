using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ListaEquipamentos;

namespace SpaceItem // espaço da classe item 
{

    interface IItem // interface item com um metodo virtual para gerar id dos itens da lista 
    {
        int GeraID();
    };
    public class RepositoryItem : IItem // classe repositorio que implementa o metodo da interface
    {
        public static int geraid; // o geraid é statico pois não deve ser criado novo, e sim deve ser unico 
        public int GeraID()
        {
            geraid++; // incrementa, isso quer dizer que nunca sera zero 
            return geraid; // retorna o valor 
        }
    };
    public class Item:RepositoryItem // a item implementa a classe repositorio, 
    {
        
        public string name, description; // um campo para o nome e outro para a descrição, além do id recebido da classe RepositoryItem
        public int id; // id do objeto que sera recebido pelo metodo GeraID 

        public Item(string name = "Empy", string description = "Description Null!") // optamos por usar argumetnos opcionais para evitar problemas, caso o usuario faça uma ação não planejada, seja ela qual for
        {
            this.id = GeraID(); // grava o id 
            this.name = name; // grava o nome 
            this.description = description; // grava a descrição
        }
        ~Item(){} // destrutor, por bom habito 

    };
    

}