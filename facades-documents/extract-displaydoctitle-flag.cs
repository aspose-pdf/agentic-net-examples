using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string configPath = "pdfconfig.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF using Aspose.Pdf Document API
            using (Document pdfDoc = new Document(pdfPath))
            {
                // The ViewerPreferences property exists only in newer versions of Aspose.Pdf.
                // To keep the source compatible with older versions we access it via dynamic
                // and handle the possible RuntimeBinderException gracefully.
                bool displayTitle = false;
                try
                {
                    dynamic doc = pdfDoc; // bypass compile‑time member check
                    var prefs = doc.ViewerPreferences; // may throw at runtime if not present
                    if (prefs != null)
                    {
                        // The DisplayDocTitle property is a bool indicating the flag state.
                        displayTitle = (bool)(prefs.DisplayDocTitle ?? false);
                    }
                }
                catch (RuntimeBinderException)
                {
                    // ViewerPreferences is not supported by the current Aspose.Pdf version.
                    // Keep the default value (false) and continue.
                    Console.WriteLine("ViewerPreferences property is not available in the referenced Aspose.Pdf library version.");
                }

                // Write the flag to a simple text configuration file
                // Format: DisplayDocTitle=true (or false)
                string configLine = $"DisplayDocTitle={displayTitle}";
                File.WriteAllText(configPath, configLine);

                Console.WriteLine($"DisplayDocTitle flag extracted: {displayTitle}");
                Console.WriteLine($"Configuration saved to: {configPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
