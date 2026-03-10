using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Added for Resolution

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output file path (including desired extension)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: ConvertPdf <input.pdf> <output.{docx|pptx|html|png|jpg|jpeg|tiff}>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        string ext = Path.GetExtension(outputPath).ToLowerInvariant();

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPath))
            {
                switch (ext)
                {
                    case ".docx":
                        // Convert to DOCX using DocSaveOptions
                        var docOpts = new DocSaveOptions
                        {
                            Format = DocSaveOptions.DocFormat.DocX
                        };
                        pdfDoc.Save(outputPath, docOpts);
                        break;

                    case ".pptx":
                        // Convert to PPTX using PptxSaveOptions
                        var pptxOpts = new PptxSaveOptions();
                        pdfDoc.Save(outputPath, pptxOpts);
                        break;

                    case ".html":
                    case ".htm":
                        // Convert to HTML using HtmlSaveOptions (required for non‑PDF output)
                        var htmlOpts = new HtmlSaveOptions
                        {
                            PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                            RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                        };
                        pdfDoc.Save(outputPath, htmlOpts);
                        break;

                    case ".png":
                    case ".jpg":
                    case ".jpeg":
                    case ".tiff":
                    case ".tif":
                        // Convert each page to an image using PdfConverter (Facade)
                        ConvertToImages(pdfDoc, outputPath, ext);
                        break;

                    default:
                        Console.Error.WriteLine($"Unsupported output format: {ext}");
                        break;
                }
            }

            Console.WriteLine($"Conversion completed: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }

    static void ConvertToImages(Document pdfDoc, string outputPath, string ext)
    {
        // Determine the desired ImageFormat based on the file extension
        ImageFormat imgFormat = ext switch
        {
            ".png" => ImageFormat.Png,
            ".jpg" => ImageFormat.Jpeg,
            ".jpeg" => ImageFormat.Jpeg,
            ".tiff" => ImageFormat.Tiff,
            ".tif" => ImageFormat.Tiff,
            _ => ImageFormat.Png
        };

        // Use PdfConverter facade to render pages as images
        using (PdfConverter converter = new PdfConverter(pdfDoc))
        {
            converter.Resolution = new Resolution(150); // Fixed: Resolution expects a Resolution object
            converter.StartPage = 1;                  // first page
            converter.EndPage = pdfDoc.Pages.Count;   // last page
            converter.DoConvert();                    // initialise conversion

            // If the document has a single page, save directly to the requested file name
            if (pdfDoc.Pages.Count == 1)
            {
                converter.GetNextImage(outputPath, imgFormat);
            }
            else
            {
                // For multi‑page PDFs generate separate files with a page number suffix
                int pageNum = 1;
                while (converter.HasNextImage())
                {
                    string pagePath = Path.Combine(
                        Path.GetDirectoryName(outputPath) ?? string.Empty,
                        $"{Path.GetFileNameWithoutExtension(outputPath)}_page{pageNum}{ext}");

                    converter.GetNextImage(pagePath, imgFormat);
                    pageNum++;
                }
            }
        }
    }
}
