using System;
using System.Drawing; // for System.Drawing.Color
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

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Define the three header lines with their colors and vertical offsets
        var headerLines = new[]
        {
            new { Text = "First Header Line",  Color = System.Drawing.Color.Blue,   YOffset = 20f },
            new { Text = "Second Header Line", Color = System.Drawing.Color.Red,    YOffset = 45f },
            new { Text = "Third Header Line",  Color = System.Drawing.Color.Green,  YOffset = 70f }
        };

        // Add a TextStamp for each line on every page
        foreach (Page page in pdfDoc.Pages)
        {
            foreach (var line in headerLines)
            {
                TextStamp stamp = new TextStamp(line.Text);
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 24;
                // Convert System.Drawing.Color to Aspose.Pdf.Color
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(line.Color.R, line.Color.G, line.Color.B);
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment = VerticalAlignment.Top;
                stamp.YIndent = line.YOffset; // distance from the top edge
                page.AddStamp(stamp);
            }
        }

        // Save the modified PDF
        pdfDoc.Save(outputPath);
        Console.WriteLine($"Multi‑line header stamp created at '{outputPath}'.");
    }
}
