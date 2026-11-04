using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample PDF in memory with at least three pages.
        byte[] pdfBytes;
        using (var seedDoc = new Document())
        {
            // Add three blank pages so page index 3 is valid.
            seedDoc.Pages.Add();
            seedDoc.Pages.Add();
            seedDoc.Pages.Add();

            using (var ms = new MemoryStream())
            {
                seedDoc.Save(ms);
                pdfBytes = ms.ToArray();
            }
        }

        // Load the PDF from the byte array via a MemoryStream.
        using (var ms = new MemoryStream(pdfBytes))
        using (var doc = new Document(ms))
        {
            // Ensure the document has at least three pages.
            if (doc.Pages.Count >= 3)
            {
                // Rotate page 3 by 180 degrees (pages are 1‑based).
                doc.Pages[3].Rotate = Rotation.on180;
            }
            else
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Save the modified PDF.
            doc.Save("output.pdf");
        }

        Console.WriteLine("Page 3 rotated and saved to 'output.pdf'.");
    }
}
