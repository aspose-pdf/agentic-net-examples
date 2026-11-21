using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document, delete the first page (if any), insert a blank page at the end,
        // then rotate the new first page (if it exists).
        using (Document doc = new Document(inputPath))
        {
            // Delete the first page if present.
            if (doc.Pages.Count >= 1)
            {
                doc.Pages.Delete(1);
            }

            // Insert a blank page at the end of the document.
            doc.Pages.Add();

            // Save the intermediate result to a memory stream so that PdfPageEditor can work on it.
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                ms.Position = 0;

                // PdfPageEditor implements IDisposable; using ensures proper release of handles.
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    editor.BindPdf(ms);

                    // Rotate the new first page 90° clockwise, if it exists.
                    if (doc.Pages.Count >= 1)
                    {
                        editor.ProcessPages = new int[] { 1 }; // 1‑based page index
                        editor.Rotation = 90;                 // 0, 90, 180, 270
                    }

                    // Save the edited PDF.
                    editor.Save(outputPath);
                }
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
