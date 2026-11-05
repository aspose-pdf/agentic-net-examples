using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        // Load the PDF, process form fields, then set metadata
        using (Document doc = new Document(inputPath))
        {
            // Example form processing: fill a text field named "Name"
            // The indexer may return a WidgetAnnotation in some versions, so we locate the field via the Fields collection.
            Field nameField = null;
            foreach (Field field in doc.Form.Fields)
            {
                if (field.PartialName == "Name")
                {
                    nameField = field;
                    break;
                }
            }

            if (nameField != null)
            {
                nameField.Value = "John Doe";
            }

            // Set PDF metadata
            doc.Info.Author = "Jane Smith";
            doc.Info.Title = "Processed Form Document";

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with metadata to '{outputPath}'.");
    }
}
