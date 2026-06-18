using System;
using Aspose.Pdf;

namespace SetPdfPermissions
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save("input.pdf");
            }

            // Open the PDF and set permissions: allow printing only, disallow copying and annotation changes
            using (Document doc = new Document("input.pdf"))
            {
                Permissions allowedPermissions = Permissions.PrintDocument;
                // Empty user password means the PDF can be opened without a password, but permissions are enforced.
                // AES 128‑bit encryption is used (AESx128). You may also use AESx256 if preferred.
                doc.Encrypt(string.Empty, "ownerPassword", allowedPermissions, CryptoAlgorithm.AESx128);
                doc.Save("output.pdf");
            }
        }
    }
}
