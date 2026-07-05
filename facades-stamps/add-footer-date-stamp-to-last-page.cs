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

        // Load the PDF document
        Document doc = new Document(inputPath);
        int lastPageNumber = doc.Pages.Count; // 1‑based index of the last page

        // Create a text stamp containing the current date (MM-dd-yyyy)
        string dateText = DateTime.Now.ToString("MM-dd-yyyy");
        TextStamp stamp = new TextStamp(dateText);
        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
        stamp.TextState.FontSize = 12;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

        // Position the stamp at the bottom‑center of the page
        stamp.HorizontalAlignment = HorizontalAlignment.Center;
        stamp.VerticalAlignment = VerticalAlignment.Bottom;
        stamp.YIndent = 10; // distance from the bottom edge in points

        // Apply the stamp only to the last page
        doc.Pages[lastPageNumber].AddStamp(stamp);

        // Save the modified PDF
        doc.Save(outputPath);
        Console.WriteLine($"Footer with date added to page {lastPageNumber} and saved as '{outputPath}'.");
    }
}
