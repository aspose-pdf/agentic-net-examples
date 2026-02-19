using System;
using System.IO;
using Aspose.Pdf;

class SetImageAltText
{
    static void Main(string[] args)
    {
        // Input and output PDF paths (adjust as needed)
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output_with_alt.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Iterate over each page in the document
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                Page page = pdfDocument.Pages[pageNumber];

                // The Resources.Images collection holds all XImage objects used on the page
                int imageIndex = 1;
                foreach (XImage xImage in page.Resources.Images)
                {
                    // Create a simple descriptive alternate text
                    string altText = $"Image {imageIndex} on page {pageNumber}";

                    // Set the alternate text for the image on this page
                    // TrySetAlternativeText returns true if the operation succeeded
                    bool success = xImage.TrySetAlternativeText(altText, page);
                    if (!success)
                    {
                        Console.WriteLine($"Warning: Could not set alt text for image {imageIndex} on page {pageNumber}.");
                    }

                    imageIndex++;
                }
            }

            // Save the modified PDF to the output path
            pdfDocument.Save(outputPdfPath);
            Console.WriteLine($"Successfully saved PDF with alternate text to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}