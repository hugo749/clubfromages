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
using CsvHelper.TypeConversion;
using CsvHelper.Configuration;

namespace model.data
{
    class paysconverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            dbal thedbal = new dbal();
            pays convertedPays = new pays();
            DAOPays myDAOPays = new DAOPays(thedbal);
            convertedPays = myDAOPays.SelectByName(text);
            return convertedPays;
        }
     
    }
}
