using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "unsigned.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileSignature facade and bind the PDF
        PdfFileSignature pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(inputPath);

        // Verify that the document reports no signatures
        bool hasSignature = pdfSign.ContainsSignature(); // expected: false

        // Attempt to verify a signature that does not exist
        bool isValid = pdfSign.VerifySigned("NonExistentSignature"); // expected: false

        Console.WriteLine($"ContainsSignature: {hasSignature}");
        Console.WriteLine($"VerifySigned returned: {isValid}");
    }
}