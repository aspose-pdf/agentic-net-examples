using System;
using System.IO;
using System.Text;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string imagesOutputFolder = "ExtractedImages";
        const string csvOutputPath = "images.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for extracted images exists
        Directory.CreateDirectory(imagesOutputFolder);

        // Prepare CSV header
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("Filename,PageNumber,Width,Height");

        // Open the PDF document
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Absorber that finds image placements on pages
            var imageAbsorber = new Aspose.Pdf.ImagePlacementAbsorber();

            // Run the absorber on each page (1‑based indexing)
            for (int pageIdx = 1; pageIdx <= pdfDoc.Pages.Count; pageIdx++)
            {
                pdfDoc.Pages[pageIdx].Accept(imageAbsorber);
            }

            int imageCounter = 1;

            // Iterate over all found image placements
            foreach (Aspose.Pdf.ImagePlacement placement in imageAbsorber.ImagePlacements)
            {
                // Build a unique filename: image_page{page}_index{counter}.png
                string imageFileName = $"image_page{placement.Page.Number}_idx{imageCounter}.png";
                string imageFilePath = Path.Combine(imagesOutputFolder, imageFileName);

                // Save the image resource to a PNG file
                using (FileStream fs = new FileStream(imageFilePath, FileMode.Create))
                {
                    placement.Image.Save(fs, ImageFormat.Png);
                }

                // Retrieve displayed dimensions from the placement rectangle
                double width = placement.Rectangle.Width;
                double height = placement.Rectangle.Height;

                // Append a line to the CSV
                csvBuilder.AppendLine($"{imageFileName},{placement.Page.Number},{width},{height}");

                imageCounter++;
            }
        }

        // Write the CSV file
        File.WriteAllText(csvOutputPath, csvBuilder.ToString());

        Console.WriteLine($"Images extracted to: {imagesOutputFolder}");
        Console.WriteLine($"CSV report generated at: {csvOutputPath}");
    }
}