using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution
using Aspose.Pdf; // for Document, etc.
using Aspose.Pdf.Text; // for FontRepository and SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";
        const string fontsFolder = "CustomFonts"; // folder containing .ttf/.otf files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfConverter implements IDisposable – wrap in using for deterministic cleanup
            using (PdfConverter converter = new PdfConverter())
            {
                // Register custom fonts if the folder exists
                if (Directory.Exists(fontsFolder))
                {
                    // Register each font file as a substitution. The wildcard "*" can be used to map any missing font
                    // to a specific file, but here we map the font name (file name without extension) to its file path.
                    foreach (string fontPath in Directory.GetFiles(fontsFolder, "*.*", SearchOption.TopDirectoryOnly))
                    {
                        string ext = Path.GetExtension(fontPath).ToLowerInvariant();
                        if (ext == ".ttf" || ext == ".otf")
                        {
                            string fontName = Path.GetFileNameWithoutExtension(fontPath);
                            FontRepository.Substitutions.Add(new SimpleFontSubstitution(fontName, fontPath));
                        }
                    }
                }

                // Load the source PDF
                converter.BindPdf(inputPdf);

                // Set the page range to convert (all pages)
                converter.StartPage = 1;
                converter.EndPage = converter.PageCount; // all pages

                // Set high‑resolution output (300 DPI)
                converter.Resolution = new Resolution(300);

                // Perform the conversion – this prepares the internal image stream
                converter.DoConvert();

                // Extract each page as a PNG image. GetNextImage infers the format from the file extension.
                for (int pageNumber = 1; pageNumber <= converter.PageCount; pageNumber++)
                {
                    string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.png");
                    converter.GetNextImage(outPath); // format derived from ".png"
                }
            }

            Console.WriteLine($"All pages have been converted to PNG in '{Path.GetFullPath(outputDir)}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
