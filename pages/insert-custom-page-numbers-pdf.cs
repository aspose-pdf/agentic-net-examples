using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for PageNumber related classes

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "paged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            int totalPages = doc.Pages.Count;

            // Iterate over each page (1‑based indexing)
            for (int i = 1; i <= totalPages; i++)
            {
                Page page = doc.Pages[i];

                // Configure PageNumber to use "/" as delimiter
                PageNumber pageNumber = new PageNumber
                {
                    Delimiter = "/",
                    Index = new PageNumber.PageIndex(),
                    TotalNum = new PageNumber.PageTotalNum()
                };

                // Generate the formatted page number string (e.g., "3/10")
                string formattedNumber = pageNumber.GetPageNumberString(i, totalPages);

                // Create a TextStamp with the formatted page number
                TextStamp stamp = new TextStamp(formattedNumber)
                {
                    // Position the stamp at the bottom‑center of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    BottomMargin        = 20, // distance from the bottom edge
                    // Optional: adjust appearance
                    TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
                };

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}