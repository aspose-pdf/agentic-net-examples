using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_output.pdf"; // signed PDF
        const string pfxPath    = "certificate.pfx";   // signing certificate
        const string pfxPassword = "pfxPassword";      // certificate password

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page
            // Fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc, rect)
            {
                // Optional visual properties
                Color = Aspose.Pdf.Color.LightGray,
                Contents = "Signature"
            };
            // Add the field to the document (fields are stored in the Form collection)
            doc.Form.Add(sigField);

            // Prepare a PKCS#7 signature using the certificate file
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword)
            {
                Reason   = "Document approved",
                Location = "Office",
                Date     = DateTime.UtcNow
            };

            // Sign the document using the created signature field
            sigField.Sign(pkcs7Signature);

            // Enable append‑only mode so that further signatures can be added later
            doc.Form.SignaturesAppendOnly = true;

            // Save the signed PDF. The default Save() performs an incremental update,
            // preserving the ability to add more signatures without invalidating existing ones.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed successfully. Output saved to '{outputPdf}'.");
    }
}