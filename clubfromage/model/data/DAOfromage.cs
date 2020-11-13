using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using DEB;
using model.business;
using model.data;
using System.Data;
using CsvHelper;
using System.Globalization;
using Model.data;

namespace model.data
{
    class DAOFromage 
    {
        private dbal _dbal;
        private DAOPays _daopays;

        public DAOFromage(dbal mydbal, DAOPays mydaopays)
        {
            this._dbal = mydbal;
            this._daopays = mydaopays;
        }

        public void insert(fromage lefromage)
        {

            string query = " fromage values " + "(" + lefromage.Id + "," + lefromage.Paysorigineid + ","+ lefromage.Nom + ","+lefromage.Creation+","+lefromage.Image+");";
            _dbal.Insert(query);
        }

        public void delete(fromage lefromage)
        {
            string query = " FROM fromage where id = " + lefromage.Id + ";";
            _dbal.Delete(query);
        }

        public void update(fromage lefromage)
        {
            string query = " fromage set nom = 'tome' where id = " + lefromage.Id + ";";
            _dbal.Update(query);
        }




        public void Insert(fromage unfromage)
        {
            string query = " fromages (id, paysorigineid, nom, creation, image) VALUES (" + unfromage.Id + "," + unfromage.Paysorigineid.Id+"," + unfromage.Nom+","+ unfromage.Creation+",'"+unfromage.Image+  "' );";
            this._dbal.Insert(query);
        }





        public List<fromage> SelectAll()
        {
            List<fromage> lesfromages = new List<fromage>();
            foreach (DataRow dl in _dbal.SelectAll("fromage").Rows)
            {
                lesfromages.Add(new fromage((int)dl["id"], (pays)dl["paysorigineid"], (string)dl["nom"], (DateTime)dl["creation"], (string)dl["image"]));
                Console.WriteLine(dl["id"] + " " + dl["paysorigineid"] + " " + dl["nom"] +" " +dl["creation"] +" " +dl["image"]);
            }
            return lesfromages;
        }

        public fromage SelectByName(string namefromage)
        {
            DataRow nom = _dbal.SelectByField("fromage", "nom like '" + namefromage + "'").Rows[0];
            fromage unfro = new fromage((int)nom["id"], (pays)nom["paysorigineid"], (string)nom["nom"], (DateTime)nom["creation"], (string)nom["image"]);
            Console.WriteLine(unfro.Id);

            return unfro;

        }

        public fromage SelectById(int idFromage)
        {
            DataRow id = _dbal.SelectById("fromage", idFromage);
            pays myPays = this._daopays.SelectById((int)id["paysorigineid"]);
            fromage idfro = new fromage((int)id["id"], myPays, (string)id["nom"], (DateTime)id["creation"], (string)id["image"]);
            Console.WriteLine(idfro.Nom);
            return idfro;
        }


        public void InsertByFile(string Chemin)
        {
            using (var reader = new StreamReader(Chemin))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.RegisterClassMap<MapFromage>();

                csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                var record = new fromage();
                var records = csv.EnumerateRecords(record);
                foreach (fromage unfromage in records)
                {
                    insert(unfromage);
                }
            }
        }
    }
}