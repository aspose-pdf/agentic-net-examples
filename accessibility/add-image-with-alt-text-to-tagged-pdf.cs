using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";
        const string imagePath = "newImage.jpg";
        const string altText = "Description of the newly inserted image";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF – existing tags are preserved automatically
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content helper (null if the PDF is not tagged)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root structure element if the document is already tagged
            dynamic root = null;
            if (taggedContent != null)
            {
                root = taggedContent.RootElement; // dynamic to avoid compile‑time type dependency
            }

            // Choose a page to insert the image (first page in this example)
            Page page = doc.Pages[1];

            // ---------------------------------------------------------------------
            // 1. Add the image to the page resources and set its alternative text.
            // ---------------------------------------------------------------------
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Add the image to the page's resource collection; the method returns the image name (string).
                string imgName = page.Resources.Images.Add(imgStream);
                XImage xImg = page.Resources.Images[imgName];

                // Set the alternative (alt) text for the image resource.
                // The method returns true if the operation succeeded.
                xImg.TrySetAlternativeText(altText, page);
            }

            // ---------------------------------------------------------------------
            // 2. Place the image on the page using an ImageStamp (absolute positioning).
            // ---------------------------------------------------------------------
            using (FileStream imgStreamForStamp = File.OpenRead(imagePath))
            {
                ImageStamp stamp = new ImageStamp(imgStreamForStamp)
                {
                    Width = 200,          // Desired width in points
                    Height = 150,         // Desired height in points
                    XIndent = 100,        // Distance from the left edge of the page
                    YIndent = 500         // Distance from the bottom edge of the page
                };
                page.AddStamp(stamp);
            }

            // ---------------------------------------------------------------------
            // 3. If the PDF is already tagged, add a Figure element that references the image.
            // ---------------------------------------------------------------------
            if (root != null)
            {
                // Create a Figure element (represents an image in the logical structure).
                dynamic figure = doc.TaggedContent.CreateFigureElement();

                // Set alternative text for the figure. Using dynamic bypasses compile‑time type checks.
                figure.AlternativeText = altText;

                // Append the new Figure element to the root of the structure tree.
                root.AppendChild(figure);
            }

            // Save the modified PDF; existing tags are retained and the new figure tag (if any) is added.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
