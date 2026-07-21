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

        // Load the document
        Document doc = new Document(inputPath);
        // Get the last page (1‑based index)
        Page lastPage = doc.Pages[doc.Pages.Count];

        // Prepare the footer text with the current date (MM-dd-yyyy)
        string dateText = DateTime.Now.ToString("MM-dd-yyyy");

        // Create a TextStamp for the footer
        TextStamp stamp = new TextStamp(dateText);
        // Set font, size and color via TextState
        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
        stamp.TextState.FontSize = 12;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
        // Position the stamp at the bottom centre of the page
        stamp.HorizontalAlignment = HorizontalAlignment.Center;
        stamp.VerticalAlignment = VerticalAlignment.Bottom;
        // Distance from the bottom edge (you can adjust as needed)
        stamp.YIndent = 10f;

        // Add the stamp only to the last page
        lastPage.AddStamp(stamp);

        // Save the modified PDF
        doc.Save(outputPath);
        // Document.Close() does not exist in Aspose.Pdf; the object is disposed by GC or a using block.

        Console.WriteLine($"Footer with date added to page {lastPage.Number}, saved as '{outputPath}'.");
    }
}
