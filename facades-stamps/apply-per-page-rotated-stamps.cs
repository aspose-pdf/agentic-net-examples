using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_stamps.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document to obtain the page count (1‑based indexing)
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // Initialize the PdfFileStamp facade and bind the document
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                fileStamp.BindPdf(doc);

                // Create a separate stamp for each page with a unique rotation angle
                for (int i = 1; i <= pageCount; i++)
                {
                    // Example rotation: 15 degrees per page index (wrap at 360)
                    float rotationAngle = (float)((i * 15) % 360);

                    // Create the stamp and configure its appearance
                    Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                    stamp.BindLogo(
                        new Aspose.Pdf.Facades.FormattedText(
                            $"Page {i}",
                            System.Drawing.Color.Red,          // Text color (System.Drawing)
                            "Helvetica",                       // Font name
                            Aspose.Pdf.Facades.EncodingType.Winansi,
                            false,                             // IsEmbedded
                            24));                              // Font size

                    stamp.Rotation = rotationAngle;          // Apply calculated rotation
                    stamp.IsBackground = false;              // Stamp appears on top
                    stamp.Pages = new int[] { i };           // Apply only to the current page

                    // Add the configured stamp to the document
                    fileStamp.AddStamp(stamp);
                }

                // Save the result and close the facade
                fileStamp.Save(outputPath);
                fileStamp.Close();
            }
        }

        Console.WriteLine($"Stamps with per‑page rotation saved to '{outputPath}'.");
    }
}