using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPath = "launch_action.pdf";
        const string fileToOpen = @"C:\Temp\example.txt";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a text fragment that will act as the clickable label
            TextFragment tf = new TextFragment("Click here to open the file");
            // Position the text on the page (coordinates are in points)
            tf.Position = new Position(100, 700);
            // Optionally set visual style
            tf.TextState.FontSize = 14;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            // Add the text fragment to the page
            page.Paragraphs.Add(tf);

            // Create a link annotation covering the text rectangle
            // The TextFragment's Rectangle property provides the bounds after layout
            LinkAnnotation link = new LinkAnnotation(page, tf.Rectangle);
            // Assign a LaunchAction that opens the external file when the link is clicked
            link.Action = new LaunchAction(fileToOpen);
            // Optional visual cue for the link (blue underline)
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link) { Width = 0 };
            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with launch action saved to '{outputPath}'.");
    }
}