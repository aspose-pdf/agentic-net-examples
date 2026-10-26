using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "clean_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages and clear their annotation collections
            foreach (Page page in doc.Pages)
            {
                page.Annotations.Clear();
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations removed. Clean PDF saved to '{outputPath}'.");
    }
}