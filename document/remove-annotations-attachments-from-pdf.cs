using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF, process, and save inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Remove all annotations (comments, markup, etc.)
            // ------------------------------------------------------------
            // Aspose.Pdf does not expose a FlattenOptions class in older versions.
            // The simplest way to strip annotations is to delete them from each page.
            foreach (Page page in doc.Pages)
            {
                // Delete all annotations on the current page.
                page.Annotations.Delete();
            }

            // ------------------------------------------------------------
            // 2. Remove all embedded files (attachments)
            // ------------------------------------------------------------
            // The EmbeddedFiles collection represents the PDF's file attachment
            // annotations. Calling Delete() without parameters removes every
            // attached file.
            doc.EmbeddedFiles.Delete();

            // ------------------------------------------------------------
            // 3. (Optional) Clean up unused resources to reduce file size
            // ------------------------------------------------------------
            // In newer Aspose.Pdf versions OptimizeResources() can be called
            // without arguments. If the overload is unavailable the call will
            // be ignored via the catch block.
            try
            {
                doc.OptimizeResources();
            }
            catch (MissingMethodException)
            {
                // OptimizeResources overload not available – ignore.
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF cleaned and saved to '{outputPath}'.");
    }
}
