using System;
using System.IO;
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

        // Verify whether the PDF contains any digital signatures.
        PdfFileSignature signatureFacade = new PdfFileSignature();
        bool hasSignature = signatureFacade.VerifySigned(pdfPath);

        // Expect false for a PDF with no signatures.
        Console.WriteLine($"PDF has signatures: {hasSignature}");
    }
}