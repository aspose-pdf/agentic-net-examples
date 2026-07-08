using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath   = "metadata_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Open the log file for writing
                using (StreamWriter writer = new StreamWriter(logPath, false))
                {
                    writer.WriteLine($"Metadata audit for {Path.GetFileName(inputPath)}");
                    writer.WriteLine($"Generated on {DateTime.UtcNow:u}");
                    writer.WriteLine();

                    // Iterate over all entries in DocumentInfo
                    foreach (var kvp in doc.Info)
                    {
                        // Skip predefined keys (Title, Author, etc.)
                        if (!DocumentInfo.IsPredefinedKey(kvp.Key))
                        {
                            writer.WriteLine($"{kvp.Key}: {kvp.Value}");
                        }
                    }
                }
            }

            Console.WriteLine($"Metadata written to '{logPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}