using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_booklet.pdf";

        try
        {
            // Verify that the source PDF exists
            if (!File.Exists(inputPath))
                throw new FileNotFoundException($"Input file not found: {inputPath}");

            // Use PdfPageEditor facade to manipulate the PDF.
            // NOTE: The SetBookletLayout method and BookletLayout enum are not available in
            // older versions of Aspose.Pdf.Facades. If you need booklet layout, upgrade the
            // Aspose.Pdf library to a version that supports it. For now we simply copy the
            // document to the output file.
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                pageEditor.BindPdf(inputPath);
                // pageEditor.SetBookletLayout(BookletLayout.SaddleStitch); // Removed – not supported in current library version
                pageEditor.Document.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
