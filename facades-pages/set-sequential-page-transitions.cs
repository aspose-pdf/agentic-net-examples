using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // PdfPageEditor is the Facade class that allows setting page transitions
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Define which pages will receive which transition.
                // Transition types are specified by their integer values:
                // 4 = Cover, 6 = Fade, 8 = Fly, 9 = Push (examples)
                int[] pageNumbers      = { 1, 2, 3, 4 };
                int[] transitionValues = { 4, 6, 8, 9 };

                for (int i = 0; i < pageNumbers.Length; i++)
                {
                    // Apply the transition to a single page (1‑based indexing)
                    editor.ProcessPages = new int[] { pageNumbers[i] };
                    editor.TransitionType = transitionValues[i];
                    editor.TransitionDuration = 1; // duration in seconds
                    editor.ApplyChanges();
                }

                // Persist the changes to a new file
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with page transitions saved to '{outputPath}'.");
    }
}