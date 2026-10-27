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
        Document doc = new Document(inputPath);

        // Retrieve the existing field definition (cast to Field)
        Field templateField = doc.Form["TemplateField"] as Field;
        if (templateField == null)
        {
            Console.Error.WriteLine("TemplateField not found or is not a form field.");
            return;
        }

        // Use the same rectangle (position) as the original field
        Aspose.Pdf.Rectangle rect = templateField.Rect;
        int targetPageNumber = 5; // 1‑based page number where the clone will be placed
        string clonedFieldName = "ClonedField";

        // Get the target Page object (Aspose.Pdf expects a Page, not an int)
        if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
        {
            Console.Error.WriteLine($"Target page {targetPageNumber} is out of range.");
            return;
        }
        Page targetPage = doc.Pages[targetPageNumber];

        // Clone the field preserving its runtime type using reflection.
        Type fieldType = templateField.GetType();
        // Most field constructors accept (Page page, Rectangle rect)
        Field clonedField = (Field)Activator.CreateInstance(fieldType, targetPage, rect);
        clonedField.PartialName = clonedFieldName;

        // Copy type‑specific properties via dynamic dispatch.
        dynamic src = templateField;
        dynamic dst = clonedField;
        string typeName = fieldType.Name;
        if (typeName == "TextBoxField")
        {
            dst.Value = src.Value;
        }
        else if (typeName == "CheckBoxField" || typeName == "RadioButtonOptionField")
        {
            // Both have a Checked property in supported versions.
            dst.Checked = src.Checked;
        }
        else if (typeName == "ListBoxField")
        {
            dst.SelectedItems = src.SelectedItems;
        }
        else if (typeName == "ComboBoxField")
        {
            dst.SelectedItem = src.SelectedItem;
        }
        else
        {
            Console.Error.WriteLine($"Field type '{typeName}' is not explicitly handled; only basic properties were copied.");
        }

        // Add the cloned field to the document's form.
        doc.Form.Add(clonedField);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Cloned field created on page {targetPageNumber} and saved to '{outputPath}'.");
    }
}
