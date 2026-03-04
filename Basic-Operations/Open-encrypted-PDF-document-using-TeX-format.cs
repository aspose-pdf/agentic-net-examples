using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted.pdf";
        const string password = "user123";
        const string outputTexPath = "output.tex";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the password
            using (Document doc = new Document(encryptedPdfPath, password))
            {
                // Initialize TeXSaveOptions (default settings)
                TeXSaveOptions texSaveOptions = new TeXSaveOptions();

                // Save the PDF as a TeX file
                doc.Save(outputTexPath, texSaveOptions);
            }

            Console.WriteLine($"Encrypted PDF successfully converted to TeX: '{outputTexPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}