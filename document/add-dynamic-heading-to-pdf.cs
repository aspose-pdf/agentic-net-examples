using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for Position, FontRepository, etc.

class Program
{
    static void Main()
    {
        // Input PDF (could be a blank template or existing document)
        const string inputPath  = "template.pdf";
        // Output PDF with dynamic heading
        const string outputPath = "output.pdf";

        // Example dynamic heading: current date and user name
        string headingText = $"Report generated on {DateTime.Now:MMMM dd, yyyy} – User: {Environment.UserName}";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load, modify, and save the PDF using proper disposal pattern
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a level‑1 heading (the constructor argument is the heading level)
            Heading heading = new Heading(1)
            {
                Text = headingText,
                // Position the heading near the top of the page
                Position = new Position(50, 800),
                // Center the heading horizontally
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Set visual style using TextState (avoid System.Drawing.Color)
            heading.TextState.Font = FontRepository.FindFont("Helvetica");
            heading.TextState.FontSize = 18;
            heading.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the heading to the page's content
            page.Paragraphs.Add(heading);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with dynamic heading to '{outputPath}'.");
    }
}