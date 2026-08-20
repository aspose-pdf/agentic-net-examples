using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Collect names of attachments that end with "_old.pdf"
            List<string> namesToDelete = new List<string>();
            foreach (var ef in doc.EmbeddedFiles) // use var to avoid compile‑time dependency on EmbeddedFile type
            {
                // Use reflection (or dynamic) to read the Name property safely
                var nameProp = ef?.GetType().GetProperty("Name");
                if (nameProp == null) continue;
                var name = nameProp.GetValue(ef) as string;
                if (!string.IsNullOrEmpty(name) && name.EndsWith("_old.pdf", StringComparison.OrdinalIgnoreCase))
                {
                    namesToDelete.Add(name);
                }
            }

            // Delete the matching attachments
            foreach (string name in namesToDelete)
            {
                doc.EmbeddedFiles.Delete(name);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachments ending with '_old.pdf' removed. Saved to '{outputPath}'.");
    }
}
