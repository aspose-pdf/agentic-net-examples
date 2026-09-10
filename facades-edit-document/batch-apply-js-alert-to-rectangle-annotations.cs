using System;
using System.IO;
using System.Drawing; // for System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class BatchJavaScriptApplier
{
    // JavaScript code to be attached to each rectangle (square) annotation
    private const string JsCode = "app.alert('Aspose.Pdf.Rectangle clicked!');";

    static void Main()
    {
        // List of PDF files to process
        string[] inputFiles = new string[]
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_js.pdf");

            try
            {
                // Load the PDF document
                using (Document doc = new Document(inputPath))
                {
                    // Create a PdfContentEditor bound to the same document
                    using (PdfContentEditor editor = new PdfContentEditor())
                    {
                        editor.BindPdf(doc);

                        // Iterate through all pages (1‑based indexing)
                        for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                        {
                            Page page = doc.Pages[pageNum];

                            // Iterate over annotations on the current page
                            for (int i = 1; i <= page.Annotations.Count; i++)
                            {
                                Annotation ann = page.Annotations[i];

                                // Process only square (rectangle) annotations
                                if (ann is SquareAnnotation squareAnn)
                                {
                                    // Convert Aspose.Pdf.Rectangle (double) to System.Drawing.Rectangle (int)
                                    Aspose.Pdf.Rectangle pdfRect = squareAnn.Rect;
                                    int x = (int)Math.Round(pdfRect.LLX);
                                    int y = (int)Math.Round(pdfRect.LLY);
                                    int width = (int)Math.Round(pdfRect.URX - pdfRect.LLX);
                                    int height = (int)Math.Round(pdfRect.URY - pdfRect.LLY);
                                    System.Drawing.Rectangle drawRect = new System.Drawing.Rectangle(x, y, width, height);

                                    // Add JavaScript link over the same rectangle area (transparent border)
                                    editor.CreateJavaScriptLink(
                                        JsCode,
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

                Console.WriteLine($"Processed '{inputPath}' → '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
