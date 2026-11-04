using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF. If the file is encrypted, a PdfException will be thrown.
            using (var doc = new Document(inputPath))
            {
                // Flatten all interactive form fields into static content.
                // The Form property may be null for PDFs without an AcroForm, so we guard against it.
                doc.Form?.Flatten();

                // Save the flattened PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Form fields flattened and saved to '{outputPath}'.");
        }
        catch (PdfException)
        {
            Console.Error.WriteLine("The PDF is encrypted and cannot be processed without a password.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
