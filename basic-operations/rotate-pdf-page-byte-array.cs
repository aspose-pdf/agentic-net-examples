using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // Load PDF bytes. If "input.pdf" does not exist, create a temporary PDF
        // with at least five blank pages so the rest of the logic can run.
        // ---------------------------------------------------------------------
        byte[] pdfBytes;
        const string inputPath = "input.pdf";
        if (File.Exists(inputPath))
        {
            pdfBytes = File.ReadAllBytes(inputPath);
        }
        else
        {
            // Create a new PDF with 5 empty pages.
            Document placeholder = new Document();
            // Aspose.Pdf creates a document with a single empty page by default.
            // Add four more pages so the total becomes five.
            for (int i = 0; i < 4; i++)
                placeholder.Pages.Add();

            using (MemoryStream msTmp = new MemoryStream())
            {
                placeholder.Save(msTmp);
                pdfBytes = msTmp.ToArray();
            }
        }

        // ---------------------------------------------------------------------
        // Work with the PDF loaded from the byte array.
        // ---------------------------------------------------------------------
        using (MemoryStream ms = new MemoryStream(pdfBytes))
        using (Document doc = new Document(ms))
        {
            // Ensure the document has at least five pages (1‑based indexing).
            if (doc.Pages.Count >= 5)
            {
                // Rotate page 5 by 180 degrees.
                // Pages collection in Aspose.Pdf is 1‑based, so index 5 is the fifth page.
                doc.Pages[5].Rotate = Rotation.on180;
            }
            else
            {
                Console.Error.WriteLine("The document does not contain a fifth page.");
            }

            // Save the modified PDF.
            doc.Save("output.pdf");
        }
    }
}
