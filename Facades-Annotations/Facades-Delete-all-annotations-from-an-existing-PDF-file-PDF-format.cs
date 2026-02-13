using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDoc = new Document(inputPath);

        // Iterate over each page and delete all annotations
        foreach (Page page in pdfDoc.Pages)
        {
            // Delete annotations using a backward loop (1‑based indexing)
            for (int i = page.Annotations.Count; i >= 1; i--)
            {
                page.Annotations.Delete(i);
            }
        }

        // Save the cleaned PDF using the prescribed save rule
        pdfDoc.Save(outputPath);

        Console.WriteLine($"All annotations have been removed. Saved to '{outputPath}'.");
    }
}