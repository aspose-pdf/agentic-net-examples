using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class MergeXfdfExample
{
    static void Main()
    {
        // Input XFDF files to be merged
        string[] xfdfFiles = new string[]
        {
            "annotations1.xfdf",
            "annotations2.xfdf",
            "annotations3.xfdf"
        };

        // Path for the merged XFDF output
        const string mergedXfdfPath = "merged_output.xfdf";

        // Create a minimal PDF document (one blank page) to serve as a container
        using (Document pdfDoc = new Document())
        {
            // Add a blank page – required because the editor works on a PDF document
            pdfDoc.Pages.Add();

            // Initialize the annotation editor and bind it to the PDF document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(pdfDoc);

                // Import annotations from each XFDF file into the PDF document
                foreach (string xfdfPath in xfdfFiles)
                {
                    if (!File.Exists(xfdfPath))
                    {
                        Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
                        continue;
                    }

                    // Import all annotations from the current XFDF file
                    editor.ImportAnnotationsFromXfdf(xfdfPath);
                }

                // Export the combined annotations to a single XFDF file
                using (FileStream outStream = new FileStream(mergedXfdfPath, FileMode.Create, FileAccess.Write))
                {
                    editor.ExportAnnotationsToXfdf(outStream);
                }

                // (Optional) Save the temporary PDF if you need to inspect the result
                // pdfDoc.Save("temp_with_merged_annotations.pdf");
            }
        }

        Console.WriteLine($"Merged XFDF saved to '{mergedXfdfPath}'.");
    }
}