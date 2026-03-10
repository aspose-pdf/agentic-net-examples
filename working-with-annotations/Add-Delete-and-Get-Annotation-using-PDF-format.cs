using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AnnotationDemo
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string addedPath = "added_annotation.pdf";
        const string finalPath = "deleted_annotation.pdf";

        // -------------------------------------------------
        // 1. Load the source PDF and add a TextAnnotation
        // -------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Create a rectangle for the annotation (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the annotation on the first page
            TextAnnotation note = new TextAnnotation(doc.Pages[1], rect)
            {
                Name = "MyNote",                     // Identifier for later retrieval
                Title = "Sample Note",                // Title shown in the popup window
                Contents = "This is a sample text annotation added via Aspose.Pdf.",
                Color = Aspose.Pdf.Color.Yellow,      // Background color of the annotation
                Icon = TextIcon.Note,                  // Icon type
                Open = true                            // Show the popup open by default
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(note);

            // Save the document with the new annotation
            doc.Save(addedPath);
        }

        // -------------------------------------------------
        // 2. Load the PDF with the added annotation and retrieve it
        // -------------------------------------------------
        using (Document doc = new Document(addedPath))
        {
            // Retrieve the annotation by its Name property
            Annotation found = doc.Pages[1].Annotations.FindByName("MyNote");

            if (found is TextAnnotation textAnno)
            {
                Console.WriteLine("Annotation found:");
                Console.WriteLine($"  Title   : {textAnno.Title}");
                Console.WriteLine($"  Contents: {textAnno.Contents}");
                Console.WriteLine($"  Color   : {textAnno.Color}");
            }
            else if (found != null)
            {
                // Fallback for other annotation types (they don't have Title/Contents)
                Console.WriteLine("Annotation found but it is not a TextAnnotation.");
                Console.WriteLine($"  Name : {found.Name}");
                Console.WriteLine($"  Color: {found.Color}");
            }
            else
            {
                Console.WriteLine("Annotation with name 'MyNote' was not found.");
            }

            // -------------------------------------------------
            // 3. Delete the retrieved annotation and save the result
            // -------------------------------------------------
            if (found != null)
            {
                // Delete by passing the annotation instance
                doc.Pages[1].Annotations.Delete(found);
            }

            // Save the document after deletion
            doc.Save(finalPath);
        }

        Console.WriteLine("Operations completed:");
        Console.WriteLine($"  Added annotation saved to: {addedPath}");
        Console.WriteLine($"  Annotation deleted and saved to: {finalPath}");
    }
}
