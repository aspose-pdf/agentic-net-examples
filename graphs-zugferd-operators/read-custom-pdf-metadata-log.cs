using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string logPath  = "metadata_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document doc = new Document(inputPdf))
            {
                // Access the built‑in DocumentInfo dictionary
                DocumentInfo info = doc.Info;

                // Open a log file for writing
                using (StreamWriter writer = new StreamWriter(logPath, false))
                {
                    writer.WriteLine($"Metadata audit for '{Path.GetFileName(inputPdf)}' at {DateTime.UtcNow:u}");
                    writer.WriteLine("Custom metadata entries:");

                    bool anyCustom = false;
                    // Iterate over all entries and output only custom (non‑predefined) keys
                    foreach (var kvp in info)
                    {
                        if (!DocumentInfo.IsPredefinedKey(kvp.Key))
                        {
                            writer.WriteLine($"{kvp.Key}: {kvp.Value}");
                            anyCustom = true;
                        }
                    }

                    if (!anyCustom)
                    {
                        writer.WriteLine("(none)");
                    }
                }

                Console.WriteLine($"Metadata written to '{logPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}