using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputCgmPath = "input.cgm";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Input CGM file not found: {inputCgmPath}");
            return;
        }

        // Load the CGM file into a PDF Document using CgmLoadOptions (CGM is input‑only)
        using (Document pdfDoc = new Document(inputCgmPath, new CgmLoadOptions()))
        {
            // Save the intermediate PDF to a memory stream so that PdfFileEditor can work with streams.
            using (MemoryStream originalPdfStream = new MemoryStream())
            {
                pdfDoc.Save(originalPdfStream);
                originalPdfStream.Position = 0; // Reset before passing to the editor.

                // Create a PdfFileEditor facade to manipulate pages (e.g., add margins)
                PdfFileEditor editor = new PdfFileEditor();

                // Add uniform margins of 50 points on all sides to all pages.
                // The int[] parameter specifies page numbers; null applies to all pages.
                // The four double values are left, right, top, bottom margins respectively.
                using (MemoryStream editedStream = new MemoryStream())
                {
                    // Use the overload that works with streams: AddMargins(Stream input, Stream output, ...)
                    editor.AddMargins(originalPdfStream, editedStream, null, 50, 50, 50, 50);
                    editedStream.Position = 0; // Reset stream before re‑reading

                    // Load the edited PDF from the stream and save it to the final file.
                    using (Document editedDoc = new Document(editedStream))
                    {
                        editedDoc.Save(outputPdfPath);
                    }
                }
            }
        }

        Console.WriteLine($"CGM converted and processed PDF saved to '{outputPdfPath}'.");
    }
}
