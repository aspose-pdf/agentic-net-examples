using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
// The Microsoft.CSharp.RuntimeBinder namespace is required only if you reference the RuntimeBinderException type explicitly.
// It is safe to include it; the compiler will ignore it if the assembly is not referenced.
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "fullscreen_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            bool preferenceSet = false;

            // Attempt to use the ViewerPreferences property (available in newer Aspose.Pdf versions).
            // If the property does not exist (older versions), fall back to a JavaScript OpenAction that forces full‑screen.
            try
            {
                // "dynamic" bypasses compile‑time member checking.
                dynamic dynDoc = doc;
                dynDoc.ViewerPreferences.FullScreen = true;
                preferenceSet = true;
            }
            catch (RuntimeBinderException)
            {
                // ViewerPreferences is not supported – use JavaScript as an alternative.
                try
                {
                    doc.OpenAction = new JavascriptAction("this.fullScreen = true;");
                    preferenceSet = true;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to set full‑screen via JavaScript: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                // Any other unexpected exception while accessing ViewerPreferences.
                Console.Error.WriteLine($"Unexpected error when setting ViewerPreferences: {ex.Message}");
            }

            if (!preferenceSet)
            {
                Console.Error.WriteLine("Full‑screen preference could not be applied. The PDF will be saved without it.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved{(File.Exists(outputPath) ? " with" : " without")} FullScreen preference: {outputPath}");
    }
}
