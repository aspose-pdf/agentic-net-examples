using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Annotations; // Added namespace for Border

class Program
{
    static void Main()
    {
        const string inputPdf   = "filled_form.pdf";      // PDF with filled form fields
        const string outputPdf  = "signed_form.pdf";      // Resulting signed PDF
        const string pfxPath    = "certificate.pfx";      // PKCS#12 certificate file
        const string pfxPassword = "pfxPassword";        // Password for the certificate

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        try
        {
            // Load the existing PDF (filled form fields are preserved)
            using (Document doc = new Document(inputPdf))
            {
                // Define the rectangle where the signature field will appear
                // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
                // Adjust coordinates as needed for your document.
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

                // Create a signature field on the first page (page index is 1‑based)
                Page firstPage = doc.Pages[1];
                SignatureField sigField = new SignatureField(firstPage, sigRect);

                // Set visual properties (background colour and border)
                sigField.Color = Aspose.Pdf.Color.LightGray;
                sigField.Border = new Border(sigField) { Width = 1 };

                // Add the signature field to the page annotations collection
                firstPage.Annotations.Add(sigField);

                // Create a concrete PKCS#7 signature object from the PFX file
                PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
                {
                    Reason      = "Document certification",
                    Location    = "Company HQ",
                    ContactInfo = "contact@example.com",
                    Date        = DateTime.UtcNow
                };

                // Sign the document using the created signature field
                sigField.Sign(pkcs7);

                // Save the signed PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF signed successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}
