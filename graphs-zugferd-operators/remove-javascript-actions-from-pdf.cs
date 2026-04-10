using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in doc.Pages)
                {
                    // Annotations collection uses 1‑based indexing
                    for (int i = 1; i <= page.Annotations.Count; i++)
                    {
                        Annotation annotation = page.Annotations[i];

                        // 1. Remove JavaScript attached directly to a LinkAnnotation via its Action property
                        if (annotation is LinkAnnotation link && link.Action is JavascriptAction)
                        {
                            link.Action = null;
                        }

                        // 2. Remove JavaScript actions from widget (form field) annotations.
                        //    WidgetAnnotation exposes an AnnotationActionCollection via the Actions property.
                        //    The collection contains a set of strongly‑typed properties (OnEnter, OnExit, …).
                        //    Not all Aspose.Pdf versions expose those properties, so we clear them safely
                        //    using reflection – this works regardless of the exact library version.
                        if (annotation is WidgetAnnotation widget && widget.Actions != null)
                        {
                            var actions = widget.Actions;
                            var actionProps = actions.GetType().GetProperties();
                            foreach (var prop in actionProps)
                            {
                                // Only clear properties that hold a JavascriptAction (or any PdfAction)
                                if (typeof(PdfAction).IsAssignableFrom(prop.PropertyType))
                                {
                                    prop.SetValue(actions, null);
                                }
                            }
                        }
                    }
                }

                // Save the cleaned PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"All JavaScript actions removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
