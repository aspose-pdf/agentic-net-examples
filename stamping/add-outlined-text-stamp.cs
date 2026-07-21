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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired value
            TextStamp stamp = new TextStamp("Outlined Text");

            // Configure the text appearance – use a bold font to simulate an outline
            stamp.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            stamp.TextState.FontSize = 48;
            stamp.TextState.ForegroundColor = Color.Red; // optional fill color
            // NOTE: TextRenderingMode is not available in this version of Aspose.Pdf.
            // The outlined effect can be approximated by using a bold font.

            // Draw the stamp as graphic operators (required for rendering modes)
            stamp.Draw = true;

            // Position the stamp (centered on the page)
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Center;

            // Add the stamp to the first page (or iterate pages as needed)
            doc.Pages[1].AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
