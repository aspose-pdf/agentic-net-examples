using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for GoToAction and ExplicitDestinationType

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document doc = new Document(inputPath);

            // Verify that the document has at least five pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document does not contain page 5.");
                return;
            }

            // Create a bookmark that navigates to page 5 (1‑based index)
            var bookmark = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Project Overview",
                Action = new GoToAction(doc.Pages[5], ExplicitDestinationType.Fit)
            };

            // Add the bookmark to the document outline
            doc.Outlines.Add(bookmark);

            // Save the updated PDF
            doc.Save(outputPath);

            Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
