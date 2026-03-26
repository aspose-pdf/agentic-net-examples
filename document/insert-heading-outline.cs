using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // Added for destination classes

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Add a heading text to the first page
            Page page = doc.Pages[1];
            Heading heading = new Heading(0);
            heading.Text = "New Section";
            heading.TextState.Font = FontRepository.FindFont("Helvetica");
            heading.TextState.FontSize = 18;
            heading.Position = new Position(50, 750);
            page.Paragraphs.Add(heading);

            // Create an outline (bookmark) that points to the heading
            // Use the page number overload of XYZExplicitDestination (available in all recent Aspose.Pdf versions)
            OutlineItemCollection outlineItem = new OutlineItemCollection(doc.Outlines);
            outlineItem.Title = "New Section";
            outlineItem.Destination = new XYZExplicitDestination(page.Number, 0, 0, 1.0f);
            doc.Outlines.Add(outlineItem);

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new heading and bookmark: {outputPath}");
    }
}
