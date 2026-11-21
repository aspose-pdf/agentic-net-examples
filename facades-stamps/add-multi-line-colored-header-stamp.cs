using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdf = new Document(inputPath);

        // ----- First header line (red) -----
        TextStamp line1 = new TextStamp("First Header Line")
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Top,
            YIndent = 20 // distance from the top edge
        };
        line1.TextState.Font = FontRepository.FindFont("Arial");
        line1.TextState.FontSize = 14;
        line1.TextState.ForegroundColor = Color.FromRgb(255, 0, 0); // Red

        // ----- Second header line (green) -----
        TextStamp line2 = new TextStamp("Second Header Line")
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Top,
            YIndent = 40 // placed below the first line
        };
        line2.TextState.Font = FontRepository.FindFont("Arial");
        line2.TextState.FontSize = 14;
        line2.TextState.ForegroundColor = Color.FromRgb(0, 153, 0); // Green

        // Add both stamps to every page of the document.
        foreach (Page page in pdf.Pages)
        {
            page.AddStamp(line1);
            page.AddStamp(line2);
        }

        // Save the stamped PDF.
        pdf.Save(outputPath);
        Console.WriteLine($"Multi‑line header stamp applied and saved to '{outputPath}'.");
    }
}
