using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string altText    = "Accessible description of the image";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Optional: set document language for accessibility
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");

            // Iterate through all pages and their image resources
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // Attempt to set alternative text for each image on the page
                    bool result = img.TrySetAlternativeText(altText, page);
                    // You can log the result if needed:
                    // Console.WriteLine($"Page {page.Number}, Image set: {result}");
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with alternative text to '{outputPath}'.");
    }
}