using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution type

class PdfConverterProgram
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output file path
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: PdfConverterProgram <input.pdf> <output.(docx|pptx|html|tiff)>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Validate input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists (null‑safe handling)
        string outDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
        {
            Directory.CreateDirectory(outDir);
        }

        // Determine desired output format from file extension
        string ext = Path.GetExtension(outputPath).ToLowerInvariant();

        try
        {
            if (ext == ".docx" || ext == ".pptx" || ext == ".html")
            {
                // Use Document class for conversion to document formats
                Document pdfDoc = new Document(inputPath);

                if (ext == ".docx")
                    pdfDoc.Save(outputPath, SaveFormat.DocX); // corrected enum name
                else if (ext == ".pptx")
                    pdfDoc.Save(outputPath, SaveFormat.Pptx); // corrected enum name
                else // .html
                    pdfDoc.Save(outputPath, SaveFormat.Html);
            }
            else if (ext == ".tiff" || ext == ".tif")
            {
                // Use PdfConverter to render each page as an image and save as a multi‑page TIFF
                using (PdfConverter converter = new PdfConverter())
                {
                    converter.BindPdf(inputPath);
                    converter.StartPage = 1;
                    converter.EndPage = converter.PageCount; // convert all pages
                    converter.Resolution = new Resolution(150); // default resolution (150 DPI)
                    converter.DoConvert();

                    // Save all pages into a single TIFF file
                    converter.SaveAsTIFF(outputPath);
                }
            }
            else
            {
                Console.Error.WriteLine($"Error: Unsupported output format '{ext}'. Supported: .docx, .pptx, .html, .tiff");
                return;
            }

            Console.WriteLine($"Conversion successful. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}