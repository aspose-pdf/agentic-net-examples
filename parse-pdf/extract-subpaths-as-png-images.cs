using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "SubpathImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the source PDF
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= srcDoc.Pages.Count; pageNum++)
            {
                Page srcPage = srcDoc.Pages[pageNum];

                // Capture all graphic elements on the current page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(srcPage);

                    int subPathIndex = 0;

                    // Filter the captured elements for SubPath objects
                    foreach (var element in absorber.Elements)
                    {
                        if (element is SubPath subPath)
                        {
                            subPathIndex++;

                            // Create a temporary PDF containing only this subpath
                            using (Document tempDoc = new Document())
                            {
                                // Add a blank page to the temporary document
                                Page tempPage = tempDoc.Pages.Add();

                                // Place the subpath onto the temporary page
                                subPath.AddOnPage(tempPage);

                                // Define the output PNG file name
                                string pngPath = Path.Combine(
                                    outputFolder,
                                    $"page{pageNum}_subpath{subPathIndex}.png");

                                // Render the temporary page to PNG
                                PngDevice pngDevice = new PngDevice();
                                using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
                                {
                                    pngDevice.Process(tempPage, pngStream);
                                }
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine("Extraction completed.");
    }
}