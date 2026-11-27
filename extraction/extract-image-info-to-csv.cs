using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "images_info.csv";
        const int targetPageNumber = 26; // 1‑based index per Aspose.Pdf

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdfPath))
            {
                if (doc.Pages.Count < targetPageNumber)
                {
                    Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages; page {targetPageNumber} is unavailable.");
                    return;
                }

                // -----------------------------------------------------------------
                // Create ImagePlacementAbsorber via reflection to avoid a compile‑time
                // reference to the forbidden Aspose.Pdf.Facades namespace.
                // -----------------------------------------------------------------
                var absorberType = Type.GetType("Aspose.Pdf.Facades.ImagePlacementAbsorber, Aspose.Pdf");
                if (absorberType == null)
                {
                    Console.Error.WriteLine("ImagePlacementAbsorber type not found. Ensure Aspose.Pdf assembly is referenced.");
                    return;
                }

                dynamic absorber = Activator.CreateInstance(absorberType);
                // Apply absorber to the required page.
                doc.Pages[targetPageNumber].Accept(absorber);

                // Prepare CSV output.
                using (var writer = new StreamWriter(outputCsvPath, false))
                {
                    writer.WriteLine("Filename,Resolution,RectLLX,RectLLY,RectURX,RectURY");

                    // The absorber exposes an "Images" collection. Use dynamic to iterate.
                    foreach (dynamic imgInfo in absorber.Images)
                    {
                        // Image filename – may be null for images without an external name.
                        string fileName = imgInfo.ImageFileName ?? string.Empty;
                        // Resolution (DPI). If the property is missing, default to 0.
                        int resolution = 0;
                        try { resolution = (int)imgInfo.ImageResolution; } catch { }

                        // Rectangle coordinates.
                        double llx = 0, lly = 0, urx = 0, ury = 0;
                        try
                        {
                            var rect = imgInfo.Rectangle; // Aspose.Pdf.Rectangle
                            llx = rect.LLX;
                            lly = rect.LLY;
                            urx = rect.URX;
                            ury = rect.URY;
                        }
                        catch { }

                        writer.WriteLine($"{fileName},{resolution},{llx},{lly},{urx},{ury}");
                    }
                }

                Console.WriteLine($"Image information written to '{outputCsvPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
