using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting EPUB file
        const string inputPdfPath = "input.pdf";
        const string outputEpubPath = "output.epub";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Aspose.Pdf (and some of its internal helpers) rely on GDI+ which is only
        // available on Windows out‑of‑the‑box. On non‑Windows platforms you must
        // install the native libgdiplus package. To avoid the "System.Drawing.Gdip"
        // TypeInitializationException we check the OS before touching any Aspose
        // APIs that may trigger GDI+ initialization.
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.Error.WriteLine(
                "The current platform does not provide GDI+. " +
                "On Linux/macOS install the native 'libgdiplus' package or run the " +
                "application on Windows.");
            return;
        }

        // Load the existing PDF, add a text annotation, and save as EPUB
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = pdfDoc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the TextAnnotation on the selected page
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note Title",                     // Title shown in the popup window
                Contents = "This is a text annotation.",    // The note text
                Color    = Aspose.Pdf.Color.Yellow,         // Annotation color
                Icon     = TextIcon.Note,                    // Icon displayed on the page
                Open     = true                              // Open the popup by default
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(textAnn);

            // Prepare EPUB save options (default constructor is sufficient for basic conversion)
            EpubSaveOptions epubOptions = new EpubSaveOptions();

            // Save the modified document as EPUB
            pdfDoc.Save(outputEpubPath, epubOptions);
        }

        Console.WriteLine($"Text annotation added and PDF saved as EPUB: '{outputEpubPath}'.");
    }
}
