using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for HtmlSaveOptions

class ExportExample
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "input.pdf";

        // Output files
        const string outputPdfPath  = "output.pdf";
        const string outputHtmlPath = "output.html";
        const string outputStreamPath = "output_stream.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document – wrapped in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Save the document as a PDF file (no stream needed)
            // -----------------------------------------------------------------
            doc.Save(outputPdfPath);

            // -----------------------------------------------------------------
            // 2. Save the document as HTML – requires explicit HtmlSaveOptions
            // -----------------------------------------------------------------
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                PartsEmbeddingMode     = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };
            doc.Save(outputHtmlPath, htmlOptions);

            // -----------------------------------------------------------------
            // 3. Export the PDF to a stream and write the stream to a file.
            //    The FileStream is closed automatically by the using block.
            // -----------------------------------------------------------------
            using (FileStream outputStream = new FileStream(outputStreamPath, FileMode.Create, FileAccess.Write))
            {
                // Save the document into the provided stream.
                // This writes a PDF byte stream to the file.
                doc.Save(outputStream);
                // No explicit call to outputStream.Close() is required;
                // the using statement ensures disposal.
            }

            // -----------------------------------------------------------------
            // 4. (Optional) Export each page as an individual PDF stream.
            //    Demonstrates per‑page stream handling.
            // -----------------------------------------------------------------
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                string pagePdfPath = $"page_{pageIndex}.pdf";

                using (FileStream pageStream = new FileStream(pagePdfPath, FileMode.Create, FileAccess.Write))
                {
                    // Create a temporary document containing only the current page.
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the selected page to the new document.
                        singlePageDoc.Pages.Add(doc.Pages[pageIndex]);

                        // Save the single‑page document into the stream.
                        singlePageDoc.Save(pageStream);
                    }
                }
            }
        }

        Console.WriteLine("Export operations completed. All FileStreams have been closed.");
    }
}