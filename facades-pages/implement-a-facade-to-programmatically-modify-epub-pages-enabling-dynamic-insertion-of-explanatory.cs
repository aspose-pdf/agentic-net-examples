using System;
using System.IO;
using System.Drawing; // Needed for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputEpub = "input.epub";
        const string outputEpub = "output.epub";

        if (!File.Exists(inputEpub))
        {
            Console.Error.WriteLine($"File not found: {inputEpub}");
            return;
        }

        // Load the EPUB file into a PDF Document (EPUB is converted to PDF internally)
        using (Document doc = new Document(inputEpub, new EpubLoadOptions()))
        {
            // Initialize the PdfContentEditor facade
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Insert explanatory text on each page
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Define the annotation rectangle (x, y, width, height)
                    // Original Aspose.Pdf.Rectangle used (left, bottom, right, top).
                    // Convert to System.Drawing.Rectangle: (x, y, width, height).
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                        50,               // x (left)
                        750,              // y (bottom in PDF coordinates)
                        500,              // width  = right - left (550 - 50)
                        50);              // height = top - bottom (800 - 750)

                    // Create a text annotation with the explanatory content
                    // Parameters: rectangle, contents, title, isOpen, icon, flags
                    editor.CreateText(
                        rect,
                        $"Explanation for page {i}",
                        $"Page {i} Note",
                        true,
                        "Note",
                        0);
                }

                // Save the modified document back to EPUB format
                EpubSaveOptions saveOptions = new EpubSaveOptions
                {
                    Title = "Modified EPUB with Explanations",
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };
                doc.Save(outputEpub, saveOptions);
            }
        }

        Console.WriteLine($"Modified EPUB saved to '{outputEpub}'.");
    }
}
