using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

// Read scaling factor from a simple configuration file (e.g., "config.txt" containing a double value)
double scalingFactor = 1.0;
const string configPath = "config.txt";
if (File.Exists(configPath))
{
    string text = File.ReadAllText(configPath).Trim();
    if (!double.TryParse(text, out scalingFactor) || scalingFactor <= 0)
    {
        Console.Error.WriteLine($"Invalid scaling factor in '{configPath}'. Using default 1.0.");
        scalingFactor = 1.0;
    }
}
else
{
    Console.Error.WriteLine($"Config file '{configPath}' not found. Using default scaling factor 1.0.");
}

// Input and output PDF paths
const string inputPdf  = "input.pdf";
const string outputPdf = "output_resized.pdf";

if (!File.Exists(inputPdf))
{
    Console.Error.WriteLine($"Input PDF '{inputPdf}' not found.");
    return;
}

// Load the PDF document
using (Document pdfDoc = new Document(inputPdf))
{
    // Iterate through all pages
    foreach (Page page in pdfDoc.Pages)
    {
        // Absorb image placements on the current page
        ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
        page.Accept(absorber);

        // Process each image placement
        foreach (ImagePlacement imgPlacement in absorber.ImagePlacements)
        {
            // Extract the original image into a memory stream
            using (MemoryStream originalStream = new MemoryStream())
            {
                imgPlacement.Image.Save(originalStream, ImageFormat.Png);
                originalStream.Position = 0;

                // Load the image with System.Drawing (Windows‑only)
                using (Bitmap originalBitmap = new Bitmap(originalStream))
                {
                    // Compute new dimensions based on the scaling factor
                    int newWidth  = (int)(originalBitmap.Width  * scalingFactor);
                    int newHeight = (int)(originalBitmap.Height * scalingFactor);

                    // Create a scaled bitmap
                    using (Bitmap scaledBitmap = new Bitmap(originalBitmap, new Size(newWidth, newHeight)))
                    {
                        // Save the scaled bitmap back to a stream
                        using (MemoryStream scaledStream = new MemoryStream())
                        {
                            scaledBitmap.Save(scaledStream, ImageFormat.Png);
                            scaledStream.Position = 0;

                            // Replace the original image in the PDF with the scaled one
                            imgPlacement.Replace(scaledStream);
                        }
                    }
                }
            }
        }
    }

    // Save the modified PDF
    pdfDoc.Save(outputPdf);
    Console.WriteLine($"Images resized with factor {scalingFactor} and saved to '{outputPdf}'.");
}