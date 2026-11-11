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

        // Load the document to obtain the page count
        Document doc = new Document(inputPath);
        int pageCount = doc.Pages.Count;
        doc.Dispose(); // No longer needed after we have the count

        // Use PdfPageEditor because the older Aspose.Pdf version does not expose the TransitionType enum.
        // TransitionType is set via its integer value (see Aspose.Pdf documentation for the mapping).
        // Mapping used here:
        //   Box      -> 0
        //   Fade     -> 3
        //   Dissolve -> 9
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            for (int i = 1; i <= pageCount; i++)
            {
                int transitionInt;
                switch (i % 3)
                {
                    case 0: // Fade
                        transitionInt = 3;
                        break;
                    case 1: // Box
                        transitionInt = 0;
                        break;
                    default: // Dissolve
                        transitionInt = 9;
                        break;
                }

                editor.ProcessPages = new int[] { i }; // 1‑based page index
                editor.TransitionType = transitionInt; // integer representation of the transition
                editor.TransitionDuration = 1; // 1 second
                editor.ApplyChanges();
            }

            editor.Save(outputPath);
        }

        Console.WriteLine($"Transitions applied and saved to '{outputPath}'.");
    }
}
