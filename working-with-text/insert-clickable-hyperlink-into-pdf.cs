using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";   // existing PDF (can be empty or a template)
        const string outputPath = "hyperlink_output.pdf";

        // Ensure the input file exists; if not, create a blank document
        if (!File.Exists(inputPath))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add(); // add a single blank page
                blank.Save(inputPath);
            }
        }

        // Load the PDF, modify it, and save
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextFragment that will hold the text
            TextFragment tf = new TextFragment("Click here: ");
            tf.Position = new Position(100, 600); // place near top-left

            // Create a TextSegment for the hyperlink text
            TextSegment linkSegment = new TextSegment("Aspose.PDF");
            // Assign a WebHyperlink (URI) to the segment
            linkSegment.Hyperlink = new WebHyperlink("https://www.aspose.com/pdf");

            // Optionally style the hyperlink segment (e.g., blue and underlined)
            linkSegment.TextState.FontSize = 12;
            linkSegment.TextState.Font = FontRepository.FindFont("Helvetica");
            linkSegment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            linkSegment.TextState.Underline = true;

            // Add the hyperlink segment to the TextFragment's segment collection
            tf.Segments.Add(linkSegment);

            // Append the TextFragment to the page's paragraphs collection
            page.Paragraphs.Add(tf);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hyperlink saved to '{outputPath}'.");
    }
}