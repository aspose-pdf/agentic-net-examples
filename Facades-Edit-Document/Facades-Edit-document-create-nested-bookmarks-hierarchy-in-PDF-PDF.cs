using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF using the core Document API (cross‑platform)
            Document doc = new Document(inputPdf);

            // Create a parent bookmark (outline item)
            OutlineItemCollection parentBookmark = new OutlineItemCollection(doc.Outlines);
            parentBookmark.Title = "Parent Outline";

            // Create a child bookmark
            OutlineItemCollection childBookmark = new OutlineItemCollection(doc.Outlines);
            childBookmark.Title = "Child Outline";

            // Build the hierarchy: child under parent
            parentBookmark.Add(childBookmark);

            // Add the parent (with its child) to the document's outline collection
            doc.Outlines.Add(parentBookmark);

            // Save the modified PDF to the final location
            doc.Save(outputPdf);

            Console.WriteLine($"Nested bookmarks created and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
