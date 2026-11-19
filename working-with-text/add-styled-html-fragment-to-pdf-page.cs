using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_html.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create an HtmlFragment with styled markup
            string html = "<p style='color:#FF0000; font-size:14pt; font-family:Arial;'>" +
                          "Hello <b>World</b> from <i>HtmlFragment</i>!" +
                          "</p>";
            HtmlFragment htmlFragment = new HtmlFragment(html);

            // Optional: customize the default TextState for the fragment
            TextState ts = new TextState
            {
                Font = FontRepository.FindFont("Arial"),
                FontSize = 12,
                ForegroundColor = Color.Black
            };
            htmlFragment.TextState = ts;

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(htmlFragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"HTML fragment added and saved to '{outputPath}'.");
    }
}