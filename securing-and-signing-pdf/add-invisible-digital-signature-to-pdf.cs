using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "signed_invisible.pdf";
        const string pfxPath    = "certificate.pfx";
        const string pfxPassword = "pfxPassword";

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

        // Load the PDF document (using rule for document disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure signatures are appended only (incremental update)
            doc.Form.SignaturesAppendOnly = true;

            // Create an invisible signature field (zero‑size rectangle)
            // Fully qualify Rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle invisibleRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            SignatureField sigField = new SignatureField(doc, invisibleRect);

            // Add the field to the first page (field collection is 1‑based)
            doc.Pages[1].Annotations.Add(sigField);

            // Prepare a PKCS#7 signature object
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);

            // Hide the visual appearance of the signature
            pkcs7.ShowProperties = false;          // no appearance text
            pkcs7.CustomAppearance = null;         // no custom appearance

            // Record the signing time (default is current time, set explicitly for clarity)
            pkcs7.Date = DateTime.Now;

            // Sign the document using the invisible field
            sigField.Sign(pkcs7);

            // Save the signed PDF (incremental update preserves original layout)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Invisible digital signature applied. Saved to '{outputPath}'.");
    }
}