using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "TableImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Use TableAbsorber to locate tables and obtain their bounding rectangles.
            var absorber = new TableAbsorber();
            // TableAbsorber.Visit expects a single Page, so iterate through all pages.
            foreach (Page pg in doc.Pages)
            {
                absorber.Visit(pg);
            }

            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found in the document.");
                return;
            }

            // PngDevice is a concrete class but does NOT implement IDisposable, so do NOT wrap it in a using.
            var pngDevice = new PngDevice(new Resolution(300))
            {
                // Render using the CropBox coordinates – we will temporarily set the page CropBox to the table bounds.
                CoordinateType = PageCoordinateType.CropBox
            };

            int tableIndex = 1;
            foreach (AbsorbedTable absorbedTable in absorber.TableList)
            {
                // The page that contains the table.
                Page page = doc.Pages[absorbedTable.PageNum];

                // Bounding rectangle of the table (in PDF user units).
                Rectangle bounds = absorbedTable.Rectangle;

                // Preserve original CropBox so we can restore it after rendering.
                Rectangle originalCropBox = page.CropBox;
                page.CropBox = bounds;

                // Build output file path.
                string outputPath = Path.Combine(outputFolder, $"Table_{tableIndex}.png");

                // Render only the area occupied by the table.
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
                {
                    // With CoordinateType set to CropBox, Process(page, stream) renders the CropBox region.
                    pngDevice.Process(page, outStream);
                }

                // Restore the original CropBox.
                page.CropBox = originalCropBox;

                Console.WriteLine($"Saved table {tableIndex} to '{outputPath}'.");
                tableIndex++;
            }
        }
    }
}
