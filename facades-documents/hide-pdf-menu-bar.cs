using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "hiddenmenu.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Use PdfContentEditor (in Aspose.Pdf.Facades) to manipulate viewer preferences
            var editor = new PdfContentEditor();
            editor.BindPdf(inputPath);

            // Hide the menu bar when the PDF is opened. Guard against older library versions
            // that may not expose ViewerPreferences by using dynamic access.
            dynamic dynEditor = editor;
            try
            {
                dynEditor.ViewerPreferences.HideMenubar = true;
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences.HideMenubar is not supported by the current Aspose.Pdf version.");
            }

            editor.Save(outputPath);
            Console.WriteLine($"PDF saved with hidden menu bar to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
