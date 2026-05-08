using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "filled_form.pdf";      // PDF with filled form fields
        const string outputPdfPath = "signed_form.pdf";      // Resulting signed PDF
        const string pfxPath       = "certificate.pfx";     // PKCS#12 certificate file
        const string pfxPassword   = "pfxPassword";         // Password for the certificate

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        try
        {
            // Load the existing PDF (must be disposed via using)
            using (Document doc = new Document(inputPdfPath))
            {
                // Define the rectangle where the signature appearance will be placed.
                // Aspose.Pdf.Rectangle constructor expects (llx, lly, urx, ury).
                // Here we place the signature at (100,100) with width 200 and height 50.
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

                // Create a signature field on the first page.
                SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);
                sigField.AlternateName = "Document Signature"; // optional tooltip

                // Add the signature field to the page annotations collection.
                doc.Pages[1].Annotations.Add(sigField);

                // Use a concrete signature class (PKCS7) – Signature is abstract.
                PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
                {
                    Reason       = "Document certification",
                    Location     = "Company HQ",
                    ContactInfo  = "contact@example.com"
                };

                // Sign the document using the signature field.
                sigField.Sign(pkcs7);

                // Save the signed PDF.
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
