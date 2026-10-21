using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // required by task specification
using Microsoft.CSharp.RuntimeBinder; // for RuntimeBinderException

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Attempt to enable FullScreen viewer preference.
            // The ViewerPreferences property exists only in newer Aspose.Pdf versions.
            // Use dynamic access to keep the code compatible with older versions.
            try
            {
                dynamic d = doc;
                d.ViewerPreferences.FullScreen = true;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not available in the current Aspose.Pdf version; FullScreen mode will not be set.");
            }

            // Iterate all pages and hide every annotation (guard against null collection)
            foreach (Page page in doc.Pages)
            {
                // page.Annotations can be null when a page has no annotations
                var annotations = page.Annotations ?? Enumerable.Empty<Annotation>();
                foreach (Annotation annot in annotations)
                {
                    // Set the Hidden flag (overwrites other flags)
                    annot.Flags = AnnotationFlags.Hidden;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
