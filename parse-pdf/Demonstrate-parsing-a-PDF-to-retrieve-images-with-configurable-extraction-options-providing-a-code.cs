using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";
        // Folder where extracted images will be saved
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Choose extraction mode
        // DefinedInResources – all images defined in the page resources
        // ActuallyUsed       – only images that are rendered on the page
        ExtractImageMode mode = ExtractImageMode.ActuallyUsed;

        // Open the PDF document inside a using block (deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                if (mode == ExtractImageMode.DefinedInResources)
                {
                    // Extract every image that exists in the page's resource dictionary
                    int imgIndex = 1;
                    foreach (XImage xImg in page.Resources.Images)
                    {
                        // Build a unique file name per page and image
                        string imgPath = Path.Combine(outputFolder,
                            $"Page{pageIndex}_ResImg{imgIndex}{GetExtension(xImg)}");

                        // Save the raw image data (no scaling)
                        using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                        {
                            xImg.Save(fs);
                        }

                        imgIndex++;
                    }
                }
                else // ExtractImageMode.ActuallyUsed
                {
                    // Use ImagePlacementAbsorber to find images that are actually drawn
                    ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                    page.Accept(absorber);

                    int placementIndex = 1;
                    foreach (ImagePlacement placement in absorber.ImagePlacements)
                    {
                        // Determine file extension based on the underlying image format
                        string imgPath = Path.Combine(outputFolder,
                            $"Page{pageIndex}_UsedImg{placementIndex}{GetExtension(placement.Image)}");

                        // Save the image with its visual transformations (scale, rotation, etc.)
                        using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                        {
                            placement.Image.Save(fs, ImageFormat.Png);
                        }

                        placementIndex++;
                    }
                }
            }
        }

        Console.WriteLine($"Image extraction completed. Files saved to '{outputFolder}'.");
    }

    // Helper to infer a file extension from an XImage's MIME type
    private static string GetExtension(XImage xImg)
    {
        // XImage does not expose MIME directly; fall back to PNG as a safe default
        return ".png";
    }
}