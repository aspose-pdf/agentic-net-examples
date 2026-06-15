using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF completely into a memory stream
        using (FileStream fileStream = File.OpenRead(inputPath))
        using (MemoryStream inputMemory = new MemoryStream())
        {
            fileStream.CopyTo(inputMemory);
            inputMemory.Position = 0; // reset for reading

            // Prepare a memory stream that will receive the edited PDF
            using (MemoryStream outputMemory = new MemoryStream())
            {
                // Create the PdfPageEditor facade and bind the input stream
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    editor.BindPdf(inputMemory);

                    // Example modifications:
                    // - Reduce page size to 80% (zoom factor 0.8)
                    // - Rotate all pages by 90 degrees
                    editor.Zoom = 0.8f;
                    editor.Rotation = 90; // valid values: 0, 90, 180, 270

                    // Apply the changes (optional; Save will also apply them)
                    editor.ApplyChanges();

                    // Save the edited document into the output memory stream
                    editor.Save(outputMemory);
                }

                // Persist the edited PDF to a file (optional, for verification)
                File.WriteAllBytes(outputPath, outputMemory.ToArray());
                Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
            }
        }
    }
}