using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class SvgToJpegFacade
{
    /// <summary>
    /// Converts an SVG image provided as a stream into a JPEG image.
    /// The conversion is performed in two steps:
    /// 1. The SVG stream is turned into a PDF using the Document class with SvgLoadOptions.
    /// 2. The resulting PDF is rasterized to JPEG using PdfConverter.
    /// </summary>
    /// <param name="svgInput">Input stream containing SVG data. The stream must be readable and positioned at the beginning.</param>
    /// <param name="jpegOutput">Output stream where the JPEG image will be written. The stream must be writable.</param>
    public static void Convert(Stream svgInput, Stream jpegOutput)
    {
        if (svgInput == null) throw new ArgumentNullException(nameof(svgInput));
        if (jpegOutput == null) throw new ArgumentNullException(nameof(jpegOutput));

        // Step 1: Load SVG directly into a PDF document using SvgLoadOptions.
        var loadOptions = new SvgLoadOptions();
        var pdfDocument = new Document(svgInput, loadOptions);

        // Step 2: Rasterize the first page of the PDF to JPEG using PdfConverter.
        using (var converter = new PdfConverter())
        {
            converter.BindPdf(pdfDocument);
            converter.DoConvert();

            // Process only the first page (break after the first iteration).
            while (converter.HasNextImage())
            {
                converter.GetNextImage(jpegOutput);
                break; // Remove this line to process all pages.
            }
        }

        // Ensure the output stream is flushed so that all data is written.
        jpegOutput.Flush();
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // In a library project this method can be removed.
    public static void Main(string[] args)
    {
        // Example usage (optional). Comment out or remove in production.
        // using var svg = File.OpenRead("example.svg");
        // using var jpeg = File.Create("output.jpg");
        // Convert(svg, jpeg);
    }
}
