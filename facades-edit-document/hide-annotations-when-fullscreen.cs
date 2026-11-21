using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_hidden_annotations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Enable FullScreen viewer preference if the current Aspose.Pdf version
            // supports the ViewerPreferences property.  Older versions do not expose
            // this member, which leads to a compile‑time error because the ViewerPreferences
            // type is missing.  To keep the source compatible with both newer and older
            // releases we use reflection to set the FullScreen flag without referencing
            // the concrete ViewerPreferences type.
            // -----------------------------------------------------------------
            try
            {
                var vpProp = doc.GetType().GetProperty("ViewerPreferences");
                if (vpProp != null)
                {
                    var vpInstance = vpProp.GetValue(doc);
                    if (vpInstance != null)
                    {
                        var fullScreenProp = vpInstance.GetType().GetProperty("FullScreen");
                        if (fullScreenProp != null && fullScreenProp.CanWrite)
                        {
                            fullScreenProp.SetValue(vpInstance, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // If anything goes wrong we simply continue – the rest of the logic
                // (hiding annotations) still works.
                Console.WriteLine($"Unable to set FullScreen viewer preference: {ex.Message}");
            }

            // Iterate over all pages and hide each annotation
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    // Add the Hidden flag while preserving any existing flags
                    annot.Flags |= AnnotationFlags.Hidden;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden annotations to '{outputPath}'.");
    }
}
