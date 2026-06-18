using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string signatureName = "ContractSigner";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfFileSignature facade to work with digital signatures
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the PDF file for inspection
            pdfSignature.BindPdf(inputPdf);

            // Verify the signature with the specified name.
            // VerifySigned returns true if the signature exists and is valid.
            bool isSignatureValid = pdfSignature.VerifySigned(signatureName);

            Console.WriteLine($"Signature '{signatureName}' valid: {isSignatureValid}");
        }
    }
}