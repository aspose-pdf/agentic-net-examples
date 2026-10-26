using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load XML and convert it to a PDF document
            using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
            {
                // Define permissions for the protected PDF (allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Apply password protection using AES‑256 encryption
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(pdfPath);
            }

            Console.WriteLine($"Protected PDF saved to '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
