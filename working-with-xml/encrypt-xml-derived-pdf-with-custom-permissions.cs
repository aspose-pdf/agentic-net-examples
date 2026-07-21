using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source XML file and the resulting encrypted PDF
        const string xmlInputPath  = "input.xml";
        const string pdfOutputPath = "output_encrypted.pdf";

        // Passwords for encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Ensure the XML source file exists
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"File not found: {xmlInputPath}");
            return;
        }

        // Load the XML file into a PDF document using XmlLoadOptions
        using (Document doc = new Document(xmlInputPath, new XmlLoadOptions()))
        {
            // Define custom permissions:
            // - Do NOT include PrintDocument or ExtractContent to restrict printing and copying.
            // - Allow modifying content as an example (adjust as needed).
            Permissions customPermissions = Permissions.ModifyContent;

            // Encrypt the document with the specified passwords, permissions, and a strong algorithm.
            doc.Encrypt(userPassword, ownerPassword, customPermissions, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(pdfOutputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{pdfOutputPath}'.");
    }
}