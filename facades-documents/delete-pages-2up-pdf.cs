using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a sample PDF in memory (3 pages) to act as the input stream.
        using (MemoryStream inputStream = new MemoryStream())
        {
            Document sampleDoc = new Document();
            // Add three blank pages.
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            // Save the sample PDF to the memory stream.
            sampleDoc.Save(inputStream);
            // Reset the stream position for reading.
            inputStream.Position = 0;

            // Stream that will hold the PDF after page deletion.
            using (MemoryStream deletedStream = new MemoryStream())
            {
                // Pages to delete (example: pages 2 and 3). Indices are 1‑based.
                int[] pagesToDelete = new int[] { 2, 3 };
                PdfFileEditor editor = new PdfFileEditor();
                bool deleteSuccess = editor.Delete(inputStream, pagesToDelete, deletedStream);
                if (!deleteSuccess)
                {
                    Console.Error.WriteLine("Failed to delete pages.");
                    return;
                }

                // Reset the position of the intermediate stream for reading.
                deletedStream.Position = 0;

                // Create the final output stream.
                using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    // Apply a 2‑up layout (2 columns, 1 row).
                    bool nupSuccess = editor.MakeNUp(deletedStream, outputStream, 2, 1);
                    if (!nupSuccess)
                    {
                        Console.Error.WriteLine("Failed to create 2‑up layout.");
                    }
                }
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
