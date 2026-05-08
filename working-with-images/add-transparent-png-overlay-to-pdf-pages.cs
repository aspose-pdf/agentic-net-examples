using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (no Facades)

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";    // source PDF
        const string overlayPng = "overlay.png"; // transparent PNG to overlay
        const string outputPdf  = "output.pdf";   // result PDF

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(overlayPng))
        {
            Console.Error.WriteLine($"Overlay image not found: {overlayPng}");
            return;
        }

        // Load the PDF (lifecycle rule: use Document constructor inside using)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp for the PNG overlay.
            // Background = false ensures the stamp is placed on top of existing content.
            ImageStamp overlayStamp = new ImageStamp(overlayPng);
            overlayStamp.Background = false; // foreground (over content)

            // Apply the stamp to every page.
            foreach (Page page in doc.Pages)
            {
                // Resize the stamp to cover the whole page.
                overlayStamp.Width  = page.MediaBox.Width;
                overlayStamp.Height = page.MediaBox.Height;

                // Position the stamp at the lower‑left corner of the page.
                overlayStamp.XIndent = 0; // horizontal offset from the left edge
                overlayStamp.YIndent = 0; // vertical offset from the bottom edge

                // AddStamp adds the stamp at the current Z‑order (top of the page).
                page.AddStamp(overlayStamp);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Transparent overlay applied and saved to '{outputPdf}'.");
    }
}
