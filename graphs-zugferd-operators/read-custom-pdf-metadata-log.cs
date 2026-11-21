using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string logPath = "metadata_audit.log";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Open the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(pdfPath))
            {
                // Prepare the log file (overwrites if it exists).
                using (StreamWriter logWriter = new StreamWriter(logPath, false))
                {
                    logWriter.WriteLine($"Metadata audit for '{Path.GetFileName(pdfPath)}' - {DateTime.UtcNow:u}");
                    logWriter.WriteLine(new string('-', 60));

                    // Write standard document info (optional, useful for auditing).
                    logWriter.WriteLine($"Title   : {doc.Info.Title}");
                    logWriter.WriteLine($"Author  : {doc.Info.Author}");
                    logWriter.WriteLine($"Subject : {doc.Info.Subject}");
                    logWriter.WriteLine($"Keywords: {doc.Info.Keywords}");
                    logWriter.WriteLine();

                    // Iterate over custom metadata entries using the DocumentInfo indexer.
                    // Custom metadata is stored in the DocumentInfo dictionary accessible via the Keys collection.
                    ICollection<string> customKeys = doc.Info.Keys;
                    if (customKeys != null && customKeys.Count > 0)
                    {
                        logWriter.WriteLine("Custom Metadata:");
                        foreach (string key in customKeys)
                        {
                            string value = doc.Info[key];
                            logWriter.WriteLine($"- {key} = {value}");
                        }
                    }
                    else
                    {
                        logWriter.WriteLine("No custom metadata found.");
                    }

                    logWriter.WriteLine(new string('-', 60));
                }
            }

            Console.WriteLine($"Metadata audit completed. Log saved to '{logPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
