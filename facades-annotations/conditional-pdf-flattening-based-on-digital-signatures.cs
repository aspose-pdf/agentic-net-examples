using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: using block for disposal)
            using (Document doc = new Document(inputPath))
            {
                // Initialize PdfFileSignature facade to inspect digital signatures
                PdfFileSignature signatureChecker = new PdfFileSignature();
                signatureChecker.BindPdf(inputPath);

                // Check if the PDF contains any digital signatures
                bool hasSignature = signatureChecker.ContainsSignature();

                if (hasSignature)
                {
                    Console.WriteLine("Document contains digital signatures; flattening is skipped to preserve signature validity.");
                }
                else
                {
                    // No signatures present – safe to flatten form fields
                    doc.Flatten(); // removes all form fields and replaces them with their appearances
                    Console.WriteLine("Document flattened successfully.");
                }

                // Save the (potentially flattened) document (save rule: direct Save for PDF)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}