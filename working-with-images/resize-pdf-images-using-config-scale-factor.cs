using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_resized.pdf";
        const string configPath = "config.txt"; // contains a single double value, e.g., 0.5

        // Validate files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Read scaling factor from configuration file
        double scaleFactor;
        try
        {
            string factorText = File.ReadAllText(configPath).Trim();
            scaleFactor = double.Parse(factorText);
            if (scaleFactor <= 0)
                throw new ArgumentException("Scaling factor must be positive.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read scaling factor: {ex.Message}");
            return;
        }

        // Process PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Iterate over each image on the page
                foreach (XImage img in page.Resources.Images)
                {
                    // Extract original image to a memory stream
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        img.Save(imgStream);
                        imgStream.Position = 0; // reset stream position

                        // Create an ImageStamp from the extracted image
                        ImageStamp stamp = new ImageStamp(imgStream)
                        {
                            Background = false,
                            Width = img.Width * scaleFactor,
                            Height = img.Height * scaleFactor,
                            // Position the stamp at the lower‑left corner (0,0).
                            // Adjust HorizontalAlignment / VerticalAlignment and indents as needed.
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Bottom,
                            XIndent = 0,
                            YIndent = 0
                        };

                        // Add the scaled stamp to the page
                        page.AddStamp(stamp);
                    }
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPdfPath}'.");
    }
}
