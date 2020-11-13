using System;
using System.Collections.Generic;
using System.Text;
using model.business;
using model.data;

namespace model.business
{
    class pays
    {
        private int _id;
        private string _nom;

        public pays (int id, string nom)
        {
            _id = id;
            _nom = nom;
        }
        public pays()
        {
            _id = 0;
            _nom = "";
        }

        public int Id { get => _id; set => _id = value; }
        public string Nom { get => _nom; set => _nom = value; }
    }
}