using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "background.jpg";
        const int pageNumber = 1; // 1‑based page index

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Validate the requested page number (Aspose.Pdf uses 1‑based indexing)
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Retrieve the target page
            Page page = doc.Pages[pageNumber];

            // Create a BackgroundArtifact (parameterless constructor only)
            BackgroundArtifact background = new BackgroundArtifact();
            background.Opacity = 0.8; // optional opacity adjustment

            // Assign the image to the artifact using the writable BackgroundImage property
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                background.BackgroundImage = imgStream;
                // Add the artifact to the page's Artifacts collection while the stream is open
                page.Artifacts.Add(background);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Background artifact added and saved to '{outputPdf}'.");
    }
}
