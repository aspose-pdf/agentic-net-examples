using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string pfxPath    = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPath) || !File.Exists(pfxPath))
        {
            Console.Error.WriteLine("Input PDF or certificate file not found.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that a second page exists (pages are 1‑based)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document has fewer than two pages.");
                return;
            }

            // Get the second page
            Page page2 = doc.Pages[2];

            // Define a rectangle for the visible signature field
            // Position: bottom‑right corner with a 20‑point margin
            double fieldWidth  = 150; // width of the signature field
            double fieldHeight = 50;  // height of the signature field
            double llx = page2.PageInfo.Width - fieldWidth - 20; // lower‑left X
            double lly = 20;                                   // lower‑left Y
            double urx = page2.PageInfo.Width - 20;            // upper‑right X
            double ury = lly + fieldHeight;                    // upper‑right Y

            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create the signature field on the second page
            SignatureField sigField = new SignatureField(page2, rect);
            // Add the field to the page's annotation collection
            page2.Annotations.Add(sigField);

            // Load the certificate (PFX) and create a concrete PKCS7 signature object
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                PKCS7 pkcs7 = new PKCS7(pfxStream, pfxPassword);
                // Optional: set additional signature properties
                pkcs7.Reason   = "Document approved";
                pkcs7.Location = "Head Office";

                // Apply the digital signature using the field
                sigField.Sign(pkcs7);
            }

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
