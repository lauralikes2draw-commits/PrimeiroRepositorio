using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal
{
    public class Conexao
    {
        public static SqlConnection Conectar()
        {
            string conexao = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BeauteCareDB;Integrated Security=True;TrustServerCertificate=True";
            return new SqlConnection(conexao);
        }
    }
}
