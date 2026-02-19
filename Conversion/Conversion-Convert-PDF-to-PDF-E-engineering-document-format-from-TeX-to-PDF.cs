using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input TeX file path and output PDF/E file path
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: TeXToPdfEConverter <input.tex> <output.pdf>");
            return;
        }

        string texPath = args[0];
        string outputPath = args[1];

        // Verify the TeX source file exists
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"Error: TeX file not found – {texPath}");
            return;
        }

        try
        {
            // Load the TeX document using TeXLoadOptions (default options)
            var texLoadOptions = new TeXLoadOptions();
            Document pdfDoc = new Document(texPath, texLoadOptions);

            // Convert the document to PDF/E format.
            var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_E_1);
            pdfDoc.Convert(conversionOptions);

            // Save the converted document as a regular PDF file (the internal format is now PDF/E).
            pdfDoc.Save(outputPath, SaveFormat.Pdf);

            Console.WriteLine($"Conversion successful. PDF/E saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
