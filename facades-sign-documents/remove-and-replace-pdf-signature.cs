using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, a temporary PDF without signatures, and the final signed PDF
        const string inputPdf   = "input.pdf";
        const string tempPdf    = "temp_no_sig.pdf";
        const string signedPdf  = "signed_output.pdf";

        // Path to the updated certificate (PFX) and its password
        const string certPath     = "updated_cert.pfx";
        const string certPassword = "certPassword";

        // Verify required files exist
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

        // ---------------------------------------------------------------------
        // 1. Remove the first existing signature (if any) and save to a temp file
        // ---------------------------------------------------------------------
        try
        {
            using (var remover = new PdfFileSignature())
            {
                remover.BindPdf(inputPdf);
                var signatures = remover.GetSignatureNames(true);
                if (signatures != null && signatures.Count > 0)
                {
                    // Remove the first signature (index 0)
                    remover.RemoveSignature(signatures[0]);
                }
                // Save the PDF that now has no signatures
                remover.Save(tempPdf);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error removing signature: {ex.Message}");
            return;
        }

        // ---------------------------------------------------------------------
        // 2. Sign the PDF (now without signatures) using the updated certificate
        // ---------------------------------------------------------------------
        try
        {
            // Create a PKCS#7 signature object from the PFX file
            var pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Document updated",
                Location = "",
                ContactInfo = ""
                // CustomAppearance can be set here if a visible appearance is required
            };

            using (var signer = new PdfFileSignature())
            {
                signer.BindPdf(tempPdf);
                // Sign on the first page, invisible appearance (visible = false)
                // Rectangle values are ignored when visible = false, but a dummy rectangle is required.
                var dummyRect = new Rectangle(0, 0, 0, 0);
                signer.Sign(1, pkcs7.Reason, pkcs7.ContactInfo, pkcs7.Location, false, dummyRect, pkcs7);
                signer.Save(signedPdf);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error signing PDF: {ex.Message}");
            return;
        }
        finally
        {
            // Delete the temporary file used for the unsigned PDF.
            if (File.Exists(tempPdf))
            {
                try { File.Delete(tempPdf); } catch { /* ignore cleanup errors */ }
            }
        }

        Console.WriteLine($"Signed PDF saved to '{signedPdf}'.");
    }
}
