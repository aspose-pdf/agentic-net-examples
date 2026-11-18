using System;
using System.IO;
using Aspose.Pdf; // Core PDF API (Matrix lives here)

class MoveVectorGraphic
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "moved_vector.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page that contains the vector graphic (1‑based index)
            const int pageNumber = 1;
            if (pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} does not exist.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Vector graphics are stored as Form XObjects (XForm) in the page resources.
            XForm targetForm = null;
            foreach (XForm form in page.Resources.Forms)
            {
                // Simple heuristic: assume the XForm has a non‑null Matrix (most vector graphics do).
                if (form.Matrix != null)
                {
                    targetForm = form;
                    break;
                }
            }

            if (targetForm == null)
            {
                Console.Error.WriteLine("No vector graphic XForm found on the specified page.");
                return;
            }

            // Current transformation matrix (if any). If null, use identity matrix.
            Aspose.Pdf.Matrix currentMatrix = targetForm.Matrix ?? new Aspose.Pdf.Matrix();

            // Define translation offsets (e.g., move 100 points right and 50 points up)
            double offsetX = 100.0;
            double offsetY = 50.0;

            // Create a translation matrix
            Aspose.Pdf.Matrix translation = new Aspose.Pdf.Matrix(1, 0, 0, 1, offsetX, offsetY);

            // Combine the existing matrix with the translation.
            // Multiplication order matters: newMatrix = translation * currentMatrix
            Aspose.Pdf.Matrix newMatrix = translation.Multiply(currentMatrix);

            // Apply the new matrix to the XForm
            targetForm.Matrix = newMatrix;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Vector graphic moved and saved to '{outputPath}'.");
    }
}
