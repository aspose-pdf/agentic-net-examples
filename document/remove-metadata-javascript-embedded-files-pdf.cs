using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ----- 1. Clear standard document information (metadata) -----
            doc.Info.Title = string.Empty;
            doc.Info.Author = string.Empty;
            doc.Info.Subject = string.Empty;
            doc.Info.Keywords = string.Empty;
            doc.Info.Creator = string.Empty;
            doc.Info.Producer = string.Empty;
            // DateTime properties are non‑nullable, use MinValue to "clear"
            doc.Info.CreationDate = DateTime.MinValue;
            doc.Info.ModDate = DateTime.MinValue;

            // ----- 2. Clear XMP metadata packet if present -----
            doc.Metadata?.Clear();

            // ----- 3. Remove all JavaScript actions -----
            if (doc.JavaScript != null)
            {
                // JavaScriptCollection does not expose Count/Clear; iterate via Keys and remove each entry
                var keys = doc.JavaScript.Keys.ToList();
                foreach (var key in keys)
                {
                    doc.JavaScript.Remove(key);
                }
            }

            // ----- 4. Remove all embedded files (attachments) -----
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                // EmbeddedFileCollection is 1‑based; delete each file by name starting from the end
                for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                {
                    var fileSpec = doc.EmbeddedFiles[i];
                    if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                    {
                        doc.EmbeddedFiles.Delete(fileSpec.Name);
                    }
                }
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}
