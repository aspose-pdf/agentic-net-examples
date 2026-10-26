using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string pngPlaceholderPath = "placeholder.png"; // PNG file to use as replacement

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(pngPlaceholderPath))
        {
            Console.Error.WriteLine($"Placeholder PNG not found: {pngPlaceholderPath}");
            return;
        }

        // Load the placeholder image once into memory
        byte[] placeholderBytes = File.ReadAllBytes(pngPlaceholderPath);

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate through all page elements (Paragraphs collection)
                // Image objects are stored in the Paragraphs collection
                for (int i = 0; i < page.Paragraphs.Count; i++)
                {
                    // Check if the current element is an Image
                    if (page.Paragraphs[i] is Image oldImage)
                    {
                        // Replace the image data with the PNG placeholder while keeping all layout properties
                        oldImage.ImageStream = new MemoryStream(placeholderBytes);

                        // No need to recreate the Image object – all positioning, sizing, and margin
                        // information (HorizontalAlignment, VerticalAlignment, FixWidth, FixHeight, Margin, etc.)
                        // is already stored in the existing instance and is therefore preserved.
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with PNG replacements: '{outputPath}'.");
    }
}
