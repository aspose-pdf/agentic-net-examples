using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string bgImage   = "background.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(bgImage))
        {
            Console.Error.WriteLine($"Background image not found: {bgImage}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages to apply the background image
            foreach (Page page in doc.Pages)
            {
                // Create an Image object for the background
                Image img = new Image();
                img.File = bgImage;

                // Create a BackgroundArtifact and configure it
                BackgroundArtifact bgArtifact = new BackgroundArtifact();
                bgArtifact.IsBackground = true;          // place behind page content
                bgArtifact.Opacity = 0.5;                // subtle shading
                bgArtifact.SetImage(bgImage);            // assign the image

                // Add the artifact to the page
                page.Artifacts.Add(bgArtifact);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Background image added and saved to '{outputPdf}'.");
    }
}