using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Create a minimal PDF if it does not already exist in the sandbox.
        if (!System.IO.File.Exists(inputPdf))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add(); // at least one blank page
                seed.Save(inputPdf);
            }
        }

        // Load the source PDF document
        Document doc = new Document(inputPdf);

        // Define the header lines (text, color, vertical offset from the top)
        var headerLines = new (string Text, Aspose.Pdf.Color Color, float YIndent)[]
        {
            ("First Header Line", Aspose.Pdf.Color.Blue, 20f),
            ("Second Header Line", Aspose.Pdf.Color.Green, 40f),
            ("Third Header Line", Aspose.Pdf.Color.Red, 60f)
        };

        // Apply the header lines to every page
        foreach (Page page in doc.Pages)
        {
            foreach (var line in headerLines)
            {
                TextStamp stamp = new TextStamp(line.Text);
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 24;
                stamp.TextState.ForegroundColor = line.Color;
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment = VerticalAlignment.Top;
                stamp.YIndent = line.YIndent; // distance from the top edge
                page.AddStamp(stamp);
            }
        }

        // Save the stamped PDF
        doc.Save(outputPdf);

        Console.WriteLine($"Multi‑line header stamp applied and saved to '{outputPdf}'.");
    }
}