using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the encrypted PDF and the output PDF
        const string encryptedPdfPath = "encrypted_input.pdf";
        const string outputPdfPath = "output.pdf";

        // Password required to open the encrypted PDF (user password)
        const string userPassword = "user123";

        // Verify that the input file exists
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found - {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the user password
            using (Document pdfDocument = new Document(encryptedPdfPath, userPassword))
            {
                // Add a TeX fragment to the first page
                Page firstPage = pdfDocument.Pages[1];
                TeXFragment texFragment = new TeXFragment(@"\frac{a}{b} = c");
                // Optional: set alignment or other properties on the fragment
                texFragment.HorizontalAlignment = HorizontalAlignment.Center;
                firstPage.Paragraphs.Add(texFragment);

                // Save the modified PDF
                pdfDocument.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF processed and saved to '{outputPdfPath}'.");
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("Error: Invalid password for the encrypted PDF.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}