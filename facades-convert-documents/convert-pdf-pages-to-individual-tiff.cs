using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfConverter
using Aspose.Pdf;          // Document (for sample PDF creation)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";                 // source PDF
        const string outputDir = "output_tiff";               // folder for TIFF pages

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // If the input PDF does not exist, create a simple one so the demo can run.
        if (!File.Exists(inputPdf))
        {
            CreateSamplePdf(inputPdf);
            Console.WriteLine($"Sample PDF created at '{inputPdf}'.");
        }

        // PdfConverter implements IDisposable, so use a using block for deterministic cleanup
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);

            // Prepare the converter for image extraction
            converter.DoConvert();

            // After DoConvert() the PageCount property is populated
            int pageCount = converter.PageCount;

            for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
            {
                string tiffPath = Path.Combine(outputDir, $"page_{pageIndex}.tiff");

                // Aspose.Pdf does not support ImageFormat.Tiff via GetNextImage.
                // Use the dedicated SaveAsTIFF overload that creates a TIFF for the specified page range.
                // Supplying the same start and end page creates a single‑page TIFF.
                converter.SaveAsTIFF(tiffPath, pageIndex, pageIndex);
            }
        }

        Console.WriteLine("PDF pages have been saved as individual TIFF files.");
    }

    /// <summary>
    /// Creates a very simple PDF with a single page containing the text "Sample PDF".
    /// This helper is used only when the expected input file is missing, allowing the
    /// sample code to run without external resources.
    /// </summary>
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            // Add a page and a paragraph with some text.
            var page = doc.Pages.Add();
            var paragraph = new Aspose.Pdf.Text.TextFragment("Sample PDF – generated for TIFF conversion demo.");
            page.Paragraphs.Add(paragraph);
            doc.Save(path);
        }
    }
}
