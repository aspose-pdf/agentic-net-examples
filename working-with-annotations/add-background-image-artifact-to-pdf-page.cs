using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string backgroundImg = "background.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(backgroundImg))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImg}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Select the page to which the background artifact will be added (first page in this example)
            Page page = doc.Pages[1];

            // Create a BackgroundArtifact instance
            BackgroundArtifact bgArtifact = new BackgroundArtifact();

            // Assign the image to the artifact (file path overload)
            bgArtifact.SetImage(backgroundImg);

            // Ensure the artifact is rendered behind the page content
            bgArtifact.IsBackground = true;

            // Add the artifact to the page's Artifacts collection
            page.Artifacts.Add(bgArtifact);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with background artifact saved to '{outputPdfPath}'.");
    }
}