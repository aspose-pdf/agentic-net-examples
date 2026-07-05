using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF (could be a template) and output path
        const string inputPath = "template.pdf";
        const string outputPath = "output.pdf";

        // Dynamic data: user name and current date
        string userName = Environment.UserName;
        string dateStr = DateTime.Now.ToString("MMMM dd, yyyy");

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a level‑1 heading (dynamic text)
            Heading heading = new Heading(1);
            heading.Text = $"Report for {userName} – {dateStr}";

            // Position the heading near the top of the first page
            heading.Position = new Position(50, 750);

            // Set visual style (font, size, color)
            heading.TextState.Font = FontRepository.FindFont("Helvetica");
            heading.TextState.FontSize = 18;
            heading.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;

            // Add the heading to the first page (page indexing is 1‑based)
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(heading);

            // Save the modified PDF (save rule for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}