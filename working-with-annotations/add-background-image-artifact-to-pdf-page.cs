using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "background.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Select the page to which the background artifact will be added (first page in this example)
            Page page = doc.Pages[1];

            // Create a BackgroundArtifact instance
            BackgroundArtifact background = new BackgroundArtifact();

            // Assign the image to the artifact (file path overload)
            background.SetImage(imagePath);

            // Ensure the artifact is placed behind the page contents
            background.IsBackground = true;

            // Add the artifact to the page's Artifacts collection
            page.Artifacts.Add(background);

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with background artifact: '{outputPdf}'.");
    }
}