using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades; // PKCS7 resides here

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "filled_form.pdf";   // PDF with filled form fields
        const string outputPdfPath = "signed_form.pdf";   // Resulting signed PDF
        const string pfxPath       = "certificate.pfx";  // PKCS#12 certificate file
        const string pfxPassword   = "pfxPassword";      // Password for the certificate

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

        // Load the PDF document (use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Explicitly reference the Form class from Aspose.Pdf.Forms to avoid ambiguity
            Aspose.Pdf.Forms.Form form = doc.Form;

            // Try to locate an existing signature field named "Signature1"
            SignatureField sigField = null;
            if (form.HasField("Signature1"))
            {
                // Cast the generic field to SignatureField
                sigField = form["Signature1"] as SignatureField;
            }

            // If no existing signature field, create one on the first page
            if (sigField == null)
            {
                // Define the rectangle where the signature appearance will be placed
                // Fully qualified to avoid ambiguity with other Rectangle types
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a new signature field on the first page
                sigField = new SignatureField(doc.Pages[1], rect)
                {
                    AlternateName = "Signature1",
                    Name = "Signature1"
                };

                // Add the signature field to the form
                form.Add(sigField);
            }

            // Create a PKCS7 signature object using the PFX file and password
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document certification",
                Location = "Company HQ",
                ContactInfo = "contact@example.com"
                // SignDate property does not exist in current API; the signing date is set automatically.
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}
