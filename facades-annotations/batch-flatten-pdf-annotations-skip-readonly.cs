using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class BatchAnnotationFlattener
{
    static void Main(string[] args)
    {
        // ---------------------------------------------------------------------
        // Configuration / arguments handling
        // ---------------------------------------------------------------------
        // Expected arguments:
        //   0 – input folder (required)
        //   1 – output folder (required)
        //   2 – optional flag "--skip-readonly" (if present, read‑only annotations are NOT flattened)
        // If arguments are missing, sensible defaults are used and the user is informed.
        // ---------------------------------------------------------------------
        string inputFolder  = args.Length > 0 ? args[0] : @"C:\Batch\Input";
        string outputFolder = args.Length > 1 ? args[1] : @"C:\Batch\Output";
        bool skipReadOnly   = args.Length > 2 && args[2].Equals("--skip-readonly", StringComparison.OrdinalIgnoreCase);

        // Ensure the input folder exists – if it does not, create it and exit early.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Creating it now.");
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine("Place PDF files into the input folder and re‑run the program.");
            return; // nothing to process yet
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // ---------------------------------------------------------------------
        // Batch processing of PDFs
        // ---------------------------------------------------------------------
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_flattened.pdf");

            // CREATE: PdfAnnotationEditor instance
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            // LOAD: bind the PDF document to the editor
            editor.BindPdf(inputPath);

            // Iterate over all pages and annotations
            foreach (Page page in editor.Document.Pages)
            {
                // Iterate backwards to avoid index shift when flattening removes the annotation.
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    Annotation ann = page.Annotations[i];

                    // If the configuration says to skip read‑only annotations, honour it.
                    if (skipReadOnly && ann.Flags.HasFlag(AnnotationFlags.ReadOnly))
                        continue;

                    // Flatten the annotation (places its appearance on the page and removes the object)
                    ann.Flatten();
                }
            }

            // SAVE: write the processed PDF to the output path
            editor.Save(outputPath);

            // Clean up the facade (optional, as it does not implement IDisposable)
            editor.Close();

            Console.WriteLine($"Processed: {inputPath} → {outputPath}");
        }
    }
}
