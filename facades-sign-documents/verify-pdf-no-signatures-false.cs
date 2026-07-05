using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF that has no digital signatures
        const string inputPdf = "unsigned.pdf";

        // Ensure the file exists before attempting to bind it
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"File '{inputPdf}' not found.");
            return;
        }

        // Initialize the PdfFileSignature facade and bind the PDF file
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPdf);

        // Check if the document contains any signatures (expected: false)
        bool containsSignature = pdfSignature.ContainsSignature();

        // Verify a signature by name using the non‑obsolete API.
        // Since the PDF has no signatures, this should return false.
        bool isSignatureValid = pdfSignature.VerifySignature("Signature1");

        // Output the results
        Console.WriteLine($"ContainsSignature: {containsSignature}");
        Console.WriteLine($"VerifySignature result: {isSignatureValid}");
    }
}