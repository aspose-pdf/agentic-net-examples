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
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Restrict printing and copying by granting no such permissions.
            // Here we set permissions to 0 (no permissions granted).
            Permissions perms = 0;

            // Encrypt the PDF with the specified passwords and permissions.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{pdfPath}'.");
    }
}