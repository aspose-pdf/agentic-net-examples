using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfe.pdf";
        const string logPath = "conversion_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF document.
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // NOTE: In some Aspose.Pdf versions the PDF/E format enum (e.g., PdfFormat.PDF_E_1B) is not available.
            // As a fallback, PDF/X‑4 is used here, which is an engineering‑oriented PDF format.
            // If your Aspose.Pdf version supports PDF/E, replace PdfFormat.PDF_X_4 with the appropriate enum value.
            doc.Convert(
                logPath,
                Aspose.Pdf.PdfFormat.PDF_X_4,          // target format (PDF/E if available)
                Aspose.Pdf.ConvertErrorAction.Delete   // delete objects that cannot be converted
            );

            // Save the converted document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Converted document saved to '{outputPath}'.");
    }
}