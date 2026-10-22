using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a minimal PDF in memory (acts as the source PDF).
        // ------------------------------------------------------------
        byte[] pdfBytes;
        using (var seedDoc = new Document())
        {
            // Add a blank page so the document is valid.
            seedDoc.Pages.Add();
            using (var ms = new MemoryStream())
            {
                seedDoc.Save(ms);
                pdfBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Load the PDF from a MemoryStream, modify it, and write back.
        // ------------------------------------------------------------
        using (var inputStream = new MemoryStream(pdfBytes))
        using (var outputStream = new MemoryStream())
        {
            // Load PDF from the input stream.
            using (var doc = new Document(inputStream))
            {
                // Create a text fragment to add.
                var fragment = new TextFragment("Added text")
                {
                    // Optional: set the position on the page.
                    Position = new Position(100, 700)
                };

                // Add the fragment to the first page (pages are 1‑based).
                doc.Pages[1].Paragraphs.Add(fragment);

                // Save the modified PDF into the output stream.
                doc.Save(outputStream);
            }

            // Example: write the result to a file (optional).
            File.WriteAllBytes("output.pdf", outputStream.ToArray());
        }
    }
}
