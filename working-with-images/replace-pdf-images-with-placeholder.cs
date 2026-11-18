using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_placeholder.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                // Collect images first to avoid modifying the collection while iterating
                List<XImage> images = new List<XImage>();
                foreach (XImage img in page.Resources.Images)
                {
                    images.Add(img);
                }

                foreach (XImage img in images)
                {
                    // Placeholder rectangle – replace with actual image bounds if needed
                    Aspose.Pdf.Rectangle placeholderRect = new Aspose.Pdf.Rectangle(
                        50,                                 // lower‑left X
                        page.PageInfo.Height - 100,        // lower‑left Y
                        250,                                // upper‑right X
                        page.PageInfo.Height - 50);        // upper‑right Y

                    // Create a link annotation that points to the original image URL
                    LinkAnnotation link = new LinkAnnotation(page, placeholderRect);
                    link.Action = new GoToURIAction("https://example.com/original-image");
                    link.Contents = "Original image"; // annotation text (no Title property)
                    link.Border = new Border(link) { Width = 0 }; // invisible border
                    link.Color = Color.Transparent;

                    // Visible placeholder annotation (text note)
                    TextAnnotation placeholder = new TextAnnotation(page, placeholderRect)
                    {
                        Title = "Image placeholder",
                        Contents = "Click to view original image",
                        Open = true,
                        Icon = TextIcon.Note,
                        Color = Color.LightGray
                    };

                    page.Annotations.Add(link);
                    page.Annotations.Add(placeholder);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with image placeholders saved to '{outputPath}'.");
    }
}
