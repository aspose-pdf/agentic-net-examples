using System;
using System.IO;
using System.Drawing; // Required for CreateJavaScriptLink color parameter
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class BatchJavaScriptApplier
{
    // JavaScript code to be attached to each rectangle annotation
    private const string JsCode = "app.alert('Rectangle clicked!');";

    static void Main()
    {
        // List of PDF files to process
        string[] pdfFiles = {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Output file name (you can change the naming scheme as needed)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_js.pdf");

            // Open the document to read existing rectangle annotations
            using (Document doc = new Document(inputPath))
            {
                // Prepare the facade for editing annotations
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    // Bind the same document instance to the editor
                    editor.BindPdf(doc);

                    // Iterate through all pages (1‑based indexing)
                    for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                    {
                        Page page = doc.Pages[pageNum];
                        // Iterate over annotations on the current page
                        foreach (Annotation annot in page.Annotations)
                        {
                            // Target only rectangle (square) annotations
                            if (annot is SquareAnnotation square)
                            {
                                // Convert Aspose.Pdf.Rectangle to System.Drawing.Rectangle
                                Aspose.Pdf.Rectangle pdfRect = square.Rect;
                                int x = (int)pdfRect.LLX;
                                int y = (int)pdfRect.LLY;
                                int width = (int)(pdfRect.URX - pdfRect.LLX);
                                int height = (int)(pdfRect.URY - pdfRect.LLY);
                                System.Drawing.Rectangle drawRect = new System.Drawing.Rectangle(x, y, width, height);

                                // Add JavaScript link on top of the rectangle area
                                editor.CreateJavaScriptLink(
                                    JsCode,          // JavaScript code
                                    drawRect,        // Clickable rectangle
                                    pageNum,         // Original page number
                                    System.Drawing.Color.Red); // Highlight color (System.Drawing.Color)
                            }
                        }
                    }

                    // Save the modified document
                    editor.Save(outputPath);
                }
            }

            Console.WriteLine($"Processed '{inputPath}' → '{outputPath}'");
        }
    }
}
