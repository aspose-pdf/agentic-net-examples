using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath1 = "encrypted1.pdf";
        const string encryptedPdfPath2 = "encrypted2.pdf";
        const string userPassword1 = "userPass1";
        const string userPassword2 = "userPass2";
        const string resultPdfPath = "comparisonResult.pdf";

        // Ensure that the encrypted PDFs exist. If they are missing we create them on‑the‑fly.
        if (!File.Exists(encryptedPdfPath1))
            CreateEncryptedPdf(encryptedPdfPath1, userPassword1, "This is the first test document.");
        if (!File.Exists(encryptedPdfPath2))
            CreateEncryptedPdf(encryptedPdfPath2, userPassword2, "This is the second test document with a small change.");

        // Load the two encrypted PDFs by providing the passwords to the constructors.
        using (Document doc1 = new Document(encryptedPdfPath1, userPassword1))
        using (Document doc2 = new Document(encryptedPdfPath2, userPassword2))
        {
            // Perform a side‑by‑side visual comparison and save the result.
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, new SideBySideComparisonOptions());
        }

        Console.WriteLine($"Comparison saved to '{resultPdfPath}'.");
    }

    /// <summary>
    /// Creates a simple one‑page PDF, encrypts it with the supplied user password,
    /// and saves it to the specified path.
    /// </summary>
    private static void CreateEncryptedPdf(string path, string userPassword, string pageText)
    {
        using (Document doc = new Document())
        {
            // Add a single page with some text so the documents are not identical.
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment(pageText));

            // Encrypt the document – only the user password is required for opening.
            string ownerPassword = "owner_" + userPassword;
            // Use the correct Permissions enum members. "ModifyContents" does not exist in the current API;
            // the valid member is "ModifyContent" (singular).
            Permissions permissions = Permissions.PrintDocument | Permissions.ModifyContent;
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx128);

            // Save the encrypted PDF.
            doc.Save(path);
        }
    }
}
