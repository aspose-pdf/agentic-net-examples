using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for LinkAnnotation, LaunchAction
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string outputPath = "launch_action_example.pdf";
        const string fileToOpen = @"C:\Temp\example.txt"; // external file to launch

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a text fragment
            TextFragment tf = new TextFragment("Click here to open a file");
            tf.Position = new Position(100, 700); // place the text
            tf.TextState.FontSize = 14;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Color.Blue;
            tf.TextState.Underline = true; // visual cue for a link

            // Add the text fragment to the page
            page.Paragraphs.Add(tf);

            // Approximate the rectangle that bounds the text fragment
            double textWidth = tf.TextState.FontSize * tf.Text.Length * 0.5; // rough estimate
            double textHeight = tf.TextState.FontSize;
            // Position.XIndent and Position.YIndent are the correct properties
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(
                tf.Position.XIndent,
                tf.Position.YIndent,
                tf.Position.XIndent + textWidth,
                tf.Position.YIndent + textHeight);

            // Create a LinkAnnotation covering the text
            LinkAnnotation link = new LinkAnnotation(page, linkRect);

            // Assign a LaunchAction that opens the external file
            link.Action = new LaunchAction(fileToOpen);

            // Optional: set link appearance (blue underline already set on text)
            link.Color = Color.Blue;

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with launch action saved to '{outputPath}'.");
    }
}
