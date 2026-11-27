using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_annotations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document using the high‑level Aspose.Pdf API
            Document pdf = new Document(inputPath);

            // Remove every annotation from every page
            foreach (Page page in pdf.Pages)
            {
                page.Annotations.Clear();
            }

            // Save the cleaned document
            pdf.Save(outputPath);

            Console.WriteLine($"All annotations removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
