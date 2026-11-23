using System;
using System.IO;
using System.Text;
using System.Drawing; // for System.Drawing.Color used in DefaultAppearance
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_summary.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Build a summary of all embedded files (media resources)
            StringBuilder summaryBuilder = new StringBuilder();
            summaryBuilder.AppendLine("Embedded Media Resources:");
            summaryBuilder.AppendLine();

            // Document.EmbeddedFiles holds the collection of embedded file specifications
            if (doc.EmbeddedFiles.Count > 0)
            {
                foreach (FileSpecification ef in doc.EmbeddedFiles)
                {
                    // Name is the file name, Description may contain additional info
                    summaryBuilder.AppendLine($"- {ef.Name}");
                    if (!string.IsNullOrEmpty(ef.Description))
                        summaryBuilder.AppendLine($"  Description: {ef.Description}");
                }
            }
            else
            {
                summaryBuilder.AppendLine("None");
            }

            // Create a FreeTextAnnotation on the first page to display the summary
            Page firstPage = doc.Pages[1]; // 1‑based indexing

            // Define a rectangle where the annotation will appear (coordinates in points)
            // Here we place it near the top of the page with a reasonable size
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 550, 500);

            // DefaultAppearance requires a font name, size and a System.Drawing.Color for the text
            var defaultAppearance = new DefaultAppearance("Helvetica", 10, System.Drawing.Color.Black);

            // Construct the annotation (page, rectangle, default appearance)
            FreeTextAnnotation freeText = new FreeTextAnnotation(firstPage, rect, defaultAppearance)
            {
                Contents = summaryBuilder.ToString(),
                // Background colour of the annotation
                Color = Aspose.Pdf.Color.LightYellow
            };

            // Border must be created with the annotation as its parent and cannot be set via object initializer
            freeText.Border = new Border(freeText) { Width = 1 };

            // Add the annotation to the page
            firstPage.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with summary annotation to '{outputPath}'.");
    }
}
