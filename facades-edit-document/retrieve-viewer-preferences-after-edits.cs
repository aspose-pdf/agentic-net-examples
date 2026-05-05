using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfContentEditor, ViewerPreference

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the content editor and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // ---- First edit operation ----
        // Hide the menu bar
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);

        // Retrieve and display the current viewer preferences
        int prefAfterFirstChange = editor.GetViewerPreference();
        Console.WriteLine($"Viewer preferences after HideMenubar: 0x{prefAfterFirstChange:X}");

        // Save the document (incremental update is applied automatically)
        editor.Save(outputPath);

        // ---- Second edit operation ----
        // Set page mode to display outlines
        editor.ChangeViewerPreference(ViewerPreference.PageModeUseOutlines);

        // Retrieve and display the updated viewer preferences
        int prefAfterSecondChange = editor.GetViewerPreference();
        Console.WriteLine($"Viewer preferences after PageModeUseOutlines: 0x{prefAfterSecondChange:X}");

        // Save again to persist the second change
        editor.Save(outputPath);

        // Release resources
        editor.Close();
    }
}