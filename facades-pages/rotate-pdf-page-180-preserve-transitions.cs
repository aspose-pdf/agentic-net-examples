using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_with_transition.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------------------------
            // 1️⃣ Apply a transition effect to the first page using PdfPageEditor.
            //    PdfPageEditor does not expose a Page.Transition property; instead the
            //    transition is set via the editor's integer based properties.
            // -------------------------------------------------------------------
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Target the first page (1‑based index)
                editor.ProcessPages = new int[] { 1 };

                // Transition type – Fade.  The integer value for Fade is 2 in the
                // Aspose.Pdf API (see PageTransitionType enum).  Using the integer
                // avoids version‑specific enum availability issues.
                editor.TransitionType = 2; // Fade transition

                // Duration in seconds (default unit is seconds)
                editor.TransitionDuration = 1; // 1 second

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // -------------------------------------------------------------------
            // 2️⃣ Rotate the same page by 180°.
            //    In recent Aspose.Pdf versions the rotation is performed directly on
            //    the Page object via the Rotation property (enum Rotation).
            // -------------------------------------------------------------------
            doc.Pages[1].Rotate = Rotation.on180;

            // Save the final document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page rotated and saved to '{outputPath}'.");
    }
}
