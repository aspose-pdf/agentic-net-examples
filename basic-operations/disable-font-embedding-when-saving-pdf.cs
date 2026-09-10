using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string defaultFont = "Arial";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document using a FileStream.
        // No specific load options are required for PDF files.
        using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(fs))
        {
            // Create save options and specify a default font.
            // When a default font is set, Aspose.Pdf substitutes missing fonts
            // with this font instead of embedding the original fonts.
            Aspose.Pdf.PdfSaveOptions saveOptions = new Aspose.Pdf.PdfSaveOptions
            {
                DefaultFontName = defaultFont
            };

            // Save the document with the custom save options.
            doc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}