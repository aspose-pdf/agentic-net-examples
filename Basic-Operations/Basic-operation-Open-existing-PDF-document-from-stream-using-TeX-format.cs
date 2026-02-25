using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source TeX file (will be opened as a stream)
        const string texFilePath = "sample.tex";

        // Path where the resulting PDF will be saved
        const string outputPdfPath = "output.pdf";

        // Verify that the TeX source file exists
        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"TeX file not found: {texFilePath}");
            return;
        }

        // Open the TeX file as a read‑only FileStream
        using (FileStream texStream = File.OpenRead(texFilePath))
        {
            // Create default TeX load options – can be customized if needed
            TeXLoadOptions texLoadOptions = new TeXLoadOptions();

            // Load the TeX content from the stream into a PDF Document.
            // The Document constructor that accepts (Stream, LoadOptions) performs the conversion.
            using (Document pdfDoc = new Document(texStream, texLoadOptions))
            {
                // Save the generated PDF to the specified output path.
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"TeX file successfully converted to PDF: {outputPdfPath}");
    }
}