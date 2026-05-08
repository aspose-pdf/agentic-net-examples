using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and XFDF files
        const string inputPdfPath  = "input.pdf";
        const string xfdfPath      = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";

        // Example user role – in a real scenario this would come from your authentication logic
        const string userRole = "Manager";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document doc = new Document(inputPdfPath))
            {
                // Import annotations from the XFDF file (API rule: use ImportAnnotationsFromXfdf)
                doc.ImportAnnotationsFromXfdf(xfdfPath);

                // Iterate over all pages (1‑based indexing rule)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate over all annotations on the page (1‑based indexing rule)
                    for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                    {
                        Annotation annotation = page.Annotations[annIndex];

                        // The Name property can be used to store a role identifier in XFDF.
                        // If the annotation's Name matches the current user role, make it visible;
                        // otherwise hide it by setting the Hidden flag.
                        if (string.Equals(annotation.Name, userRole, StringComparison.OrdinalIgnoreCase))
                        {
                            // Ensure the Hidden flag is cleared (visible)
                            annotation.Flags &= ~AnnotationFlags.Hidden;
                        }
                        else
                        {
                            // Set the Hidden flag to hide the annotation for this user
                            annotation.Flags |= AnnotationFlags.Hidden;
                        }
                    }
                }

                // Save the modified PDF (lifecycle rule: use Save inside using)
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations imported and visibility set based on role '{userRole}'.");
            Console.WriteLine($"Output saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}