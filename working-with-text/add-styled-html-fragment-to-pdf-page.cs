using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for TextState and FontRepository

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // HTML fragment with styled markup
            string html = "<p style=\"color:#FF0000; font-size:16pt; font-family:Helvetica;\">Hello <b>World</b>!</p>";
            HtmlFragment htmlFragment = new HtmlFragment(html);

            // Optional: set a default TextState (applies when HTML does not specify styling)
            htmlFragment.TextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Color.Blue
            };

            // Add the fragment to the first page (pages are 1‑based)
            Page page = doc.Pages[1];
            page.Paragraphs.Add(htmlFragment);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}