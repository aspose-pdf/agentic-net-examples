using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string headingTitle = "New Chapter";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a new outline (bookmark) item
            OutlineItemCollection newOutline = new OutlineItemCollection(doc.Outlines)
            {
                Title = headingTitle,
                // Set the destination to the first page (adjust as needed)
                Destination = new GoToAction(doc.Pages[1])
            };

            // Insert the new outline at the end of the outline collection
            doc.Outlines.Add(newOutline);
            // Alternatively, insert at a specific position:
            // doc.Outlines.Insert(0, newOutline); // inserts at the top

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Outline '{headingTitle}' added and saved to '{outputPath}'.");
    }
}