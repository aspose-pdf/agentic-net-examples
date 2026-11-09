using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    // Toggles the FullScreen viewer preference of a PDF.
    // If the current Aspose.Pdf version supports ViewerPreferences,
    // the method sets the FullScreen flag via reflection.
    // Otherwise it leaves the document unchanged.
    static void ToggleFullScreen(string inputPdfPath, string outputPdfPath, bool enableFullScreen)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPdfPath))
            {
                // Attempt to obtain the ViewerPreferences property via reflection.
                PropertyInfo viewerPrefProp = doc.GetType().GetProperty("ViewerPreferences", BindingFlags.Public | BindingFlags.Instance);
                if (viewerPrefProp != null)
                {
                    // Get the ViewerPreferences instance.
                    object viewerPrefs = viewerPrefProp.GetValue(doc);
                    if (viewerPrefs != null)
                    {
                        // Try to set the FullScreen property on the ViewerPreferences object.
                        PropertyInfo fullScreenProp = viewerPrefs.GetType().GetProperty("FullScreen", BindingFlags.Public | BindingFlags.Instance);
                        if (fullScreenProp != null && fullScreenProp.CanWrite)
                        {
                            fullScreenProp.SetValue(viewerPrefs, enableFullScreen);
                            Console.WriteLine($"FullScreen viewer preference set to {enableFullScreen}.");
                        }
                        else
                        {
                            Console.WriteLine("FullScreen property not found or read‑only on ViewerPreferences.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ViewerPreferences instance is null.");
                    }
                }
                else
                {
                    // The current Aspose.Pdf version does not expose ViewerPreferences.
                    Console.WriteLine("ViewerPreferences property not available in this Aspose.Pdf version. No changes applied.");
                }

                // Save the (potentially modified) document.
                doc.Save(outputPdfPath);
                Console.WriteLine($"Document saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }

    static void Main(string[] args)
    {
        // Example usage:
        // args[0] = input PDF path
        // args[1] = output PDF path
        // args[2] = "on" or "off" to enable/disable FullScreen
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <inputPdf> <outputPdf> <on|off>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        bool enable = string.Equals(args[2], "on", StringComparison.OrdinalIgnoreCase);

        ToggleFullScreen(inputPath, outputPath, enable);
    }
}