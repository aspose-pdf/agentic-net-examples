using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string outputPdfPath = "output.pdf";     // destination PDF
        const string imagePath     = "background.png"; // background image file

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: wrap in using)
            using (Document doc = new Document(inputPdfPath))
            {
                // Select the page to which the background will be applied (first page here)
                Page page = doc.Pages[1];

                // Create a BackgroundArtifact instance
                BackgroundArtifact background = new BackgroundArtifact();

                // Assign the image to the artifact (method SetImage(string) is valid)
                background.SetImage(imagePath);

                // Ensure the artifact is rendered behind page contents
                background.IsBackground = true;

                // Add the artifact to the page's Artifacts collection
                page.Artifacts.Add(background);

                // Save the modified document (lifecycle rule: save inside using)
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF with background artifact saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}