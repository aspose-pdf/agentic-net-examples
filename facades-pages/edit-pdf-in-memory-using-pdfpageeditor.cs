using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // -------------------------------------------------------------------
        // 1. Create a minimal PDF in memory – this replaces the missing "input.pdf"
        // -------------------------------------------------------------------
        byte[] sourcePdfBytes;
        using (var seedDoc = new Document())
        {
            // Add a single blank page (or any content you need for the demo)
            seedDoc.Pages.Add();
            using (var tempStream = new MemoryStream())
            {
                seedDoc.Save(tempStream);
                sourcePdfBytes = tempStream.ToArray();
            }
        }

        // -------------------------------------------------------------------
        // 2. Edit the PDF entirely in memory using PdfPageEditor
        // -------------------------------------------------------------------
        using (var sourceStream = new MemoryStream(sourcePdfBytes))
        using (var editor = new PdfPageEditor())
        {
            // Bind the PDF from the source memory stream.
            editor.BindPdf(sourceStream);

            // Example edits: set zoom to 50% and rotate all pages by 90 degrees.
            editor.Zoom = 0.5f;          // 0.5 = 50%
            editor.Rotation = 90;       // Valid values: 0, 90, 180, 270

            // Prepare a destination memory stream to receive the edited PDF.
            using (var destinationStream = new MemoryStream())
            {
                // Save the modified PDF into the destination stream.
                editor.Save(destinationStream);

                // For demonstration, write the edited PDF back to a file.
                File.WriteAllBytes("edited_output.pdf", destinationStream.ToArray());
            }
        }

        Console.WriteLine("PDF edited in memory and saved to 'edited_output.pdf'.");
    }
}
