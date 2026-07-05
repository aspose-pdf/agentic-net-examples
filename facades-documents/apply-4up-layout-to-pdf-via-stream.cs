using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    // Applies a 4‑up (2 columns × 2 rows) layout to a PDF provided as a stream.
    // Returns the resulting PDF in a MemoryStream.
    static MemoryStream ApplyFourUp(Stream inputPdfStream)
    {
        if (inputPdfStream == null) throw new ArgumentNullException(nameof(inputPdfStream));

        // Ensure the input stream is positioned at the beginning.
        if (inputPdfStream.CanSeek)
            inputPdfStream.Position = 0;

        // Output will be written to a memory stream.
        MemoryStream outputPdfStream = new MemoryStream();

        // PdfFileEditor provides the N‑up functionality via stream overloads.
        PdfFileEditor editor = new PdfFileEditor();

        // 2 columns (x) and 2 rows (y) produce a 4‑up layout.
        bool result = editor.MakeNUp(inputPdfStream, outputPdfStream, 2, 2);

        if (!result)
            throw new InvalidOperationException("Failed to create 4‑up PDF.");

        // Reset the output stream position so it can be read from the beginning.
        if (outputPdfStream.CanSeek)
            outputPdfStream.Position = 0;

        return outputPdfStream;
    }

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_4up.pdf";

        // Prepare an input stream – if the file does not exist, create a simple PDF in memory.
        using (MemoryStream inputStream = new MemoryStream())
        {
            if (File.Exists(inputPath))
            {
                // Load the existing file into memory.
                byte[] bytes = File.ReadAllBytes(inputPath);
                inputStream.Write(bytes, 0, bytes.Length);
                inputStream.Position = 0;
            }
            else
            {
                // Create a minimal PDF document on‑the‑fly.
                Document doc = new Document();
                var page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample PDF – generated because 'input.pdf' was not found."));
                doc.Save(inputStream);
                inputStream.Position = 0;
            }

            // Apply the 4‑up transformation.
            using (MemoryStream resultStream = ApplyFourUp(inputStream))
            {
                // Write the resulting PDF to a physical file.
                using (FileStream outputFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    resultStream.CopyTo(outputFile);
                }
            }
        }

        Console.WriteLine("4‑up PDF created successfully.");
    }
}
