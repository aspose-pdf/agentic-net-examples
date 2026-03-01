using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "accessible_images.pdf";
        const string altText    = "Accessible image description";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content to ensure the document is treated as accessible.
                ITaggedContent taggedContent = doc.TaggedContent;
                taggedContent.SetLanguage("en-US");

                // Iterate all pages and set alternate text on each image resource.
                foreach (Page page in doc.Pages)
                {
                    foreach (XImage img in page.Resources.Images)
                    {
                        // TrySetAlternativeText returns true if the alt text was applied.
                        img.TrySetAlternativeText(altText, page);
                    }
                }

                // Save the modified PDF.
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