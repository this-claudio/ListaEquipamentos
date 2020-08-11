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

namespace SpaceItem
{

    interface IItem
    {
        int GeraID();
    };
    public class RepositoryItem : IItem
    {
        public static int geraid;
        public int GeraID()
        {
            geraid++;
            return geraid;
        }
    };
    public class Item:RepositoryItem
    {
        
        public string name, description; 
        public int id; 
        public Item(string name = "Empy", string description = "Description Null!")
        {
            this.id = GeraID();
            this.name = name;
            this.description = description;
        }
        ~Item(){}
        
        

    };
    

}