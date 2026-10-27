using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the output PDF, and the file to embed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string embedFilePath = "data.bin";

        // ------------------------------------------------------------
        // 1. Ensure the source PDF exists – create a minimal placeholder
        //    if it is missing (only on Windows, because Aspose.Pdf may need
        //    GDI+ for rendering). On non‑Windows platforms we simply abort
        //    with a clear message.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                var placeholder = new Document();
                placeholder.Pages.Add();
                placeholder.Save(inputPdfPath);
                Console.WriteLine($"Placeholder PDF created at '{inputPdfPath}'.");
            }
            else
            {
                Console.WriteLine($"Input PDF '{inputPdfPath}' not found and cannot create a placeholder on this OS.");
                return;
            }
        }

        // ------------------------------------------------------------
        // 2. Load the source PDF.
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // --------------------------------------------------------
            // 3. Prepare the byte array to embed.
            //    If the file does not exist we fall back to a small
            //    in‑memory dummy payload so the example still runs.
            // --------------------------------------------------------
            byte[] fileBytes;
            if (File.Exists(embedFilePath))
            {
                fileBytes = File.ReadAllBytes(embedFilePath);
            }
            else
            {
                Console.WriteLine($"Embed file '{embedFilePath}' not found – using dummy data.");
                fileBytes = new byte[] { 0xDE, 0xAD, 0xBE, 0xEF };
            }

            // --------------------------------------------------------
            // 4. Create a FileSpecification from the byte array.
            //    The Name property defines how the attachment appears
            //    in PDF viewers. The Contents stream must stay alive
            //    until the document is saved, therefore we keep the
            //    MemoryStream instance in a local variable.
            // --------------------------------------------------------
            using var contentStream = new MemoryStream(fileBytes);
            var fileSpec = new FileSpecification
            {
                Name = Path.GetFileName(embedFilePath), // display name
                Description = "Embedded binary data",
                // Assign the stream that holds the file bytes
                Contents = contentStream
            };

            // --------------------------------------------------------
            // 5. Add the attachment to the document.
            // --------------------------------------------------------
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // --------------------------------------------------------
            // 6. Save the modified PDF.
            // --------------------------------------------------------
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"File '{embedFilePath}' embedded into '{outputPdfPath}'.");
    }
}
