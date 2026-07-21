using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string tempPdf    = "temp_with_field.pdf"; // intermediate PDF with signature field
        const string outputPdf  = "signed_output.pdf";   // final signed PDF
        const string certPath   = "certificate.pfx";    // signing certificate
        const string certPass   = "password";           // certificate password
        const string appearance = "signature.png";      // image for signature appearance

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }
        if (!File.Exists(appearance))
        {
            Console.Error.WriteLine($"Signature appearance image not found: {appearance}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Add a signature field named "DigitalSignature" to the PDF
        // -----------------------------------------------------------------
        // FormEditor is a Facades class used to manipulate form fields.
        // It does NOT implement IDisposable, so we do not wrap it in a using block.
        FormEditor formEditor = new FormEditor();
        formEditor.BindPdf(inputPdf); // load the source PDF

        // Add a signature field on page 1 with the specified rectangle.
        // FieldType.Signature creates a signature form field.
        // Rectangle coordinates are (llx, lly, urx, ury) in points.
        bool fieldAdded = formEditor.AddField(FieldType.Signature,
                                              "DigitalSignature", // field name
                                              1,                  // page number (1‑based)
                                              100, 100,           // lower‑left X,Y
                                              200, 150);          // upper‑right X,Y

        if (!fieldAdded)
        {
            Console.Error.WriteLine("Failed to add signature field.");
            return;
        }

        // Save the PDF that now contains the empty signature field.
        formEditor.Save(tempPdf);
        formEditor.Close(); // release any resources held by the facade

        // ---------------------------------------------------------------
        // Step 2: Sign the newly created signature field using PdfFileSignature
        // ---------------------------------------------------------------
        // PdfFileSignature is a Facades class for digital signing.
        // It also does NOT implement IDisposable, so we manage it manually.
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(tempPdf); // load the PDF that contains the field

        // Set a graphic appearance for the signature (optional).
        pdfSigner.SignatureAppearance = appearance;

        // Provide the certificate that will be used for signing.
        pdfSigner.SetCertificate(certPath, certPass);

        // Create a PKCS1 signature object using the same certificate.
        // The PKCS1 class derives from Signature and holds signing options.
        PKCS1 pkcs1Signature = new PKCS1(certPath, certPass);

        // Sign the field named "DigitalSignature". All metadata (reason,
        // contact, location) can be set on the PKCS1 object if needed.
        pdfSigner.Sign("DigitalSignature", pkcs1Signature);

        // Save the signed PDF.
        pdfSigner.Save(outputPdf);
        pdfSigner.Close(); // close the facade

        // Cleanup the intermediate file.
        try { File.Delete(tempPdf); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}