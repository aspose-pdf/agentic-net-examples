using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;          // needed for Heading (inherits TextFragment)

class Program
{
    static void Main()
    {
        // Input PDF path (must exist)
        const string inputPath  = "input.pdf";
        // Output PDF path
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Dynamic values for the heading
        string userName = Environment.UserName;
        string today    = DateTime.Now.ToString("MMMM dd, yyyy");

        // Open the PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // OPTIONAL: set the document title (PDF metadata)
            doc.SetTitle($"Report for {userName} - {today}");

            // Create a heading (level 1) and set its text dynamically
            Heading heading = new Heading(1)               // level 1 heading
            {
                Text = $"Report generated for {userName} on {today}",
                // Position the heading near the top of the page (x=50, y=750)
                Position = new Position(50, 750),
                // Use a larger font size for visibility
                TextState = { FontSize = 20, Font = FontRepository.FindFont("Helvetica") },
                // Center the heading horizontally
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Add the heading to the first page
            Page firstPage = doc.Pages[1];                  // 1‑based indexing
            firstPage.Paragraphs.Add(heading);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with dynamic heading to '{outputPath}'.");
    }
}