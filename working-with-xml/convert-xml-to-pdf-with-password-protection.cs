using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API (includes Document, Permissions, CryptoAlgorithm)
using Aspose.Pdf.Text;          // Required for any text‑related types (not needed here but safe)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath      = "input.xml";          // Source XML file
        const string pdfPath      = "output.pdf";         // PDF after conversion
        const string protectedPdf = "output_protected.pdf"; // Encrypted PDF

        // Verify source XML exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // ---------- Load XML and create PDF ----------
            // XmlLoadOptions has a parameterless constructor for default conversion
            XmlLoadOptions loadOptions = new XmlLoadOptions();

            // Load the XML file, converting it to a PDF document
            using (Document pdfDoc = new Document(xmlPath, loadOptions))
            {
                // Save the intermediate PDF (optional, can be omitted if only the protected PDF is needed)
                pdfDoc.Save(pdfPath);

                // ---------- Apply password protection ----------
                // Define permissions – allow printing and content extraction, deny everything else
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt with user password, owner password and AES‑256 algorithm
                string userPassword  = "user123";
                string ownerPassword = "owner123";

                pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                pdfDoc.Save(protectedPdf);
            }

            Console.WriteLine($"PDF created at '{pdfPath}' and encrypted version saved at '{protectedPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}