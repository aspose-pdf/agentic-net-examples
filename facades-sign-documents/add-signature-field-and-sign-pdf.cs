using System;
using System.IO;
using System.Drawing; // for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades; // Facade API for signature operations

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, and certificate (PFX) paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "certPassword";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Define signature field properties
        const string fieldName = "SignatureField1";
        const int pageNumber = 1; // 1‑based page index
        const double llx = 100;   // lower‑left X coordinate
        const double lly = 100;   // lower‑left Y coordinate
        const double urx = 300;   // upper‑right X coordinate
        const double ury = 200;   // upper‑right Y coordinate

        // ---------------------------------------------------------------------
        // 1. Load the PDF and add an empty signature field using the Document API
        // ---------------------------------------------------------------------
        Document doc = new Document(inputPdfPath);

        // Create the rectangle that defines the field bounds (Aspose.Pdf.Rectangle)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

        // Create the signature field on the required page
        SignatureField sigField = new SignatureField(doc.Pages[pageNumber], rect)
        {
            PartialName = fieldName
        };

        // Add the field to the form (the second argument is the 1‑based page index)
        doc.Form.Add(sigField, pageNumber);

        // Save the intermediate PDF that now contains the empty signature field
        const string tempPdfPath = "temp_with_field.pdf";
        doc.Save(tempPdfPath);

        // ---------------------------------------------------------------------
        // 2. Sign the PDF using PdfFileSignature, referencing the field we just added
        // ---------------------------------------------------------------------
        // Prepare the PKCS#7 certificate object
        PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword);
        pkcs7.Reason = "Document signed"; // optional metadata
        pkcs7.ContactInfo = "contact@example.com"; // optional metadata
        pkcs7.Location = "Location"; // optional metadata

        // Convert the Aspose rectangle to System.Drawing.Rectangle (required by Sign overload)
        int width = (int)(urx - llx);
        int height = (int)(ury - lly);
        System.Drawing.Rectangle signRect = new System.Drawing.Rectangle((int)llx, (int)lly, width, height);

        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the PDF that already contains the signature field
            pdfSignature.BindPdf(tempPdfPath);

            // Sign the document. The overload used is:
            // Sign(int pageNumber, string reason, string contactInfo, string location,
            //      bool isVisible, System.Drawing.Rectangle rectangle, Signature signature)
            pdfSignature.Sign(pageNumber,
                               pkcs7.Reason,
                               pkcs7.ContactInfo,
                               pkcs7.Location,
                               true,               // make the signature visible
                               signRect,
                               pkcs7);

            // Save the signed PDF to the final output path
            pdfSignature.Save(outputPdfPath);
        }

        // Clean up the temporary file (optional)
        if (File.Exists(tempPdfPath))
        {
            try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }
        }

        Console.WriteLine($"Signature field '{fieldName}' added and document signed. Output saved to '{outputPdfPath}'.");
    }
}
