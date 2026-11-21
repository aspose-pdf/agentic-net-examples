using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "Signature";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to obtain page dimensions (must be disposed)
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 5 exists
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document contains fewer than 5 pages.");
                return;
            }

            Page page = doc.Pages[5];
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Desired field size (exact dimensions)
            const float fieldWidth = 150f; // points
            const float fieldHeight = 50f; // points
            const float margin = 10f;      // points from right and bottom edges

            // Calculate lower‑right rectangle (origin is bottom‑left)
            float llx = (float)(pageWidth - fieldWidth - margin); // lower‑left X
            float lly = margin;                                 // lower‑left Y
            float urx = (float)(pageWidth - margin);            // upper‑right X
            float ury = margin + fieldHeight;                   // upper‑right Y

            // Reposition the existing field (if it exists) using FormEditor.MoveField
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPath);

                // Attempt to move the field. If the field does not exist on the document,
                // MoveField will throw; in that case we add a new signature field.
                try
                {
                    editor.MoveField(fieldName, llx, lly, urx, ury);
                }
                catch (Exception)
                {
                    // Field not present – add a new signature field on page 5
                    editor.AddField(FieldType.Signature, fieldName, 5, llx, lly, urx, ury);
                }

                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Field \"{fieldName}\" moved to lower right of page 5 and saved as \"{outputPath}\".");
    }
}
