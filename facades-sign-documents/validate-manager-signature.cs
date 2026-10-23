using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string signatureField = "ManagerSignature";

        // Ensure the PDF file exists before attempting verification
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileSignature does not implement IDisposable, so no using block is required
        PdfFileSignature signer = new PdfFileSignature();

        // Bind the PDF document to the signature handler
        signer.BindPdf(pdfPath);

        // Verify the integrity of the specified signature field
        // VerifySignature returns true if the signature is valid, false otherwise
        bool isSignatureValid = signer.VerifySignature(signatureField);

        // Output the verification result
        Console.WriteLine($"Signature field '{signatureField}' validation result: {isSignatureValid}");
    }
}