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

        // Load the template PDF (contains the background page)
        using (Document templateDoc = new Document(templatePath))
        {
            // Create a stamp from the first page of the template
            // The stamp will be placed as a background on each target page
            PdfPageStamp backgroundStamp = new PdfPageStamp(templateDoc.Pages[1])
            {
                Background = true   // render behind page content
            };

            // Create the target document
            using (Document outputDoc = new Document())
            {
                // Example: generate 5 pages that share the same background
                for (int i = 1; i <= 5; i++)
                {
                    // Add a new blank page
                    Page page = outputDoc.Pages.Add();

                    // Add simple text to demonstrate that content appears over the background
                    TextFragment tf = new TextFragment($"Page {i}");
                    tf.Position = new Position(100, 700); // place text near top-left
                    page.Paragraphs.Add(tf);

                    // Apply the background stamp to the newly created page
                    page.AddStamp(backgroundStamp);
                }

                // Save the resulting PDF
                outputDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF created with background template: {outputPath}");
    }
}