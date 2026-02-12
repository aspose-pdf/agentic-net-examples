using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the encrypted PDF file
        const string inputPdfPath = "encrypted.pdf";
        // Password for the encrypted PDF (user password)
        const string pdfPassword = "myPassword";
        // Path for the output EPUB file
        const string outputEpubPath = "output.epub";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Open the encrypted PDF using the password constructor
            // Document(string, string) loads an encrypted PDF with the provided password
            Document pdfDocument = new Document(inputPdfPath, pdfPassword);

            // Save the document as EPUB; the format is inferred from the .epub extension
            pdfDocument.Save(outputEpubPath);

            Console.WriteLine($"Successfully converted '{inputPdfPath}' to EPUB at '{outputEpubPath}'.");
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("Error: The provided password is incorrect.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}