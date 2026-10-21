using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated_output.pdf";
        const string logPath    = "annotation_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open a verbose log file
        using (StreamWriter log = new StreamWriter(logPath, false))
        {
            try
            {
                log.WriteLine($"{DateTime.Now}: Starting annotation workflow.");

                // Load the source PDF document
                using (Document pdfDoc = new Document(inputPath))
                {
                    log.WriteLine($"{DateTime.Now}: PDF document loaded from '{inputPath}'.");

                    // Create the Facade editor for annotations
                    using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
                    {
                        annotEditor.BindPdf(pdfDoc);
                        log.WriteLine($"{DateTime.Now}: PdfAnnotationEditor bound to document.");

                        // Define the annotation rectangle (llx, lly, urx, ury)
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                        log.WriteLine($"{DateTime.Now}: Annotation rectangle defined: {rect}.");

                        // Create a text annotation attached to page 1
                        TextAnnotation txtAnnot = new TextAnnotation(pdfDoc.Pages[1], rect)
                        {
                            Title    = "Diagnostic Note",
                            Contents = "This annotation was added in diagnostic mode.",
                            Open     = true,
                            Icon     = TextIcon.Note
                        };
                        log.WriteLine($"{DateTime.Now}: TextAnnotation instance created.");

                        // Add (modify) the annotation to the document using ModifyAnnotations
                        annotEditor.ModifyAnnotations(1, pdfDoc.Pages.Count, txtAnnot);
                        log.WriteLine($"{DateTime.Now}: Annotation added to page 1 via ModifyAnnotations.");
                    }

                    // Save the modified PDF
                    pdfDoc.Save(outputPath);
                    log.WriteLine($"{DateTime.Now}: Annotated PDF saved to '{outputPath}'.");
                }

                log.WriteLine($"{DateTime.Now}: Annotation workflow completed successfully.");
            }
            catch (Exception ex)
            {
                log.WriteLine($"{DateTime.Now}: ERROR - {ex.GetType().Name}: {ex.Message}");
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
