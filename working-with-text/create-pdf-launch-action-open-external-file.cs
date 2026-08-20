using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF file
        const string outputPdf = "launch_action.pdf";
        // External file to be opened when the text is clicked
        const string externalFile = "example.txt";

        // Optional: create the external file for demonstration purposes
        // File.WriteAllText(externalFile, "Sample content for the external file.");

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Add visible text that the user will click
            TextFragment text = new TextFragment("Click here to open the external file");
            text.Position = new Position(100, 700); // place near top-left
            text.TextState.FontSize = 14;
            text.TextState.ForegroundColor = Aspose.Pdf.Color.Blue; // make it look like a link
            page.Paragraphs.Add(text);

            // Define a rectangle that roughly covers the displayed text
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 680, 350, 720);

            // Create a link annotation and assign a LaunchAction to open the external file
            LinkAnnotation link = new LinkAnnotation(page, linkRect);
            link.Action = new LaunchAction(externalFile);
            // Make the annotation border invisible
            link.Color = Aspose.Pdf.Color.Transparent;

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with launch action saved to '{outputPdf}'.");
    }
}