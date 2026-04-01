using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string unsignedPdfPath = "unsigned.pdf";
        const string dummySignatureName = "Signature1";

        // Create a simple PDF if it does not exist
        if (!File.Exists(unsignedPdfPath))
        {
            using (Document doc = new Document())
            {
                // Add a blank page
                doc.Pages.Add();
                doc.Save(unsignedPdfPath);
            }
        }

        // Load the PDF with PdfFileSignature facade
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(unsignedPdfPath);

        // Check whether any signature exists – should be false
        bool containsSignature = pdfSignature.ContainsSignature();
        Console.WriteLine($"ContainsSignature: {containsSignature}");

        // Verify a (non‑existent) signature – should return false without throwing
        bool verifyResult = false;
        try
        {
            // VerifySignature is the replacement for the obsolete VerifySigned method.
            // It returns true only when the named signature field exists and is valid.
            verifyResult = pdfSignature.VerifySignature(dummySignatureName);
        }
        catch (ArgumentException)
        {
            // Aspose throws ArgumentException when the field name is not present.
            // In this scenario we treat it as a verification failure (false).
            verifyResult = false;
        }
        Console.WriteLine($"VerifySignature('{dummySignatureName}') returned {verifyResult}");
    }
}
