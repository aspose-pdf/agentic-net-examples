using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string headingTitle = "Chapter 1"; // Title of the heading (bookmark) to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Remove the outline item (bookmark) with the specified title.
                // If the title does not exist, the call is a no‑op.
                doc.Outlines.Delete(headingTitle);

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Heading '{headingTitle}' removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}