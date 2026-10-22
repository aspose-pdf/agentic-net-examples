using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load PDF and ensure deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                int pageCount = doc.Pages.Count;

                // Pages are 1‑based
                for (int i = 1; i <= pageCount; i++)
                {
                    Page page = doc.Pages[i];

                    // Create a footer with page number placeholder
                    TextFragment footer = new TextFragment($"Page {i} of {pageCount}");
                    footer.TextState.FontSize = 10;
                    footer.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                    footer.TextState.HorizontalAlignment = HorizontalAlignment.Center;

                    // Position near bottom center of the page
                    double x = page.PageInfo.Width / 2;
                    double y = 30; // 30 units from bottom edge
                    footer.Position = new Position(x, y);

                    // Add footer to the page
                    page.Paragraphs.Add(footer);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Footer added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}