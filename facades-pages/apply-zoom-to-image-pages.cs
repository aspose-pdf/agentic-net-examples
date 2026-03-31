using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document sourceDoc = new Document(inputPath))
        {
            using (Document resultDoc = new Document())
            {
                int pageCount = sourceDoc.Pages.Count;
                for (int i = 1; i <= pageCount; i++)
                {
                    // Detect if the current page contains any image objects
                    ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                    sourceDoc.Pages[i].Accept(absorber);
                    bool hasImage = absorber.ImagePlacements.Count > 0;

                    if (hasImage)
                    {
                        // Create a temporary single‑page document that holds the current page
                        using (Document singlePageDoc = new Document())
                        {
                            // Copy the source page into the temporary document
                            // Page.CopyTo is not available in newer Aspose.Pdf versions; use Pages.Add instead
                            singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                            // Save the temporary page to a file (required for PdfPageEditor)
                            string tempPagePath = Path.GetTempFileName();
                            singlePageDoc.Save(tempPagePath);

                            // Apply zoom using PdfPageEditor
                            PdfPageEditor editor = new PdfPageEditor();
                            editor.BindPdf(tempPagePath);
                            editor.Zoom = 0.5f; // 50 % zoom
                            string zoomedPagePath = Path.GetTempFileName();
                            editor.Save(zoomedPagePath);

                            // Load the zoom‑adjusted page and add it to the result document
                            using (Document zoomedDoc = new Document(zoomedPagePath))
                            {
                                // Again, use Pages.Add because CopyTo is unavailable
                                resultDoc.Pages.Add(zoomedDoc.Pages[1]);
                            }

                            // Clean up temporary files
                            File.Delete(tempPagePath);
                            File.Delete(zoomedPagePath);
                        }
                    }
                    else
                    {
                        // Page without images – copy it directly to the result document
                        resultDoc.Pages.Add(sourceDoc.Pages[i]);
                    }
                }

                resultDoc.Save(outputPath);
                Console.WriteLine("Zoom applied to pages with images. Saved as " + outputPath);
            }
        }
    }
}
