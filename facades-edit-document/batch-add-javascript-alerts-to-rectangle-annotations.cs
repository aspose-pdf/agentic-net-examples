using System;
using System.IO;
using System.Drawing; // Required for PdfContentEditor rectangle and color parameters
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class BatchJavaScriptApplier
{
    static void Main()
    {
        // Input PDF files (adjust paths as needed)
        string[] inputFiles = new string[]
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // JavaScript code to be attached to each rectangle annotation
        const string jsCode = "app.alert('Rectangle clicked!');";

        // Output directory for processed PDFs
        string outputDir = "Processed";
        Directory.CreateDirectory(outputDir);

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            string outputPath = Path.Combine(outputDir,
                Path.GetFileNameWithoutExtension(inputPath) + "_js.pdf");

            // Load the document (read‑only) to inspect existing annotations
            using (Document doc = new Document(inputPath))
            {
                // Facade for editing content (adding JavaScript links)
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    // Bind the existing document to the editor
                    editor.BindPdf(doc);

                    // Iterate through all pages (1‑based indexing)
                    for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                    {
                        Page page = doc.Pages[pageNum];

                        // Examine each annotation on the page
                        foreach (Annotation ann in page.Annotations)
                        {
                            // Target only rectangle (square) annotations
                            if (ann is SquareAnnotation squareAnn)
                            {
                                // Convert Aspose.Pdf.Rectangle to System.Drawing.Rectangle
                                Aspose.Pdf.Rectangle pdfRect = squareAnn.Rect;
                                int width = (int)(pdfRect.URX - pdfRect.LLX);
                                int height = (int)(pdfRect.URY - pdfRect.LLY);
                                System.Drawing.Rectangle drawRect = new System.Drawing.Rectangle(
                                    (int)pdfRect.LLX,
                                    (int)pdfRect.LLY,
                                    width,
                                    height);

                                // Add a JavaScript link over the same rectangle area
                                // NOTE: CreateJavaScriptLink expects System.Drawing.Color for the border color.
                                // Use System.Drawing.Color.Transparent instead of Aspose.Pdf.Color.Transparent.
                                editor.CreateJavaScriptLink(
                                    jsCode,
                                    drawRect,
                                    pageNum,
                                    System.Drawing.Color.Transparent);
                            }
                        }
                    }

                    // Save the modified document
                    editor.Save(outputPath);
                }
            }

            Console.WriteLine($"Processed file saved to '{outputPath}'.");
        }
    }
}
