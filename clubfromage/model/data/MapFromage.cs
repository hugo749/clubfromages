using CsvHelper.Configuration;
using model.business;
using System;
using System.Collections.Generic;
using System.Text;
using model.data;

namespace Model.data
{
    class MapFromage : ClassMap<fromage>
    {
        public MapFromage()
        {

            Map(m => m.Id);
            Map(m => m.Nom);
            Map(m => m.Paysorigineid).TypeConverter<paysconverter>();
            Map(m=>m.Image);
            Map(m=>m.Creation);
        }
    }
}
