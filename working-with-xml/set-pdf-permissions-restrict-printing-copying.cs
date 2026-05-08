using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        // Paths for the source XML and the resulting PDF
        const string xmlInputPath  = "source.xml";
        const string pdfOutputPath = "restricted.pdf";

        // Passwords for encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify the XML source exists
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        // Load the XML file into a PDF document using XmlLoadOptions (input‑only format)
        XmlLoadOptions loadOpts = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlInputPath, loadOpts))
        {
            // Define custom permissions:
            // - Do NOT include PrintDocument or ExtractContent to block printing and copying.
            // - Allow other typical operations (e.g., modifying content) as needed.
            Permissions customPerms = Permissions.ModifyContent |
                                      Permissions.ModifyTextAnnotations |
                                      Permissions.FillForm |
                                      Permissions.AssembleDocument |
                                      Permissions.PrintingQuality; // printing allowed only at low quality

            // Encrypt the PDF with the custom permissions using AES‑256 (preferred algorithm)
            pdfDoc.Encrypt(userPassword, ownerPassword, customPerms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF. No SaveOptions needed because the output format is PDF.
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"Encrypted PDF with restricted printing/copying saved to '{pdfOutputPath}'.");
    }
}
