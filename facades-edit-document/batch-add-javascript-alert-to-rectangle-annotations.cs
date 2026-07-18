using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

// Aliases to avoid ambiguity between System.Drawing and Aspose.Pdf types
using PdfRect = Aspose.Pdf.Rectangle;
using PdfColor = Aspose.Pdf.Color;
using SysRect = System.Drawing.Rectangle;
using SysColor = System.Drawing.Color;

class BatchJavaScriptApplier
{
    // JavaScript code to be added to each rectangle annotation
    private const string JsCode = "app.alert('Aspose.Pdf.Rectangle clicked!');";

    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = @"C:\InputPdfs";
        // Folder where the modified PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process every PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the document to read existing rectangle annotations
                using (Document doc = new Document(inputPath))
                {
                    // Prepare the content editor facade for the same document
                    using (PdfContentEditor editor = new PdfContentEditor())
                    {
                        editor.BindPdf(doc);

                        // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                        for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                        {
                            Page page = doc.Pages[pageNum];

                            // Examine each annotation on the page
                            foreach (Annotation ann in page.Annotations)
                            {
                                // Aspose.Pdf rectangle annotations are represented by SquareAnnotation
                                if (ann is SquareAnnotation squareAnn)
                                {
                                    // Get the rectangle that defines the annotation (Aspose.Pdf.Rectangle)
                                    PdfRect pdfRect = squareAnn.Rect;

                                    // Convert to System.Drawing.Rectangle for the CreateJavaScriptLink method
                                    int x = (int)pdfRect.LLX;
                                    int y = (int)pdfRect.LLY;
                                    int width = (int)(pdfRect.URX - pdfRect.LLX);
                                    int height = (int)(pdfRect.URY - pdfRect.LLY);
                                    SysRect drawRect = new SysRect(x, y, width, height);

                                    // Add a JavaScript link over the same rectangle area
                                    editor.CreateJavaScriptLink(
                                        JsCode,          // JavaScript to execute
                                        drawRect,        // Clickable rectangle (System.Drawing.Rectangle)
                                        pageNum,         // Page where the rectangle resides
                                        SysColor.Blue); // Visual color for the link rectangle (System.Drawing.Color)
                                }
                            }
                        }

                        // Save the modified document to the output path
                        editor.Save(outputPath);
                    }
                }

                Console.WriteLine($"Processed: {fileName} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}
