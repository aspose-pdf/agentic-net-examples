using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input file path and output PDF path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputFile> <outputPdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            string ext = Path.GetExtension(inputPath).ToLowerInvariant();

            if (ext == ".md")
            {
                // Convert Markdown (MD) to PDF using MdLoadOptions
                MdLoadOptions mdOptions = new MdLoadOptions();

                using (Document pdfDocument = new Document(inputPath, mdOptions))
                {
                    // Save as regular PDF
                    pdfDocument.Save(outputPath);
                }
            }
            else
            {
                // Assume the input is a PDF/A (or any PDF). Load and save as standard PDF.
                using (Document pdfDocument = new Document(inputPath))
                {
                    // Save as regular PDF (non‑PDF/A)
                    pdfDocument.Save(outputPath);
                }
            }

            Console.WriteLine($"Conversion completed successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}