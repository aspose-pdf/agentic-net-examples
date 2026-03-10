using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted.pdf";
        const string userPassword      = "user123";
        const string texOutputPath     = "output.tex";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the password
            using (Document pdfDoc = new Document(encryptedPdfPath, userPassword))
            {
                // Prepare TeX save options
                TeXSaveOptions texOptions = new TeXSaveOptions();

                // Save the document as a TeX file
                pdfDoc.Save(texOutputPath, texOptions);
            }

            Console.WriteLine($"Encrypted PDF opened and saved as TeX: {texOutputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}