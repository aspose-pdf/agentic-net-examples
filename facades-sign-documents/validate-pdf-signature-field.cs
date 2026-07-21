using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // Path to the PDF to be checked
        const string signatureName = "ManagerSignature"; // Name of the signature field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfFileSignature facade and bind the loaded document
            PdfFileSignature pdfSignature = new PdfFileSignature();
            pdfSignature.BindPdf(doc); // Bind using the Document overload

            // Verify the integrity of the specified signature field
            bool isValid = pdfSignature.VerifySignature(signatureName);

            // Output the verification result
            Console.WriteLine($"Signature '{signatureName}' is valid: {isValid}");
        }
    }
}