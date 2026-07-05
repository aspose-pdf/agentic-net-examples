using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "rotated_page4.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a sample PDF with at least four pages in memory.
        // ---------------------------------------------------------------------
        using (var sourceDoc = new Document())
        {
            // Add four blank pages (you can add content if you wish).
            for (int i = 0; i < 4; i++)
                sourceDoc.Pages.Add();

            // Save the document to a MemoryStream so we never rely on a physical file.
            using (var inputStream = new MemoryStream())
            {
                sourceDoc.Save(inputStream);
                inputStream.Position = 0; // Reset stream position for reading.

                // -----------------------------------------------------------------
                // 2. Rotate only page 4 using PdfPageEditor (stream overload).
                // -----------------------------------------------------------------
                using (var editor = new PdfPageEditor())
                {
                    // Bind the PDF from the stream instead of a file path.
                    editor.BindPdf(inputStream);

                    // Restrict editing to page 4 only.
                    editor.ProcessPages = new int[] { 4 };

                    // Set rotation to 180 degrees for the selected page(s).
                    editor.Rotation = 180;

                    // Apply the changes.
                    editor.ApplyChanges();

                    // Save the result to the desired output file.
                    editor.Save(outputPath);
                }
            }
        }

        Console.WriteLine($"Page 4 rotated 180° and saved to '{outputPath}'.");
    }
}
