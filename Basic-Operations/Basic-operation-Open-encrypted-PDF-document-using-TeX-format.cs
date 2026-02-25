using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Text;          // For any text-related options (not needed here)
using Aspose.Pdf;               // TeXLoadOptions and TeXSaveOptions are also in this namespace

class Program
{
    static void Main()
    {
        // Paths to the encrypted PDF and the output TeX file
        const string encryptedPdfPath = "encrypted.pdf";
        const string password = "user123";          // User password for the encrypted PDF
        const string outputTexPath = "output.tex";

        // Verify the input file exists
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the password.
            // Document(string, string) constructor handles password-protected PDFs.
            using (Document pdfDoc = new Document(encryptedPdfPath, password))
            {
                // OPTIONAL: If you need to decrypt the document before further processing,
                // you can call Decrypt() and then Save() the decrypted PDF.
                // pdfDoc.Decrypt();
                // pdfDoc.Save("decrypted.pdf");

                // Save the PDF as a TeX file using TeXSaveOptions.
                // TeXSaveOptions resides in the Aspose.Pdf namespace.
                TeXSaveOptions texSaveOptions = new TeXSaveOptions();

                // Save the document in TeX format.
                pdfDoc.Save(outputTexPath, texSaveOptions);
            }

            Console.WriteLine($"Encrypted PDF opened and saved as TeX: '{outputTexPath}'.");
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