using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Use FormEditor that works with a Document instance
        using (FormEditor formEditor = new FormEditor(pdfDoc))
        {
            // Iterate only over page 2
            const int targetPage = 2;
            if (targetPage <= pdfDoc.Pages.Count)
            {
                Page page = pdfDoc.Pages[targetPage];
                foreach (Annotation annotation in page.Annotations)
                {
                    // We are interested only in form fields (WidgetAnnotation)
                    if (annotation is WidgetAnnotation widget)
                    {
                        // The field name is stored in the widget's Name property
                        string fieldName = widget.Name;

                        // Original rectangle of the field (double values)
                        var rect = widget.Rect;

                        // Shift the field 5 points to the right – cast to float as required by MoveField
                        float newLlx = (float)rect.LLX + 5f;
                        float newLly = (float)rect.LLY;
                        float newUrx = (float)rect.URX + 5f;
                        float newUry = (float)rect.URY;

                        // Apply the new position
                        formEditor.MoveField(fieldName, newLlx, newLly, newUrx, newUry);
                        Console.WriteLine($"Moved field '{fieldName}' on page {targetPage} by 5 points to the right.");
                    }
                }
            }
            else
            {
                Console.Error.WriteLine($"Document does not contain page {targetPage}.");
            }

            // Save the modified PDF
            formEditor.Save(outputPath);
        }
    }
}
