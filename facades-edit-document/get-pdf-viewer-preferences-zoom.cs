using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // for RuntimeBinderException (optional, can be omitted)

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // ViewerPreferences were added in later Aspose.Pdf versions.  To keep the code
        // compilable with older versions we access the member via dynamic and handle the
        // possible RuntimeBinderException at runtime.
        try
        {
            dynamic doc = pdfDocument; // bypass compile‑time member check
            var viewer = doc.ViewerPreferences; // may throw RuntimeBinderException if not present

            // Page layout (e.g., SinglePage, OneColumn, TwoColumnLeft, etc.)
            Console.WriteLine($"Page Layout: {viewer.PageLayout}");

            // Zoom factor (percentage) – some versions expose it as an integer, others as double.
            // Use Convert.ToInt32 to normalise the output.
            Console.WriteLine($"Zoom: {Convert.ToInt32(viewer.Zoom)}%");
            Console.WriteLine($"Zoom Mode: {viewer.ZoomMode}");

            // Additional viewer preferences (optional)
            Console.WriteLine($"Hide Toolbar: {viewer.HideToolbar}");
            Console.WriteLine($"Hide Menubar: {viewer.HideMenubar}");
            Console.WriteLine($"Display Document Title: {viewer.DisplayDocTitle}");
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
        }
        catch (Exception ex)
        {
            // Any other unexpected exception – log and continue.
            Console.WriteLine($"An error occurred while reading viewer preferences: {ex.Message}");
        }
    }
}
