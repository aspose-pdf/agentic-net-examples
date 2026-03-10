using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facade classes (e.g., PdfConverter) are available here
using Aspose.Pdf.Text;      // Required for text‑related types if needed
using Aspose.Pdf.Devices;   // Required for Resolution type

class PdfTransformer
{
    /// <summary>
    /// Converts a PDF document to the specified output format while keeping its content,
    /// layout and metadata intact.
    /// Supported formats: PDF (copy), HTML, DOCX, XLSX, PPTX, EPUB, XML, SVG.
    /// </summary>
    /// <param name="inputPdfPath">Full path to the source PDF file.</param>
    /// <param name="outputPath">Full path to the destination file (extension determines format).</param>
    public static void Transform(string inputPdfPath, string outputPath)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF – this also loads all metadata and tagged content.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Determine the desired output format from the file extension.
            string ext = Path.GetExtension(outputPath).ToLowerInvariant();

            // Choose the appropriate SaveOptions subclass.
            // All SaveOptions live in the Aspose.Pdf namespace (no Aspose.Pdf.Saving namespace).
            switch (ext)
            {
                case ".pdf":
                    // Simple copy – metadata and layout are preserved automatically.
                    pdfDoc.Save(outputPath);
                    break;

                case ".html":
                case ".htm":
                    // HTML conversion requires explicit HtmlSaveOptions.
                    HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                    {
                        // Preserve the original layout as closely as possible.
                        PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                        RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                    };
                    pdfDoc.Save(outputPath, htmlOpts);
                    break;

                case ".docx":
                    // DOCX conversion – use DocSaveOptions with the correct format.
                    DocSaveOptions docOpts = new DocSaveOptions
                    {
                        Format = DocSaveOptions.DocFormat.DocX
                    };
                    pdfDoc.Save(outputPath, docOpts);
                    break;

                case ".xlsx":
                    // XLSX conversion – use ExcelSaveOptions.
                    ExcelSaveOptions xlsOpts = new ExcelSaveOptions
                    {
                        Format = ExcelSaveOptions.ExcelFormat.XLSX
                    };
                    pdfDoc.Save(outputPath, xlsOpts);
                    break;

                case ".pptx":
                    // PPTX conversion – use PptxSaveOptions.
                    PptxSaveOptions pptxOpts = new PptxSaveOptions();
                    pdfDoc.Save(outputPath, pptxOpts);
                    break;

                case ".epub":
                    // EPUB conversion – specify the recognition mode.
                    EpubSaveOptions epubOpts = new EpubSaveOptions
                    {
                        ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                    };
                    pdfDoc.Save(outputPath, epubOpts);
                    break;

                case ".xml":
                    // XML representation of the PDF structure.
                    XmlSaveOptions xmlOpts = new XmlSaveOptions();
                    pdfDoc.Save(outputPath, xmlOpts);
                    break;

                case ".svg":
                    // SVG conversion – use SvgSaveOptions.
                    SvgSaveOptions svgOpts = new SvgSaveOptions();
                    pdfDoc.Save(outputPath, svgOpts);
                    break;

                case ".tiff":
                case ".tif":
                    // Image‑based conversion using the PdfConverter facade.
                    // This produces a multi‑page TIFF that keeps the visual appearance.
                    using (PdfConverter converter = new PdfConverter(pdfDoc))
                    {
                        // Optional: set resolution (default 150 DPI).
                        converter.Resolution = new Resolution(150);
                        // Convert all pages to a single TIFF file.
                        converter.DoConvert();
                        converter.SaveAsTIFF(outputPath);
                    }
                    break;

                default:
                    Console.Error.WriteLine($"Unsupported output format: {ext}");
                    break;
            }
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }

    // Example usage
    static void Main()
    {
        // Adjust these paths as needed.
        const string sourcePdf = "input.pdf";
        const string targetFile = "output.html"; // change extension to desired format

        Transform(sourcePdf, targetFile);
    }
}
