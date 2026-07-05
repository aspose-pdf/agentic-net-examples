using System;
using System.IO;
using System.Drawing; // for System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class BatchJavaScriptApplier
{
    static void Main()
    {
        // List of PDF files to process
        string[] inputFiles = new string[]
        {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
        };

        // JavaScript code to attach to each rectangle annotation
        const string jsCode = "app.alert('Aspose.Pdf.Rectangle clicked!');";

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Prepare output file name
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? "",
                Path.GetFileNameWithoutExtension(inputPath) + "_js.pdf");

            // Load the document – needed to enumerate pages and annotations
            using (Document doc = new Document(inputPath))
            {
                // Bind the PDF to the editor facade (modifies the same file in memory)
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    editor.BindPdf(inputPath);

                    // Iterate through every page and its annotations
                    for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                    {
                        Page page = doc.Pages[pageNumber];
                        foreach (Annotation ann in page.Annotations)
                        {
                            // We are interested only in rectangle (square) annotations
                            if (ann.AnnotationType != AnnotationType.Square)
                                continue;

                            // Annotation rectangle in Aspose.Pdf coordinates
                            Aspose.Pdf.Rectangle aspRect = ann.Rect;

                            // Convert to System.Drawing.Rectangle required by CreateJavaScriptLink
                            System.Drawing.Rectangle sysRect = new System.Drawing.Rectangle(
                                (int)aspRect.LLX,
                                (int)aspRect.LLY,
                                (int)(aspRect.URX - aspRect.LLX),
                                (int)(aspRect.URY - aspRect.LLY));

                            // Add a transparent JavaScript link over the same area
                            // NOTE: CreateJavaScriptLink expects System.Drawing.Color for the border color.
                            editor.CreateJavaScriptLink(
                                jsCode,
                                sysRect,
                                pageNumber,
                                System.Drawing.Color.Transparent);
                        }
                    }

                    // Save the modified PDF
                    editor.Save(outputPath);
                }
            }

            Console.WriteLine($"Processed '{inputPath}' → '{outputPath}'");
        }
    }
}
