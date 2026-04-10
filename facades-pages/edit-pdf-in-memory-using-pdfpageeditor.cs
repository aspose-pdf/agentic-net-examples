using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a sample PDF in memory (so we don't need an external file)
        // ------------------------------------------------------------
        using (MemoryStream sourceStream = new MemoryStream())
        {
            // Build a simple PDF document
            Document sampleDoc = new Document();
            Page page = sampleDoc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Hello, Aspose.Pdf!"));
            // Save the sample PDF into the memory stream
            sampleDoc.Save(sourceStream);
            // Reset the stream position to the beginning before reading
            sourceStream.Position = 0;

            // ------------------------------------------------------------
            // 2. Prepare a memory stream for the edited PDF output
            // ------------------------------------------------------------
            using (MemoryStream destinationStream = new MemoryStream())
            // 3. Create the PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the source PDF stream to the editor
                editor.BindPdf(sourceStream);

                // Example edit operations:
                // Set zoom to 50% (0.5f corresponds to 100% = 1.0f)
                editor.Zoom = 0.5f;
                // Rotate all pages by 90 degrees
                editor.Rotation = 90;

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the edited PDF into the destination memory stream
                editor.Save(destinationStream);

                // Optionally retrieve the edited PDF bytes
                byte[] editedPdfBytes = destinationStream.ToArray();

                // For demonstration purposes, write the result to a file (can be omitted)
                File.WriteAllBytes("output.pdf", editedPdfBytes);
            }
        }
    }
}
