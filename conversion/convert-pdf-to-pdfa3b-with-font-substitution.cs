using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_pdfa3b.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Substitute missing fonts globally.
        // Example: if the source PDF uses "Helvetica" which is not available,
        // replace it with "Arial" (must be installed on the machine).
        // Add more substitutions as needed.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("TimesNewRomanPSMT", "Times New Roman"));

        // Load the source PDF.
        using (Document doc = new Document(inputPdf))
        {
            // Prepare conversion options for PDF/A‑3B.
            var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_3B)
            {
                // Handle objects that cannot be converted.
                ErrorAction = ConvertErrorAction.Delete,

                // Optional: reduce file size by optimizing fonts.
                // OptimizeFileSize = true,
                // ExcludeFontsStrategy = PdfFormatConversionOptions.RemoveFontsStrategy.SubsetFonts |
                //                         PdfFormatConversionOptions.RemoveFontsStrategy.RemoveDuplicatedFonts
            };

            // Perform the conversion.
            bool success = doc.Convert(conversionOptions);
            if (!success)
            {
                Console.Error.WriteLine("PDF/A‑3B conversion failed.");
                return;
            }

            // Save the converted document.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF successfully converted to PDF/A‑3B: {outputPdf}");
    }
}