using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Configure the PageNumber format: current/total (e.g., 1/10)
            PageNumber pageNumberFormat = new PageNumber
            {
                Delimiter = "/",                     // Use '/' as separator
                Index = new PageNumber.PageIndex(),   // Placeholder for current page index
                TotalNum = new PageNumber.PageTotalNum() // Placeholder for total pages
            };

            int totalPages = doc.Pages.Count;

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= totalPages; i++)
            {
                // Generate the formatted page number string for this page
                string pageNumberText = pageNumberFormat.GetPageNumberString(i, totalPages);

                // Create a TextStamp with the generated text
                TextStamp stamp = new TextStamp(pageNumberText)
                {
                    // Position the stamp at the bottom‑right corner
                    BottomMargin = 20,
                    RightMargin = 20,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom
                };

                // Set TextState properties (TextState is read‑only, so we modify the existing instance)
                stamp.TextState.FontSize = 12;
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.ForegroundColor = Color.Black;

                // Apply the stamp to the current page
                doc.Pages[i].AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
