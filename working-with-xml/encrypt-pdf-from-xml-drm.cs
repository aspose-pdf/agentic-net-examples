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

        // Load the XML file and convert it to a PDF document
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Define permissions for the encrypted PDF
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the PDF using AES-256 algorithm
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{pdfPath}'.");
    }
}