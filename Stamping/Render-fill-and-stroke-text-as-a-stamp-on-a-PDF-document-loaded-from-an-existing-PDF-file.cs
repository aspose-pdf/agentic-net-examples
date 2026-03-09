using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;   // required for Color and other drawing utilities

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a TextStamp with the desired text
            TextStamp stamp = new TextStamp("Sample Text");

            // Render the stamp as text (not as graphic operators)
            stamp.Draw = false;

            // Configure the text rendering mode to fill and stroke
            stamp.TextState.RenderingMode = TextRenderingMode.FillThenStrokeText;

            // Optional visual settings
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 48;
            stamp.TextState.ForegroundColor = Color.Blue;   // Fill color
            // NOTE: StrokeColor and StrokeWidth are only available in Aspose.Pdf 23.9+.
            // If you are using an earlier version, they are not part of the API.
            // In that case the stroke will use the same color as the fill.
            // Uncomment the lines below when using a compatible version:
            // stamp.TextState.StrokeColor = Color.Red;        // Stroke color
            // stamp.TextState.StrokeWidth = 1.5f;             // Stroke width

            // Position the stamp (example: centered on each page)
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamp applied and saved to '{outputPdf}'.");
    }
}
