using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "zoomed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // PdfPageEditor is the correct API for setting per‑page zoom.
            // It works on a Document instance; after setting ProcessPages and Zoom,
            // call ApplyChanges() to persist the modification.
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                int pageCount = doc.Pages.Count;

                for (int i = 1; i <= pageCount; i++)
                {
                    float zoomFactor = 1.0f + 0.1f * (i - 1); // 100%, 110%, 120%, ...

                    editor.ProcessPages = new int[] { i }; // select the current page (1‑based index)
                    editor.Zoom = zoomFactor;               // set the desired zoom for that page
                    editor.ApplyChanges();                  // apply the change to the document
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with per‑page zoom to '{outputPath}'.");
    }
}
