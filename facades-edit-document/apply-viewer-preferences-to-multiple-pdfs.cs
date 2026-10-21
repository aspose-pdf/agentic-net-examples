using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        // List of PDF file paths to process
        List<string> pdfFiles = new List<string>
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        foreach (string path in pdfFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"File not found: {path}");
                continue;
            }

            try
            {
                // Open each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(path))
                {
                    // Attempt to apply viewer preferences using dynamic access.
                    // This works only with Aspose.Pdf versions that expose the ViewerPreferences property.
                    dynamic d = doc;
                    try
                    {
                        var prefs = d.ViewerPreferences;
                        prefs.HideToolbar = true;
                        prefs.HideMenubar = true;
                        prefs.CenterWindow = true;
                        prefs.DisplayDocTitle = true;
                    }
                    catch (RuntimeBinderException)
                    {
                        Console.WriteLine($"ViewerPreferences property is not supported by the current Aspose.Pdf version for file '{path}'.");
                    }

                    // Overwrite the original file with the (potentially) updated preferences
                    doc.Save(path);
                }

                Console.WriteLine($"Viewer preferences processed for: {path}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{path}': {ex.Message}");
            }
        }
    }
}
