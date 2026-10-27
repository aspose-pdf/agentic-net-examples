using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "updated.pdf";
        const string oldTitle = "OldNote";          // Title of the note to be removed
        const string newTitle = "UpdatedNote";      // Title for the new note
        const string newContents = "This is the updated note text."; // New note text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages to locate the target TextAnnotation
            foreach (Page page in doc.Pages)
            {
                TextAnnotation targetAnnotation = null;
                Aspose.Pdf.Rectangle targetRect = null;

                // Search annotations on the current page
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is TextAnnotation textAnnot && textAnnot.Title == oldTitle)
                    {
                        targetAnnotation = textAnnot;
                        // Preserve the rectangle of the original note
                        targetRect = new Aspose.Pdf.Rectangle(
                            textAnnot.Rect.LLX,
                            textAnnot.Rect.LLY,
                            textAnnot.Rect.URX,
                            textAnnot.Rect.URY);
                        break; // Assuming only one note with this title per page
                    }
                }

                if (targetAnnotation != null && targetRect != null)
                {
                    // Remove the existing note annotation
                    page.Annotations.Remove(targetAnnotation);

                    // Create a new note annotation with updated content
                    TextAnnotation newAnnotation = new TextAnnotation(page, targetRect)
                    {
                        Title = newTitle,
                        Contents = newContents,
                        Open = true,
                        Icon = TextIcon.Note
                    };

                    // Add the new annotation to the page
                    page.Annotations.Add(newAnnotation);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Note replaced and saved to '{outputPath}'.");
    }
}