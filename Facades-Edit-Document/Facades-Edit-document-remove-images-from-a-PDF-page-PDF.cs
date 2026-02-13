using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace (optional for other operations)

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Specify the page number from which images will be removed (1‑based index)
            int pageNumber = 1;

            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > pdfDocument.Pages.Count)
            {
                Console.Error.WriteLine($"Error: Page {pageNumber} does not exist in the document.");
                return;
            }

            // Create an absorber to locate all image placements on the selected page
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

            // Accept the absorber for the target page
            pdfDocument.Pages[pageNumber].Accept(absorber);

            // Hide (remove) each found image placement from the page
            foreach (ImagePlacement placement in absorber.ImagePlacements)
            {
                placement.Hide(); // Removes the image from the page content
            }

            // Save the modified document (using the provided lifecycle rule)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Images removed from page {pageNumber}. Modified PDF saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}