using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "form_template.pdf";
        const string outputPath = "filled_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document containing the form.
        // The Document implements IDisposable, so wrap it in a using block.
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm object.
            Form form = doc.Form;

            // ----- Fill a text box field -----
            // The field is identified by its name (as defined in the PDF).
            TextBoxField nameField = form["NameField"] as TextBoxField;
            if (nameField != null)
            {
                // Set the field value.
                nameField.Value = "John Doe";

                // Appearance customization – use dynamic to stay compatible with
                // different Aspose.Pdf versions where the exact property names may vary.
                dynamic dyn = nameField;
                try { dyn.Font = FontRepository.FindFont("Arial"); } catch { }
                try { dyn.FontSize = 12; } catch { }
                try { dyn.TextColor = Color.Blue; } catch { }
            }

            // ----- Set a check box field -----
            // Use runtime type inspection and dynamic dispatch to avoid compile‑time
            // dependencies on specific checkbox class names.
            var agreeWidget = form["AgreeCheck"];
            if (agreeWidget != null)
            {
                string widgetType = agreeWidget.GetType().Name;
                if (widgetType == "CheckBoxField" || widgetType == "CheckboxField")
                {
                    dynamic chk = agreeWidget;
                    try { chk.Checked = true; } catch { }
                }
            }

            // ----- Optional: flatten the form -----
            // After flattening, form fields become regular content and are no longer editable.
            form.Flatten();

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form processing completed. Output saved to '{outputPath}'.");
    }
}
