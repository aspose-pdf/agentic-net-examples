using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "FooterNote";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to obtain page dimensions.
        using (Document doc = new Document(inputPath))
        {
            // FormEditor works on the PDF document directly.
            FormEditor formEditor = new FormEditor();
            formEditor.BindPdf(doc); // bind the loaded document

            // Iterate over all pages and reposition the field.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Page size (points)
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Bottom‑margin rectangle for the field.
                double margin      = 20.0;   // 20 points from left/right and bottom
                double fieldHeight = 30.0;   // desired field height

                double llx = margin;                     // lower‑left X
                double lly = margin;                     // lower‑left Y (from bottom)
                double urx = pageWidth - margin;         // upper‑right X
                double ury = margin + fieldHeight;       // upper‑right Y

                // MoveField(name, llx, lly, urx, ury) repositions the field.
                // The method applies to the current page context; because we iterate
                // through each page, the field will be placed on every page.
                formEditor.MoveField(fieldName,
                                     (float)llx,
                                     (float)lly,
                                     (float)urx,
                                     (float)ury);
            }

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' moved to bottom margin on each page and saved to '{outputPath}'.");
    }
}