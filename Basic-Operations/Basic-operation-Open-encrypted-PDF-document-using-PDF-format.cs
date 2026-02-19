using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <inputPdf> <outputPdf> [userPassword]
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputPdf> <outputPdf> [userPassword]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string userPassword = args.Length >= 3 ? args[2] : string.Empty;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            Document pdfDocument;

            // Open the PDF. If a password is supplied, use the constructor that accepts it.
            if (string.IsNullOrEmpty(userPassword))
                pdfDocument = new Document(inputPath);
            else
                pdfDocument = new Document(inputPath, userPassword);

            // Save the document using the standard save rule.
            pdfDocument.Save(outputPath);

            Console.WriteLine($"PDF opened successfully and saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}