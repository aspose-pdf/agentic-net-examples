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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // ----- Form processing (example) -----
            // Fill a form field named "Name" if it exists
            Form form = doc.Form;
            if (form != null && form.Count > 0)
            {
                // Retrieve the field by name and set its value using the Field API
                Field nameField = null;
                foreach (Field f in form.Fields)
                {
                    if (string.Equals(f.Name, "Name", StringComparison.OrdinalIgnoreCase))
                    {
                        nameField = f;
                        break;
                    }
                }

                if (nameField != null)
                {
                    nameField.Value = "John Doe";
                }
            }

            // ----- Set PDF metadata -----
            doc.Info.Author = "John Doe";
            doc.Info.Title = "Processed Form Document";
            doc.Info.Subject = "Form processing example";
            doc.Info.Keywords = "Aspose.Pdf, metadata, form";

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
