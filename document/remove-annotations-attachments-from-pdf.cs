using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF, process, and save within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Remove all annotations (comments, markup, etc.)
            // The simple overload flattens and discards annotations.
            doc.Flatten();

            // Remove all embedded files (attachments)
            doc.EmbeddedFiles.Delete();

            // Optional: clean up unused resources after removals
            doc.OptimizeResources();

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF cleaned and saved to '{outputPath}'.");
    }
}
