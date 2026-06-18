using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and XFDF files
        const string pdfPath   = "input.pdf";
        const string xfdfPath  = "annotations.xfdf";
        const string outputPdf = "output.pdf";

        // Example user role – in a real scenario this would come from your authentication logic
        string userRole = GetCurrentUserRole(); // e.g., "admin", "viewer", etc.

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        // Load the PDF, import XFDF annotations, adjust visibility, and save
        using (Document doc = new Document(pdfPath))
        {
            // Import all annotations from the XFDF file
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Iterate over every page and its annotations
            foreach (Page page in doc.Pages)
            {
                // Annotation collection uses 1‑based indexing; foreach abstracts that detail
                foreach (Annotation ann in page.Annotations)
                {
                    // Example rule: hide annotations whose Name starts with "AdminOnly"
                    // unless the current user is an administrator.
                    bool shouldHide = ann.Name != null &&
                                      ann.Name.StartsWith("AdminOnly", StringComparison.OrdinalIgnoreCase) &&
                                      !string.Equals(userRole, "admin", StringComparison.OrdinalIgnoreCase);

                    if (shouldHide)
                    {
                        // Add the Hidden flag to make the annotation invisible
                        ann.Flags |= AnnotationFlags.Hidden;
                    }
                    else
                    {
                        // Ensure the Hidden flag is cleared so the annotation is visible
                        ann.Flags &= ~AnnotationFlags.Hidden;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }

    // Placeholder for obtaining the current user's role.
    // Replace with actual role‑retrieval logic as needed.
    static string GetCurrentUserRole()
    {
        // For demonstration purposes, return a fixed role.
        return "viewer";
    }
}