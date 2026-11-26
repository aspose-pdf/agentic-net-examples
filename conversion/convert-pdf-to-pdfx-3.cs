using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfx.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure conversion options for PDF/X‑3 format
            var convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_3)
            {
                // Optional: enable file size optimization
                OptimizeFileSize = true
                // Additional conversion settings can be added here
            };

            // Perform the conversion using the correct API overload
            doc.Convert(convOptions);

            // Save the converted PDF/X document (standard PDF save is sufficient)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/X file saved to '{outputPath}'.");
    }
}
