using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

namespace PdfPasswordBatchChanger
{
    class Program
    {
        // CSV format: InputPdfPath,OutputPdfPath,OldOwnerPassword,NewUserPassword,NewOwnerPassword
        static void Main()
        {
            const string csvPath = "passwords.csv";

            if (!File.Exists(csvPath))
            {
                Console.Error.WriteLine($"CSV file not found: {csvPath}");
                return;
            }

            try
            {
                foreach (var record in ReadCsv(csvPath))
                {
                    string inputPath = record[0];
                    string outputPath = record[1];
                    string oldOwnerPassword = record[2];
                    string newUserPassword = record[3];
                    string newOwnerPassword = record[4];

                    if (!File.Exists(inputPath))
                    {
                        Console.Error.WriteLine($"Input PDF not found: {inputPath}");
                        continue;
                    }

                    try
                    {
                        // Initialize the facade and bind the source PDF
                        using (PdfFileSecurity security = new PdfFileSecurity())
                        {
                            security.BindPdf(inputPath);

                            // Change passwords while preserving existing security settings
                            bool changed = security.ChangePassword(oldOwnerPassword, newUserPassword, newOwnerPassword);
                            if (!changed)
                            {
                                Console.Error.WriteLine($"Failed to change password for: {inputPath}");
                                continue;
                            }

                            // Save the result to the specified output file
                            security.Save(outputPath);
                        }

                        Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read CSV: {ex.Message}");
            }
        }

        // Simple CSV parser (comma‑separated, no quoted fields handling)
        static IEnumerable<string[]> ReadCsv(string path)
        {
            foreach (var line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Split on commas
                var parts = line.Split(',');

                // Expect exactly 5 columns; skip malformed lines
                if (parts.Length != 5)
                {
                    Console.Error.WriteLine($"Skipping malformed CSV line: {line}");
                    continue;
                }

                // Trim whitespace from each field
                for (int i = 0; i < parts.Length; i++)
                    parts[i] = parts[i].Trim();

                yield return parts;
            }
        }
    }
}