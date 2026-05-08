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

        // Create a text stamp with the desired content
        TextStamp stamp = new TextStamp("Sample Stamp");
        // Configure text appearance
        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
        stamp.TextState.FontSize = 36;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
        // Position the stamp (coordinates are from the lower‑left corner)
        stamp.XIndent = 200f;
        stamp.YIndent = 400f;
        // Rotate the stamp 45 degrees around its centre
        stamp.RotateAngle = 45f;

        // Load the source PDF and apply the stamp to each page (or select pages as needed)
        Document pdfDocument = new Document(inputPath);
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(stamp);
        }

        pdfDocument.Save(outputPath);
        Console.WriteLine($"PDF with rotated text stamp saved to '{outputPath}'.");
    }
}
