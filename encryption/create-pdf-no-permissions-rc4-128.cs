using System;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string outputPath = "privForbidAll.pdf";

        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Encrypt with user password "u2", owner password "o2", RC4x128 and no permissions (all rights forbidden)
            Permissions perms = (Permissions)0; // equivalent to "no permissions"
            doc.Encrypt("u2", "o2", perms, CryptoAlgorithm.RC4x128);

            // Save the encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
