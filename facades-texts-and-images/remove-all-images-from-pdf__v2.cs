using System;
using System.IO;
using Aspose.Pdf;

class ImageRemover
{
    /// <summary>
    /// Removes every image from the source PDF and writes the result to a new file.
    /// </summary>
    /// <param name="inputPdf">Path to the original PDF.</param>
    /// <param name="outputPdf">Path where the image‑free PDF will be saved.</param>
    public static void RemoveAllImages(string inputPdf, string outputPdf)
    {
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF using the high‑level Document API.
        Document pdfDocument = new Document(inputPdf);

        // Iterate over each page and clear the Images collection in the page resources.
        foreach (Page page in pdfDocument.Pages)
        {
            // The Images collection holds all image objects referenced by the page.
            // Clearing it removes the images from the page content.
            page.Resources.Images.Clear();
        }

        // Save the modified document to the specified output path.
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"All images removed. Saved to '{outputPdf}'.");
    }

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_images.pdf";

        RemoveAllImages(inputPath, outputPath);
    }
}
