using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // For RuntimeBinderException when using dynamic

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "single_page_view.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Attempt to set viewer preference to single‑page layout.
            // The ViewerPreferences property and LayoutMode enum may be missing in older Aspose.Pdf versions.
            // Use reflection/dynamic to set the value without a compile‑time dependency on the enum.
            try
            {
                dynamic d = doc;
                // Resolve the LayoutMode enum type at runtime.
                var layoutEnum = doc.GetType().Assembly.GetType("Aspose.Pdf.LayoutMode");
                if (layoutEnum != null)
                {
                    // Parse the enum value "SinglePage".
                    var singlePageValue = Enum.Parse(layoutEnum, "SinglePage");
                    // Assign the value to the Layout property via dynamic dispatch.
                    d.ViewerPreferences.Layout = singlePageValue;
                }
                else
                {
                    // If the enum type cannot be found, fall back to the reflection block below.
                    throw new RuntimeBinderException("LayoutMode enum not found");
                }
            }
            catch (RuntimeBinderException)
            {
                // Property or enum not present – try to set via reflection as a graceful fallback.
                var vpProp = doc.GetType().GetProperty("ViewerPreferences");
                if (vpProp != null)
                {
                    var vpInstance = vpProp.GetValue(doc);
                    var layoutProp = vpInstance?.GetType().GetProperty("Layout");
                    var layoutEnum = doc.GetType().Assembly.GetType("Aspose.Pdf.LayoutMode");
                    if (layoutProp != null && layoutEnum != null)
                    {
                        var singlePageValue = Enum.Parse(layoutEnum, "SinglePage");
                        layoutProp.SetValue(vpInstance, singlePageValue);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Viewer preference attempted to be set to single‑page layout. Saved to '{outputPath}'.");
    }
}
