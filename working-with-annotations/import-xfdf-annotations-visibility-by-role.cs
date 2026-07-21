using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF, XFDF file with annotations, and output PDF paths
        const string inputPdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";

        // User role can be passed as a command‑line argument; default to "user"
        string userRole = args.Length > 0 ? args[0] : "user";

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

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Import annotations from the XFDF file into the document
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate over all annotations on the page (1‑based indexing)
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation annotation = page.Annotations[i];

                    // Determine visibility based on the user role
                    bool isVisible = IsAnnotationVisibleForRole(annotation, userRole);

                    if (!isVisible)
                    {
                        // Hide the annotation by setting the Hidden flag
                        annotation.Flags |= AnnotationFlags.Hidden;
                    }
                    else
                    {
                        // Ensure the Hidden flag is cleared so the annotation is visible
                        annotation.Flags &= ~AnnotationFlags.Hidden;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations imported and visibility set. Output saved to '{outputPdfPath}'.");
    }

    // Placeholder visibility logic:
    // - "admin" role sees all annotations.
    // - Any other role hides all annotations.
    // Real implementations could inspect annotation.Title, Contents, or custom data.
    static bool IsAnnotationVisibleForRole(Annotation annotation, string role)
    {
        return role.Equals("admin", StringComparison.OrdinalIgnoreCase);
    }
}