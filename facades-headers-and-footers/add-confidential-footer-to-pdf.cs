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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        var document = new Document(inputPath);

        // Create a TextStamp for the footer
        var footerStamp = new TextStamp("Company Confidential");
        footerStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        footerStamp.TextState.FontSize = 12f;
        footerStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
        footerStamp.HorizontalAlignment = HorizontalAlignment.Center;
        footerStamp.VerticalAlignment = VerticalAlignment.Bottom;
        footerStamp.YIndent = 20f; // 20 points from the bottom edge

        // Add the stamp to every page
        foreach (Page page in document.Pages)
        {
            page.AddStamp(footerStamp);
        }

        // Save the modified PDF
        document.Save(outputPath);

        Console.WriteLine($"Footer added and saved to '{outputPath}'.");
    }
}
