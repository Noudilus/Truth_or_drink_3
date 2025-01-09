using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
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
            var dbFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.db3");

            // Controleer of de map bestaat, zo niet maak deze aan
            var directory = Path.GetDirectoryName(dbFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            _database = new SQLiteAsyncConnection(dbFilePath);

            // Initialisatie asynchroon starten
            Task.Run(async () => await InitializeDatabaseAsync()).Wait();
        }

        // Initialiseer de database en maak de Player tabel als deze nog niet bestaat
        public async Task InitializeDatabaseAsync()
        {
            var exists = File.Exists(_database.DatabasePath);  // Controleer of het bestand al bestaat

            if (!exists)
            {
                await _database.CreateTableAsync<Player>();
            }

            // Forceer het aanmaken van de Player tabel als die niet bestaat
            try
            {
                await _database.CreateTableAsync<Player>();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
        }

        // Haal de lijst van spelers op, gesorteerd op ID
        public Task<List<Player>> GetPlayersAsync()
        {
            return _database.Table<Player>().OrderBy(p => p.Id).ToListAsync();
        }

        // Voeg een nieuwe speler toe aan de database
        public async Task<int> AddPlayerAsync(Player player)
        {
            try
            {
                return await _database.InsertAsync(player);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding player: {ex.Message}");
                throw;
            }
        }

        // Werk de score van een speler bij
        public async Task<int> UpdatePlayerScoreAsync(int playerId, int newScore)
        {
            try
            {
                return await _database.ExecuteAsync("UPDATE Player SET Score = ? WHERE Id = ?", newScore, playerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating player score: {ex.Message}");
                throw;
            }
        }

        // Werk de drinkteller van een speler bij
        public async Task<int> UpdatePlayerDrinkCounterAsync(int playerId, int drinkCounter)
        {
            try
            {
                return await _database.ExecuteAsync("UPDATE Player SET DrinkCounter = ? WHERE Id = ?", drinkCounter, playerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating player drink counter: {ex.Message}");
                throw;
            }
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

        // Verwijder alle spelers uit de database
        public Task<int> DeleteAllPlayersAsync()
        {
            return _database.DeleteAllAsync<Player>();
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

        public int DrinkCounter { get; set; } // Voeg een veld toe voor de drinkteller

        public string DateAdded { get; set; }

        // Constructor met standaard waarden
        public Player()
        {
            Score = 0; // Standaard score
            DrinkCounter = 0; // Standaard drinkteller
            DateAdded = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
