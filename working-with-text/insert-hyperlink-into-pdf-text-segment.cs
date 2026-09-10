using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Base text fragment
            TextFragment tf = new TextFragment("Click here: ");
            tf.Position = new Position(100, 700);
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Text segment that will act as a hyperlink
            TextSegment linkSegment = new TextSegment("Aspose PDF");
            // Assign a WebHyperlink (URI) to the segment
            linkSegment.Hyperlink = new WebHyperlink("https://www.aspose.com/pdf");
            // Optional styling for the hyperlink segment
            linkSegment.TextState.FontSize = 12;
            linkSegment.TextState.Font = FontRepository.FindFont("Helvetica");
            linkSegment.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
            linkSegment.TextState.Underline = true;

            // Add the hyperlink segment to the fragment
            tf.Segments.Add(linkSegment);

            // Append the fragment (with hyperlink) to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}