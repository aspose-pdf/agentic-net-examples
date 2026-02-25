using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string outputTexPath = "output.tex";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the user password
            using (Document doc = new Document(encryptedPdfPath, userPassword))
            {
                // Save the document as TeX using TeXSaveOptions
                TeXSaveOptions texOptions = new TeXSaveOptions();
                doc.Save(outputTexPath, texOptions);
            }

            Console.WriteLine($"Encrypted PDF opened and saved as TeX to '{outputTexPath}'.");
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