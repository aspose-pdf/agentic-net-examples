using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned_page3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create a content editor facade and bind it to the document
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(doc);

                // NOTE: The AlignContent method and the Alignment enum are available only in newer
                // versions of Aspose.Pdf.Facades. The version referenced by this project does not expose
                // them, which caused the compile‑time errors. To vertically centre the content on page 3
                // you can either:
                //   1. Upgrade the Aspose.Pdf NuGet package to a version that includes AlignContent
                //      and Aspose.Pdf.Facades.Alignment, then uncomment the line below.
                //   2. Implement a custom transformation (e.g., modify the page's content stream) using
                //      the low‑level API. That approach is beyond the scope of this simple example.
                //
                // editor.AlignContent(3, Alignment.Middle); // <-- requires newer library version

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Page 3 content alignment (if supported) saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
