using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class HighlightRequiredFields
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form
            Form form = doc.Form;
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                doc.Save(outputPath); // Save unchanged document
                return;
            }

            // Iterate over all fields in the form
            foreach (Field field in form)
            {
                // Check if the field is marked as required
                if (field.Required)
                {
                    // Set the field's border/annotation color to a highlight color
                    field.Color = Aspose.Pdf.Color.Yellow;

                    // Optionally, increase border width for better visibility
                    if (field.Border != null)
                    {
                        field.Border.Width = 2;
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Required fields highlighted and saved to '{outputPath}'.");
    }
}