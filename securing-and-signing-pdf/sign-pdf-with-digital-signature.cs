using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_certified.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the signature field (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

            // Create a signature field and add it to the document's form
            SignatureField sigField = new SignatureField(doc, rect);
            doc.Form.Add(sigField);

            // Create a PKCS#1 signature object using the PFX certificate
            PKCS1 pkcs1 = new PKCS1(pfxPath, pfxPassword);

            // NOTE: The core Aspose.Pdf API does not expose a way to apply a DocMDP certification
            // (which would require PdfFileSignature from the Facades namespace). Therefore we apply a
            // regular digital signature. If a certification with specific permissions is required,
            // it must be done via the Facades API, which is prohibited by the given namespace restriction.
            sigField.Sign(pkcs1);

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document signed and saved to '{outputPath}'.");
    }
}
