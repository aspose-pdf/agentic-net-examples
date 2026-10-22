using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to work with
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // HTML fragment with styled markup
            string html = "<div style='font-family:Arial; font-size:14pt; color:#0066CC;'><b>Welcome</b> to <i>Aspose.Pdf</i>!</div>";
            HtmlFragment htmlFragment = new HtmlFragment(html);

            // Optional TextState customization
            htmlFragment.TextState.Font = FontRepository.FindFont("Arial");
            htmlFragment.TextState.FontSize = 14;
            htmlFragment.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0, 102, 204); // matches #0066CC

            // Add the HTML fragment to the page's paragraph collection
            page.Paragraphs.Add(htmlFragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"HTML fragment added and saved to '{outputPath}'.");
    }
}