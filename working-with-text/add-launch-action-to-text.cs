using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class LaunchActionExample
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        // File to be launched when the text is clicked
        const string launchFile = @"C:\Temp\example.txt";

        // Ensure the input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Choose the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Create a text fragment that will be displayed in the PDF
            TextFragment tf = new TextFragment("Click here to open a file");
            tf.Position = new Position(100, 700); // place the text on the page
            tf.TextState.FontSize = 14;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Color.Blue;
            tf.TextState.Underline = true; // visual cue for a link

            // Add the text fragment to the page
            page.Paragraphs.Add(tf);

            // Determine the rectangle that encloses the text fragment.
            // Approximate width based on font size and character count.
            double textWidth = tf.TextState.FontSize * tf.Text.Length * 0.5; // rough estimate
            double textHeight = tf.TextState.FontSize + 2;
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(
                tf.Position.XIndent,
                tf.Position.YIndent,
                tf.Position.XIndent + textWidth,
                tf.Position.YIndent + textHeight);

            // Create a link annotation covering the text rectangle
            LinkAnnotation link = new LinkAnnotation(page, linkRect);
            // Set visual appearance (optional)
            link.Color = Color.Transparent;
            // Assign a LaunchAction that opens the external file
            link.Action = new LaunchAction(launchFile);
            // Set border after the annotation has been instantiated
            link.Border = new Border(link) { Width = 0 };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with launch action: {outputPdf}");
    }
}
