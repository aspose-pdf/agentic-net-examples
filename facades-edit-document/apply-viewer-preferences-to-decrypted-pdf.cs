using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";
        const string outputPath    = "output.pdf";
        const string password      = "user123";

        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF providing the correct password.
            // Use dynamic to avoid compile‑time dependency on ViewerPreferences (available only in newer Aspose.Pdf versions).
            dynamic doc = new Document(encryptedPath, password);

            try
            {
                // Attempt to set viewer preferences if the property exists at runtime.
                doc.ViewerPreferences.HideToolbar    = true;
                doc.ViewerPreferences.HideMenubar    = true;
                doc.ViewerPreferences.CenterWindow   = true;
                doc.ViewerPreferences.FitWindow      = true;
                doc.ViewerPreferences.DisplayDocTitle = true;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            }

            // Save the modified PDF (now with the new viewer preferences if they were applied).
            doc.Save(outputPath);
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
