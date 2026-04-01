using System;
using System.IO;
using Aspose.Pdf;

public class ResizePdfPages
{
    public static void Main()
    {
        string inputPath = "input.pdf";
        string templatePath = "template.pdf";
        string outputPath = "output.pdf";

        // Validate that the required files exist before attempting to open them.
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the template PDF and read the dimensions of its first page.
        using (Document templateDoc = new Document(templatePath))
        {
            Page templatePage = templateDoc.Pages[1];
            double templateWidth = templatePage.PageInfo.Width;
            double templateHeight = templatePage.PageInfo.Height;

            // Load the PDF that needs to be resized.
            using (Document inputDoc = new Document(inputPath))
            {
                foreach (Page page in inputDoc.Pages)
                {
                    // Apply the template dimensions via PageInfo (the recommended API).
                    page.PageInfo.Width = templateWidth;
                    page.PageInfo.Height = templateHeight;
                    // Preserve orientation based on the template's aspect ratio.
                    page.PageInfo.IsLandscape = templateWidth > templateHeight;
                }

                // Save the resized document.
                inputDoc.Save(outputPath);
                Console.WriteLine($"Resized PDF saved to {outputPath}");
            }
        }
    }
}
