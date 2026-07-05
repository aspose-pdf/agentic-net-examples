using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Initialize the PdfFileSignature facade with the loaded document
            PdfFileSignature signatureFacade = new PdfFileSignature(doc);

            // Verify the cryptographic integrity of the signature field named "ManagerSignature"
            bool isSignatureValid = signatureFacade.VerifySignature("ManagerSignature");

            Console.WriteLine($"Signature 'ManagerSignature' valid: {isSignatureValid}");
        }
    }
}