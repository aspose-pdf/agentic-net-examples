using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths and password
        const string encryptedPdfPath = "encrypted.pdf";
        const string password = "user123";
        const string outputTexPath = "output.tex";

        // Verify input file exists
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
                // Initialize TeX save options
                TeXSaveOptions texOptions = new TeXSaveOptions();

                // Save the document as a TeX file
                doc.Save(outputTexPath, texOptions);
            }

            Console.WriteLine($"Encrypted PDF opened and saved as TeX: {outputTexPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}