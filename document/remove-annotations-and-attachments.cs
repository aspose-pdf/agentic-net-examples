using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // ----- Remove all annotations (comments, markup, etc.) -----
            // Annotations collection uses 1‑based indexing.
            foreach (Page page in doc.Pages)
            {
                // Delete annotations one by one until the collection is empty
                while (page.Annotations.Count > 0)
                {
                    page.Annotations.Delete(1);
                }
            }

            // ----- Remove all embedded files (attachments) -----
            // EmbeddedFiles.Delete() removes every attached file.
            doc.EmbeddedFiles.Delete();

            // Optional: clean up unused objects and streams after removals.
            // The OptimizeResources method that accepts OptimizationOptions is not available in all versions.
            // If needed, you can call the parameter‑less overload (if present) or simply rely on the default save behavior.
            // doc.OptimizeResources(); // Uncomment if your Aspose.Pdf version provides this overload.

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}
