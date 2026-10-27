using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Integer values that correspond to the Aspose.Pdf.TransitionType enum.
    // These values are based on the enum order in the library version that
    // does not expose the enum directly.
    private const int TransitionFade  = 11; // TransitionType.Fade
    private const int TransitionSplit = 7;  // TransitionType.Split
    private const int TransitionWipe  = 8;  // TransitionType.Wipe

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the document to obtain the page count (PdfPageEditor does not expose GetPageCount).
            Document doc = new Document(inputPath);
            int pageCount = doc.Pages.Count;

            // Initialise the PdfPageEditor and bind the source PDF.
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(inputPath);

            // Apply a different transition to each page based on its index modulo three.
            for (int i = 1; i <= pageCount; i++)
            {
                int transitionValue;
                switch (i % 3)
                {
                    case 0: // pages where index % 3 == 0 -> Fade
                        transitionValue = TransitionFade;
                        break;
                    case 1: // pages where index % 3 == 1 -> Split
                        transitionValue = TransitionSplit;
                        break;
                    default: // pages where index % 3 == 2 -> Wipe
                        transitionValue = TransitionWipe;
                        break;
                }

                // Specify the page to edit.
                editor.ProcessPages = new int[] { i };
                // Set the transition type using the integer value.
                editor.TransitionType = transitionValue;
                // Set the duration (seconds).
                editor.TransitionDuration = 1;
                // Apply the changes for the current page.
                editor.ApplyChanges();
            }

            // Save the modified PDF with the applied transitions.
            editor.Save(outputPath);
            Console.WriteLine($"PDF saved with page transitions to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
