using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the background images and the output PDF
        const string imgPath1 = "bg1.jpg";
        const string imgPath2 = "bg2.jpg";
        const string outputPdf = "output.pdf";

        // Verify that both image files exist
        if (!File.Exists(imgPath1) || !File.Exists(imgPath2))
        {
            Console.Error.WriteLine("One or both background images not found.");
            return;
        }

        // Create a new PDF document and ensure it is disposed properly
        using (Document pdf = new Document())
        {
            // Add two pages (Aspose.Pdf uses 1‑based indexing)
            Page page1 = pdf.Pages.Add();
            Page page2 = pdf.Pages.Add();

            // Load the first image stream and assign it as a background artifact to page 1
            using (FileStream imgStream1 = File.OpenRead(imgPath1))
            {
                BackgroundArtifact bg1 = new BackgroundArtifact();
                bg1.BackgroundImage = imgStream1; // assign image via stream
                page1.Artifacts.Add(bg1);          // add artifact to the page
            }

            // Load the second image stream and assign it as a background artifact to page 2
            using (FileStream imgStream2 = File.OpenRead(imgPath2))
            {
                BackgroundArtifact bg2 = new BackgroundArtifact();
                bg2.BackgroundImage = imgStream2; // assign image via stream
                page2.Artifacts.Add(bg2);          // add artifact to the page
            }

            // Save the PDF document
            pdf.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
