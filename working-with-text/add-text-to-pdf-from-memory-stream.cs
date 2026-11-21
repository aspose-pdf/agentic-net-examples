using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPath);
        }

        // Load PDF bytes from the file (or any other source).
        byte[] pdfBytes = File.ReadAllBytes(inputPath);

        // Wrap the byte array in a MemoryStream for loading.
        using (var inputStream = new MemoryStream(pdfBytes))
        {
            // Load the PDF document from the memory stream.
            using (var doc = new Document(inputStream))
            {
                // Aspose.Pdf uses 1‑based page indexing.
                var page = doc.Pages[1];

                // Create a text fragment to add.
                var tf = new TextFragment("Hello from Aspose.Pdf!");
                // Optional: set the position of the text on the page.
                tf.Position = new Position(100, 700);

                // Add the text fragment to the page's paragraph collection.
                page.Paragraphs.Add(tf);

                // Prepare an output memory stream to receive the modified PDF.
                using (var outputStream = new MemoryStream())
                {
                    // Save the modified document into the output stream (PDF format).
                    doc.Save(outputStream);

                    // Write the resulting PDF bytes to a file.
                    File.WriteAllBytes(outputPath, outputStream.ToArray());
                }
            }
        }
    }
}
