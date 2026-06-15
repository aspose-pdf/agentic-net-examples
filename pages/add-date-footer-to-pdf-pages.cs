using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a new footer object
                HeaderFooter footer = new HeaderFooter();

                // Optional: set bottom margin for the footer
                footer.Margin = new MarginInfo { Bottom = 20 };

                // Create a text fragment with the current date
                TextFragment dateFragment = new TextFragment(DateTime.Now.ToString("yyyy-MM-dd"));
                // Set text appearance
                dateFragment.TextState.FontSize = 10;
                dateFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                dateFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                // Position the text at the left side of the footer
                dateFragment.Position = new Position(0, 0);

                // Add the text fragment to the footer's paragraph collection
                footer.Paragraphs.Add(dateFragment);

                // Assign the footer to the current page
                page.Footer = footer;
            }

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with date footer: {outputPath}");
    }
}