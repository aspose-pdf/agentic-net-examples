using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // original PDF with background artifact
        const string highResImg = "background_highres.jpg"; // higher‑resolution image
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(highResImg))
        {
            Console.Error.WriteLine($"High‑resolution image not found: {highResImg}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate through artifacts on the page
                for (int i = 1; i <= page.Artifacts.Count; i++)
                {
                    Artifact art = page.Artifacts[i];
                    // Look for a BackgroundArtifact
                    if (art is BackgroundArtifact bgArtifact)
                    {
                        // Replace the background image while preserving layout
                        // Use SetImage to assign the new image stream
                        using (FileStream imgStream = File.OpenRead(highResImg))
                        {
                            bgArtifact.SetImage(imgStream);
                        }

                        // Alternatively, you could assign directly to BackgroundImage:
                        // using (FileStream imgStream = File.OpenRead(highResImg))
                        // {
                        //     bgArtifact.BackgroundImage = imgStream;
                        // }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with higher‑resolution background: {outputPdf}");
    }
}