using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_beveled.pdf";
        const string fieldName = "myTextField"; // replace with your field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the widget annotation that represents the form field
            var widget = doc.Form[fieldName];

            if (widget == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found.");
            }
            else
            {
                // Explicitly cast the widget to a Field (the explicit conversion operator is provided by Aspose.Pdf)
                Field field = (Field)widget;

                // Create a Border instance that is linked to the field (Border requires the parent annotation)
                field.Border = new Border(field)
                {
                    Style = BorderStyle.Beveled,
                    Width = 1 // 1 point width
                };

                // Set the border colour via the annotation's own Color property (Border has no Color property)
                field.Color = Color.Black;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with beveled border on field '{fieldName}' to '{outputPath}'.");
    }
}
