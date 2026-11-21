using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfAnnotationEditor resides here

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string annotationName = "myAnnotation"; // name/id of the annotation to delete

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Create the annotation editor (facade API)
        Aspose.Pdf.Facades.PdfAnnotationEditor editor = new Aspose.Pdf.Facades.PdfAnnotationEditor();

        // Initial bind of the PDF document
        editor.BindPdf(inputPdfPath);

        bool deletionSucceeded = false;
        int attempt = 0;
        const int maxAttempts = 2; // original try + one retry

        while (!deletionSucceeded && attempt < maxAttempts)
        {
            attempt++;
            try
            {
                // Delete the annotation by its name (single‑argument overload)
                editor.DeleteAnnotation(annotationName);
                deletionSucceeded = true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Attempt {attempt} failed: {ex.Message}");

                // If we have remaining attempts, re‑bind the PDF and retry
                if (attempt < maxAttempts)
                {
                    editor.BindPdf(inputPdfPath);
                }
            }
        }

        // Save the modified PDF (whether deletion succeeded or not)
        editor.Save(outputPdfPath);
        Console.WriteLine($"Annotation deletion {(deletionSucceeded ? "succeeded" : "failed")} – saved to '{outputPdfPath}'.");
    }
}