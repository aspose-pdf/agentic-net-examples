using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "signed_input.pdf";
        const string outputPath = "privileged_output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the already signed PDF
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. (Optional) Apply a Document MDP signature that disallows any changes.
            //    If you have a certificate you can sign the MDP, otherwise the
            //    encryption permissions below are sufficient for the requested
            //    restrictions (disable content extraction and form filling).
            // -----------------------------------------------------------------
            // Example (requires a valid certificate):
            // var mdp = new DocMDPSignature(doc, DocMDPAccessPermissions.NoChanges);
            // mdp.Sign("mycert.pfx", "certPassword");

            // -----------------------------------------------------------------
            // 2. Encrypt the document while disabling content extraction and form filling.
            //    Only allow printing.
            // -----------------------------------------------------------------
            Permissions perms = Permissions.PrintDocument; // allow printing only
            // Do NOT include Permissions.ExtractContent or Permissions.FillForm
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the protected PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with restricted privileges to '{outputPath}'.");
    }
}
