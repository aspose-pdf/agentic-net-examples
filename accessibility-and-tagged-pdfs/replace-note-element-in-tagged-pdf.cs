using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Identify the note to replace
        const string oldTitle   = "Old Note";
        const string newTitle   = "Updated Note";
        const string newContent = "This is the updated note text.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Gather annotations that match the old note
                List<Annotation> toRemove = new List<Annotation>();
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is TextAnnotation textAnnot && textAnnot.Title == oldTitle)
                    {
                        toRemove.Add(textAnnot);
                    }
                }

                // Remove the old note annotations
                foreach (Annotation rem in toRemove)
                {
                    page.Annotations.Remove(rem);
                }

                // If any note was removed, add the updated note
                if (toRemove.Count > 0)
                {
                    // Define the rectangle where the note will appear
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                    // Create a new TextAnnotation (note) with updated content
                    TextAnnotation newAnnot = new TextAnnotation(page, rect)
                    {
                        Title    = newTitle,
                        Contents = newContent,
                        Open     = true,
                        Icon     = TextIcon.Note
                    };

                    // Add the new annotation to the page
                    page.Annotations.Add(newAnnot);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}