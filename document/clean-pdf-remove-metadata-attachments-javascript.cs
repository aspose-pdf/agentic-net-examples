using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "cleaned.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdf))
            {
                // 1. Remove all document metadata
                doc.RemoveMetadata();

                // 2. Remove all embedded files (attachments)
                if (doc.EmbeddedFiles != null)
                {
                    doc.EmbeddedFiles.Delete(); // deletes all embedded files
                }

                // 3. Remove all JavaScript actions
                if (doc.JavaScript != null)
                {
                    // Copy keys to a list to avoid modifying the collection while iterating
                    var scriptKeys = doc.JavaScript.Keys.Cast<string>().ToList();
                    foreach (string key in scriptKeys)
                    {
                        doc.JavaScript.Remove(key);
                    }
                }

                // Optional: clean up unused resources and linearize the file
                doc.OptimizeResources();
                doc.Optimize();

                // Save the cleaned PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Cleaned PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}