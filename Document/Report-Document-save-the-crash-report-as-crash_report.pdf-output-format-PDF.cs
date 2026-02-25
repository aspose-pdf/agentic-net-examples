using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "crash_report.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Prepare the crash report text
            string reportText = "Crash Report\n\n" +
                                "Details:\n" +
                                "- Error: NullReferenceException\n" +
                                "- StackTrace: ...\n" +
                                "- Timestamp: " + DateTime.Now.ToString("u");

            // Create a TextFragment with the report content
            TextFragment fragment = new TextFragment(reportText);
            fragment.TextState.FontSize = 12;
            fragment.TextState.Font = FontRepository.FindFont("Arial"); // fallback to a common font
            fragment.Position = new Position(50, 750); // Position near the top-left corner

            // Append the text fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Crash report saved to '{outputPath}'.");
    }
}