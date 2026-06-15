using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePath = "template.pdf";   // PDF page to be used as background
        const string outputPath   = "output.pdf";    // Resulting PDF

        // Verify template exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Load the template PDF (the page that will serve as background)
        using (Document templateDoc = new Document(templatePath))
        {
            // Ensure the template has at least one page
            if (templateDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Template PDF contains no pages.");
                return;
            }

            // Create the target document
            using (Document outputDoc = new Document())
            {
                // Example: create 3 pages that will share the same background
                for (int i = 1; i <= 3; i++)
                {
                    // Add a blank page to the output document
                    Page newPage = outputDoc.Pages.Add();

                    // Create a stamp from the first page of the template
                    PdfPageStamp bgStamp = new PdfPageStamp(templateDoc.Pages[1]);

                    // Place the stamp behind the page content
                    bgStamp.Background = true;

                    // Optionally adjust scaling, margins, etc.
                    // bgStamp.Zoom = 1.0; // default – full size
                    // bgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                    // bgStamp.VerticalAlignment   = VerticalAlignment.Center;

                    // Apply the background stamp to the new page
                    newPage.AddStamp(bgStamp);

                    // Add some sample content to demonstrate that the background stays behind
                    TextFragment tf = new TextFragment($"Page {i} – sample content");
                    tf.Position = new Position(100, 700);
                    tf.TextState.FontSize = 14;
                    tf.TextState.Font = FontRepository.FindFont("Helvetica");
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
                    newPage.Paragraphs.Add(tf);
                }

                // Save the resulting PDF
                outputDoc.Save(outputPath);
                Console.WriteLine($"PDF created with background template: {outputPath}");
            }
        }
    }
}