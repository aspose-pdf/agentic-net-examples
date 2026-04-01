using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (AutoFiller autoFiller = new AutoFiller())
            {
                // Attempt to bind the PDF. If the file is encrypted this will throw InvalidPasswordException.
                autoFiller.BindPdf(inputPath);

                // For demonstration we simply save the (unchanged) document.
                autoFiller.Save(outputPath);
                Console.WriteLine($"PDF processed and saved to '{outputPath}'.");
            }
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine("Failed to bind PDF: the file is encrypted and requires a password.");
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}