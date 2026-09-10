using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string logPath = "metadata_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Access the document's info dictionary (custom metadata stored here)
                DocumentInfo info = doc.Info;

                // Open a StreamWriter to create the audit log
                using (StreamWriter writer = new StreamWriter(logPath, false))
                {
                    writer.WriteLine($"Metadata audit for '{inputPdf}'");
                    writer.WriteLine($"Generated on {DateTime.UtcNow:u}");
                    writer.WriteLine();

                    // Iterate over all entries and write only custom (non‑predefined) keys
                    foreach (var kvp in info)
                    {
                        if (DocumentInfo.IsPredefinedKey(kvp.Key))
                            continue; // Skip standard keys like Title, Author, etc.

                        writer.WriteLine($"{kvp.Key}: {kvp.Value}");
                    }
                }

                Console.WriteLine($"Custom metadata written to '{logPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}