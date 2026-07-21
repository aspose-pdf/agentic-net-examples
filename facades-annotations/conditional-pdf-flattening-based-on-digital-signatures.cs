using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Check for existing digital signatures using PdfFileSignature (Facades API)
            bool hasSignature;
            using (PdfFileSignature signatureChecker = new PdfFileSignature())
            {
                signatureChecker.BindPdf(inputPath);
                hasSignature = signatureChecker.ContainsSignature();
            }

            // Load the PDF document (core API) for possible flattening
            using (Document doc = new Document(inputPath))
            {
                if (hasSignature)
                {
                    // PDF contains signatures – skip flattening to preserve their validity
                    Console.WriteLine("Document contains digital signatures; flattening skipped.");
                }
                else
                {
                    // No signatures – safe to flatten form fields and annotations
                    doc.Flatten(); // removes form fields, keeping their visual values
                    Console.WriteLine("Document flattened successfully.");
                }

                // Save the (possibly flattened) document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}