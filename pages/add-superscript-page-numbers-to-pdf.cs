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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create a stamp that prints the page number.
            PageNumberStamp stamp = new PageNumberStamp();

            // Superscript‑like appearance.
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 8; // smaller than body text
            stamp.YIndent = 4;            // move upward a little

            // Position at bottom‑right.
            stamp.HorizontalAlignment = HorizontalAlignment.Right;
            stamp.VerticalAlignment = VerticalAlignment.Bottom;

            // Set margins – PageNumberStamp uses BottomMargin and RightMargin properties
            // (MarginInfo is not available on this stamp type).
            stamp.BottomMargin = 20;
            stamp.RightMargin = 20;

            // Apply the stamp to every page.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers with superscript‑like formatting saved to '{outputPath}'.");
    }
}
