using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main(string[] args)
    {
        // ---------------------------------------------------------------------
        // INPUT PDF
        // ---------------------------------------------------------------------
        // The PDF file can be supplied as a command‑line argument; if omitted we
        // fall back to the default "sample.pdf" name used in the original sample.
        // ---------------------------------------------------------------------
        string inputPdf = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the file actually exists before attempting to load it.
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Error: Input PDF file '{inputPdf}' was not found.\n" +
                              "Please provide a valid path either as a command‑line argument " +
                              "or place the file in the working directory.");
            return; // Gracefully exit – no unhandled exception.
        }

        // ---------------------------------------------------------------------
        // OUTPUT FOLDER – where extracted images will be written.
        // ---------------------------------------------------------------------
        const string outputFolder = "ExtractedImages";
        Directory.CreateDirectory(outputFolder);

        // ---------------------------------------------------------------------
        // Extraction mode – can be toggled here. The two options are:
        //   * DefinedInResources – every XImage present in the page resources.
        //   * ActuallyUsed       – only images that are drawn on the page.
        // ---------------------------------------------------------------------
        ExtractImageMode mode = ExtractImageMode.ActuallyUsed;

        // ---------------------------------------------------------------------
        // Load the PDF document inside a using block for deterministic disposal.
        // ---------------------------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf.
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                if (mode == ExtractImageMode.DefinedInResources)
                {
                    // -------------------------------------------------------------
                    // MODE: DefinedInResources – enumerate all XImage objects in the
                    // page's Resources.Images collection. These images may or may not
                    // be actually rendered on the page.
                    // -------------------------------------------------------------
                    int imgCounter = 1;
                    foreach (XImage img in page.Resources.Images)
                    {
                        string imgPath = Path.Combine(
                            outputFolder,
                            $"Page{pageIndex}_ResImg{imgCounter}{GetImageExtension(img)}");

                        using (FileStream fs = File.Create(imgPath))
                        {
                            img.Save(fs);
                        }
                        imgCounter++;
                    }
                }
                else // ExtractImageMode.ActuallyUsed
                {
                    // -------------------------------------------------------------
                    // MODE: ActuallyUsed – use ImagePlacementAbsorber to find only those
                    // images that are placed (drawn) on the page.
                    // -------------------------------------------------------------
                    ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                    page.Accept(absorber);

                    int imgCounter = 1;
                    foreach (ImagePlacement placement in absorber.ImagePlacements)
                    {
                        XImage img = placement.Image;
                        string imgPath = Path.Combine(
                            outputFolder,
                            $"Page{pageIndex}_UsedImg{imgCounter}{GetImageExtension(img)}");

                        using (FileStream fs = File.Create(imgPath))
                        {
                            img.Save(fs);
                        }
                        imgCounter++;
                    }
                }
            }
        }

        Console.WriteLine($"Image extraction completed. Files saved to '{outputFolder}'.");
    }

    // ---------------------------------------------------------------------
    // Helper: derive a file extension based on the image's format.
    // The simplest, version‑agnostic approach is to always return ".png".
    // ---------------------------------------------------------------------
    private static string GetImageExtension(XImage img)
    {
        return ".png";
    }
}
