using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF and images
        const string inputPdfPath = "input.pdf";
        const string replacementImagePath = "newImage.png";
        const string outputPdfPath = "output.pdf";

        // Validate files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(replacementImagePath))
        {
            Console.Error.WriteLine($"Error: Replacement image not found – {replacementImagePath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Create an absorber to find image placements on the first page
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

            // Accept the absorber for the first page (1‑based indexing)
            pdfDocument.Pages[1].Accept(absorber);

            // Process each found image
            int index = 0;
            foreach (ImagePlacement placement in absorber.ImagePlacements)
            {
                index++;

                // Example 1: Replace the first image with a new one
                if (index == 1)
                {
                    using (FileStream newImgStream = new FileStream(replacementImagePath, FileMode.Open, FileAccess.Read))
                    {
                        placement.Replace(newImgStream);
                    }
                    Console.WriteLine($"Image #{index} replaced with '{replacementImagePath}'.");
                }
                // Example 2: Hide the second image
                else if (index == 2)
                {
                    placement.Hide();
                    Console.WriteLine($"Image #{index} hidden.");
                }
                // Additional images can be processed here (e.g., extract, rotate, etc.)
            }

            // Save the edited PDF
            pdfDocument.Save(outputPdfPath);
            Console.WriteLine($"Edited PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}