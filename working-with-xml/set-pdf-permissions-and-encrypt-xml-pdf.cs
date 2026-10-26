using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";
        const string userPassword = "";          // empty user password allows opening without prompt
        const string ownerPassword = "owner123"; // owner password required for permission changes

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load the XML file and convert it to a PDF document
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Restrict printing and copying by granting no permissions (view‑only).
            // Using (Permissions)0 creates a permission set with all flags cleared.
            Permissions perms = (Permissions)0;

            // Encrypt the document using AES‑256 algorithm with the specified passwords and permissions
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the secured PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with restricted permissions to '{outputPdf}'.");
    }
}