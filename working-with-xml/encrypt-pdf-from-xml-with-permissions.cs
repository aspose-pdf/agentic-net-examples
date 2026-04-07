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

        // Load the XML file and convert it to PDF
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Define permissions (allow printing and content extraction)
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the PDF using AES-256
            pdfDoc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{pdfPath}'.");
    }
}