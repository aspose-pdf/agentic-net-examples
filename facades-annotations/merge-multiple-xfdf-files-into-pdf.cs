using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class MergeXfdfAndImport
{
    static void Main()
    {
        // Input PDF to which the merged XFDF will be applied
        const string inputPdfPath  = "input.pdf";
        // Output PDF after importing merged XFDF
        const string outputPdfPath = "output.pdf";
        // XFDF files to be merged
        string[] xfdfFiles = { "form1.xfdf", "form2.xfdf", "comments.xfdf" };

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        foreach (var xfdf in xfdfFiles)
        {
            if (!File.Exists(xfdf))
            {
                Console.Error.WriteLine($"XFDF not found: {xfdf}");
                return;
            }
        }

        // -----------------------------------------------------------------
        // Step 1: Merge all XFDF files into a single XFDF stream.
        // -----------------------------------------------------------------
        // Create a temporary PDF document that will hold the combined
        // annotations/fields. A blank page is added because some Aspose.Pdf
        // operations expect at least one page.
        using (Document tempDoc = new Document())
        {
            // Ensure the document has a page (required for annotation handling)
            tempDoc.Pages.Add();

            // Import each XFDF file into the temporary document.
            foreach (string xfdfPath in xfdfFiles)
            {
                // Open the XFDF file as a read‑only stream.
                using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                {
                    // Import annotations from the XFDF into the document.
                    // If the XFDF also contains form field values, they are
                    // imported as well via ReadFields.
                    XfdfReader.ReadAnnotations(xfdfStream, tempDoc);
                    // Reset the stream position to reuse it for field import.
                    xfdfStream.Position = 0;
                    XfdfReader.ReadFields(xfdfStream, tempDoc);
                }
            }

            // Export the merged content to an in‑memory XFDF stream.
            using (MemoryStream mergedXfdf = new MemoryStream())
            {
                tempDoc.ExportAnnotationsToXfdf(mergedXfdf);
                mergedXfdf.Position = 0; // rewind for reading

                // -----------------------------------------------------------------
                // Step 2: Import the merged XFDF into the target PDF.
                // -----------------------------------------------------------------
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the existing PDF.
                    editor.BindPdf(inputPdfPath);

                    // Import all annotations and form fields from the merged XFDF.
                    editor.ImportAnnotationsFromXfdf(mergedXfdf);

                    // Save the resulting PDF.
                    editor.Save(outputPdfPath);
                }
            }
        }

        Console.WriteLine($"Merged XFDF imported successfully. Output saved to '{outputPdfPath}'.");
    }
}