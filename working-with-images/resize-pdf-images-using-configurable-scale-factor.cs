using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath    = "scale.config"; // file containing a single double value

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
            string txt = File.ReadAllText(configPath).Trim();
            if (!double.TryParse(txt, out scaleFactor) || scaleFactor <= 0)
            {
                Console.Error.WriteLine("Invalid scaling factor in config file.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading config: {ex.Message}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Absorb image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Save the original image to a memory stream
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        placement.Image.Save(imgStream);
                        imgStream.Position = 0;

                        // Hide the original image instance
                        placement.Hide();

                        // Compute new rectangle based on scaling factor
                        double llx = placement.Rectangle.LLX;
                        double lly = placement.Rectangle.LLY;
                        double width  = placement.Rectangle.Width  * scaleFactor;
                        double height = placement.Rectangle.Height * scaleFactor;
                        double urx = llx + width;
                        double ury = lly + height;

                        // Add the scaled image back to the page at the new rectangle
                        page.AddImage(imgStream, new Aspose.Pdf.Rectangle(llx, lly, urx, ury));
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Images resized with factor {scaleFactor} and saved to '{outputPdfPath}'.");
    }
}