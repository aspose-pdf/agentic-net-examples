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

        // Load the PDF into the signature facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);

            // Verify a signature that does not exist; should return false
            bool isValid = pdfSign.VerifySigned("NonExistentSignature");

            Console.WriteLine($"VerifySigned returned: {isValid} (expected false)");
        }
    }
}