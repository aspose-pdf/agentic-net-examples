using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextFragment, Heading, Position, FontRepository

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Dynamic data for the heading (e.g., current date and user name)
        string userName = Environment.UserName;
        string headingText = $"Report generated for {userName} on {DateTime.Now:yyyy-MM-dd}";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, add a heading, and save
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Heading element (level 1) and set its properties
            Heading heading = new Heading(1);
            heading.Text = headingText;                     // dynamic heading text
            heading.Position = new Position(50, 800);       // place near top‑left
            heading.TextState.Font = FontRepository.FindFont("Helvetica");
            heading.TextState.FontSize = 24;                // larger font for heading
            heading.TextState.ForegroundColor = Color.Blue; // optional color

            // Add the heading to the page's paragraph collection
            page.Paragraphs.Add(heading);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with dynamic heading: {outputPath}");
    }
}
