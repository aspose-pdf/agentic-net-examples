using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_vectorized.pdf";
        const string vectorFolder = "vectors"; // folder containing .svg files with the same base names as raster images

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!Directory.Exists(vectorFolder))
        {
            Console.Error.WriteLine($"Vector folder not found: {vectorFolder}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    var imagesToReplace = new List<XImage>();

                    // Collect raster images that have a matching SVG counterpart
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Image name may be null; skip if we cannot map it
                        if (string.IsNullOrEmpty(img.Name))
                            continue;

                        string ext = Path.GetExtension(img.Name).ToLowerInvariant();
                        bool isRaster = ext == ".png" || ext == ".jpg" || ext == ".jpeg" ||
                                         ext == ".bmp" || ext == ".tif" || ext == ".tiff";

                        if (!isRaster)
                            continue;

                        string baseName = Path.GetFileNameWithoutExtension(img.Name);
                        string vectorPath = Path.Combine(vectorFolder, baseName + ".svg");

                        if (File.Exists(vectorPath))
                            imagesToReplace.Add(img);
                    }

                    // Replace each raster image with its SVG version
                    foreach (XImage rasterImg in imagesToReplace)
                    {
                        string rasterName = rasterImg.Name;
                        string baseName = Path.GetFileNameWithoutExtension(rasterName);
                        string vectorPath = Path.Combine(vectorFolder, baseName + ".svg");

                        // Remove the raster image from the page resources
                        page.Resources.Images.Delete(rasterName);

                        // Add the SVG image; preserve the original name so existing references stay valid
                        using (FileStream vecStream = File.OpenRead(vectorPath))
                        {
                            // Add returns the name (string) of the newly added image
                            string addedImageName = page.Resources.Images.Add(vecStream);
                            // Retrieve the XImage object using the returned name
                            XImage vectorImg = page.Resources.Images[addedImageName];
                            // Keep the original name to satisfy existing references
                            vectorImg.Name = rasterName;
                        }
                    }
                }

                doc.Save(outputPdf);
                Console.WriteLine($"Vectorized PDF saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
