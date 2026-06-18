using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a sample PDF that contains a JavaScript action
        using (Document doc = new Document())
        {
            // Add a single page (evaluation mode allows up to 4 pages)
            Page page = doc.Pages.Add();

            // Define a rectangle for the link annotation (use Aspose.Pdf.Rectangle to match the constructor signature)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create a link annotation and assign a JavaScript action to it
            LinkAnnotation link = new LinkAnnotation(page, rect);
            string jsCode = "app.alert('Hello from JavaScript!');";
            JavascriptAction jsAction = new JavascriptAction(jsCode);
            link.Action = jsAction;

            // Add the annotation to the page (only one annotation, within limit)
            page.Annotations.Add(link);

            // Save the sample PDF
            doc.Save("input.pdf");
        }

        // Load the PDF and remove all JavaScript actions
        using (Document doc = new Document("input.pdf"))
        {
            // Iterate through each page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Collect link annotations that contain JavaScript actions
                List<Annotation> toRemove = new List<Annotation>();
                foreach (Annotation annotation in page.Annotations)
                {
                    if (annotation is LinkAnnotation link && link.Action is JavascriptAction)
                    {
                        toRemove.Add(link);
                    }
                }

                // Remove the collected annotations from the page
                foreach (Annotation annotation in toRemove)
                {
                    page.Annotations.Delete(annotation);
                }
            }

            // Remove any document‑level open action that could contain JavaScript
            doc.OpenAction = null;

            // Save the cleaned PDF
            doc.Save("output.pdf");
        }
    }
}