using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Microsoft.CSharp.RuntimeBinder; // needed for RuntimeBinderException

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "properties.md";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with PdfContentEditor (does NOT implement IDisposable)
        var editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Access viewer preferences via dynamic to stay compatible with older Aspose.Pdf versions
        bool hideMenuBar = false;
        bool fitWindow = false;
        try
        {
            dynamic doc = editor.Document; // dynamic bypasses compile‑time checks
            // ViewerPreferences may not exist – catch RuntimeBinderException
            try
            {
                hideMenuBar = (bool)doc.ViewerPreferences.HideMenuBar;
                fitWindow   = (bool)doc.ViewerPreferences.FitWindow;
            }
            catch (RuntimeBinderException)
            {
                // Property not available in this library version – keep defaults
                Console.Error.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            }
        }
        catch (Exception ex)
        {
            // Any other unexpected error
            Console.Error.WriteLine($"Unable to read viewer preferences: {ex.Message}");
        }

        // Build a markdown table summarizing the values
        var sb = new StringBuilder();
        sb.AppendLine("| Property   | Value |");
        sb.AppendLine("|------------|-------|");
        sb.AppendLine($"| HideMenuBar | {hideMenuBar} |");
        sb.AppendLine($"| FitWindow   | {fitWindow} |");

        string markdown = sb.ToString();

        // Output to console
        Console.WriteLine(markdown);

        // Save the markdown to a file
        File.WriteAllText(outputPath, markdown);
        Console.WriteLine($"Markdown table saved to '{outputPath}'.");
    }
}
