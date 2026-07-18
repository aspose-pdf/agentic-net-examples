using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Convert to PDF/A if the document is not already compliant
            if (!doc.IsPdfaCompliant)
            {
                // Configure conversion options (optional non‑specification flags)
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
                convOptions.NonSpecificationCases.CheckDifferentNamesInFontDictionaries = true;

                // Perform the conversion
                doc.Convert(convOptions);
            }

            // Use the PdfFileInfo facade to write the updated PDF/A document
            PdfFileInfo fileInfo = new PdfFileInfo(doc);
            fileInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"PDF/A compliant file saved to '{outputPath}'.");
    }
}