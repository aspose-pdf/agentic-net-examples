using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string newImagePath  = "newImage.jpg";       // image to insert
        const string outputPdfPath = "output.pdf";         // result PDF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Image file not found: {newImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages and replace images
            foreach (Page page in doc.Pages)
            {
                // OPTIONAL: clear existing image resources.
                // This removes the original images from the page's resource dictionary.
                // If the collection does not support Clear(), this line can be omitted.
                page.Resources.Images.Clear();

                // Create an ImageStamp from the new image file
                ImageStamp imgStamp = new ImageStamp(newImagePath)
                {
                    // Position the stamp at the lower‑left corner of the page.
                    // Adjust XIndent/YIndent, Width, Height as needed.
                    XIndent = 0,
                    YIndent = 0,
                    // Keep the original image dimensions.
                    // Width and Height can be set explicitly if required.
                };

                // Add the stamp to the current page, effectively replacing the old image.
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image replacement completed. Output saved to '{outputPdfPath}'.");
    }
}