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
        string userRole = GetCurrentUserRole(); // e.g., "Admin", "User", "Guest"

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Import annotations from the XFDF file into the document
                doc.ImportAnnotationsFromXfdf(xfdfPath);

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate over all annotations on the current page
                    for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                    {
                        Annotation annotation = page.Annotations[annIndex];

                        // Determine visibility based on the user role
                        // Example rule: only "Admin" can see all annotations;
                        // other roles see only those that are not marked as hidden.
                        if (userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                        {
                            // Ensure the annotation is visible
                            annotation.Flags &= ~AnnotationFlags.Hidden;
                        }
                        else
                        {
                            // Hide the annotation for non‑admin users
                            annotation.Flags |= AnnotationFlags.Hidden;
                        }
                    }
                }

                // Save the modified PDF
                doc.Save(outputPdf);
                Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Placeholder for obtaining the current user's role.
    // Replace with actual implementation as needed.
    static string GetCurrentUserRole()
    {
        // For demonstration purposes, return a fixed role.
        return "User";
    }
}