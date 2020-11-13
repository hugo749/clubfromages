using System;
using System.Collections.Generic;
using System.Text;
using model.data;
using System.IO;
using CsvHelper;
using System.Globalization;
using model.business;
using System.Linq;
using DEB;
using System.Data;

namespace model.data
{
    class DAOPays
    {
        private dbal _dbal;

        public DAOPays(dbal mydbal)
        {
            this._dbal = mydbal;
        }

        public void insert(pays pays)
        {

            string query = " pays values " + "(" + pays.Id + ",'" + pays.Nom + "');";
            _dbal.Insert(query);
        }

        public void delete(pays pays)
        {
            string query = " FROM pays where id = " + pays.Id + ";";
            _dbal.Delete(query);
        }

        public void update(pays pays)
        {
            string query = " pays set nom = 'Allemagne' where id = " + pays.Id + ";";
            _dbal.Update(query);
        }


        public void InsertByFile(string Chemin)
        {
            using (var reader = new  StreamReader(Chemin))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";

                csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                var record = new pays();
                var records = csv.EnumerateRecords(record);
                foreach (pays unpays in records)
                {
                    Insert(unpays);
                }
            }
        }
        
        public void Insert (pays unpays)
        {
            string query = " pays (id, nom) VALUES (" + unpays.Id + ",'" + unpays.Nom.Replace("'", "''") + "' );";
                this._dbal.Insert(query);
        }


        public List<pays> SelectAll() 
        {
            List<pays> lespays = new List<pays>();
            foreach ( DataRow dl in _dbal.SelectAll("pays").Rows)
            {
                lespays.Add (new pays( (int)dl["id"], (string)dl["nom"]));
                Console.WriteLine(dl["id"] +" "+ dl["nom"]);
            }
            return lespays;
        }

        public pays SelectByName(string namePays)
        {
            DataRow nom = _dbal.SelectByField("pays", "nom like '" + namePays + "'").Rows[0];
            pays un = new pays((int)nom["id"], (string)nom["nom"]);
            Console.WriteLine(un.Id);
            
            return un;

        }

        public pays SelectById(int idPays)
        {
            DataRow id = _dbal.SelectById("pays", idPays);
            pays deux = new pays((int)id["id"], (string)id["nom"]);
            Console.WriteLine(deux.Nom);
            return deux;
        }

        

    }
}
