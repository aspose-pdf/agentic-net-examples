using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Define styled HTML markup
            string html = "<p style='font-family:Helvetica; font-size:14pt; color:#0000FF;'>This is <b>bold</b> and <i>italic</i> text.</p>";

            // Create an HtmlFragment from the markup
            HtmlFragment htmlFragment = new HtmlFragment(html);

            // Optional: customize the fragment's text appearance via TextState
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 14,
                ForegroundColor = Aspose.Pdf.Color.Blue
            };
            htmlFragment.TextState = textState;

            // Add the fragment to the first page (1‑based indexing)
            Page page = doc.Pages[1];
            page.Paragraphs.Add(htmlFragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"HTML fragment added and saved to '{outputPath}'.");
    }
}