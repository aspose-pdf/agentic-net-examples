using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_lastpage_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);
        // Get the last page (pages are 1‑based)
        Page lastPage = doc.Pages[doc.Pages.Count];

        // Prepare footer text (current date in MM-dd-yyyy format)
        string footerText = DateTime.Now.ToString("MM-dd-yyyy");

        // Create a text stamp for the footer
        TextStamp stamp = new TextStamp(footerText);
        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
        stamp.TextState.FontSize = 12;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
        stamp.HorizontalAlignment = HorizontalAlignment.Center;
        stamp.VerticalAlignment = VerticalAlignment.Bottom;
        stamp.YIndent = 10; // distance from the bottom edge

        // Add the stamp only to the last page
        lastPage.AddStamp(stamp);

        // Save the modified PDF
        doc.Save(outputPath);
        Console.WriteLine($"Footer stamp added to last page. Output saved to '{outputPath}'.");
    }
}
