using System;
using System.IO;
using System.Drawing; // Needed for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input EPUB file and output EPUB file paths
        const string inputEpubPath  = "input.epub";
        const string outputEpubPath = "output.epub";

        // Verify input file exists
        if (!File.Exists(inputEpubPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputEpubPath}");
            return;
        }

        // Load the EPUB file as a PDF document (default A4 page size)
        using (Document pdfDoc = new Document(inputEpubPath, new EpubLoadOptions()))
        {
            // Initialize the PDF content editor facade and bind the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(pdfDoc);

            // Define the page where explanatory content will be inserted (1‑based index)
            int targetPage = 1;

            // Define the rectangle area (x, y, width, height) where the text will appear.
            // System.Drawing.Rectangle expects (x, y, width, height). The original
            // coordinates were (llx, lly, urx, ury) = (50, 750, 550, 800).
            // Hence width = 550 - 50 = 500, height = 800 - 750 = 50.
            System.Drawing.Rectangle textRect = new System.Drawing.Rectangle(50, 750, 500, 50);

            // Explanatory content to insert
            string explanatoryText = "This paragraph provides additional explanation for the content on this page.";

            // Insert the text using the facade.
            // Parameters: rectangle, text, font name, isBold, font color, page number
            editor.CreateText(textRect, explanatoryText, "Helvetica", false, "Black", targetPage);

            // After editing, retrieve the underlying Document and save it back as EPUB
            // using the appropriate save options.
            EpubSaveOptions epubSaveOptions = new EpubSaveOptions
            {
                // Optional: set a title for the resulting EPUB
                Title = "Modified EPUB with Explanatory Content"
            };

            pdfDoc.Save(outputEpubPath, epubSaveOptions);
        }

        Console.WriteLine($"EPUB successfully modified and saved to '{outputEpubPath}'.");
    }
}
