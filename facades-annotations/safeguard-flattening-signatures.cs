using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            // Initialize the signature facade with the loaded document
            PdfFileSignature signatureFacade = new PdfFileSignature(document);
            bool hasSignature = signatureFacade.ContainsSignature();

            if (hasSignature)
            {
                Console.WriteLine("Document contains digital signatures. Skipping flattening to preserve signature validity.");
            }
            else
            {
                // No signatures present – safe to flatten the form fields
                document.Flatten();
                Console.WriteLine("Document flattened successfully.");
            }

            // Save the (potentially flattened) document
            document.Save(outputPath);
        }

        Console.WriteLine($"Processed file saved as '{outputPath}'.");
    }
}