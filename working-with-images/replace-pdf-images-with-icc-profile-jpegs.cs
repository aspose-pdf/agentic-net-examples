using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagesFolder = "NewImages"; // folder with replacement images that already contain ICC profiles

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!Directory.Exists(imagesFolder))
        {
            Console.Error.WriteLine($"Images folder not found: {imagesFolder}");
            return;
        }

        // Get all replacement image file paths
        string[] replacementImages = Directory.GetFiles(imagesFolder, "*.*", SearchOption.TopDirectoryOnly);
        if (replacementImages.Length == 0)
        {
            Console.Error.WriteLine("No replacement images found.");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdf))
            {
                // Iterate through each page (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Remove existing page contents (including any images)
                    page.Paragraphs.Clear();

                    // Starting Y position (from top of the page)
                    double yPos = page.PageInfo.Height - 50; // start near the top margin

                    foreach (string imgPath in replacementImages)
                    {
                        // Use ImageStamp – the core API that accepts a file path or stream.
                        // ImageStamp keeps the original image dimensions if Width/Height are not set.
                        ImageStamp imgStamp = new ImageStamp(imgPath)
                        {
                            // Position the stamp. YIndent is measured from the bottom of the page,
                            // therefore we subtract the desired top offset from the page height.
                            XIndent = 50,
                            YIndent = yPos - 50 // a small offset from the calculated top position
                        };

                        // Add the stamp to the page.
                        page.AddStamp(imgStamp);

                        // Move the Y position down for the next image (add some spacing).
                        yPos -= 150; // adjust this value based on typical image height or desired spacing
                    }
                }

                // Save the modified PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF saved with ICC‑profiled images to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
