using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Adjust annotation visibility based on the current user's role.
    // Annotations whose Name does not match the allowed role are hidden.
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";   // source PDF
        const string xfdfPath       = "annotations.xfdf"; // XFDF file with annotations
        const string outputPdfPath  = "output.pdf";  // result PDF
        const string currentUserRole = "Manager";    // example role

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Import annotations from the XFDF file (provided API)
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Iterate over all pages (1‑based indexing) and their annotations
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                // Annotation collection also uses 1‑based indexing
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // The Name property corresponds to the /T entry in the annotation dictionary.
                    // If the annotation's name does not match the user's role, hide it.
                    if (!string.Equals(ann.Name, currentUserRole, StringComparison.OrdinalIgnoreCase))
                    {
                        // Set the Hidden flag (makes the annotation invisible to the user)
                        ann.Flags |= AnnotationFlags.Hidden;
                    }
                    else
                    {
                        // Ensure the annotation is visible (remove Hidden flag if previously set)
                        ann.Flags &= ~AnnotationFlags.Hidden;
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: use Save without extra options)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations imported and visibility adjusted. Saved to '{outputPdfPath}'.");
    }
}