using System;
using System.IO;
using Aspose.Pdf.Facades;   // Form and PdfFileSignature facades

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";          // source PDF with AcroForm
        const string outputPdf     = "signed_output.pdf";  // result PDF
        const string certPath      = "certificate.pfx";    // signing certificate
        const string certPassword  = "password";           // certificate password

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Fill AcroForm fields using the Form facade
        // -----------------------------------------------------------------
        using (Form form = new Form(inputPdf))
        {
            // Example: fill a text field named "FirstName"
            form.FillField("FirstName", "John");

            // Example: check a checkbox field named "AgreeTerms"
            form.FillField("AgreeTerms", true);

            // Save the modified PDF into a memory stream for the next step
            using (MemoryStream modifiedPdf = new MemoryStream())
            {
                form.Save(modifiedPdf);
                modifiedPdf.Position = 0; // reset stream position for reading

                // -----------------------------------------------------------------
                // 2. Sign the modified PDF using PdfFileSignature facade
                // -----------------------------------------------------------------
                using (PdfFileSignature signer = new PdfFileSignature())
                {
                    // Bind the PDF stream to the signer
                    signer.BindPdf(modifiedPdf);

                    // Optional: set a visual appearance image for the signature
                    // signer.SignatureAppearance = "signature_appearance.png";

                    // Provide the signing certificate
                    signer.SetCertificate(certPath, certPassword);

                    // Define a visible signature rectangle (System.Drawing.Rectangle)
                    // Parameters: x, y, width, height (units are points)
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

                    // Sign on page 1 with reason, contact, location and visibility flag
                    signer.Sign(
                        page:          1,
                        SigReason:     "Document approved",
                        SigContact:    "john.doe@example.com",
                        SigLocation:   "New York",
                        visible:       true,
                        annotRect:     rect);

                    // Save the signed PDF to the output file
                    signer.Save(outputPdf);
                }
            }
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}