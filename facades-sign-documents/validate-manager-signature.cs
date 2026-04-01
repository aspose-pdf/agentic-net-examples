using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);
            // The SignatureName enum does not expose custom field names like "ManagerSignature".
            // Use the overload that accepts the signature field name as a string.
            bool isValid = pdfSignature.VerifySignature("ManagerSignature");
            Console.WriteLine($"ManagerSignature valid: {isValid}");
        }
    }
}
