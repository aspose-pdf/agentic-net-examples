using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_preserved.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Step 1: Export existing annotations to an in‑memory XFDF stream
        using (PdfAnnotationEditor exportEditor = new PdfAnnotationEditor())
        {
            exportEditor.BindPdf(inputPdfPath);

            using (MemoryStream xfdfStream = new MemoryStream())
            {
                exportEditor.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // rewind for later import

                // Step 2: Perform content edits (e.g., replace text) while annotations are detached
                string tempEditedPath = Path.Combine(Path.GetDirectoryName(outputPdfPath) ?? ".", "temp_edited.pdf");

                using (PdfContentEditor contentEditor = new PdfContentEditor())
                {
                    contentEditor.BindPdf(inputPdfPath);

                    // Example edit: replace all occurrences of "OldText" with "NewText"
                    contentEditor.ReplaceText("OldText", "NewText");

                    // Save the edited PDF to a temporary file
                    contentEditor.Save(tempEditedPath);
                }

                // Step 3: Re‑import the previously saved annotations onto the edited PDF
                using (PdfAnnotationEditor importEditor = new PdfAnnotationEditor())
                {
                    importEditor.BindPdf(tempEditedPath);
                    importEditor.ImportAnnotationsFromXfdf(xfdfStream);
                    importEditor.Save(outputPdfPath);
                }

                // Clean up the temporary edited file
                try { File.Delete(tempEditedPath); } catch { /* ignore cleanup errors */ }
            }
        }

        Console.WriteLine($"Edited PDF with preserved annotations saved to '{outputPdfPath}'.");
    }
}