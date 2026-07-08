using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class AnnotationDiagnostic
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "annotated_output.pdf";
        const string logFile    = "annotation_workflow.log";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Open a StreamWriter for verbose logging
            using (StreamWriter logger = new StreamWriter(logFile, false))
            {
                logger.WriteLine($"[{DateTime.Now}] Loaded document \"{inputPdf}\". Page count: {doc.Pages.Count}");

                // Initialize PdfAnnotationEditor facade and bind the document
                PdfAnnotationEditor annotEditor = new PdfAnnotationEditor();
                annotEditor.BindPdf(doc);
                logger.WriteLine($"[{DateTime.Now}] PdfAnnotationEditor bound to document.");

                // Create a text annotation on the first page
                // Fully qualify Rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                TextAnnotation textAnn = new TextAnnotation(doc.Pages[1], rect)
                {
                    Title    = "Diagnostic Note",
                    Contents = "This annotation was added in diagnostic mode.",
                    Color    = Aspose.Pdf.Color.Yellow,
                    Open     = true,
                    Icon     = TextIcon.Note
                };
                doc.Pages[1].Annotations.Add(textAnn);
                logger.WriteLine($"[{DateTime.Now}] Added TextAnnotation to page 1 at {rect}.");

                // Export current annotations to an XFDF file (optional step for diagnostics)
                string xfdfPath = Path.ChangeExtension(outputPdf, ".xfdf");
                using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    annotEditor.ExportAnnotationsToXfdf(xfdfStream);
                }
                logger.WriteLine($"[{DateTime.Now}] Exported annotations to XFDF file \"{xfdfPath}\".");

                // Save the modified PDF
                doc.Save(outputPdf);
                logger.WriteLine($"[{DateTime.Now}] Saved annotated document as \"{outputPdf}\".");

                // Close the facade (optional, as it does not implement IDisposable)
                annotEditor.Close();
                logger.WriteLine($"[{DateTime.Now}] PdfAnnotationEditor closed.");
            }
        }

        Console.WriteLine("Annotation workflow completed. See log file for details.");
    }
}
