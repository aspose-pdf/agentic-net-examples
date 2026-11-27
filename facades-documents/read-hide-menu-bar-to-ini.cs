using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string iniPath = "settings.ini";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Open the PDF with PdfContentEditor as required.
        var editor = new PdfContentEditor();
        editor.BindPdf(pdfPath);

        // Use dynamic access to ViewerPreferences to stay compatible with older Aspose.Pdf versions.
        bool hideMenuBar = false; // default value if the property is unavailable
        try
        {
            // PdfContentEditor exposes the underlying Document via the "Document" property.
            // We cast it to dynamic so the compiler does not require the ViewerPreferences member.
            dynamic doc = editor.Document;
            var prefs = doc.ViewerPreferences;
            hideMenuBar = (bool)prefs.HideMenuBar;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences.HideMenuBar is not supported by the current Aspose.Pdf library version. Using default value 'false'.");
        }
        catch (Exception ex)
        {
            // Any other unexpected error should be reported but not crash the program.
            Console.WriteLine($"Unexpected error while reading ViewerPreferences: {ex.Message}");
        }

        // Prepare INI‑style content.
        string iniContent = $"HideMenuBar={hideMenuBar.ToString().ToLowerInvariant()}";

        // Write the configuration to a file.
        try
        {
            File.WriteAllText(iniPath, iniContent);
            Console.WriteLine($"HideMenuBar value ({hideMenuBar}) written to '{iniPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write INI file: {ex.Message}");
        }
    }
}
