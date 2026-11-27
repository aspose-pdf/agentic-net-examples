using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "centered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfContentEditor implements IDisposable, so wrap it in a using block.
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the source PDF.
                editor.BindPdf(inputPath);

                // Attempt to set the ViewerPreferences.CenterWindow property.
                // The ViewerPreferences property exists only in newer Aspose.Pdf versions.
                // To keep the code compatible with older versions we use reflection/dynamic access.
                try
                {
                    // Dynamic access bypasses compile‑time checks.
                    dynamic doc = editor.Document;
                    doc.ViewerPreferences.CenterWindow = true;
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                {
                    // Property not available – fall back to reflection for older runtimes.
                    var docType = editor.Document.GetType();
                    var vpProp = docType.GetProperty("ViewerPreferences");
                    if (vpProp != null)
                    {
                        var vpObj = vpProp.GetValue(editor.Document);
                        var centerProp = vpObj?.GetType().GetProperty("CenterWindow");
                        if (centerProp != null && centerProp.CanWrite)
                        {
                            centerProp.SetValue(vpObj, true);
                        }
                        else
                        {
                            Console.Error.WriteLine("CenterWindow property not found on ViewerPreferences. The PDF will be saved without this preference.");
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("ViewerPreferences property not supported by the current Aspose.Pdf library version. The PDF will be saved without this preference.");
                    }
                }

                // Save the modified PDF.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Viewer preference updated (if supported) and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
