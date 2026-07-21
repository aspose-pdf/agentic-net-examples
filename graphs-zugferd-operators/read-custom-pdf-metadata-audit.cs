using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // source PDF
        const string logPath   = "metadata_audit.log"; // audit log file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // DocumentInfo holds standard and custom metadata (key/value pairs)
            DocumentInfo info = doc.Info;

            // Prepare the log file (overwrites if it exists)
            using (StreamWriter logWriter = new StreamWriter(logPath, false))
            {
                logWriter.WriteLine($"Metadata audit for '{Path.GetFileName(pdfPath)}' at {DateTime.UtcNow:u}");
                logWriter.WriteLine(new string('-', 60));

                // Iterate over all entries in the DocumentInfo dictionary
                foreach (var kvp in info)
                {
                    string key = kvp.Key;
                    string value = kvp.Value;

                    // Skip predefined keys (Title, Author, etc.) – we only want custom metadata
                    if (DocumentInfo.IsPredefinedKey(key))
                        continue;

                    // Write each custom key/value pair to the log
                    logWriter.WriteLine($"{key}: {value}");
                }

                logWriter.WriteLine(new string('-', 60));
                logWriter.WriteLine("End of audit.");
            }

            Console.WriteLine($"Custom metadata written to '{logPath}'.");
        }
    }
}