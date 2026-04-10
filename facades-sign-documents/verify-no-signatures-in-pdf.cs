using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "unsigned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the signature facade and bind the PDF.
        PdfFileSignature pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(inputPath);

        // Verify that the document reports no signatures.
        bool containsSignature = pdfSign.ContainsSignature();
        Console.WriteLine($"ContainsSignature: {containsSignature}");

        // Attempt to verify a non‑existent signature.
        // Expected result: false because the PDF has no signatures.
        bool isSignatureValid = pdfSign.VerifySigned("DummySignature");
        Console.WriteLine($"VerifySigned (expected false): {isSignatureValid}");
    }
}