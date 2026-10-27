using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // Needed for RuntimeBinderException

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Try to set viewer preferences – this property exists only in newer Aspose.Pdf versions.
            // Using dynamic bypasses compile‑time checks and allows the code to compile against older versions.
            try
            {
                dynamic d = doc;               // Cast to dynamic to avoid compile‑time member validation
                var prefs = d.ViewerPreferences; // May throw RuntimeBinderException if property is absent
                prefs.PageMode = PageMode.UseOutlines; // Show outline (bookmarks) panel by default
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Viewer preference applied (if supported) and saved to '{outputPath}'.");
    }
}
