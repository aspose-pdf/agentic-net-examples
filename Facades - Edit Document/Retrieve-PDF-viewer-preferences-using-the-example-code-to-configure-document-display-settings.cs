using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "example.pdf";

        // Verify that the input file exists before attempting to bind it
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file '{inputPath}' not found. Please place the PDF in the working directory or provide a correct path.");
            return;
        }

        // Use a using‑statement to ensure the PdfContentEditor is disposed correctly
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(inputPath);

            // Retrieve the current viewer preferences as a bitmask
            int prefValue = editor.GetViewerPreference();

            // Example: check if the document is set to hide the menu bar
            if ((prefValue & (int)ViewerPreference.HideMenubar) != 0)
            {
                Console.WriteLine("Menu bar is hidden.");
            }
            else
            {
                Console.WriteLine("Menu bar is visible.");
            }

            // Example: check if the document uses the outline view mode
            if ((prefValue & (int)ViewerPreference.PageModeUseOutlines) != 0)
            {
                Console.WriteLine("Page mode: Use Outlines.");
            }

            // Optionally modify viewer preferences (e.g., hide the toolbar)
            editor.ChangeViewerPreference(ViewerPreference.HideToolbar);

            // Save the modified PDF to a new file
            const string outputPath = "example_modified.pdf";
            editor.Save(outputPath);

            // No need to call Close() explicitly – the using block will dispose the editor
            Console.WriteLine($"Viewer preferences retrieved and saved to '{outputPath}'.");
        }
    }
}
