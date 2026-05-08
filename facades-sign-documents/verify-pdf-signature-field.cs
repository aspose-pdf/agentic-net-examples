using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF that contains the signature field named "ManagerSignature"
        const string pdfPath = "input.pdf";

        // Ensure the PDF file exists before attempting to verify the signature
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use PdfFileSignature facade to work with digital signatures
        using (PdfFileSignature signatureFacade = new PdfFileSignature())
        {
            // Bind the PDF file for editing/verification
            signatureFacade.BindPdf(pdfPath);

            // Verify the integrity of the signature field "ManagerSignature"
            // Use the overload that accepts the field name as a string (SignatureName has no public ctor)
            bool isSignatureValid = signatureFacade.VerifySignature("ManagerSignature");

            // Output the verification result
            Console.WriteLine($"Signature 'ManagerSignature' valid: {isSignatureValid}");
        }
    }
}