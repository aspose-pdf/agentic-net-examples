using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment that will hold the segment
            TextFragment fragment = new TextFragment("Click here:");
            fragment.Position = new Position(100, 600);
            fragment.TextState.FontSize = 12;
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Create a new text segment
            TextSegment segment = new TextSegment(" Aspose.Pdf");
            // Assign a web hyperlink to the segment
            segment.Hyperlink = new WebHyperlink("https://www.aspose.com/pdf");

            // Add the segment to the fragment's segment collection
            fragment.Segments.Add(segment);

            // Append the fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlinked PDF saved to '{outputPath}'.");
    }
}