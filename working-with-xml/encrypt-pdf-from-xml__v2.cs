using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Permissions and CryptoAlgorithm are defined here

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";          // Source XML file
        const string pdfPath = "output.pdf";         // Intermediate PDF (optional)
        const string encryptedPath = "secure_output.pdf"; // Final DRM‑protected PDF
        const string userPassword = "user123";       // Password for opening the PDF
        const string ownerPassword = "owner123";     // Password for changing permissions

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the XML and convert it to a PDF document
            using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
            {
                // Optional: save the plain PDF before encryption
                doc.Save(pdfPath);

                // Define allowed permissions for the encrypted PDF
                // "ModifyAnnotations" does not exist in older Aspose.Pdf versions; use a valid flag such as ModifyTextAnnotations.
                Permissions perms = Permissions.PrintDocument |
                                    Permissions.ModifyTextAnnotations |
                                    Permissions.ExtractContent;

                // Apply DRM by encrypting the document with AES‑256
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            Console.WriteLine($"Secure PDF saved to '{encryptedPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
