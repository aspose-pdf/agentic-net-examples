using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and dynamic heading text (e.g., date or user name)
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        string dynamicHeading = $"Report generated on {DateTime.Now:yyyy-MM-dd} by {Environment.UserName}";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add a heading, and save
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // Create a heading (level 1) and set its text dynamically
            Heading heading = new Heading(1) // level 1 heading
            {
                Text = dynamicHeading,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Modify the existing TextState (read‑only property) instead of assigning a new one
            heading.TextState.Font = FontRepository.FindFont("Helvetica");
            heading.TextState.FontSize = 18;
            heading.TextState.ForegroundColor = Color.Blue;

            // Insert the heading at the top of the page
            page.Paragraphs.Insert(1, heading);

            // Optionally set the document title (metadata)
            doc.SetTitle("Dynamic Heading Example");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with dynamic heading to '{outputPath}'.");
    }
}
