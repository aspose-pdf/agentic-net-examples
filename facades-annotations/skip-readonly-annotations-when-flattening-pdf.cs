using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class BatchAnnotationFlattener
{
    // Set this flag to true to skip flattening annotations that are marked as read‑only.
    private static readonly bool SkipReadOnlyAnnotations = true;

    static void Main()
    {
        // Input folder containing PDFs to process
        const string inputFolder = @"C:\PdfBatch\Input";
        // Output folder for processed PDFs
        const string outputFolder = @"C:\PdfBatch\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document
                Document pdfDoc = new Document(inputPath);

                // Iterate through all pages and flatten annotations according to the flag
                foreach (Page page in pdfDoc.Pages)
                {
                    // Collect annotations to flatten (cannot modify collection while iterating)
                    var toFlatten = new List<Annotation>();
                    foreach (Annotation annot in page.Annotations)
                    {
                        // The read‑only state is represented by the ReadOnly flag in Annotation.Flags.
                        if (SkipReadOnlyAnnotations && annot is MarkupAnnotation markup &&
                            (markup.Flags & AnnotationFlags.ReadOnly) != 0)
                        {
                            // Skip read‑only annotation
                            continue;
                        }
                        toFlatten.Add(annot);
                    }

                    // Flatten the selected annotations
                    foreach (Annotation annot in toFlatten)
                    {
                        annot.Flatten();
                    }
                }

                // Save the processed PDF
                pdfDoc.Save(outputPath);

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
