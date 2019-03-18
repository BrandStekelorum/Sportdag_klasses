using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Sportdag_klasses
{
    public class MijnDB
    {
        //velden
        static string _connectiePad = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Z:\6IB\Software\databanken\Acces Databanken\normalistatie Sportdag.accdb";
        OleDbConnection _conn = new OleDbConnection(_connectiePad);

        //properties


        //functies en methoden
        List<Leerling> VraagLlnOp()
        {
            List<Leerling> antwoord = new List<Leerling>();

            string sql = "SELECT * FROM Leerling";
            OleDbCommand mijnCommando = new OleDbCommand(sql, _conn);
            OleDbDataReader mijnReader = mijnCommando.ExecuteReader();

            int id = 0, idPakket = 0;
            string naam = "";

            _conn.Open();

            while (mijnReader.Read())
            {
                id = Convert.ToInt32(mijnReader["Id"]);
                naam = Convert.ToString(mijnReader["Naam"]);
                idPakket = Convert.ToInt32(mijnReader["IdPakket"]);

                Leerling nieuweLeerling = new Leerling(id, naam, idPakket);
                antwoord.Add(nieuweLeerling);
            }

            _conn.Close();

            return antwoord;
        }

        List<Pakket> VraagPakketOp()
        {
            List<Pakket> antwoord = new List<Pakket>();
            string sql = "SELECT * FROM Pakket";
            OleDbCommand mijnCommando = new OleDbCommand(sql, _conn);
            OleDbDataReader mijnReader = mijnCommando.ExecuteReader();

            _conn.Open();

            int id = 0;
            string naam = "";
            List<Leerling> leerlingen = new List<Leerling>();

            while (mijnReader.Read())
            {
                id = Convert.ToInt32(mijnReader["Id"]);
                naam = Convert.ToString(mijnReader["PakketNaam"]);
                leerlingen = ZoekLlnVanPakket(id);

                Pakket nieuwPakket = new Pakket(id, naam, leerlingen);

                antwoord.Add(nieuwPakket);
            }

            _conn.Close();

            return antwoord;
        }

        List<Leerling> ZoekLlnVanPakket(int ontvId)
        {
            List<Leerling> antwoord = new List<Leerling>();

            string sql = "SELECT * FROM Leerling WHERE IdPakket = " + Convert.ToString(ontvId);

            OleDbCommand mijnCommando = new OleDbCommand(sql, _conn);
            OleDbDataReader mijnReader = mijnCommando.ExecuteReader();

            int id = 0, idPakket = 0;
            string naam = "";

            _conn.Open();

            while (mijnReader.Read())
            {
                id = Convert.ToInt32(mijnReader["Id"]);
                naam = Convert.ToString(mijnReader["Naam"]);
                idPakket = Convert.ToInt32(mijnReader["IdPakket"]);

                Leerling nieuweLeerling = new Leerling(id, naam, idPakket);
                antwoord.Add(nieuweLeerling);
            }

            _conn.Close();


            return antwoord;
        }

        public void PakketToevoegen(string ontvNaam)
        {
            string sql = "INSERT INTO Pakket (PakketNaam) VALUES('"+ontvNaam+"')";
            OleDbCommand mijnCommando = new OleDbCommand(sql, _conn);

            _conn.Open();

            mijnCommando.ExecuteNonQuery();

            _conn.Close();

        }

        public void LlnToevoegen(string ontvNaam)
        {
            string sql = "INSERT INTO Leerling(Naam) VALUES('"+ontvNaam+"')";
            OleDbCommand mijnCommando = new OleDbCommand(sql, _conn);

            _conn.Open();

            mijnCommando.ExecuteNonQuery();

            _conn.Close();
        }

        public void PakketVeranderen(int ontvId, string ontvNaam)
        {
            string sql = "UPDATE Pakket SET PakketNaam = '" +ontvNaam+"' WHERE Id = "+ Convert.ToString(ontvId)+"";
            OleDbCommand mijnCommando = new OleDbCommand(sql, _conn);

            _conn.Open();

            mijnCommando.ExecuteNonQuery();

            _conn.Close();
        }

        public void LlnVeranderen(int ontvId, string ontvNaam)
        {
            string sql = "UPDATE Leerling SET Naam = '" + ontvNaam + "' WHERE Id = " + Convert.ToString(ontvId) + "";
            OleDbCommand mijnCommando = new OleDbCommand(sql, _conn);

            _conn.Open();

            mijnCommando.ExecuteNonQuery();

            _conn.Close();
        }

        public void PakketVerwijderen(int ontvId)
        {
            string sql = "DELETE FROM Pakket WHERE Id = "+Convert.ToString(ontvId);
            OleDbCommand mijnCommando = new OleDbCommand(sql, _conn);

            _conn.Open();

            mijnCommando.ExecuteNonQuery();

            _conn.Close();
        }

        public void LlnVerwijderen(int ontvId)
        {
            string sql = "";
            OleDbCommand mijnCommando = new OleDbCommand(sql, _conn);

            _conn.Open();

            mijnCommando.ExecuteNonQuery();

            _conn.Close();
        }

        public void KeuzeToevoegen(int ontvIdLln, int ontvIdPakket)
        {
            string sql = "INSERT INTO Leerling (IdPakket) VALUES(" +Convert.ToString(ontvIdPakket) + ") WHERE Id = " + Convert.ToString(ontvIdLln) + "";
            OleDbCommand mijnCommando = new OleDbCommand(sql, _conn);

            _conn.Open();

            mijnCommando.ExecuteNonQuery();

            _conn.Close();
        }


        public void KeuzeVeranderen(int ontvIdLln, int ontvIdPakket)
        {
            string sql = "UPDATE Leerling SET IdPakket = " + Convert.ToString(ontvIdPakket) + " WHERE Id = " + Convert.ToString(ontvIdLln) + "";
            OleDbCommand mijnCommando = new OleDbCommand(sql, _conn);

            _conn.Open();

            mijnCommando.ExecuteNonQuery();

            _conn.Close();
        }



        //constructors
        public MijnDB()
        {

        }
    }
}
