using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_hidden.pdf";
        const string fieldName = "myField"; // name of the form field to hide

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the widget annotation that represents the form field
            WidgetAnnotation widget = doc.Form[fieldName] as WidgetAnnotation;
            if (widget != null)
            {
                // Preserve existing flags and add the Hidden flag so the field does not appear when rendered
                widget.Flags |= AnnotationFlags.Hidden;
            }
            else
            {
                // If the field is not a widget (unlikely for a form field), fall back to a HideAction on page open
                HideAction hide = new HideAction(fieldName, true);
                // PageActionCollection exposes OnOpen (not Open)
                doc.Pages[1].Actions.OnOpen = hide;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' hidden and saved to '{outputPath}'.");
    }
}
