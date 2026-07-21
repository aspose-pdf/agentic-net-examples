using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class LaunchActionExample
{
    static void Main()
    {
        // Output PDF path
        const string outputPdf = "LaunchActionDemo.pdf";

        // Path to the external file that will be opened when the text is clicked
        const string externalFile = @"C:\Temp\example.txt";

        // Ensure the external file exists for demonstration purposes
        if (!File.Exists(externalFile))
        {
            File.WriteAllText(externalFile, "This is a sample external file.");
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle that will act as the clickable area
            // (llx, lly, urx, ury) – coordinates are in points (1/72 inch)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the LinkAnnotation first
            LinkAnnotation link = new LinkAnnotation(page, linkRect);
            // Set visual appearance
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link) { Width = 1 };
            // Assign the LaunchAction – this opens the external file
            link.Action = new LaunchAction(externalFile);
            // Add the annotation to the page
            page.Annotations.Add(link);

            // Add visible text inside the rectangle so the user knows what to click
            TextFragment tf = new TextFragment("Open external file")
            {
                // Position the text roughly inside the link rectangle
                Position = new Position(110, 520),
                // Optional styling
                TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.Blue }
            };
            page.Paragraphs.Add(tf);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'. Click the text to launch '{externalFile}'.");
    }
}
