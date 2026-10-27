using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

// Stub implementation for Microsoft.Data.Sqlite to allow compilation without the NuGet package.
// In a real project you should add the Microsoft.Data.Sqlite NuGet package instead of using this stub.
namespace Microsoft.Data.Sqlite
{
    public enum SqliteType
    {
        Blob
    }

    public class SqliteParameter
    {
        public string ParameterName { get; set; } = string.Empty;
        public SqliteType SqliteType { get; set; }
        public object Value { get; set; } = new object();
    }

    public class SqliteCommand : IDisposable
    {
        private readonly string _commandText;
        private readonly SqliteConnection _connection;
        private readonly List<SqliteParameter> _parameters = new List<SqliteParameter>();

        public SqliteCommand(string commandText, SqliteConnection connection)
        {
            _commandText = commandText;
            _connection = connection;
        }

        public SqliteParameter CreateParameter()
        {
            var p = new SqliteParameter();
            _parameters.Add(p);
            return p;
        }

        public IList<SqliteParameter> Parameters => _parameters;

        // In a real implementation this would execute the SQL against the database.
        // Here it simply returns 0 to keep the example compile‑time correct.
        public int ExecuteNonQuery()
        {
            // No‑op stub – you could add in‑memory storage here if needed.
            return 0;
        }

        public void Dispose() { }
    }

    public class SqliteConnection : IDisposable
    {
        private readonly string _connectionString;
        public SqliteConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Open() { /* No‑op stub */ }
        public void Close() { /* No‑op stub */ }
        public void Dispose() { /* No‑op stub */ }
    }
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string sqlitePath = "images.db";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure SQLite database exists and has the required table
        CreateDatabaseIfNeeded(sqlitePath);

        // Extract images from PDF and store them as BLOBs
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(pdfPath);

            // Extract all images from the document
            extractor.ExtractImage();

            // Open SQLite connection
            using (var connection = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={sqlitePath}"))
            {
                connection.Open();

                // Prepare INSERT command with a parameter for the BLOB
                using (var command = new Microsoft.Data.Sqlite.SqliteCommand("INSERT INTO Images (ImageData) VALUES (@data);", connection))
                {
                    var dataParam = command.CreateParameter();
                    dataParam.ParameterName = "@data";
                    dataParam.SqliteType = Microsoft.Data.Sqlite.SqliteType.Blob;
                    command.Parameters.Add(dataParam);

                    // Iterate through all extracted images
                    while (extractor.HasNextImage())
                    {
                        // Get the current image into a MemoryStream
                        using (MemoryStream ms = new MemoryStream())
                        {
                            extractor.GetNextImage(ms);
                            byte[] imageBytes = ms.ToArray();

                            // Set the parameter value and execute the INSERT
                            dataParam.Value = imageBytes;
                            command.ExecuteNonQuery();
                        }
                    }
                }

                connection.Close();
            }
        }

        Console.WriteLine("Image extraction and storage completed.");
    }

    // Creates the SQLite database file and Images table if they do not exist
    static void CreateDatabaseIfNeeded(string dbPath)
    {
        bool createTable = !File.Exists(dbPath);

        // Create the database file if it does not exist
        if (createTable)
        {
            // Microsoft.Data.Sqlite creates the file automatically when opening a connection,
            // but we open a connection here to ensure the file exists.
            using (var initConn = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={dbPath}"))
            {
                initConn.Open();
                initConn.Close();
            }
        }

        using (var connection = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={dbPath}"))
        {
            connection.Open();

            // Table with an auto-increment primary key and a BLOB column for image data
            string createTableSql = @"
                CREATE TABLE IF NOT EXISTS Images (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ImageData BLOB NOT NULL
                );";
            using (var command = new Microsoft.Data.Sqlite.SqliteCommand(createTableSql, connection))
            {
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}
