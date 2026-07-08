using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    // Configuration: set to true to skip flattening of read‑only annotations
    private static readonly bool SkipReadOnlyAnnotations = true;

    static void Main()
    {
        // Folder containing PDFs to process
        string inputFolder = "InputFolder";
        // Folder where processed PDFs will be saved
        string outputFolder = "OutputFolder";

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Verify the input folder exists; if not, create it and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Creating it now. Place PDF files in this folder and re‑run the program.");
            Directory.CreateDirectory(inputFolder);
            return; // Nothing to process on first run
        }

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'." );
            return;
        }

        // Process each PDF file in the input folder
        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_flattened.pdf");

            // CREATE – PdfAnnotationEditor instance
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // LOAD – bind the source PDF
                editor.BindPdf(inputPath);

                // Access the underlying Document to iterate pages/annotations
                Document doc = editor.Document;

                // Iterate all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];

                    // Iterate annotations in reverse order to allow safe removal
                    for (int annIdx = page.Annotations.Count; annIdx >= 1; annIdx--)
                    {
                        Annotation ann = page.Annotations[annIdx];

                        // Skip flattening if the annotation is read‑only and the config flag is set
                        if (SkipReadOnlyAnnotations && (ann.Flags & AnnotationFlags.ReadOnly) != 0)
                            continue;

                        // FLATTEN – place annotation content directly on the page
                        ann.Flatten();
                    }
                }

                // SAVE – write the modified PDF to the output path
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed: {outputPath}");
        }
    }
}
