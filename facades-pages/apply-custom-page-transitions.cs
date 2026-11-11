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

        // Load the PDF document (Document implements IDisposable but we keep it alive for the whole operation)
        Document doc = new Document(inputPath);

        // Apply a custom transition to each page based on its index using PdfPageEditor
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Target only the current page
                editor.ProcessPages = new int[] { i };

                if (i % 2 == 1)
                {
                    // Odd pages – Dissolve transition (enum value 5)
                    editor.TransitionType = 5; // TransitionType.Dissolve
                }
                else
                {
                    // Even pages – Split transition (enum value 1)
                    editor.TransitionType = 1; // TransitionType.Split
                    // Horizontal split is the default; if a direction property exists it can be set here.
                }

                // Duration is expressed in seconds
                editor.TransitionDuration = 1;
                editor.ApplyChanges();
            }
        }

        // Save the modified PDF to a new file
        doc.Save(outputPath);
        Console.WriteLine($"PDF with page transitions saved to '{outputPath}'.");
    }
}
