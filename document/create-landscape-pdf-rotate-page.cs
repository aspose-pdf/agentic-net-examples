using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "landscape.pdf";

        // Ensure the output directory exists (if any)
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Rotate the page 90 degrees to achieve landscape orientation
            page.Rotate = Rotation.on90; // Correct enum value

            // Optional: add some sample text to visualize the orientation
            TextFragment tf = new TextFragment("Landscape Page");
            tf.Position = new Position(100, 500); // Position is relative to the unrotated page
            page.Paragraphs.Add(tf);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
