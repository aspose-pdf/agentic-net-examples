using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
// Removed: using Aspose.Pdf.XForms; // XForm type is accessed via dynamic to avoid missing namespace

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfOutput = "annotations.xfdf";
        const string appearanceFolder = "AppearanceStreams";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(appearanceFolder);

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------
            // 1. Export all annotations to XFDF (resource: annotations)
            // -------------------------------------------------
            doc.ExportAnnotationsToXfdf(xfdfOutput);
            Console.WriteLine($"Annotations exported to XFDF: {xfdfOutput}");

            // -------------------------------------------------
            // 2. Extract appearance streams from each annotation
            // -------------------------------------------------
            int streamCounter = 0;

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all annotations on the page
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // The Appearance property holds a dictionary of appearance streams (if any)
                    if (annotation.Appearance != null && annotation.Appearance.Count > 0)
                    {
                        foreach (var entry in annotation.Appearance)
                        {
                            // Use dynamic to avoid compile‑time dependency on XForm type
                            try
                            {
                                dynamic xForm = entry.Value; // XForm implements Save(string)
                                string fileName = Path.Combine(
                                    appearanceFolder,
                                    $"page{pageIndex}_ann{annIndex}_{entry.Key}_{++streamCounter}.pdf");

                                xForm.Save(fileName);
                                Console.WriteLine($"Saved appearance stream to: {fileName}");
                            }
                            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                            {
                                // The appearance object is not an XForm (or does not expose Save)
                                Console.WriteLine($"Skipped non‑XForm appearance object on page {pageIndex}, annotation {annIndex}, key {entry.Key}.");
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}
