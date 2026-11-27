using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Microsoft.CSharp.RuntimeBinder; // required for dynamic binding exceptions

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string txtPath = "hideToolbar.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // PdfContentEditor does NOT implement IDisposable, so do NOT wrap it in a using block.
        var editor = new PdfContentEditor();
        editor.BindPdf(pdfPath);

        // The Document class may or may not expose ViewerPreferences depending on the Aspose.Pdf version.
        // We therefore access it via dynamic and handle the case where the property is absent.
        using (Document doc = editor.Document)
        {
            bool hideToolbar = false; // default value if the property cannot be read
            try
            {
                // Cast to dynamic to bypass compile‑time member checking.
                dynamic dynDoc = doc;
                // Attempt to read ViewerPreferences.HideToolbar.
                hideToolbar = (bool)dynDoc.ViewerPreferences.HideToolbar;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Assuming 'false'.");
            }
            catch (Exception ex)
            {
                // Any other unexpected exception should be reported but not crash the program.
                Console.WriteLine($"Unexpected error while reading ViewerPreferences: {ex.Message}");
            }

            // Write the boolean value to a text file.
            File.WriteAllText(txtPath, hideToolbar.ToString());
            Console.WriteLine($"HideToolbar flag is '{hideToolbar}'. Written to '{txtPath}'.");
        }

        // No need to call editor.Save() because we only read information.
    }
}
