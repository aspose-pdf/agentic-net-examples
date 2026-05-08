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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Use PdfFileSignature (facade) to check for existing digital signatures
            using (PdfFileSignature signatureChecker = new PdfFileSignature())
            {
                // Bind the already loaded document to avoid re-reading the file
                signatureChecker.BindPdf(doc);

                bool hasSignature = signatureChecker.ContainsSignature();

                if (hasSignature)
                {
                    // PDF contains signatures – skip flattening to preserve signature validity
                    Console.WriteLine("Document contains digital signatures; flattening skipped.");
                }
                else
                {
                    // No signatures – safe to flatten form fields and annotations
                    doc.Flatten();
                    Console.WriteLine("Document flattened successfully.");
                }
            }

            // Save the (potentially flattened) PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
            Console.WriteLine($"Saved result to '{outputPath}'.");
        }
    }
}