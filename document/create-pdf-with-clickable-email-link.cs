using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "email_link.pdf";

        // Ensure the output directory exists
        string outDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Add visible text that will act as the link label
            TextFragment tf = new TextFragment("Contact us via email");
            tf.Position = new Position(100, 700); // Position on the page
            tf.TextState.FontSize = 12;
            page.Paragraphs.Add(tf);

            // Define a rectangle that covers the text (adjust as needed)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 680, 300, 720);

            // Create a link annotation with a mailto: URI action
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                Action = new GoToURIAction("mailto:support@example.com")
            };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with email link saved to '{outputPath}'.");
    }
}
