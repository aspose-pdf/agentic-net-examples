using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class MergeXfdfExample
{
    static void Main()
    {
        // Input PDF that will receive the merged XFDF data
        const string inputPdfPath = "input.pdf";
        // Output PDF after importing merged XFDF
        const string outputPdfPath = "output_merged.pdf";
        // List of XFDF files to merge
        List<string> xfdfFiles = new List<string>
        {
            "form1.xfdf",
            "form2.xfdf",
            "annotations.xfdf"
        };

        // -----------------------------------------------------------------
        // Ensure the source PDF exists – create a minimal one if it does not.
        // -----------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (Document placeholder = new Document())
            {
                // Add a single blank page so that the document is valid.
                placeholder.Pages.Add();
                placeholder.Save(inputPdfPath);
                Console.WriteLine($"Placeholder PDF created at '{inputPdfPath}'.");
            }
        }

        // -----------------------------------------------------------------
        // Directly import each XFDF file into the target PDF.
        // This avoids the need for a temporary document and prevents the
        // ObjectDisposedException that occurred when exporting from a
        // disposed temporary document.
        // -----------------------------------------------------------------
        using (Document targetPdf = new Document(inputPdfPath))
        {
            foreach (string xfdfPath in xfdfFiles)
            {
                if (!File.Exists(xfdfPath))
                {
                    Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
                    continue;
                }

                // Import field values (Form facade)
                using (FileStream fldStream = File.OpenRead(xfdfPath))
                using (Form formFacade = new Form(targetPdf))
                {
                    formFacade.ImportXfdf(fldStream);
                }

                // Import annotations (PdfAnnotationEditor facade)
                using (FileStream annStream = File.OpenRead(xfdfPath))
                using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor(targetPdf))
                {
                    annotEditor.ImportAnnotationsFromXfdf(annStream);
                }
            }

            // Save the updated PDF
            targetPdf.Save(outputPdfPath);
        }

        Console.WriteLine($"Merged XFDF imported and saved to '{outputPdfPath}'.");
    }
}
