using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = "InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = "OutputPdfs";

        // Configuration: when true, annotations marked as read‑only are NOT flattened
        const bool skipReadOnlyAnnotations = true;

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Verify the input folder exists; if not, create it and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Creating it now. Place PDF files in this folder and re‑run the program.");
            Directory.CreateDirectory(inputFolder);
            return; // Nothing to process yet
        }

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Use PdfAnnotationEditor (Facade) to work with annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Access the underlying Document object
                Document doc = editor.Document;

                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate through all annotations on the current page
                    for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                    {
                        Annotation annotation = page.Annotations[annIndex];

                        // Determine if the annotation is read‑only via its Flags collection
                        bool isReadOnly = (annotation.Flags & AnnotationFlags.ReadOnly) == AnnotationFlags.ReadOnly;

                        // Skip flattening if the annotation is read‑only and the option is enabled
                        if (skipReadOnlyAnnotations && isReadOnly)
                            continue;

                        // Flatten the annotation – its visual content becomes part of the page
                        annotation.Flatten();
                    }
                }

                // Save the modified PDF to the output location
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed: {fileName}");
        }
    }
}
