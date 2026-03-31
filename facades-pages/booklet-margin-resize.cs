using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string resizedPath = "resized.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Load the PDF and increase margins by 15%
        using (Document document = new Document(inputPath))
        {
            foreach (Page page in document.Pages)
            {
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;
                float marginHorizontal = (float)(pageWidth * 0.15);
                float marginVertical = (float)(pageHeight * 0.15);
                // Use MarginInfo (the correct class) to set margins
                page.PageInfo.Margin = new MarginInfo(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
            }

            document.Save(resizedPath);
        }

        // Create booklet from the resized PDF
        try
        {
            PdfFileEditor editor = new PdfFileEditor();
            // MakeBooklet returns void, so we just call it
            editor.MakeBooklet(resizedPath, outputPath);
            Console.WriteLine("Booklet created successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Failed to create booklet: " + ex.Message);
        }

        // Clean up temporary file
        try
        {
            File.Delete(resizedPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Could not delete temporary file: " + ex.Message);
        }
    }
}
