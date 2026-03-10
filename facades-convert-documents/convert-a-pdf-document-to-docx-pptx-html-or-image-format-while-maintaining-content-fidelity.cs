using System;
using System.Drawing.Imaging;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // needed for Resolution

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <inputPdf> <outputFile> <format>
        // format: docx | pptx | html | png
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: ConvertPdf <inputPdf> <outputFile> <format>");
            return;
        }

        string inputPdf = args[0];
        string outputFile = args[1];
        string format = args[2].ToLowerInvariant();

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document pdfDoc = new Document(inputPdf))
            {
                switch (format)
                {
                    case "docx":
                        // Convert to DOCX using DocSaveOptions (save-to-non-pdf always use SaveOptions)
                        var docOptions = new DocSaveOptions();
                        // The property that controls the conversion mode is "Mode" in the current API.
                        docOptions.Mode = DocSaveOptions.RecognitionMode.Flow;
                        pdfDoc.Save(outputFile, docOptions);
                        Console.WriteLine($"PDF converted to DOCX: {outputFile}");
                        break;

                    case "pptx":
                        // Convert to PPTX using PptxSaveOptions
                        var pptxOptions = new PptxSaveOptions();
                        pdfDoc.Save(outputFile, pptxOptions);
                        Console.WriteLine($"PDF converted to PPTX: {outputFile}");
                        break;

                    case "html":
                        // Convert to HTML using HtmlSaveOptions
                        var htmlOptions = new HtmlSaveOptions
                        {
                            // Embed all resources into a single HTML file for fidelity
                            PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                            // Preserve raster images as PNG inside the HTML
                            RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                        };
                        pdfDoc.Save(outputFile, htmlOptions);
                        Console.WriteLine($"PDF converted to HTML: {outputFile}");
                        break;

                    case "png":
                        // Convert each page to PNG using PdfConverter (image format)
                        using (PdfConverter converter = new PdfConverter(pdfDoc))
                        {
                            // Set resolution for higher fidelity (default is 150 DPI)
                            converter.Resolution = new Resolution(300);
                            // Start and end page (1‑based indexing)
                            converter.StartPage = 1;
                            converter.EndPage = pdfDoc.Pages.Count;

                            // Initialize conversion
                            converter.DoConvert();

                            // Iterate through pages and save each as PNG
                            int pageNumber = 1;
                            while (converter.HasNextImage())
                            {
                                string pageOutput = Path.GetFileNameWithoutExtension(outputFile) +
                                                    $"_page{pageNumber}.png";
                                converter.GetNextImage(pageOutput, ImageFormat.Png);
                                Console.WriteLine($"Saved page {pageNumber} as PNG: {pageOutput}");
                                pageNumber++;
                            }
                        }
                        break;

                    default:
                        Console.Error.WriteLine($"Unsupported format: {format}");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
