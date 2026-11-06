using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add an annotation, and validate integrity
        using (Document doc = new Document(inputPath))
        {
            // ---- Add a simple text annotation (cross‑platform) ----
            // Use fully qualified Rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(doc.Pages[1], rect)
            {
                Title    = "Sample Note",
                Contents = "This is a test annotation.",
                Open     = true,
                Icon     = TextIcon.Note
            };
            doc.Pages[1].Annotations.Add(annotation);

            // Validate the modified document
            bool isValid = ValidatePdfIntegrity(doc);
            Console.WriteLine($"PDF integrity after annotation: {(isValid ? "OK" : "Corrupted")}");

            // Save only if the document is valid
            if (isValid)
            {
                doc.Save(outputPath);
                Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Document is corrupted; not saved.");
            }
        }
    }

    // Validates PDF integrity by attempting to save to a memory stream.
    // Any exception during the save indicates a problem with the PDF structure.
    static bool ValidatePdfIntegrity(Document pdfDocument)
    {
        try
        {
            // Use a memory stream to avoid creating a file on disk.
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the document; if the PDF contains corrupted objects,
                // Aspose.Pdf will throw a PdfException.
                pdfDocument.Save(ms);
            }
            // No exception -> PDF is considered valid.
            return true;
        }
        catch (PdfException ex)
        {
            // Log the specific PDF error.
            Console.Error.WriteLine($"PDF validation error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            // Catch any other unexpected errors.
            Console.Error.WriteLine($"Unexpected error during validation: {ex.Message}");
            return false;
        }
    }
}