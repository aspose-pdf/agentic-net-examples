using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string outputPdfPath = "converted.pdf";

        // Verify that the source PDF exists before attempting to load it.
        if (!File.Exists(sourcePdfPath))
        {
            Console.WriteLine($"Error: The source PDF file '{sourcePdfPath}' was not found.");
            return;
        }

        // Temporary folder for page images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposePdfConversion");
        Directory.CreateDirectory(tempFolder);

        try
        {
            // Load the source PDF
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                // Convert each page to PNG using PngDevice (cross‑platform, no System.Drawing)
                var resolution = new Resolution(150); // DPI
                var pngDevice = new PngDevice(resolution);

                for (int pageNum = 1; pageNum <= sourceDoc.Pages.Count; pageNum++)
                {
                    string imagePath = Path.Combine(tempFolder, $"page_{pageNum}.png");
                    using (FileStream imageStream = new FileStream(imagePath, FileMode.Create))
                    {
                        pngDevice.Process(sourceDoc.Pages[pageNum], imageStream);
                    }
                }

                // Assemble the PNG images back into a PDF
                using (Document imagePdf = new Document())
                {
                    foreach (string imgFile in Directory.GetFiles(tempFolder, "page_*.png"))
                    {
                        Page page = imagePdf.Pages.Add();

                        var img = new Aspose.Pdf.Image
                        {
                            File = imgFile,
                            // Fit the image to the page size (optional)
                            FixWidth = page.PageInfo.Width,
                            FixHeight = page.PageInfo.Height,
                            IsInLineParagraph = false
                        };

                        page.Paragraphs.Add(img);
                    }

                    imagePdf.Save(outputPdfPath);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
        finally
        {
            // Clean up temporary images – ignore any errors during cleanup.
            try
            {
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
            catch
            {
                // Suppress cleanup exceptions; they are non‑critical.
            }
        }
    }
}
