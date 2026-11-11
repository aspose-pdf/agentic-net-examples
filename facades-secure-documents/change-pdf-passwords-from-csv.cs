using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace as requested

namespace PdfPasswordChanger
{
    class Program
    {
        // Represents a single CSV record
        private class CsvRecord
        {
            public string InputPath { get; set; }
            public string OutputPath { get; set; }
            public string NewUserPassword { get; set; }
            public string NewOwnerPassword { get; set; }
        }

        static void Main(string[] args)
        {
            // Expect the CSV file path as the first argument
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Usage: PdfPasswordChanger <csvFilePath>");
                return;
            }

            string csvFile = args[0];
            if (!File.Exists(csvFile))
            {
                Console.Error.WriteLine($"CSV file not found: {csvFile}");
                return;
            }

            List<CsvRecord> records = LoadCsv(csvFile);
            foreach (var rec in records)
            {
                try
                {
                    ChangePdfPasswords(rec);
                    Console.WriteLine($"Processed: {rec.InputPath} -> {rec.OutputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{rec.InputPath}': {ex.Message}");
                }
            }
        }

        // Loads CSV lines into CsvRecord objects (simple split on commas)
        private static List<CsvRecord> LoadCsv(string path)
        {
            var list = new List<CsvRecord>();
            foreach (var line in File.ReadLines(path))
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Optional: skip header line if it contains non‑file text
                if (line.StartsWith("InputPath", StringComparison.OrdinalIgnoreCase))
                    continue;

                string[] parts = line.Split(',');
                if (parts.Length < 4)
                    continue; // malformed line

                list.Add(new CsvRecord
                {
                    InputPath = parts[0].Trim(),
                    OutputPath = parts[1].Trim(),
                    NewUserPassword = parts[2].Trim(),
                    NewOwnerPassword = parts[3].Trim()
                });
            }
            return list;
        }

        // Performs the password change using Aspose.Pdf.Document
        private static void ChangePdfPasswords(CsvRecord rec)
        {
            if (!File.Exists(rec.InputPath))
                throw new FileNotFoundException("Input PDF not found.", rec.InputPath);

            // Ensure the output directory exists
            string outDir = Path.GetDirectoryName(rec.OutputPath);
            if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
                Directory.CreateDirectory(outDir);

            // Open the source PDF, apply new encryption, and save to the target path
            using (Document doc = new Document(rec.InputPath))
            {
                // Define permissions – adjust as needed
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Apply encryption with the new passwords and AES‑256 algorithm
                doc.Encrypt(
                    rec.NewUserPassword,
                    rec.NewOwnerPassword,
                    perms,
                    CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(rec.OutputPath);
            }
        }
    }
}