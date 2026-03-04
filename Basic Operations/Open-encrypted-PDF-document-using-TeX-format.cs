using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf;               // TeXSaveOptions is also in this namespace

class Program
{
    static void Main()
    {
        // Paths to the encrypted PDF and the output TeX file
        const string encryptedPdfPath = "encrypted_input.pdf";
        const string texOutputPath    = "output.tex";

        // Password for the encrypted PDF (user password)
        const string userPassword = "myPassword";

        // Verify the input file exists
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the constructor that accepts a password
            using (Document pdfDoc = new Document(encryptedPdfPath, userPassword))
            {
                // Initialize TeX save options (default constructor)
                TeXSaveOptions texSaveOptions = new TeXSaveOptions();

                // Save the PDF content as a TeX file using the save options
                pdfDoc.Save(texOutputPath, texSaveOptions);
            }

            Console.WriteLine($"Encrypted PDF successfully opened and saved as TeX: '{texOutputPath}'");
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