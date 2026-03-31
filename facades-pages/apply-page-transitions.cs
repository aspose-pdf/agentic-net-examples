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

        using (Document doc = new Document(inputPath))
        {
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    // Edit only the current page
                    editor.ProcessPages = new int[] { pageIndex };

                    // Choose a transition based on the page index
                    if (pageIndex % 3 == 0)
                    {
                        editor.TransitionType = PdfPageEditor.SPLITVOUT; // Out vertical split
                    }
                    else if (pageIndex % 2 == 0)
                    {
                        editor.TransitionType = PdfPageEditor.BLINDV; // Vertical blinds
                    }
                    else
                    {
                        editor.TransitionType = PdfPageEditor.DISSOLVE; // Dissolve effect
                    }

                    // Set transition duration (in seconds)
                    editor.TransitionDuration = 2;

                    // Apply the changes for this page
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"PDF with custom transitions saved to '{outputPath}'.");
        }
    }
}
