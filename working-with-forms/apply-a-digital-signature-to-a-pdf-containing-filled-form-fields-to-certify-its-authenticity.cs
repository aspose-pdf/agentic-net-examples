using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input_filled.pdf";
        const string outputPdf = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        // Verify required files exist
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
            // Load the PDF document (using block ensures proper disposal)
            using (Document doc = new Document(inputPdf))
            {
                // Define the rectangle where the signature field will appear
                var sigRect = new Rectangle(100, 100, 300, 200);

                // Create a signature field on the first page
                var sigField = new SignatureField(doc.Pages[1], sigRect)
                {
                    Name = "Signature1"
                };

                // Add the signature field to the document's form collection
                doc.Form.Add(sigField);

                // Create a concrete PKCS7 signature object from the PFX certificate
                var pkcs7 = new PKCS7(pfxPath, pfxPassword)
                {
                    Reason = "Document certification",
                    Location = "Company HQ",
                    ContactInfo = "contact@example.com",
                    Date = DateTime.Now
                };

                // Sign the signature field using the PKCS7 object
                sigField.Sign(pkcs7);

                // Save the signed PDF (incremental update)
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
