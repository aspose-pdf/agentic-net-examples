using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "output.pdf";

        // Determine the current user's role (replace with real logic as needed)
        string userRole = GetUserRole();

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

        // Load the PDF and import XFDF annotations
        using (Document doc = new Document(pdfPath))
        {
            // Import annotations from the XFDF file (Aspose.Pdf core API)
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Adjust visibility of each imported annotation based on the user role
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    if (userRole.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    {
                        // Ensure the annotation is visible: clear the Hidden flag if set
                        annot.Flags &= ~AnnotationFlags.Hidden;
                    }
                    else
                    {
                        // Hide the annotation for non‑admin users
                        annot.Flags |= AnnotationFlags.Hidden;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }

    // Placeholder for role retrieval logic
    static string GetUserRole()
    {
        // In a real application, replace this with actual authentication/authorization logic
        return "user"; // default role
    }
}
