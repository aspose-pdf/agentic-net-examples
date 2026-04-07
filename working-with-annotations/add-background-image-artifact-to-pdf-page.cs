using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath  = "background.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the target page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a BackgroundArtifact and assign the image
            BackgroundArtifact bgArtifact = new BackgroundArtifact();
            bgArtifact.SetImage(imagePath);   // set background image from file
            bgArtifact.IsBackground = true;   // place behind page contents

            // Add the artifact to the page's artifact collection
            page.Artifacts.Add(bgArtifact);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with background artifact at '{outputPath}'.");
    }
}