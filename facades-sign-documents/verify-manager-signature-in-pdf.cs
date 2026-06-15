using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // PDF containing the signature field
        const string signatureName = "ManagerSignature";   // Name of the signature field to verify

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the PdfFileSignature facade and bind the loaded document
            using (PdfFileSignature pdfSignature = new PdfFileSignature())
            {
                pdfSignature.BindPdf(doc);

                // Verify the integrity of the specified signature field
                bool isSignatureValid = pdfSignature.VerifySignature(signatureName);

                // Output the verification result
                Console.WriteLine($"Signature '{signatureName}' validity: {isSignatureValid}");
            }
        }
    }
}