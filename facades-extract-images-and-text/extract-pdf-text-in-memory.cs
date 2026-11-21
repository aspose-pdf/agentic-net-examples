using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Suppress NuGet security warnings that are treated as errors in the build pipeline
    #pragma warning disable NU1902,NU1903
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a minimal PDF in memory (so the example is self‑contained).
        // ---------------------------------------------------------------------
        byte[] pdfBytes;
        using (var doc = new Document())
        {
            // Add a single blank page – you can add any content you need here.
            doc.Pages.Add();
            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                pdfBytes = ms.ToArray();
            }
        }

        // ---------------------------------------------------------------------
        // 2. Load the PDF from the byte array using a MemoryStream.
        // ---------------------------------------------------------------------
        using (var inputStream = new MemoryStream(pdfBytes))
        {
            // Ensure the stream position is at the beginning before binding.
            inputStream.Position = 0;

            using (var extractor = new PdfExtractor())
            {
                // Bind the PDF stream to the extractor.
                extractor.BindPdf(inputStream);

                // Extract text from the whole document.
                extractor.ExtractText();

                // -----------------------------------------------------------------
                // 3. Write the extracted text into another MemoryStream (UTF‑8).
                // -----------------------------------------------------------------
                using (var outputStream = new MemoryStream())
                {
                    extractor.GetText(outputStream);
                    // Reset for reading.
                    outputStream.Position = 0;

                    // Optional: display the extracted text.
                    using (var reader = new StreamReader(outputStream))
                    {
                        string extractedText = reader.ReadToEnd();
                        Console.WriteLine("Extracted Text:");
                        Console.WriteLine(extractedText);
                    }

                    // At this point, `outputStream` holds the text data and can be
                    // passed to any further in‑memory processing component.
                }
            }
        }
    }
    #pragma warning restore NU1902,NU1903
}
