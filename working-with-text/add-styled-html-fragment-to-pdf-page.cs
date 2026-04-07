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

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF (document disposal handled by using)
            using (Document doc = new Document(inputPath))
            {
                // Access the first page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages[1];

                // Define an HTML fragment with inline styling
                string html = "<p style=\"font-family:Helvetica; font-size:14pt; color:#FF0000;\">Hello <b>World</b>!</p>";
                HtmlFragment htmlFragment = new HtmlFragment(html);

                // Optional: override styling via TextState (font, size, color)
                htmlFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                htmlFragment.TextState.FontSize = 14;
                htmlFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

                // Add the HTML fragment to the page's paragraph collection
                page.Paragraphs.Add(htmlFragment);

                // Save the modified PDF (no SaveOptions needed for PDF output)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}