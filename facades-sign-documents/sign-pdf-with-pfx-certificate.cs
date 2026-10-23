using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string outputPath  = "signed_output.pdf";
        const string pfxPath     = "certificate.pfx";
        const string pfxPassword = "pfxPassword";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document doc = new Document(inputPath);

            // Create a signature field on the first page (pages are 1‑based)
            SignatureField sigField = new SignatureField(
                doc,
                new Aspose.Pdf.Rectangle(100, 100, 250, 150));
            sigField.PartialName = "Signature1";

            // Add the signature field to the document's form collection on page 1
            doc.Form.Add(sigField, 1);

            // Create a PKCS#1 signature using the PFX file
            PKCS1 pkcs1Signature = new PKCS1(pfxPath, pfxPassword);

            // Sign the field directly – this also sets a default appearance
            sigField.Sign(pkcs1Signature);

            // Save the signed PDF
            doc.Save(outputPath);

            Console.WriteLine($"PDF signed successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error signing PDF: {ex.Message}");
        }
    }
}
