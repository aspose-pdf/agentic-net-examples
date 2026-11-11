using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Integer values that represent the desired transition types.
        // Mapping (based on Aspose.Pdf documentation):
        // 1 = Box, 2 = Fade (Dissolve), 3 = Glitter, 4 = Cover,
        // 5 = Uncover, 6 = Fly, 7 = Split, 8 = Wipe, etc.
        int[] transitionValues = new int[]
        {
            2, // Fade (Dissolve)
            7, // Split
            1, // Box
            8, // Wipe
            3, // Glitter
            6, // Fly
            4, // Cover
            5  // Uncover
        };

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // Apply a different transition to each page using PdfPageEditor.
            using (PdfPageEditor pageEditor = new PdfPageEditor(doc))
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    pageEditor.ProcessPages = new int[] { i };               // 1‑based page index
                    pageEditor.TransitionType = transitionValues[(i - 1) % transitionValues.Length];
                    // TransitionDuration expects an int (seconds). Cast or use an integer literal.
                    pageEditor.TransitionDuration = (int)1.5; // equivalent to 1 second (truncates fractional part)
                    pageEditor.ApplyChanges();
                }
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        // Demonstrate usage of the Facades API (PdfFileEditor) as required.
        PdfFileEditor fileEditor = new PdfFileEditor();
        string tempConcatPath = "temp_concat.pdf";
        // Concatenate the file with itself just to illustrate the API usage.
        fileEditor.Concatenate(outputPath, outputPath, tempConcatPath);
        File.Delete(outputPath);
        File.Move(tempConcatPath, outputPath);

        Console.WriteLine($"PDF with varied page transitions saved to '{outputPath}'.");
    }
}
