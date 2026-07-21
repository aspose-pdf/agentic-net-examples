using System;
using System.IO;
using Aspose.Pdf;
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

        // Initialize the facade for signature operations
        PdfFileSignature pdfSign = new PdfFileSignature();

        // Bind the PDF file to the facade (load operation)
        pdfSign.BindPdf(inputPath);

        // Check that the document contains no signatures
        bool containsSignature = pdfSign.ContainsSignature();
        Console.WriteLine($"ContainsSignature: {containsSignature}");

        // VerifySigned should return false when there is no signature.
        // An empty string is used as the signature name because none exist.
        bool isSignatureValid = pdfSign.VerifySigned(string.Empty);
        Console.WriteLine($"VerifySigned (no signature) returned: {isSignatureValid}");

        // Expected outcome: containsSignature == false && isSignatureValid == false
        if (!containsSignature && !isSignatureValid)
        {
            Console.WriteLine("Verification succeeded: PDF without signatures returns false as expected.");
        }
        else
        {
            Console.WriteLine("Verification failed: Unexpected result.");
        }
    }
}