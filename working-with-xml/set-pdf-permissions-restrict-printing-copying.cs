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
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load the XML file and convert it to a PDF document
        XmlLoadOptions loadOpts = new XmlLoadOptions();
        using (Document doc = new Document(xmlPath, loadOpts))
        {
            // Set permissions to restrict printing and copying (no PrintDocument or ExtractContent bits)
            Permissions perms = (Permissions)0; // no permissions granted
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(pdfPath); // save the encrypted PDF
        }

        Console.WriteLine($"PDF saved with restricted permissions to '{pdfPath}'.");
    }
}