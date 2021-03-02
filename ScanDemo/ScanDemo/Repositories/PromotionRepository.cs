using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ScanDemo.Models;
using SQLite;

namespace ScanDemo.Repositories
{
    public class PromotionRepository
    {
        private SQLiteAsyncConnection connection;

        public string StatusMessage { get; set; }
        public PromotionRepository(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<Promotion>();
        }

        public async Task AddNewPromotionAsync(string nom)
        {
            int result = 0;

            try
            {
                result = await connection.InsertAsync(new Promotion { Nom = nom });

                StatusMessage = $"{result} promotion ajouté : {nom}";
            }
            catch( Exception ex)
            {
                StatusMessage = $"Impossible d'ajouter la promotion : {nom}. \n Erreur : {ex.Message}";
            }
        }

        public async Task<List<Promotion>> GetPromotionsAsync()
        {
            try
            {
                return await connection.Table<Promotion>().ToListAsync() ;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Impossible de récupérer les promotions : {ex.Message}";
            }
            return new List<Promotion>();
        }
    }
}
