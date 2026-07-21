using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Loads a filled PDF and returns its content as a byte array without touching the file system.
    static byte[] LoadPdfToByteArray(string pdfPath)
    {
        // Load the PDF with the high‑level Document class (supports in‑memory operations).
        Document doc = new Document(pdfPath);
        using (MemoryStream memory = new MemoryStream())
        {
            // Save the document directly into the memory stream.
            doc.Save(memory);
            // Return the underlying byte array.
            return memory.ToArray();
        }
    }

    static void Main()
    {
        const string inputPdf = "filled_form.pdf";

        // -----------------------------------------------------------------
        // Ensure the input PDF exists. In the sandbox there are no pre‑existing
        // files, so we create a minimal PDF on‑the‑fly before reading it.
        // -----------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            using (Document doc = new Document())
            {
                // Add a single blank page – enough for the viewer to work.
                doc.Pages.Add();
                doc.Save(inputPdf);
            }
        }

        // Convert the PDF to a byte array for transmission.
        byte[] pdfBytes = LoadPdfToByteArray(inputPdf);

        Console.WriteLine($"PDF byte array length: {pdfBytes.Length}");
        // The byte array can now be sent over a web API without writing to disk.
    }
}
