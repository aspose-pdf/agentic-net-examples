using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths and passwords
        const string encryptedPdfPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string decryptedPdfPath = "decrypted.pdf";

        // Verify encrypted PDF exists
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the user password
            using (Document doc = new Document(encryptedPdfPath, userPassword))
            {
                // Decrypt the document (no parameters required)
                doc.Decrypt();

                // Save the decrypted PDF
                doc.Save(decryptedPdfPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{decryptedPdfPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error opening encrypted PDF: {ex.Message}");
        }

        // -----------------------------------------------------------------
        // Demonstrate loading a CGM file (CGM is input‑only in Aspose.Pdf)
        // -----------------------------------------------------------------
        const string cgmPath = "input.cgm";
        const string cgmPdfOutput = "cgm_converted.pdf";

        if (File.Exists(cgmPath))
        {
            try
            {
                // Use CgmLoadOptions to import the CGM file
                CgmLoadOptions loadOptions = new CgmLoadOptions();

                using (Document cgmDoc = new Document(cgmPath, loadOptions))
                {
                    // Save the imported CGM as a PDF (CGM cannot be saved as CGM)
                    cgmDoc.Save(cgmPdfOutput);
                }

                Console.WriteLine($"CGM converted to PDF: '{cgmPdfOutput}'.");
            }
            catch (PdfException ex)
            {
                Console.Error.WriteLine($"CGM load error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing CGM: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("CGM file not found; skipping CGM import example.");
        }
    }
}