using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ---------- Add a TextAnnotation ----------
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Define the rectangle where the annotation will appear (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create a TextAnnotation on the first page
            Aspose.Pdf.Annotations.TextAnnotation textAnn =
                new Aspose.Pdf.Annotations.TextAnnotation(doc.Pages[1], rect);

            // Set annotation properties
            textAnn.Title = "John Doe";
            textAnn.Subject = "Review Note";
            textAnn.Contents = "Please review this paragraph.";
            textAnn.Name = "MyNote"; // optional identifier for later lookup

            // Add the annotation to the page's collection
            doc.Pages[1].Annotations.Add(textAnn);

            // Save the document with the new annotation
            doc.Save(outputPath);
        }

        // ---------- Get and Delete the annotation ----------
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(outputPath))
        {
            Aspose.Pdf.Page page = doc.Pages[1];
            Aspose.Pdf.Annotations.AnnotationCollection annColl = page.Annotations;

            Console.WriteLine("Annotations on page 1:");
            foreach (Aspose.Pdf.Annotations.Annotation ann in annColl)
            {
                // Only markup annotations expose Title, Subject, and Contents
                if (ann is Aspose.Pdf.Annotations.MarkupAnnotation markup)
                {
                    Console.WriteLine($"- Name: {markup.Name}");
                    Console.WriteLine($"  Title: {markup.Title}");
                    Console.WriteLine($"  Subject: {markup.Subject}");
                    Console.WriteLine($"  Contents: {markup.Contents}");
                }
            }

            // Delete the annotation we added earlier (identified by its Name)
            Aspose.Pdf.Annotations.Annotation toDelete = annColl.FindByName("MyNote");
            if (toDelete != null)
            {
                annColl.Delete(toDelete);
                Console.WriteLine("Deleted annotation 'MyNote'.");
            }
            else
            {
                Console.WriteLine("Annotation 'MyNote' not found.");
            }

            // Save the final document after deletion
            doc.Save(outputPath);
        }
    }
}