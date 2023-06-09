﻿using Microsoft.EntityFrameworkCore;
using UbertweakNfcReaderWeb.Models;

namespace UbertweakNfcReaderWeb
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Card> Cards => Set<Card>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Scan> Scans => Set<Scan>();
        public DbSet<Purchase> Purchases => Set<Purchase>();
        public DbSet<ShopItem> ShopItems => Set<ShopItem>();

        public readonly string DbPath;

        public DatabaseContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "ubertweak-nfc-reader.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        // protected override void OnConfiguring(DbContextOptionsBuilder options)
            // => options.UseSqlite($"Data Source={DbPath}");


            protected override void OnConfiguring(DbContextOptionsBuilder options)
            {
                var connectionString = "server=localhost;user=ubertweak;password=ubertweak;database=ubertweak";
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));
                options.UseMySql(connectionString, serverVersion);
            }
    }
}
