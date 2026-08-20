using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";   // existing PDF or blank file
        const string outputPdf = "output.pdf";

        // Ensure the input file exists; if not, create a new empty PDF
        if (!File.Exists(inputPdf))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // add a blank page
                doc.Save(inputPdf);
            }
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextFragment that will hold the segment
            TextFragment tf = new TextFragment();
            tf.Position = new Position(100, 600); // place the text on the page

            // Create a TextSegment with the visible text
            TextSegment segment = new TextSegment("Click here to visit Aspose");
            // Assign a web hyperlink to the segment (URI action)
            segment.Hyperlink = new WebHyperlink("https://www.aspose.com");

            // Optionally set visual style for the segment
            segment.TextState.Font = FontRepository.FindFont("Helvetica");
            segment.TextState.FontSize = 12;
            segment.TextState.ForegroundColor = Color.Blue;
            segment.TextState.Underline = true;

            // Add the segment to the fragment's segment collection
            tf.Segments.Add(segment);

            // Append the fragment (with the clickable segment) to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with clickable text saved to '{outputPdf}'.");
    }
}