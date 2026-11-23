using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // source PDF (must have at least 4 pages)
        const string outputPdfPath = "output.pdf";    // result PDF
        const string imagePath = "background.jpg";   // image to use as background

        // Verify files exist
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

        // Load the PDF document first (so the stream can be reused later)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has at least four pages
            if (pdfDoc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The PDF does not contain a fourth page.");
                return;
            }

            // Load the image bytes and create a MemoryStream for the background artifact
            using (MemoryStream imgStream = new MemoryStream(File.ReadAllBytes(imagePath)))
            {
                // Create and configure the BackgroundArtifact
                var bgArtifact = new BackgroundArtifact
                {
                    // Position expects Aspose.Pdf.Point (not Aspose.Pdf.Drawing.Point)
                    Position = new Aspose.Pdf.Point(0, 0),
                    Opacity = 1.0,
                    // BackgroundImage property is a Stream, assign the MemoryStream directly
                    BackgroundImage = imgStream
                };

                // Attach the background artifact to page four (1‑based indexing)
                Page targetPage = pdfDoc.Pages[4];
                targetPage.Artifacts.Add(bgArtifact);

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Background artifact added and saved to '{outputPdfPath}'.");
    }
}
