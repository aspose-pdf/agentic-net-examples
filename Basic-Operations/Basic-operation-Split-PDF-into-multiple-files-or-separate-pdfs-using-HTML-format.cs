using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        const string inputPdfPath = "input.pdf";
        const string outputDirectory = "HtmlPages";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure HTML save options – use the current property name "SplitIntoPages"
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                SplitIntoPages = true
            };

            // Save the document. Providing a file name inside the output folder makes Aspose
            // generate one HTML file per page (e.g., page_1.html, page_2.html, …) in that folder.
            string dummyOutputPath = Path.Combine(outputDirectory, "output.html");
            pdfDocument.Save(dummyOutputPath, htmlOptions);

            Console.WriteLine($"PDF has been split into {pdfDocument.Pages.Count} HTML files in folder \"{outputDirectory}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
