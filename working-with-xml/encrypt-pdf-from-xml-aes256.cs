using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlInputPath   = "input.xml";
        const string pdfOutputPath  = "output.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        // Load XML and convert to PDF
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlInputPath, loadOptions))
        {
            // Apply encryption with a strong algorithm
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save encrypted PDF
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{pdfOutputPath}'.");
    }
}