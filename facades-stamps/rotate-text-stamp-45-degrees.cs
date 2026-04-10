using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // for Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_stamp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);
        Page firstPage = doc.Pages[1]; // 1‑based index

        // Create a text stamp
        TextStamp stamp = new TextStamp("Rotated Text");

        // Configure text appearance
        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
        stamp.TextState.FontSize = 24;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

        // Rotate 45 degrees around its centre
        stamp.RotateAngle = 45.0; // <-- correct property for rotation

        // Position the stamp at the centre of the page using XIndent/YIndent (SetOrigin is obsolete)
        float pageWidth = (float)firstPage.PageInfo.Width;   // cast double → float
        float pageHeight = (float)firstPage.PageInfo.Height; // cast double → float
        stamp.XIndent = pageWidth / 2f;
        stamp.YIndent = pageHeight / 2f;

        // Add the stamp to the page
        firstPage.AddStamp(stamp);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Stamp with 45° rotation applied and saved to '{outputPath}'.");
    }
}
