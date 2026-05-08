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

        // Load the source PDF
        Document doc = new Document(inputPath);

        // Retrieve the document creation date and format it as yyyy-MM-dd
        DateTime creationDate = doc.Info.CreationDate;
        string dateText = creationDate.ToString("yyyy-MM-dd");

        // Create a text stamp that will be placed at the top‑left corner
        TextStamp stamp = new TextStamp(dateText);
        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
        stamp.TextState.FontSize = 12;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black; // fully‑qualified Aspose color
        stamp.HorizontalAlignment = HorizontalAlignment.Left;
        stamp.VerticalAlignment = VerticalAlignment.Top;
        stamp.XIndent = 10f; // margin from the left edge
        stamp.YIndent = 10f; // margin from the top edge

        // Apply the stamp to every page (or change to a specific page if needed)
        foreach (Page page in doc.Pages)
        {
            page.AddStamp(stamp);
        }

        // Save the stamped PDF
        doc.Save(outputPath);
        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
