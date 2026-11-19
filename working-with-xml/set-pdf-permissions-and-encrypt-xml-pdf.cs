using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "output_restricted.pdf";
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
            // Allow only annotation modifications, explicitly disallow printing and content extraction (copying)
            Permissions perms = Permissions.ModifyTextAnnotations;

            // Apply encryption with the defined permissions. Permissions are enforced only when the document is encrypted.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the PDF with the restricted permissions
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with restricted permissions to '{outputPdf}'.");
    }
}
