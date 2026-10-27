using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Rectangle used by PdfFileSignature
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "certPassword";
        const string tempPath = "temp_with_field.pdf"; // intermediate file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load the PDF and add a visible signature field on the
        //    bottom‑right corner of the last page.
        // ------------------------------------------------------------
        Document doc = new Document(inputPath);
        int lastPageNumber = doc.Pages.Count; // 1‑based page index used by Form.Add
        Page lastPage = doc.Pages[lastPageNumber];

        // Page dimensions (points)
        double pageWidth = lastPage.PageInfo.Width;
        double pageHeight = lastPage.PageInfo.Height;

        // Define the rectangle for the visible signature (bottom‑right)
        const double sigWidth = 150;
        const double sigHeight = 50;
        const double margin = 10;
        // Aspose.Pdf.Rectangle: lower‑left X/Y, upper‑right X/Y
        Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(
            pageWidth - sigWidth - margin, // LLX
            margin,                        // LLY
            pageWidth - margin,            // URX
            margin + sigHeight);           // URY

        // Create the signature field and add it to the form
        SignatureField sigField = new SignatureField(doc.Pages[lastPageNumber], sigRect)
        {
            PartialName = "Signature1"
        };
        // The second argument (page index) is 1‑based
        doc.Form.Add(sigField, lastPageNumber);

        // Save the intermediate PDF that now contains the signature field
        doc.Save(tempPath);

        // ------------------------------------------------------------
        // 2. Sign the PDF using PdfFileSignature. The signature will be
        //    placed into the previously created field (visible).
        // ------------------------------------------------------------
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(tempPath);

        // Prepare PKCS7 object with a custom appearance
        PKCS7 pkcs7 = new PKCS7(certPath, certPassword);
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance
        {
            ShowContactInfo = true,
            ShowLocation = true,
            ContactInfoLabel = "Contact:",
            LocationLabel = "Location:"
        };
        pkcs7.CustomAppearance = customAppearance;

        // Convert Aspose.Pdf.Rectangle to System.Drawing.Rectangle (required by Sign method)
        System.Drawing.Rectangle drawingRect = new System.Drawing.Rectangle(
            (int)sigRect.LLX,
            (int)sigRect.LLY,
            (int)(sigRect.URX - sigRect.LLX),
            (int)(sigRect.URY - sigRect.LLY));

        // Sign the document. The page index is zero‑based; we use the last page.
        pdfSignature.Sign(
            lastPageNumber - 1,               // zero‑based page index
            "Approved",                      // reason
            "contact@example.com",           // contact info
            "Office",                        // location
            true,                             // make signature visible
            drawingRect,                      // rectangle where the signature will appear (System.Drawing.Rectangle)
            pkcs7);                           // PKCS7 object containing the certificate and appearance

        // Save the final signed PDF
        pdfSignature.Save(outputPath);

        // Clean up the temporary file (optional)
        try { File.Delete(tempPath); } catch { /* ignore */ }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
