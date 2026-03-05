using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input file can be a CGM file or a PDF/A file.
        const string inputPath  = "input.cgm";   // change to your source file
        const string outputPath = "output.pdf";  // desired PDF output

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string ext = Path.GetExtension(inputPath).ToLowerInvariant();

        try
        {
            if (ext == ".cgm")
            {
                // Load CGM using CgmLoadOptions (input‑only format) and save as PDF.
                using (Document doc = new Document(inputPath, new CgmLoadOptions()))
                {
                    doc.Save(outputPath); // saves as PDF by default
                }
            }
            else if (ext == ".pdf")
            {
                // Load PDF (could be PDF/A) and remove PDF/A compliance before saving.
                using (Document doc = new Document(inputPath))
                {
                    // Ensure the resulting file is a regular PDF, not PDF/A.
                    doc.RemovePdfaCompliance();
                    doc.Save(outputPath);
                }
            }
            else
            {
                Console.Error.WriteLine("Unsupported input format. Use .cgm or .pdf files.");
                return;
            }

            Console.WriteLine($"Conversion completed successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}