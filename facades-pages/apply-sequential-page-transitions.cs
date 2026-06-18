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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor and bind the document
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Define a set of transition types to apply sequentially
            int[] transitions = new int[]
            {
                PdfPageEditor.BLINDH,
                PdfPageEditor.BLINDV,
                PdfPageEditor.BTWIPE,
                PdfPageEditor.DGLITTER,
                PdfPageEditor.DISSOLVE,
                PdfPageEditor.INBOX,
                PdfPageEditor.LRGLITTER,
                PdfPageEditor.LRWIPE,
                PdfPageEditor.OUTBOX,
                PdfPageEditor.RLWIPE,
                PdfPageEditor.SPLITHIN,
                PdfPageEditor.SPLITHOUT,
                PdfPageEditor.SPLITVIN,
                PdfPageEditor.SPLITVOUT,
                PdfPageEditor.TBGLITTER,
                PdfPageEditor.TBWIPE
            };

            // Apply a different transition to each page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Select transition based on page index
                int transition = transitions[(i - 1) % transitions.Length];

                // Specify the page to edit
                editor.ProcessPages = new int[] { i };

                // Set transition type and duration (seconds)
                editor.TransitionType = transition;
                editor.TransitionDuration = 2; // 2 seconds per transition

                // Apply the changes to the current page
                editor.ApplyChanges();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with transitions saved to '{outputPath}'.");
    }
}