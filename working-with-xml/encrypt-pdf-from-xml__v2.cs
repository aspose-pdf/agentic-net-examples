using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Permissions and CryptoAlgorithm enums

class Program
{
    static void Main()
    {
        const string xmlPath = "source.xml";
        const string encryptedPdfPath = "secure_output.pdf";
        const string asposeDll = "Aspose.PDF.dll";

        // Verify that the Aspose.PDF assembly is available before any Aspose types are used.
        if (!File.Exists(asposeDll))
        {
            Console.Error.WriteLine($"Required assembly not found: {asposeDll}. Ensure the Aspose.PDF NuGet package is installed or the DLL is copied to the output folder.");
            return;
        }
        // Load the assembly explicitly to provide a clearer error if it cannot be loaded.
        try
        {
            Assembly.LoadFrom(Path.GetFullPath(asposeDll));
        }
        catch (Exception loadEx)
        {
            Console.Error.WriteLine($"Failed to load {asposeDll}: {loadEx.Message}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the XML and convert it directly to a PDF document
            using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
            {
                // Define the permissions that will be allowed for the end user
                Permissions perms = Permissions.PrintDocument |
                                     Permissions.ModifyContent |
                                     Permissions.ExtractContent;

                // Apply DRM encryption using AES‑256
                string userPassword = "user123";
                string ownerPassword = "owner123";
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF for secure distribution
                doc.Save(encryptedPdfPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{encryptedPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
