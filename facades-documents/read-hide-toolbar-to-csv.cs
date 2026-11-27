using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string csvPath = "output.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor to load the PDF (as requested).
        var editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // The ViewerPreferences property is only available in newer Aspose.Pdf versions.
        // Access it via dynamic to keep the code compatible with older versions.
        bool hideToolbar = false;
        try
        {
            dynamic dynEditor = editor;               // dynamic bypasses compile‑time checks
            var viewerPrefs = dynEditor.Document?.ViewerPreferences; // may be null or missing
            hideToolbar = viewerPrefs?.HideToolbar ?? false;
        }
        catch (RuntimeBinderException)
        {
            // ViewerPreferences property does not exist in the referenced Aspose.Pdf version.
            // Default to false (toolbar visible).
            hideToolbar = false;
        }
        catch (Exception ex)
        {
            // Any other unexpected error – report and exit.
            Console.Error.WriteLine($"Error while reading viewer preferences: {ex.Message}");
            return;
        }

        // Write the result to a CSV file (add header if the file is new).
        bool writeHeader = !File.Exists(csvPath);
        using (var writer = new StreamWriter(csvPath, append: true))
        {
            if (writeHeader)
            {
                writer.WriteLine("FileName,HideToolbar");
            }
            writer.WriteLine($"{Path.GetFileName(inputPdf)},{hideToolbar}");
        }

        Console.WriteLine($"HideToolbar={hideToolbar} written to {csvPath}");
    }
}
