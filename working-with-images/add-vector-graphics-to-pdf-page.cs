using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace
using Aspose.Pdf.Drawing;            // For XForm type

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string outputPdfPath = "vector_graphics_added.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load the source PDF and create a new empty PDF document
        using (Document srcDoc = new Document(sourcePdfPath))
        using (Document destDoc = new Document())
        {
            // Collect all vector graphics (Form XObjects) from the source document
            List<XForm> extractedGraphics = new List<XForm>();

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= srcDoc.Pages.Count; i++)
            {
                Page srcPage = srcDoc.Pages[i];

                // XForm objects represent vector graphics (Form XObjects)
                foreach (XForm form in srcPage.Resources.Forms)
                {
                    extractedGraphics.Add(form);
                }
            }

            // Add a new blank page to the destination document
            Page newPage = destDoc.Pages.Add();

            // Add the collected vector graphics to the new page.
            // The Forms collection does not expose an AddRange overload, so we add each XForm individually.
            foreach (XForm form in extractedGraphics)
            {
                newPage.Resources.Forms.Add(form);
            }

            // Save the resulting PDF
            destDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Vector graphics added to new page and saved as '{outputPdfPath}'.");
    }
}
