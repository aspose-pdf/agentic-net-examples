using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;          // TextFragment, TextSegment, WebHyperlink, TextBuilder

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // existing PDF (can be empty or a template)
        const string outputPdf = "output.pdf";
        const string url       = "https://www.example.com";

        // Ensure the input file exists; if not, create a blank PDF with one page
        if (!File.Exists(inputPdf))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add();               // add a single empty page
                blank.Save(inputPdf);
            }
        }

        // Load the PDF document (lifecycle: create/load)
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextFragment that will hold the clickable text
            TextFragment tf = new TextFragment("Click here");
            tf.Position = new Position(100, 600); // place the text on the page

            // Optionally set visual appearance of the fragment
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            tf.TextState.Underline = true;

            // Create a TextSegment (the actual segment inside the fragment)
            TextSegment segment = new TextSegment("Click here");

            // Assign a hyperlink to the segment using WebHyperlink (Hyperlink property expects a Hyperlink object)
            segment.Hyperlink = new WebHyperlink(url);

            // Add the segment to the fragment's Segments collection
            tf.Segments.Add(segment);

            // Append the fragment (with the linked segment) to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with clickable text saved to '{outputPdf}'.");
    }
}