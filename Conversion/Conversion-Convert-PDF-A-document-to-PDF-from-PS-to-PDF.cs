using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_pdfa.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF/A document
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Save as a regular PDF (default format)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion completed: '{outputPath}'");
        }
        catch (Aspose.Pdf.PdfException ex)
        {
            Console.Error.WriteLine($"PDF error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}