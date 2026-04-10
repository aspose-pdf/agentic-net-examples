using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "TempField";

        // Ensure the source PDF exists – if not, create a minimal PDF with the target field.
        if (!File.Exists(inputPdf))
        {
            using (Document placeholder = new Document())
            {
                // Add a single page.
                Page page = placeholder.Pages.Add();

                // Define a rectangle for the form field (left, bottom, right, top).
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 650);

                // Create an empty TextBoxField with the required name.
                TextBoxField tempField = new TextBoxField(placeholder.Pages[1], rect)
                {
                    PartialName = fieldName,
                    Value = string.Empty // explicitly empty
                };
                placeholder.Form.Add(tempField);

                // Save the placeholder PDF so the rest of the logic can operate on a real file.
                placeholder.Save(inputPdf);
            }
        }

        // Load the source PDF (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Access the form fields via the Facades Form class (fully qualified to avoid ambiguity)
            Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(doc);

            // Retrieve the current value of the field (null or empty means no user input)
            string currentValue = form.GetField(fieldName);

            // If the field is empty, remove it using FormEditor
            if (string.IsNullOrEmpty(currentValue))
            {
                // FormEditor works on the same Document instance
                using (FormEditor editor = new FormEditor())
                {
                    editor.BindPdf(doc);                 // Initialize the editor with the document
                    editor.RemoveField(fieldName);       // Remove the specified field
                    editor.Save(outputPdf);              // Persist changes to the output file
                }
            }
            else
            {
                // Field contains data – simply save the original document unchanged
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPdf}'.");
    }
}
