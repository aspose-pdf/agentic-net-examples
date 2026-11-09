using System;
using System.IO;
using System.Drawing;               // for System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class BatchProcessPdf
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\Pdf\Input";
        // Folder where processed PDFs will be written
        const string outputFolder = @"C:\Pdf\Output";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Standard disclaimer text to add as a text annotation
        const string disclaimerTitle = "Disclaimer";
        const string disclaimerContent = "This document is confidential and intended solely for the designated recipient.";

        // Iterate over all PDF files in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string finalPath = Path.Combine(outputFolder, fileName + "_final.pdf");

            // ---------------------------------------------------------------
            // 1. Load the PDF, remove all attachments, and add the disclaimer
            // ---------------------------------------------------------------
            Document doc = new Document(pdfPath);

            // Remove every embedded file (attachment) from the PDF.
            // EmbeddedFileCollection uses 1‑based indexing and does not provide a Clear() method.
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                {
                    var fileSpec = doc.EmbeddedFiles[i];
                    if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                    {
                        doc.EmbeddedFiles.Delete(fileSpec.Name);
                    }
                }
            }

            // ---------------------------------------------------------------
            // 2. Add a standardized disclaimer annotation to the first page
            // ---------------------------------------------------------------
            // Define the rectangle where the annotation will appear.
            // System.Drawing.Rectangle uses (x, y, width, height) where (x, y) is the upper‑left corner.
            // Aspose.Pdf.Rectangle expects lower‑left (llx, lly) and upper‑right (urx, ury).
            System.Drawing.Rectangle sysRect = new System.Drawing.Rectangle(50, 750, 500, 100);
            Aspose.Pdf.Rectangle aspRect = new Aspose.Pdf.Rectangle(
                sysRect.X,
                sysRect.Y,
                sysRect.X + sysRect.Width,
                sysRect.Y + sysRect.Height);

            // Create and configure the text annotation.
            Page firstPage = doc.Pages[1]; // Pages collection is 1‑based.
            TextAnnotation txtAnn = new TextAnnotation(firstPage, aspRect)
            {
                Title = disclaimerTitle,
                Contents = disclaimerContent
                // Optional visual tweaks (background color, border, etc.) can be added here.
                // Example: Color = Aspose.Pdf.Color.Yellow;
            };
            firstPage.Annotations.Add(txtAnn);

            // Save the processed PDF.
            doc.Save(finalPath);

            Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)} → {Path.GetFileName(finalPath)}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
