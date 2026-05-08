using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing portfolio outline items
        const string inputPath  = "portfolio.pdf";
        // Output PDF after removal
        const string outputPath = "portfolio_cleaned.pdf";
        // Description (title) of the outline item to delete
        const string descriptionToDelete = "Quarterly Report";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Delete outline items whose title matches the given description.
            // OutlineCollection.Delete(string) removes the item with the specified title.
            doc.Outlines.Delete(descriptionToDelete);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Deleted outline item \"{descriptionToDelete}\" and saved to '{outputPath}'.");
    }
}