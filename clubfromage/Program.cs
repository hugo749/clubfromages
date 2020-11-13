using System;
using MySql;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Collections;
using model.data;
using model.business;
using System.ComponentModel.Design;
using System.Data;
using MySqlX.XDevAPI.Relational;

namespace DEB
{
    class Program
    {


        static void Main(string[] arg)
        {
            

            dbal ledbal = new dbal();
            //FP.Execquery("INSERT INTO Pays (id, nom) values (1, 'France')");
            //pays unpays = new pays(2, "France");
            DAOPays monDAOpays = new DAOPays(ledbal);
            DAOFromage monDAOfromage = new DAOFromage(ledbal, monDAOpays);

            //pays.insert(unpays);
            //pays.delete(unpays);
            //pays.update(unpays);

            pays unpays = monDAOpays.SelectById(2);
            Console.WriteLine("pays: " + unpays.Nom);


            //fromage monfromage = new fromage(1, unpays, "Abondance", new DateTime(02/01/2001), "image");
            //monDAOfromage.Insert(monfromage);




            //monDAOfromage.SelectAll();
            //monDAOfromage.SelectByName("Abondance");
            //monDAOfromage.SelectById(12);
            //Console.WriteLine("pays: " + fromage.Nom);


            fromage unfromage = monDAOfromage.SelectById(2);
            Console.WriteLine("pays: " + unfromage.Nom);



            monDAOfromage.InsertByFile("C://Users//SIO2//source//repos//clubfromage//fromages.csv");
            //pays.InsertByFile("C://Users//SIO2//source//repos//clubfromage//pays.csv");



            DataSet lesPays = ledbal.RQuery("SELECT * FROM Pays");
            foreach (DataRow r in lesPays.Tables[0].Rows)
            {
                Console.WriteLine(r["id"]);
            }



            foreach (DataRow maligne in ledbal.SelectAll("pays").Rows)
            {
                
                    
                Console.WriteLine(maligne["nom"]);
                
            }
            
            
            /*foreach (DataRow row in ledbal.SelectByFile("Pays", "nom like 'P%'").Rows)
            {
                Console.WriteLine(row["nom"]);
            }*/

            DataRow id = ledbal.SelectById("Pays", 3);
            Console.WriteLine(id["id"]+"  "+id["nom"].ToString());

            monDAOpays.SelectAll();

            monDAOpays.SelectByName("France");

            
            

            
        }

    }
}