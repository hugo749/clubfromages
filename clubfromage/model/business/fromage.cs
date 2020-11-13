using System;
using System.Collections.Generic;
using System.Text;
using model.business;
using model.data;

namespace model.business
{
    class fromage
    {
        private int _id;
        private pays _paysorigineid;
        private string _nom;
        private DateTime _creation;
        private string _image;
        

        public fromage (int id=0, pays paysorigineid=null, string nom="", DateTime creation=new DateTime(), string image="" )
        {
            _id=id;
            _paysorigineid=paysorigineid;
            _nom=nom;
            _creation=creation;
            _image=image;
        }

        public int Id { get => _id; set => _id = value; }
        public string Nom { get => _nom; set => _nom = value; }
        public pays Paysorigineid { get => _paysorigineid; set => _paysorigineid = value; }
        public DateTime Creation { get => _creation; set => _creation = value; }
        public string Image { get => _image; set => _image = value; }
    }
    
}