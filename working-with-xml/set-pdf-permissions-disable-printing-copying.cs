using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlInputPath = "input.xml";
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"Input file not found: {xmlInputPath}");
            return;
        }

        // Load the XML file as a PDF document using XmlLoadOptions
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        using (Document doc = new Document(xmlInputPath, loadOptions))
        {
            // Define permissions that exclude printing and copying (ExtractContent)
            Permissions allowedPermissions = Permissions.ModifyContent |
                                            Permissions.ModifyTextAnnotations |
                                            Permissions.FillForm |
                                            Permissions.AssembleDocument |
                                            Permissions.PrintingQuality;

            // Encrypt the document with AES-256; empty passwords allow opening without a password
            doc.Encrypt(userPassword: "", ownerPassword: "", permissions: allowedPermissions, cryptoAlgorithm: CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(pdfOutputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{pdfOutputPath}' with printing and copying restricted.");
    }
}