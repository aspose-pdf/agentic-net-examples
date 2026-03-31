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
        const string outputPath = "decorated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            // Iterate over all form fields and apply styling to ListBox fields
            foreach (Aspose.Pdf.Forms.ListBoxField listField in document.Form.Fields)
            {
                // Set a uniform border width
                Border border = new Border(listField) { Width = 2 };
                listField.Border = border;

                // Set border color (also serves as visual cue for the field)
                listField.Color = Aspose.Pdf.Color.Blue;

                // Set a background-like color using the field's Color property as a placeholder
                // Aspose.Pdf does not expose a direct background color for list fields.
                // The chosen color provides a consistent appearance.
                listField.Color = Aspose.Pdf.Color.LightGray;
            }

            document.Save(outputPath);
        }

        Console.WriteLine($"List box fields have been decorated and saved to '{outputPath}'.");
    }
}