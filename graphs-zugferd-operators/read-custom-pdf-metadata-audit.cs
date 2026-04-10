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
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // DocumentInfo holds both predefined and custom metadata entries
            DocumentInfo info = doc.Info;

            // Open the log file for writing (creates or overwrites)
            using (StreamWriter logWriter = new StreamWriter(logPath, false))
            {
                logWriter.WriteLine($"Metadata audit for '{Path.GetFileName(pdfPath)}' at {DateTime.UtcNow:u}");
                logWriter.WriteLine(new string('-', 60));

                // Iterate over all metadata entries
                foreach (var kvp in info)
                {
                    string key   = kvp.Key;
                    string value = kvp.Value;

                    // Skip predefined keys (Title, Author, etc.) – we only want custom metadata
                    if (DocumentInfo.IsPredefinedKey(key))
                        continue;

                    logWriter.WriteLine($"{key}: {value}");
                }

                logWriter.WriteLine("End of audit.");
            }

            Console.WriteLine($"Custom metadata written to '{logPath}'.");
        }
    }
}