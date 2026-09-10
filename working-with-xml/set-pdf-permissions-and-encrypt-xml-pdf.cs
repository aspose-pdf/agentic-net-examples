using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath        = "input.xml";          // source XML file
        const string outputPdfPath  = "output.pdf";         // encrypted PDF output
        const string userPassword   = "user123";            // password for opening
        const string ownerPassword  = "owner123";           // password for permissions change

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load XML and convert to PDF
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Define permissions: exclude PrintDocument and ExtractContent
            // Here we allow only content modification; adjust as needed.
            Permissions perms = Permissions.ModifyContent;

            // Encrypt with AES-256 and the defined permissions
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPdfPath}'.");
    }
}