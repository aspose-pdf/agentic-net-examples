using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    // Expected CSV columns: InputPath,OutputPath,OldOwnerPassword,NewUserPassword,NewOwnerPassword
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <csvFilePath>");
            return;
        }

        string csvPath = args[0];
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        var entries = new List<CsvEntry>();
        try
        {
            using (StreamReader reader = new StreamReader(csvPath))
            {
                bool firstLine = true;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Skip header
                    if (firstLine)
                    {
                        firstLine = false;
                        if (line.StartsWith("InputPath", StringComparison.OrdinalIgnoreCase))
                            continue;
                    }

                    var parts = line.Split(',');
                    if (parts.Length < 5)
                    {
                        Console.Error.WriteLine($"Invalid line (expected 5 columns): {line}");
                        continue;
                    }

                    entries.Add(new CsvEntry
                    {
                        InputPath = parts[0].Trim(),
                        OutputPath = parts[1].Trim(),
                        OldOwnerPassword = parts[2].Trim(),
                        NewUserPassword = parts[3].Trim(),
                        NewOwnerPassword = parts[4].Trim()
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading CSV: {ex.Message}");
            return;
        }

        foreach (var entry in entries)
        {
            if (!File.Exists(entry.InputPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {entry.InputPath}");
                continue;
            }

            try
            {
                // Ensure output directory exists
                string outDir = Path.GetDirectoryName(entry.OutputPath);
                if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
                    Directory.CreateDirectory(outDir);

                using (PdfFileSecurity security = new PdfFileSecurity(entry.InputPath, entry.OutputPath))
                {
                    bool success = security.ChangePassword(
                        entry.OldOwnerPassword,
                        entry.NewUserPassword,
                        entry.NewOwnerPassword);

                    Console.WriteLine(success
                        ? $"Password changed: {entry.InputPath} -> {entry.OutputPath}"
                        : $"Failed to change password for: {entry.InputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{entry.InputPath}': {ex.Message}");
            }
        }
    }

    private class CsvEntry
    {
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public string OldOwnerPassword { get; set; }
        public string NewUserPassword { get; set; }
        public string NewOwnerPassword { get; set; }
    }
}