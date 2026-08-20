using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Flatten the document to remove annotations and form fields
            doc.Flatten();

            // Remove any embedded file attachments
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                // Delete items from the collection in reverse order using the file name (string)
                for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                {
                    string name = doc.EmbeddedFiles[i].Name;
                    doc.EmbeddedFiles.Delete(name);
                }
            }

            // Optimize resources to discard any now‑unused objects/streams
            OptimizationOptions optOptions = new OptimizationOptions
            {
                RemoveUnusedObjects = true,
                RemoveUnusedStreams = true
            };
            doc.OptimizeResources(optOptions);

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}
