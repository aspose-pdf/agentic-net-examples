using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for Border

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least two pages (1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Get the second page
            Page page2 = doc.Pages[2];

            // Define the size and position of the signature field (bottom‑right corner)
            const double fieldWidth = 150;   // width of the visible field
            const double fieldHeight = 50;   // height of the visible field
            const double margin = 20;        // margin from right and bottom edges

            double llx = page2.Rect.Width - fieldWidth - margin; // lower‑left X
            double lly = margin;                                 // lower‑left Y
            double urx = llx + fieldWidth;                       // upper‑right X
            double ury = lly + fieldHeight;                      // upper‑right Y

            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create the visible signature field on the second page
            SignatureField sigField = new SignatureField(page2, sigRect);
            sigField.Color = Aspose.Pdf.Color.LightGray; // optional border color
            sigField.Border = new Border(sigField) { Width = 1 }; // set border width

            // Add the signature field to the page's annotation collection
            page2.Annotations.Add(sigField);

            // Load the certificate (PFX) and create a concrete PKCS7 signature object
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com",
                Authority = "John Doe"
            };

            // Apply the digital signature using the created field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
