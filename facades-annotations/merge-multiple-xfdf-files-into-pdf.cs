using System;
using System.IO;
using Aspose.Pdf;                                 // Core PDF API
using Aspose.Pdf.Facades;                        // Facades for forms and annotations

class MergeXfdfExample
{
    static void Main()
    {
        // Input PDF that will receive the merged XFDF data
        const string inputPdfPath = "input.pdf";
        // Output PDF after importing merged XFDF
        const string outputPdfPath = "output_merged.pdf";
        // XFDF files to be merged
        string[] xfdfFiles = { "data1.xfdf", "data2.xfdf", "data3.xfdf" };

        // Validate files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        foreach (var xfdf in xfdfFiles)
        {
            if (!File.Exists(xfdf))
            {
                Console.Error.WriteLine($"XFDF file not found: {xfdf}");
                return;
            }
        }

        // -----------------------------------------------------------------
        // Step 1: Create a temporary PDF document to accumulate all XFDF data
        // -----------------------------------------------------------------
        using (Aspose.Pdf.Document tempDoc = new Aspose.Pdf.Document())
        {
            // Add a single blank page – required for the annotation editor
            tempDoc.Pages.Add();

            // Bind the temporary document to the annotation editor
            using (Aspose.Pdf.Facades.PdfAnnotationEditor annotEditor = new Aspose.Pdf.Facades.PdfAnnotationEditor(tempDoc))
            {
                // Import each XFDF file into the temporary document
                foreach (var xfdfPath in xfdfFiles)
                {
                    annotEditor.ImportAnnotationsFromXfdf(xfdfPath);
                }

                // Export the combined annotations to an in‑memory XFDF stream
                using (MemoryStream combinedXfdfStream = new MemoryStream())
                {
                    annotEditor.ExportAnnotationsToXfdf(combinedXfdfStream);
                    combinedXfdfStream.Position = 0; // Reset for reading

                    // ---------------------------------------------------------
                    // Step 2: Load the target PDF and import the merged XFDF data
                    // ---------------------------------------------------------
                    using (Aspose.Pdf.Document targetDoc = new Aspose.Pdf.Document(inputPdfPath))
                    {
                        // Use the Form facade to import field values from XFDF
                        using (Aspose.Pdf.Facades.Form formFacade = new Aspose.Pdf.Facades.Form(targetDoc))
                        {
                            formFacade.ImportXfdf(combinedXfdfStream);
                        }

                        // Save the resulting PDF
                        targetDoc.Save(outputPdfPath);
                    }
                }
            }
        }

        Console.WriteLine($"Merged XFDF imported and saved to '{outputPdfPath}'.");
    }
}