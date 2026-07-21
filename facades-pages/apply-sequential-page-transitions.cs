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
            // Initialize the page editor with the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Define a sequence of transition types (one per page)
            int[] transitionTypes = new int[]
            {
                PdfPageEditor.BLINDH,      // Page 1
                PdfPageEditor.DISSOLVE,   // Page 2
                PdfPageEditor.LRWIPE,     // Page 3
                PdfPageEditor.SPLITHOUT,  // Page 4
                PdfPageEditor.TBGLITTER   // Page 5
                // Add more types as needed
            };

            // Apply transitions sequentially across pages
            for (int i = 1; i <= doc.Pages.Count && i <= transitionTypes.Length; i++)
            {
                // Edit only the current page
                editor.ProcessPages = new int[] { i };

                // Set the transition type and duration for this page
                editor.TransitionType = transitionTypes[i - 1];
                editor.TransitionDuration = 2; // duration in seconds

                // Apply the changes to the page
                editor.ApplyChanges();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transitions applied and saved to '{outputPath}'.");
    }
}