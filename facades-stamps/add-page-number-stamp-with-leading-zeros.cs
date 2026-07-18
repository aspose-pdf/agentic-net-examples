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

        // ---------------------------------------------------------------------
        // Ensure an input PDF exists for the sandbox. Create a minimal document
        // if the file is not present. This satisfies the "hardcoded-input-file-
        // generate-inline-first" rule.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                // Add a single blank page (optionally add some content).
                Page page = seed.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample page"));
                seed.Save(inputPath);
            }
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Define common stamp settings
        Font font = FontRepository.FindFont("Helvetica");
        float fontSize = 12f;
        Aspose.Pdf.Color fontColor = Aspose.Pdf.Color.Black;
        HorizontalAlignment hAlign = HorizontalAlignment.Center;
        VerticalAlignment vAlign = VerticalAlignment.Bottom;
        float yIndent = 20f; // distance from the bottom edge

        // Add a page‑number stamp with leading zeros to every page
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            // Format the page number with leading zeros (e.g., 001, 002, ...)
            string pageNumber = i.ToString("D3"); // adjust the digit count as needed

            TextStamp stamp = new TextStamp(pageNumber);
            stamp.TextState.Font = font;
            stamp.TextState.FontSize = fontSize;
            stamp.TextState.ForegroundColor = fontColor;
            stamp.HorizontalAlignment = hAlign;
            stamp.VerticalAlignment = vAlign;
            stamp.YIndent = yIndent;

            doc.Pages[i].AddStamp(stamp);
        }

        // Save the stamped PDF
        doc.Save(outputPath);
    }
}
