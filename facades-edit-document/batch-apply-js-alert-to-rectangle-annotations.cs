using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class BatchJavaScriptApplier
{
    static void Main()
    {
        // List of PDF files to process
        string[] pdfFiles = new string[]
        {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
        };

        // JavaScript code to attach to each rectangle annotation
        const string jsCode = "app.alert('Aspose.Pdf.Rectangle clicked!');";

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Output file name (original name with suffix)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_js.pdf");

            // Use PdfContentEditor facade to edit annotations
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the source PDF
                editor.BindPdf(inputPath);

                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= editor.Document.Pages.Count; pageNum++)
                {
                    Page page = editor.Document.Pages[pageNum];

                    // Examine each annotation on the page
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // Target only rectangle (square) annotations
                        if (annotation is SquareAnnotation square)
                        {
                            // Calculate rectangle bounds as integers
                            int x = (int)square.Rect.LLX;
                            int y = (int)square.Rect.LLY;
                            int width = (int)(square.Rect.URX - square.Rect.LLX);
                            int height = (int)(square.Rect.URY - square.Rect.LLY);

                            // System.Drawing.Rectangle is required by CreateJavaScriptLink
                            System.Drawing.Rectangle drawRect = new System.Drawing.Rectangle(x, y, width, height);

                            // Use a transparent System.Drawing.Color so the original appearance is unchanged
                            System.Drawing.Color transparent = System.Drawing.Color.Transparent;

                            // Create a JavaScript link over the same rectangle area
                            editor.CreateJavaScriptLink(jsCode, drawRect, pageNum, transparent);
                        }
                    }
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed '{inputPath}' → '{outputPath}'");
        }
    }
}
