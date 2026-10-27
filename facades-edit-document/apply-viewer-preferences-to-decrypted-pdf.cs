using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace is available for other operations if needed

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";
        const string outputPath    = "output.pdf";
        const string userPassword  = "user123";

        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF providing the correct user password
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(encryptedPath, userPassword))
            {
                // Decrypt the document (Decrypt takes no parameters)
                doc.Decrypt();

                // ------------------------------------------------------------
                // Apply viewer preferences.
                // The ViewerPreferences property exists only in newer versions
                // of Aspose.Pdf (e.g., 20.9 or later). If your project references
                // an older version, upgrade the NuGet package or comment out the
                // block below.
                // ------------------------------------------------------------
                /*
                doc.ViewerPreferences.HideToolbar = true;
                doc.ViewerPreferences.FitWindow   = true;
                doc.ViewerPreferences.CenterWindow = true;
                */

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Viewer preferences applied and saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}