using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_submit.pdf";
        const string interactionLogPath = "interaction_log.xfdf";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Add a submit button that posts form data as XFDF (XML) to a URL
            // -----------------------------------------------------------------
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(pdfDoc);

                // Add a submit button named "SubmitBtn" on page 1.
                // Parameters: field name, page number, button label, target URL,
                // lower‑left X, lower‑left Y, upper‑right X, upper‑right Y.
                formEditor.AddSubmitBtn(
                    "SubmitBtn",
                    1,
                    "Submit",
                    "https://example.com/handler",
                    100, 500, 200, 550);

                // Configure the button to submit the form data as XFDF (XML).
                // SubmitFormFlag.Xfdf is the enum value that triggers XFDF export.
                formEditor.SetSubmitFlag("SubmitBtn", SubmitFormFlag.Xfdf);

                // Save the modified PDF (including the new button).
                formEditor.Save(outputPdfPath);
            }

            // ---------------------------------------------------------------
            // 2. Export the interaction log (annotations) to an XFDF XML file
            // ---------------------------------------------------------------
            using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
            {
                annotationEditor.BindPdf(pdfDoc);

                // Export all annotations (including the submit button action) to XFDF.
                using (FileStream fs = new FileStream(interactionLogPath, FileMode.Create, FileAccess.Write))
                {
                    annotationEditor.ExportAnnotationsToXfdf(fs);
                }
            }
        }

        Console.WriteLine($"Modified PDF saved to: {outputPdfPath}");
        Console.WriteLine($"Interaction log exported to: {interactionLogPath}");
    }
}