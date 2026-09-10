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
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Use PdfFileSignature facade to check for digital signatures
                using (PdfFileSignature signatureFacade = new PdfFileSignature())
                {
                    signatureFacade.BindPdf(inputPath);
                    bool hasSignature = signatureFacade.ContainsSignature();

                    if (hasSignature)
                    {
                        // PDF contains signatures – skip flattening to preserve them
                        Console.WriteLine("Document contains digital signatures; flattening skipped.");
                    }
                    else
                    {
                        // No signatures – safe to flatten form fields and annotations
                        doc.Flatten();
                        Console.WriteLine("Document flattened successfully.");
                    }
                }

                // Save the (possibly flattened) document
                doc.Save(outputPath);
                Console.WriteLine($"Saved result to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}