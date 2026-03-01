using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged; // for ITaggedContent (optional, not required for alt text)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "accessible_images.pdf";
        const string altText    = "Accessible image description";

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
                // Optional: ensure the document is marked as tagged (helps accessibility tools)
                ITaggedContent tagged = doc.TaggedContent;
                tagged.SetLanguage("en-US");

                // Iterate over each page and each image resource on the page
                foreach (Page page in doc.Pages)
                {
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Set alternative text for the image; returns true if successful
                        bool success = img.TrySetAlternativeText(altText, page);
                        if (!success)
                        {
                            Console.WriteLine("Warning: Could not set alt text for an image on page " + page.Number);
                        }
                    }
                }

                // Save the modified PDF (default PDF format)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with alternate text saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}