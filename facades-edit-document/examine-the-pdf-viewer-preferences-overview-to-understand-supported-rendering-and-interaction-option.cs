using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_modified.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PDF document (required for some operations, e.g., printing)
        // -----------------------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 2. Examine current viewer preferences using PdfContentEditor
            // -----------------------------------------------------------------
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc); // Bind the in‑memory Document instance

            int currentPrefs = editor.GetViewerPreference();

            Console.WriteLine("Current Viewer Preference Flags:");
            PrintViewerPreferenceFlags(currentPrefs);

            // -----------------------------------------------------------------
            // 3. Modify viewer preferences (example: hide menu bar and toolbar)
            // -----------------------------------------------------------------
            int newPrefs = currentPrefs |
                           ViewerPreference.HideMenubar |
                           ViewerPreference.HideToolbar;

            editor.ChangeViewerPreference(newPrefs);
            editor.Save(outputPdf); // Save the modified PDF

            Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
            editor.Close(); // Explicitly close the editor (optional, Dispose not required)
        }

        // -----------------------------------------------------------------
        // 4. Demonstrate printing the modified PDF with PdfViewer (optional)
        // -----------------------------------------------------------------
        PdfViewer viewer = new PdfViewer();
        try
        {
            viewer.AutoResize = true;          // Fit to printable area
            viewer.AutoRotate = true;          // Auto‑rotate pages if needed
            viewer.PrintPageDialog = false;    // Suppress page‑number dialog
            viewer.BindPdf(outputPdf);         // Load the modified PDF

            // Print to the default printer
            viewer.PrintDocument();
        }
        finally
        {
            viewer.Close(); // Ensure resources are released
        }
    }

    // Helper method to decode and display individual ViewerPreference flags
    static void PrintViewerPreferenceFlags(int flags)
    {
        // ViewerPreference defines many const int fields; we check a few common ones.
        if ((flags & ViewerPreference.HideMenubar) != 0)          Console.WriteLine("- HideMenubar");
        if ((flags & ViewerPreference.HideToolbar) != 0)          Console.WriteLine("- HideToolbar");
        if ((flags & ViewerPreference.HideWindowUI) != 0)         Console.WriteLine("- HideWindowUI");
        if ((flags & ViewerPreference.FitWindow) != 0)           Console.WriteLine("- FitWindow");
        if ((flags & ViewerPreference.CenterWindow) != 0)        Console.WriteLine("- CenterWindow");
        if ((flags & ViewerPreference.DisplayDocTitle) != 0)     Console.WriteLine("- DisplayDocTitle");
        if ((flags & ViewerPreference.PageModeUseOutlines) != 0) Console.WriteLine("- PageModeUseOutlines");
        if ((flags & ViewerPreference.PageLayoutTwoColumnLeft) != 0) Console.WriteLine("- PageLayoutTwoColumnLeft");
        // Add more checks as needed for other flags.
    }
}