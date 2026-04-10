using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_pdfa.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Create conversion options for PDF/A (e.g., PDF/A-1B) and enable auto‑tagging
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
            // Assign the default auto‑tagging settings (auto‑tagging enabled)
            convOptions.AutoTaggingSettings = AutoTaggingSettings.Default;
            // Optionally ensure auto‑tagging is turned on explicitly
            convOptions.AutoTaggingSettings.EnableAutoTagging = true;

            // Perform the conversion using the options object
            bool success = doc.Convert(convOptions);
            if (!success)
            {
                Console.Error.WriteLine("Conversion failed.");
                return;
            }

            // Save the converted PDF/A document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF/A file with auto‑tagging saved to '{outputPdf}'.");
    }
}