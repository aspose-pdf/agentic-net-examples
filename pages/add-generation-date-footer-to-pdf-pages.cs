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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Prepare the footer text (generation date)
            string footerText = $"Generated on {DateTime.Now:yyyy-MM-dd}";

            // Iterate over all pages (1‑based indexing rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextFragment with the footer text
                TextFragment tf = new TextFragment(footerText);

                // Position the footer near the bottom of the page
                // Adjust X/Y as needed; here we center it horizontally
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;
                double marginBottom = 20; // points from bottom edge

                // Center horizontally
                tf.Position = new Position(pageWidth / 2, marginBottom);

                // Center alignment requires setting the text alignment
                tf.TextState.HorizontalAlignment = HorizontalAlignment.Center;

                // Optional styling
                tf.TextState.FontSize = 9;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                // Append the text fragment to the page using TextBuilder
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(tf);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with footer: '{outputPath}'");
    }
}