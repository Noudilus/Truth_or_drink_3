using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Truth_or_drink_3
{
    // De DatabaseService klasse voor database operaties
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        // Constructor die de database verbindt en de Player tabel aanmaakt
        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Player>().Wait(); // Zorgt ervoor dat de Player tabel wordt aangemaakt
        }

        // Haal de lijst van spelers op, gesorteerd op ID of naam
        public Task<List<Player>> GetPlayersAsync()
        {
            return _database.Table<Player>().OrderBy(p => p.Id).ToListAsync(); // Altijd gesorteerd op ID
        }

        // Voeg een nieuwe speler toe aan de database
        public Task<int> AddPlayerAsync(Player player)
        {
            return _database.InsertAsync(player);
        }

        // Werk de score van een speler bij
        public Task<int> UpdatePlayerScoreAsync(int playerId, int newScore)
        {
            return _database.ExecuteAsync("UPDATE Player SET Score = ? WHERE Id = ?", newScore, playerId);
        }

        // Haal een specifieke speler op
        public Task<Player> GetPlayerByIdAsync(int playerId)
        {
            return _database.Table<Player>().Where(p => p.Id == playerId).FirstOrDefaultAsync();
        }

        // Verwijder een speler uit de database
        public Task<int> DeletePlayerAsync(int playerId)
        {
            return _database.Table<Player>().DeleteAsync(p => p.Id == playerId);
        }
    }

    // De Player klasse voor het opslaan van speler gegevens
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        public int Score { get; set; }

        public string DateAdded { get; set; }

        public Player()
        {
            Score = 0; // Standaard score
            DateAdded = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
