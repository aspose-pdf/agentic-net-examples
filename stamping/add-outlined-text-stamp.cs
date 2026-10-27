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

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Create a text stamp with the desired text
            Aspose.Pdf.TextStamp textStamp = new Aspose.Pdf.TextStamp("Outlined Text");

            // Position the stamp (XIndent = left offset, YIndent = bottom offset)
            textStamp.XIndent = 100; // distance from the left side of the page
            textStamp.YIndent = 500; // distance from the bottom of the page

            // Configure the visual appearance of the stamp
            // Use a bold font to simulate an outlined effect (StrokeColor / TextRenderingMode are not available)
            textStamp.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            textStamp.TextState.FontSize = 36;
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red; // simulated outline color

            // Add the stamp to the first page (change index to apply to other pages)
            doc.Pages[1].AddStamp(textStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
