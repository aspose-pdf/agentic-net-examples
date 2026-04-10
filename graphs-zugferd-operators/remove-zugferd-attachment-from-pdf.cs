using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_without_zugferd.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Collect the names of all embedded files that match the ZUGFeRD pattern
            List<string> namesToDelete = new List<string>();

            // EmbeddedFiles collection uses 1‑based indexing
            for (int i = 1; i <= doc.EmbeddedFiles.Count; i++)
            {
                var fileSpec = doc.EmbeddedFiles[i];
                if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                {
                    // ZUGFeRD attachments usually contain "ZUGFeRD" in the file name
                    if (fileSpec.Name.IndexOf("ZUGFeRD", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        namesToDelete.Add(fileSpec.Name);
                    }
                }
            }

            // Delete the identified ZUGFeRD attachments by name
            foreach (string name in namesToDelete)
            {
                doc.EmbeddedFiles.Delete(name);
            }

            // Save the modified PDF, preserving all other content
            doc.Save(outputPath);
        }

        Console.WriteLine($"ZUGFeRD attachment removed. Output saved to '{outputPath}'.");
    }
}