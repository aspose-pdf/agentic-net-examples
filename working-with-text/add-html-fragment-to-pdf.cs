using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for FontRepository and TextState

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create an HTML fragment with styled markup
            HtmlFragment htmlFragment = new HtmlFragment("<b>Hello <i>World</i> from <span style=\"color:red;\">HTML</span></b>");

            // Optional: customize the text appearance via TextState
            // FontRepository is in Aspose.Pdf.Text namespace
            htmlFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            htmlFragment.TextState.FontSize = 14;
            htmlFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(htmlFragment);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with HTML fragment saved to '{outputPath}'.");
    }
}