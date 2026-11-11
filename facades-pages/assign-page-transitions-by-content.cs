using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Create a PdfPageEditor and bind the document (no using because it does not implement IDisposable)
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(doc);

        // Iterate through each page (1‑based indexing)
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            Page page = doc.Pages[i];

            // Determine if the page contains images
            bool hasImages = page.Resources.Images.Count > 0;

            // Determine if the page contains text
            TextAbsorber txtAbsorber = new TextAbsorber();
            page.Accept(txtAbsorber);
            bool hasText = !string.IsNullOrWhiteSpace(txtAbsorber.Text);

            // Choose a transition based on content type and map it to the integer value expected by PdfPageEditor
            int transitionTypeValue;
            if (hasImages && !hasText)
            {
                // Image‑only pages – use a Fade (Dissolve) transition. 2 = Dissolve/Fade
                transitionTypeValue = 2;
            }
            else if (hasText && !hasImages)
            {
                // Text‑only pages – use a Split transition. 9 = Split
                transitionTypeValue = 9;
            }
            else
            {
                // Mixed or empty pages – use a Blinds transition. 10 = Blinds
                transitionTypeValue = 10;
            }

            // Apply the transition to the current page using PdfPageEditor
            editor.ProcessPages = new int[] { i };          // target the current page
            editor.TransitionType = transitionTypeValue;    // integer value representing the transition
            editor.TransitionDuration = 1;                  // 1 second (adjust as needed)
        }

        // Save the modified PDF. All per‑page changes are now persisted in the bound Document.
        editor.Save(outputPath);

        Console.WriteLine($"PDF saved with page transitions to '{outputPath}'.");
    }
}
