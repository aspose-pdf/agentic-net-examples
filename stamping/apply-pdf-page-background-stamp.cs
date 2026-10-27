using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string targetPdfPath = "target.pdf";      // PDF to receive the background
        const string sourcePdfPath = "background.pdf"; // PDF containing the background page
        const string outputPdfPath = "stamped_output.pdf";
        const int backgroundPageNum = 1;                // 1‑based page number in source PDF

        if (!File.Exists(targetPdfPath) || !File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        try
        {
            // Load the source PDF.
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                if (backgroundPageNum < 1 || backgroundPageNum > sourceDoc.Pages.Count)
                {
                    Console.Error.WriteLine("Background page number is out of range.");
                    return;
                }

                // Convert the selected page to a PNG image using a device class.
                using (MemoryStream pngStream = new MemoryStream())
                {
                    // The device writes the PNG bytes into the stream.
                    PngDevice pngDevice = new PngDevice();
                    pngDevice.Process(sourceDoc.Pages[backgroundPageNum], pngStream);
                    pngStream.Position = 0; // reset for reading

                    // Load the target document that will receive the background.
                    using (Document targetDoc = new Document(targetPdfPath))
                    {
                        // Apply the rendered image as a background stamp to every page.
                        foreach (Page page in targetDoc.Pages)
                        {
                            // ImageStamp can be created from a stream.
                            ImageStamp stamp = new ImageStamp(pngStream)
                            {
                                Width = page.PageInfo.Width,
                                Height = page.PageInfo.Height,
                                Background = true, // place behind existing content
                                TopMargin = 0,
                                LeftMargin = 0
                            };

                            page.AddStamp(stamp);
                        }

                        // Save the modified document.
                        targetDoc.Save(outputPdfPath);
                    }
                }
            }

            Console.WriteLine($"Background applied successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
