using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_header_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the header text (same for all pages)
            const string headerContent = "Custom Header Text";

            // Iterate over each page and add header/footer directly.
            foreach (Page page in doc.Pages)
            {
                // ----- Header -----
                TextFragment header = new TextFragment(headerContent)
                {
                    Position = new Position(page.PageInfo.Width / 2, page.PageInfo.Height - 20),
                    TextState =
                    {
                        FontSize = 12,
                        Font = FontRepository.FindFont("Helvetica"),
                        ForegroundColor = Aspose.Pdf.Color.Gray,
                        HorizontalAlignment = HorizontalAlignment.Center
                    }
                };

                // ----- Footer -----
                TextFragment footer = new TextFragment($"Page {page.Number}")
                {
                    Position = new Position(page.PageInfo.Width / 2, 20),
                    TextState =
                    {
                        FontSize = 12,
                        Font = FontRepository.FindFont("Helvetica"),
                        ForegroundColor = Aspose.Pdf.Color.Gray,
                        HorizontalAlignment = HorizontalAlignment.Center
                    }
                };

                // Append the fragments to the page. Using TextBuilder ensures the fragments are placed
                // without disturbing existing page content.
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(header);
                builder.AppendText(footer);
            }

            // Save the modified PDF. No SaveOptions are needed because the output format is PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header and footer added. Output saved to '{outputPath}'.");
    }
}
