using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted.pdf";
        const string outputPath = "output.pdf";
        const string password   = "user123"; // replace with actual password if known

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Document pdfDoc = null;
        try
        {
            // Try to load the PDF without a password.
            pdfDoc = new Document(inputPath);
        }
        catch (InvalidPasswordException)
        {
            // The PDF is encrypted – attempt to open it with the supplied password.
            Console.WriteLine("PDF is encrypted – attempting decryption with provided password.");
            try
            {
                pdfDoc = new Document(inputPath, password);
            }
            catch (InvalidPasswordException)
            {
                Console.Error.WriteLine("Unable to open the PDF – the password is incorrect or not provided.");
                return;
            }
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF processing error while loading: {ex.Message}");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error while loading PDF: {ex.Message}");
            return;
        }

        try
        {
            // Extract the first page to a new PDF.
            if (pdfDoc.Pages.Count > 0)
            {
                Document firstPageDoc = new Document();
                // Add a copy of the first page.
                firstPageDoc.Pages.Add(pdfDoc.Pages[1]);
                firstPageDoc.Save(outputPath);
                Console.WriteLine($"First page extracted to: {outputPath}");
            }
            else
            {
                Console.Error.WriteLine("The source PDF contains no pages.");
            }
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF processing error during extraction: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error during extraction: {ex.Message}");
        }
    }
}
