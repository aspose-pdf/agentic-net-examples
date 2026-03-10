using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged; // For ITaggedContent (optional, not strictly needed for alt text)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "accessible_images.pdf";
        const string altText    = "Accessible image description";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Optional: set language for the document (helps accessibility)
                ITaggedContent taggedContent = doc.TaggedContent;
                taggedContent.SetLanguage("en-US");

                // Iterate over all pages and set alternate text for each image
                foreach (Page page in doc.Pages)
                {
                    // XImageCollection is enumerable; iterate directly
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Try to set alternative text; returns false if ambiguous or not found
                        bool success = img.TrySetAlternativeText(altText, page);
                        if (!success)
                        {
                            Console.WriteLine("Could not set alt text for an image on page " + page.Number);
                        }
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Saved → '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}