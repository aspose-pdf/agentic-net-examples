using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string bgImagePath = "background.jpg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(bgImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {bgImagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least three pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The PDF contains fewer than 3 pages.");
                return;
            }

            // Open the image file as a stream; the using ensures it is closed after use
            using (FileStream imgStream = File.OpenRead(bgImagePath))
            {
                // Create a BackgroundArtifact and set its BackgroundImage from the stream
                BackgroundArtifact bg = new BackgroundArtifact();
                bg.BackgroundImage = imgStream; // assign the stream directly (property is writable)

                // Attach the background artifact to page three (index 3)
                doc.Pages[3].Artifacts.Add(bg);
            } // imgStream is closed here (the artifact keeps a reference until Save)

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with background on page 3: {outputPdfPath}");
    }
}
