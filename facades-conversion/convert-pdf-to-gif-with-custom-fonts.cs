using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution
using Aspose.Pdf.Text;   // for FontRepository and SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputFolder = "GifPages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // PdfConverter implements IDisposable – wrap in using for deterministic cleanup
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(pdfPath);

            // Register custom fonts (PDF font name -> local TTF/OTF file)
            // Use FontRepository.Substitutions with SimpleFontSubstitution (AddFontFile was removed in newer versions)
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("CustomFont1", @"C:\Fonts\CustomFont1.ttf"));
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("CustomFont2", @"C:\Fonts\CustomFont2.otf"));

            // Set the resolution for rasterization (dots per inch)
            converter.Resolution = new Resolution(180);

            // Convert the whole document (all pages)
            converter.StartPage = 1;
            converter.EndPage   = converter.PageCount; // after BindPdf the PageCount is known
            converter.DoConvert();

            // Save each page as a GIF image. GetNextImage writes the file whose format is inferred from the extension.
            for (int pageNumber = 1; pageNumber <= converter.PageCount; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.gif");
                converter.GetNextImage(outputPath);
                Console.WriteLine($"Saved page {pageNumber} as GIF → {outputPath}");
            }

            // Optional explicit close (Dispose will also handle it)
            converter.Close();
        }
    }
}
