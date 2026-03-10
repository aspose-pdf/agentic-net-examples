using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // Facade API classes
using Aspose.Pdf.Text;            // Needed for SaveOptions enums
using Aspose.Pdf.Devices;         // Resolution struct
using System.Drawing.Imaging;    // ImageFormat enum for GetNextImage

class Program
{
    static void Main()
    {
        // Input PDF file – replace with your actual file path
        const string inputPdf = "sample.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // --------------------------------------------------------------------
        // 1. Convert PDF to HTML with layout and image options (facade: Document.Save)
        // --------------------------------------------------------------------
        const string htmlOutput = "sample_converted.html";
        try
        {
            using (Document pdfDoc = new Document(inputPdf))
            {
                // HtmlSaveOptions lives in Aspose.Pdf namespace (no Aspose.Pdf.Saving)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources (images, CSS) into the single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Save raster images as PNG embedded into SVG (high quality)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Optional: enable multi‑threading for faster conversion
                    IsMultiThreading = true
                };

                // Save the PDF as HTML using the explicit options.
                pdfDoc.Save(htmlOutput, htmlOpts);
                Console.WriteLine($"PDF → HTML saved to '{htmlOutput}'.");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ (Windows only). Handle gracefully on other OSes.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"HTML conversion error: {ex.Message}");
        }

        // --------------------------------------------------------------------
        // 2. Convert each PDF page to PNG images with custom resolution and quality
        //    (facade: PdfConverter)
        // --------------------------------------------------------------------
        const string imagePrefix = "page_";
        const string imageExtension = ".png";
        try
        {
            PdfConverter converter = new PdfConverter();
            converter.BindPdf(inputPdf);          // Bind the source PDF
            converter.DoConvert();                // Prepare for conversion

            // Set desired resolution (dots per inch). Higher = better quality, slower.
            converter.Resolution = new Resolution(300);

            int pageNumber = 1;
            while (converter.HasNextImage())
            {
                string imagePath = $"{imagePrefix}{pageNumber}{imageExtension}";
                // Save as PNG – ImageFormat comes from System.Drawing.Imaging
                converter.GetNextImage(imagePath, ImageFormat.Png);
                pageNumber++;
            }

            converter.Close(); // Release resources
            Console.WriteLine($"PDF pages converted to PNG images (prefix '{imagePrefix}*{imageExtension}').");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Image conversion error: {ex.Message}");
        }

        // --------------------------------------------------------------------
        // 3. Convert PDF to DOCX with layout options (facade: Document.Save)
        // --------------------------------------------------------------------
        const string docxOutput = "sample_converted.docx";
        try
        {
            using (Document pdfDoc = new Document(inputPdf))
            {
                // DocSaveOptions lives in Aspose.Pdf namespace
                DocSaveOptions docOpts = new DocSaveOptions
                {
                    // Choose DOCX format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use Flow mode for better layout detection
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Enable bullet detection (useful for lists)
                    RecognizeBullets = true,
                    // Optional: set image resolution for embedded images
                    ImageResolutionX = 150,
                    ImageResolutionY = 150,
                    // Optional: enable multi‑threading
                    IsMultiThreading = true
                };

                pdfDoc.Save(docxOutput, docOpts);
                Console.WriteLine($"PDF → DOCX saved to '{docxOutput}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"DOCX conversion error: {ex.Message}");
        }

        // --------------------------------------------------------------------
        // 4. Convert PDF to another PDF format (e.g., PDF/A‑2B) using PdfFormatConversionOptions
        // --------------------------------------------------------------------
        const string pdfaOutput = "sample_converted_pdfa.pdf";
        try
        {
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Create conversion options for PDF/A‑2B with default error handling
                PdfFormatConversionOptions convOpts = new PdfFormatConversionOptions(PdfFormat.PDF_A_2B)
                {
                    // Optimize file size during conversion
                    OptimizeFileSize = true,
                    // Align text to avoid overlapping after conversion
                    AlignText = true
                };

                // Perform conversion; returns true on success
                bool success = pdfDoc.Convert(convOpts);
                if (success)
                {
                    pdfDoc.Save(pdfaOutput);
                    Console.WriteLine($"PDF → PDF/A-2B saved to '{pdfaOutput}'.");
                }
                else
                {
                    Console.WriteLine("PDF/A conversion reported failure.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"PDF/A conversion error: {ex.Message}");
        }
    }
}
