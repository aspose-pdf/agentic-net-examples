using System;
using System.IO;
using System.Drawing; // For System.Drawing.Rectangle used by PdfContentEditor
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // <-- Added namespace for PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string tempPdf = "temp_with_annotation.pdf";
        const string outputPdf = "output_with_subject.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Create a text annotation on page 1 using PdfContentEditor
        // ------------------------------------------------------------
        using (PdfContentEditor contentEditor = new PdfContentEditor())
        {
            // Bind the source PDF
            contentEditor.BindPdf(inputPdf);

            // Define the annotation rectangle (x, y, width, height) – System.Drawing.Rectangle is required here
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 100, 100);

            // Create a text (sticky‑note) annotation.
            // Parameters: rectangle, contents, title, open flag, icon name, page number
            contentEditor.CreateText(annotRect, "This is a sample note.", "Sample Title", true, "Note", 1);

            // Save the PDF that now contains the annotation
            contentEditor.Save(tempPdf);
        }

        // ------------------------------------------------------------
        // Step 2: Assign a Subject to the newly created annotation
        // ------------------------------------------------------------
        // Load the PDF that contains the annotation
        Document doc = new Document(tempPdf);
        // Get the first page (pages are 1‑based)
        Page page = doc.Pages[1];
        // Assuming the annotation we just added is the first one on the page
        if (page.Annotations.Count > 0 && page.Annotations[1] is TextAnnotation textAnnot)
        {
            textAnnot.Subject = "Category: Review";
        }
        else
        {
            Console.Error.WriteLine("No TextAnnotation found on page 1.");
        }
        // Save the final PDF with the Subject assigned
        doc.Save(outputPdf);

        Console.WriteLine($"Annotation subject assigned and saved to '{outputPdf}'.");
    }
}
