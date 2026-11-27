using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution
using Aspose.Pdf.Text;   // <-- added to resolve TextFragment

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Directory where PNG images will be saved
        const string outputDir = "PagesAsPng";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Create a minimal placeholder PDF if the expected file does not exist.
        // This makes the example self‑contained and prevents a FileNotFoundException at runtime.
        if (!File.Exists(pdfPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            // Optionally add some simple content so the pages are not blank.
            placeholder.Pages[1].Paragraphs.Add(new TextFragment("Placeholder page for conversion"));
            placeholder.Save(pdfPath);
        }

        // Pages that should be converted (1‑based indexing)
        int[] pagesToConvert = { 2, 4, 5 };

        // PdfConverter implements IDisposable, so wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF document
            converter.BindPdf(pdfPath);

            // Set the desired resolution BEFORE setting StartPage/EndPage
            // Resolution is a struct from Aspose.Pdf.Devices
            converter.Resolution = new Resolution(300);

            // Loop through the requested pages and convert each to PNG
            foreach (int pageNumber in pagesToConvert)
            {
                // Validate page number against the document bounds
                if (pageNumber < 1 || pageNumber > converter.PageCount)
                {
                    Console.WriteLine($"Page {pageNumber} is out of range and will be skipped.");
                    continue;
                }

                // Define the range for the current page (single‑page conversion)
                converter.StartPage = pageNumber;
                converter.EndPage   = pageNumber;

                // Prepare the converter for the defined range
                converter.DoConvert();

                string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.png");

                // Save the current page as PNG. GetNextImage infers the format from the file extension.
                converter.GetNextImage(outPath);

                Console.WriteLine($"Page {pageNumber} saved as PNG to '{outPath}'.");
            }

            // Release all resources held by the converter
            converter.Close();
        }
    }
}
