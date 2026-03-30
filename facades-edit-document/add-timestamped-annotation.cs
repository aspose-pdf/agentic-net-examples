using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "annotated.pdf";

        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text annotation on the page
            TextAnnotation annotation = new TextAnnotation(page, rect);
            annotation.Title = "Note";
            annotation.Contents = "Created with timestamp.";
            annotation.CreationDate = DateTime.UtcNow;
            annotation.Modified = DateTime.UtcNow;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine("Annotation added with creation date set to current UTC time.");
    }
}