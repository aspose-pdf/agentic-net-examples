using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the source template and the resulting PDF
        const string inputPath  = "template.pdf";
        const string outputPath = "output.pdf";

        // Dynamic data that will appear in the heading
        string userName = "John Doe";
        string date     = DateTime.Now.ToString("MMMM dd, yyyy");

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Create a level‑1 heading (Heading(int) where the int is the level)
            Heading heading = new Heading(1);
            heading.Text = $"Report for {userName} – {date}";

            // Optional styling for the heading
            heading.TextState.Font = FontRepository.FindFont("Helvetica");
            heading.TextState.FontSize = 18;
            heading.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the heading to the first page (page indexing is 1‑based)
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(heading);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}