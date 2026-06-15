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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use PdfFileSignature facade to detect existing digital signatures
            using (PdfFileSignature sigFacade = new PdfFileSignature())
            {
                sigFacade.BindPdf(inputPath);

                bool hasSignature = sigFacade.ContainsSignature();

                if (hasSignature)
                {
                    // PDF contains a digital signature – skip flattening to preserve validity
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
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}