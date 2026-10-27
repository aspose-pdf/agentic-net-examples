using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int pageNumber = 1;          // 1‑based page index
        const int imageIndex = 1;          // 1‑based index of the raster image to delete on the page

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate page number
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Validate image index
            if (imageIndex < 1 || imageIndex > page.Resources.Images.Count)
            {
                Console.Error.WriteLine("Invalid image index on the specified page.");
                return;
            }

            // Retrieve the target XImage and its internal name (e.g., "Im1")
            XImage targetImage = page.Resources.Images[imageIndex];
            string imageName = targetImage.Name;

            // Remove the image from the page resources
            page.Resources.Images.Delete(imageName);

            // Remove any content object that references the deleted image.
            // We avoid a direct compile‑time reference to ImageXObject (which may be missing in older Aspose.Pdf versions)
            // by using reflection/dynamic inspection.
            var indicesToRemove = new List<int>();
            for (int i = 1; i <= page.Contents.Count; i++) // 1‑based indexing for Contents
            {
                var content = page.Contents[i];
                if (content == null) continue;

                // Check if the content object is an ImageXObject (by name) and whether its ImageName matches.
                Type ct = content.GetType();
                // Some versions expose ImageXObject as a class named "ImageXObject"; fallback to checking the property directly.
                PropertyInfo imgNameProp = ct.GetProperty("ImageName");
                if (imgNameProp != null)
                {
                    var imgName = imgNameProp.GetValue(content) as string;
                    if (string.Equals(imgName, imageName, StringComparison.Ordinal))
                    {
                        indicesToRemove.Add(i);
                    }
                }
            }

            // Delete collected contents in reverse order to keep indices valid
            for (int i = indicesToRemove.Count - 1; i >= 0; i--)
            {
                page.Contents.Delete(indicesToRemove[i]);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Raster image removed and saved to '{outputPath}'.");
    }
}
