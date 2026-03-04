using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source TeX file (will be opened as a stream)
        const string texFilePath = "input.tex";

        // Path where the resulting PDF will be saved
        const string outputPdfPath = "output.pdf";

        // Verify that the TeX source file exists
        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"File not found: {texFilePath}");
            return;
        }

        // Open the TeX file as a read‑only stream
        using (FileStream texStream = File.OpenRead(texFilePath))
        {
            // Create default TeX load options (customize if needed)
            TeXLoadOptions texLoadOptions = new TeXLoadOptions();

            // Load the TeX content from the stream and convert it to a PDF document
            using (Document pdfDocument = new Document(texStream, texLoadOptions))
            {
                // Save the generated PDF; Document.Save(string) writes PDF by default
                pdfDocument.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Conversion completed: '{texFilePath}' → '{outputPdfPath}'.");
    }
}