using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        // Verify that the PDF file exists before attempting to bind it.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: The file '{inputPath}' was not found. Please ensure the path is correct.");
            return;
        }

        // Use a using‑statement to guarantee that resources are released even if an exception occurs.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPath);

            // Get viewer preferences – the method returns an int, so keep it as int.
            int viewerPref = editor.GetViewerPreference();

            // Example checks for specific viewer preferences using bitwise operations.
            if ((viewerPref & ViewerPreference.HideMenubar) != 0)
            {
                Console.WriteLine("HideMenubar flag is set.");
            }

            if ((viewerPref & ViewerPreference.FitWindow) != 0)
            {
                Console.WriteLine("FitWindow flag is set.");
            }

            if ((viewerPref & ViewerPreference.PageModeUseOutlines) != 0)
            {
                Console.WriteLine("PageModeUseOutlines flag is set.");
            }
        }
    }
}
