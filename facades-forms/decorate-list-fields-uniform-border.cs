using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
// System.Drawing is kept for Color, but all ambiguous types are fully qualified where needed.
using System.Drawing; // only for System.Drawing.Color

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
            // Bind FormEditor to the document
            var editor = new FormEditor();
            editor.BindPdf(doc);

            // Retrieve all field names from the document's Form collection
            var fieldNames = doc.Form?.Fields?
                .Where(f => f is ListBoxField)
                .Select(f => f.Name)
                .ToArray() ?? Array.Empty<string>();

            // Define a uniform appearance for list fields via FormFieldFacade
            editor.Facade = new FormFieldFacade
            {
                BorderColor = System.Drawing.Color.Black,
                BackgroundColor = System.Drawing.Color.FromArgb(230, 230, 230), // light gray background
                BorderWidth = 1,
                BorderStyle = FormFieldFacade.BorderStyleSolid,
                FontSize = 12,
                // Fully qualify FontStyle to the one in Aspose.Pdf.Facades
                Font = Aspose.Pdf.Facades.FontStyle.Helvetica,
                TextColor = System.Drawing.Color.Black,
                Alignment = FormFieldFacade.AlignLeft
            };

            // Apply the appearance to every ListBox field
            foreach (string name in fieldNames)
            {
                editor.DecorateField(name);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All list fields have been decorated and saved to '{outputPath}'.");
    }
}
