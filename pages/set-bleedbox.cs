using System;
using System.IO;
using Aspose.Pdf;

namespace Example
{
    class SetBleedBox
    {
        static void Main(string[] args)
        {
            // Resolve the input PDF path (relative to the executable folder)
            string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.pdf");
            string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.pdf");

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Load the source PDF file
            using (Document document = new Document(inputPath))
            {
                // Process each page in the document
                for (int pageNumber = 1; pageNumber <= document.Pages.Count; pageNumber++)
                {
                    Page page = document.Pages[pageNumber];

                    // Retrieve the current CropBox; if not set, fall back to the page rectangle
                    Rectangle cropBox = page.CropBox ?? page.Rect;

                    // Extend the box by 5 points on each side to create the BleedBox
                    double left   = cropBox.LLX - 5.0;
                    double bottom = cropBox.LLY - 5.0;
                    double right  = cropBox.URX + 5.0; // URX = LLX + Width
                    double top    = cropBox.URY + 5.0; // URY = LLY + Height

                    // Aspose.Pdf.Rectangle constructor expects lower‑left and upper‑right coordinates
                    Rectangle bleedBox = new Rectangle(left, bottom, right, top);
                    page.BleedBox = bleedBox;
                }

                // Save the modified PDF
                document.Save(outputPath);
                Console.WriteLine($"BleedBox applied and saved to: {outputPath}");
            }
        }
    }
}
